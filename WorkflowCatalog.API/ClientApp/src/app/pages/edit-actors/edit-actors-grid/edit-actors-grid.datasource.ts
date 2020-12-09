import { cloneDeep } from 'lodash';
import { LocalDataSource } from 'ng2-smart-table';
import { BehaviorSubject } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { ActorDto, ActorsClient, CreateActorCommand, DeleteActorCommand, UpdateActorCommand } from 'src/app/web-api-client';

export class ActorsDataSource extends LocalDataSource {

    setupId: string;
    paging: BehaviorSubject<{ pageSize: number, itemsCount: number }> = new BehaviorSubject({ pageSize: 5, itemsCount: 0 });

    constructor(setup: string, protected actorsClient: ActorsClient) {
        super();
        this.setupId = setup;
    }

    

    getElements(): Promise<any>{
        const query = {
            page: 1,
            pageSize: 5,
            filters: '',
            sorts: '',
        };

        if (this.sortConf) {
            let sorting = '';
            this.sortConf.forEach((fieldConf) => {
                sorting = sorting + `${fieldConf.direction.toUpperCase() === 'DESC' ? '-' : ''}${fieldConf.field},`;
            });
            query.sorts = sorting;
        }

         if (this.pagingConf && this.pagingConf['page'] && this.pagingConf['perPage']) {
            query.page = this.pagingConf['page'];
            query.pageSize = this.pagingConf['perPage'];
        } 

        if (this.filterConf.filters) {
            let filter = '';
            this.filterConf.filters.forEach((fieldConf) => {
                if (fieldConf['search']) {
                    let condition = '@=*';
                    if (fieldConf['field'] === 'id' || fieldConf['field'] === 'status') {
                        condition = '==';
                    }
                    filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
                }
            });
            filter += `${filter.length > 0 ? ',' : ''}setup.id==${this.setupId}`;
            query.filters = filter;
        }
        return this.actorsClient.getActors(query.filters,query.sorts,query.page,query.pageSize)
        .pipe(
            map(res => {
                this.paging.next({pageSize: query.pageSize, itemsCount: res.totalCount});
                this.data = res.items;
                return res.items;
            }),
            debounceTime(300),
        ).toPromise();
    }

    find(element: ActorDto): Promise<any> {
        const found = this.data.find(el => el.id === element.id);
        if (found) {
            return Promise.resolve(found);
        }

        return Promise.reject(new Error('Element was not found in the dataset'));
    }
    setPage(page, doEmit) {
        return this;
    }

    prepend(element) {
        return new Promise((resolve, reject) => {
            this.actorsClient.createActor(this.setupId, new CreateActorCommand({setupId:this.setupId,name:element.name,description:element.description})).subscribe(res => {
               let newelement = new ActorDto(element);
                newelement.id = res;
                newelement.setup = this.setupId;
                this.data.push(newelement);
                this.emitOnAdded(newelement);
                this.emitOnChanged('add');
                resolve();
                //super.add(element)
                //return super.prepend(newelement).then(()=>resolve() ).catch(err => reject(err));
                //super.add(newelement).then(() =>{ resolve()}).catch(err => reject(err));

            }, err => {
                reject(err);
            })
        })
    }

    remove(element: any): Promise<any> {
        return new Promise((resolve, reject) => {
            this.actorsClient.deleteActor(element.id, new DeleteActorCommand({ id: element.id })).subscribe(res => {
                super.remove(element).then(() => resolve()).catch(err => reject(err));
            }, err => {
                reject(err);
            });
        })
    }


    /* static deepExtend = function(...objects: Array<any>): any {
        if (arguments.length < 1 || typeof arguments[0] !== 'object') {
          return false;
        }
      
        if (arguments.length < 2) {
          return arguments[0];
        }
      
        const target = arguments[0];
      
        // convert arguments to array and cut off target object
        const args = Array.prototype.slice.call(arguments, 1);
      
        let val, src;
      
        args.forEach((obj: any) => {
          // skip argument if it is array or isn't object
          if (typeof obj !== 'object' || Array.isArray(obj)) {
            return;
          }
      
          Object.keys(obj).forEach(function (key) {
            src = target[key]; // source value
            val = obj[key]; // new value
      
            // recursion prevention
            if (val === target) {
              return;
      
              
            } else if (typeof val !== 'object' || val === null) {
              target[key] = val;
              return;
      
              // just clone arrays (and recursive clone objects inside)
            } else if (Array.isArray(val)) {
              target[key] = cloneDeep(val);
              return;
      
              // overwrite by new value if source isn't object or array
            } else if (typeof src !== 'object' || src === null || Array.isArray(src)) {
              target[key] = ActorsDataSource.deepExtend({}, val);
              return;
      
              // source value and new value is objects both, extending...
            } else {
              target[key] = ActorsDataSource.deepExtend(src, val);
              return;
            }
          });
        });
      
        return target;
      }; */


    update(element: ActorDto, values) {
        return new Promise((resolve, reject) => {
            this.actorsClient.updateActor(element.id, new UpdateActorCommand(
                {
                    id: element.id,
                    name: values.name,
                    description: values.description,
                }
            )).subscribe(res => {
                /* super.update(element, values)
                    .then(() => {
                        debugger;
                        resolve();
                    })
                    .catch(er => {
                        reject(er)
                    }); */
                    
                this.find(element).then(found => {
                    found.name = values.name;
                    found.description = values.description; 
                    //found = ActorsDataSource.deepExtend(found,values);
                    this.emitOnUpdated(element);
                    this.emitOnChanged('update');
                    
                    resolve();
                }).catch(err => {
                    reject(err);
                })
            }, err => {
                reject(err);
            })
        })
    }





}
import { LocalDataSource } from "ng2-smart-table"
import { BehaviorSubject } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { deepExtend } from 'src/app/helpers/objects';
import { CreateSetupCommand, DeleteSetupCommand, EnumsClient, PaginatedListOfSetupsDto, SetupsClient, SetupsDto, UpdateSetupDetailsCommand } from 'src/app/web-api-client';

export class SetupsDataSource extends LocalDataSource {

    paging: BehaviorSubject<{ pageSize: number, itemsCount: number }> = new BehaviorSubject({ pageSize: 5, itemsCount: 0 });

    constructor(protected setupsClient: SetupsClient, protected enumsClient: EnumsClient) {
        super();
    }

    find(element: SetupsDto): Promise<any> {
        const found = this.data.find(el => el.id === element.id);
        if (found) {
            return Promise.resolve(found);
        }

        return Promise.reject(new Error('Element was not found in the dataset'));
    }

    update(element: SetupsDto, values) {
        return new Promise((resolve, reject) => {
            this.setupsClient.updateSetup(element.id, new UpdateSetupDetailsCommand(
                {
                    id: element.id,
                    name: values.name,
                    shortName: values.shortName,
                    description: values.description,
                    status: values.status
                }
            )).subscribe(res => {
                this.find(element).then((found) => {
                    found = deepExtend(found, values);
                    /*const elIndx = this.data.findIndex(s => s.id === element.id);
                    this.data[elIndx] = found;*/
                    super.update(found, values).then(resolve).catch(reject);
                }).catch(reject);
            }, err => {
                reject(err);
            })
        })
    }

    remove(element: any): Promise<any> {
        return new Promise((resolve, reject) => {
            this.setupsClient.deleteSetup(element.id, new DeleteSetupCommand({ id: element.id })).subscribe(res => {
                super.remove(element).then(() => resolve()).catch(err => reject(err));
            }, err => {
                reject(err);
            });
        })
    }

    prepend(element) {
        debugger;
        return new Promise((resolve, reject) => {
            this.setupsClient.createSetup(new CreateSetupCommand(element)).subscribe(res => {
                super.add(element).then(() => resolve()).catch(err => reject(err));
            }, err => {
                reject(err);
            })
        })
    }

    setPage(page, doEmit) {
        return this;
    }

    getElements(): Promise<any> {
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
            query.filters = filter;
        }

        return this.setupsClient.getSetups(query.filters, query.sorts, query.page, query.pageSize)
            .pipe(
                map(res => {
                    this.paging.next({ pageSize: query.pageSize, itemsCount: +res.totalCount })
                    this.data = res.items;
                    return res.items;
                }),
                debounceTime(300),
            )
            .toPromise();

    }
}
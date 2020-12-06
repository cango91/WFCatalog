import { NgbPaginationNumber } from '@ng-bootstrap/ng-bootstrap';
import { doesNotReject } from 'assert';
import { LocalDataSource } from 'ng2-smart-table';
import { BehaviorSubject } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { CreateWorkflowCommand, DeleteWorkflowCommand, EnumInfoDto, EnumsClient, PaginatedListOfWorkflowsDto, UpdateWorkflowDetailsCommand, WorkflowsClient, WorkflowsDto } from 'src/app/web-api-client';

export class WokrflowsGridDataSource extends LocalDataSource {

    workflowTypes: EnumInfoDto[];
    paging: BehaviorSubject<{ pageSize: number, itemsCount: number }> = new BehaviorSubject({ pageSize: 5, itemsCount: 0 });
    setupId: string;

    constructor(setupId: string, private workflowsClient: WorkflowsClient, private enumsClient: EnumsClient){
        super();

        this.setupId = setupId;
        this.enumsClient.getEnumsInfo().subscribe(x => {
            this.workflowTypes = x.workflowTypes;
        })
    }
    
    find(element: WorkflowsDto): Promise<any> {
        const found = this.data.find(el => el.id === element.id);
        if (found) {
            return Promise.resolve(found);
        }

        return Promise.reject(new Error('Element was not found in the dataset'));
    }

    update(element: WorkflowsDto, values) {
        return new Promise((resolve, reject) => {
            this.workflowsClient.updateWorkflow(element.id, new UpdateWorkflowDetailsCommand(
                {
                    id: element.id,
                    name: values.name,
                    description: values.description,
                    workflowType: values.workflowType,
                    
                }
            )).subscribe(res => {
                super.update(element, values)
                    .then(() => {
                        resolve();
                    })
                    .catch(er => {
                        reject(er)
                    });
            }, err => {
                reject(err);
            })
        })
    }

    remove(element: any): Promise<any> {
        return new Promise((resolve, reject) => {
            this.workflowsClient.deleteWorkflow(element.id, new DeleteWorkflowCommand({ id: element.id })).subscribe(res => {
                super.remove(element).then(() => resolve()).catch(err => reject(err));
            }, err => {
                reject(err);
            });
        })
    }

    add(element) {
        return new Promise((resolve, reject) => {
            this.workflowsClient.createWorkflow(new CreateWorkflowCommand(element)).subscribe(res => {
                super.add(element).then(() => resolve()).catch(err => reject(err));
            }, err => {
                reject(err);
            })
        })
    }

    setPage(page, doEmit) {
        return this;
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
                    if (fieldConf['field'] === 'id' || fieldConf['field'] === 'status' || fieldConf['field'] === 'workflowType') {
                        condition = '==';
                    }
                    filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
                }
            });
            filter += `${filter.length > 0 ? ',' : ''}setupId==${this.setupId}`;
            query.filters = filter;
        }
        

        return this.workflowsClient.getWorkflows(query.filters,query.sorts,query.page,query.pageSize)
        .pipe(
            map(res => {
                    this.paging.next({ pageSize: query.pageSize, itemsCount: +res.totalCount });
                    this.data = res.items;
                    return res.items;
            }),
            debounceTime(300),
        ).toPromise();
    }




}
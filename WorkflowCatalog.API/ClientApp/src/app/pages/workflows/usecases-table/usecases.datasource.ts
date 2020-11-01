import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { UseCasesClient, WorkflowTypeDto } from 'src/app/web-api-client';
import { isNil } from 'lodash';

export class UseCasesDatasource extends LocalDataSource {
    lastRequestCount: number = 0;
    workflowId: number;

    /**
     *
     */
    constructor(protected useCasesClient: UseCasesClient, workflowId?: number) {
        super();
        this.workflowId = workflowId;
    }

    count(): number {
        return this.lastRequestCount;
    }

    updateWorkflow(workflowId: number) {
        this.workflowId = workflowId;
        this.refresh();
    }

    getElements(): Promise<any> {
        if (isNil(this.workflowId)) {
            return Promise.resolve([]);
        }
        const query = {
            page: 1,
            pageSize: 50,
            filters: '',
            sorts: ''
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
                    if (fieldConf['field'] === 'id' || fieldConf['field'] === 'type') {
                        condition = '==';
                    }
                    filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
                }
            });
            filter = filter + `${filter.length > 0 ? ',' : ''}workflowId==${this.workflowId}`;
            query.filters = filter;
        }

        return this.useCasesClient.get(query.filters, query.sorts, query.page, query.pageSize)
            .pipe(
                map(res => {
                    this.lastRequestCount = +res.totalCount;
                    return res.items;
                }),
                debounceTime(300),
            ).toPromise();
    }
}
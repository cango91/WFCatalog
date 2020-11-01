import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { WorkflowsClient, WorkflowTypeDto } from 'src/app/web-api-client';
import { isNil } from 'lodash';

export class WorkflowsDatasource extends LocalDataSource {
    lastRequestCount: number = 0;
    setupId: number;
    workflowTypes: Array<WorkflowTypeDto> = [];

    /**
     *
     */
    constructor(protected workflowsClient: WorkflowsClient, setupId?: number) {
        super();
        this.setupId = setupId;
    }

    count(): number {
        return this.lastRequestCount;
    }

    updateSetup(id: number) {
        this.setupId = id;
        this.refresh();
    }

    getElements(): Promise<any> {
        if (isNil(this.setupId)) {
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
            filter = filter + `${filter.length > 0 ? ',' : ''}setupId==${this.setupId}`;
            query.filters = filter;
        }

        return this.workflowsClient.getWorkflows(query.filters, query.sorts, query.page, query.pageSize)
            .pipe(
                map(res => {
                    this.lastRequestCount = +res.workflows.totalCount;
                    this.workflowTypes = res.workflowTypes;
                    return res.workflows.items;
                }),
                debounceTime(300),
            ).toPromise();
    }
}
import { AfterViewInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { CreateWorkflowCommand, DeleteWorkflowCommand, EnumInfoDto, EnumsClient, PaginatedListOfWorkflowsDto, UpdateWorkflowDetailsCommand, WorkflowsClient } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';

export class WokrflowsGridDataSource extends LocalDataSource {

    private _page: number = 1;
    private _pageSize: number = 5;
    private _sorts = '';
    private _filters = '';

    paginatedWorkflows: PaginatedListOfWorkflowsDto;
    lastRequestCount: number;

    workflowTypes;

    setupId: string;

    get filters(): string {
        return this._filters;
    }
    set filters(val: string) {
        this._filters = val;
    }

    get sorts(): string {
        return this._sorts;
    }

    set sorts(val:string){
        this._sorts = val;
    }

    get pageCount(): number {
        return this.paginatedWorkflows ? (this.paginatedWorkflows.totalPages ? this.paginatedWorkflows.totalPages : 0) : 0;
    }
    get pageSize(): number {
        return this._pageSize;
    };
    set pageSize(val: number){
        this._pageSize = val;
    }

    get page():number {
        return this._page;
    }

    set page(val:number){
        this._page = val;
    }

    get itemCount(): number{
        return this.paginatedWorkflows ? (this.paginatedWorkflows.totalCount ? this.paginatedWorkflows.totalCount : 0) : 0;
    }

    constructor(setupId: string, private workflowsClient: WorkflowsClient, private enumsClient: EnumsClient, config: PaginatedQueryConfig){
        super();
        this.setupId = setupId;
        this.page = config.page;
        this.pageSize = config.pageSize;

        if(config.filters){
            this.filterConf = config.filters;
        }

        if(config.sorts){
            this.sortConf = config.sorts;
        }

        this.enumsClient.getEnumsInfo().subscribe(x => {
            this.workflowTypes = x.workflowTypes;
        })
    }
    

    getElements(): Promise<any>{
        const query = {
            page: this.page,
            pageSize: this.pageSize,
            filters: this.filters,
            sorts: this.sorts,
        };

        if (this.sortConf) {
            let sorting = '';
            this.sortConf.forEach((fieldConf) => {
                sorting = sorting + `${fieldConf.direction.toUpperCase() === 'DESC' ? '-' : ''}${fieldConf.field},`;
            });
            query.sorts = sorting;
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
                this.lastRequestCount = +res.totalCount;
                this.paginatedWorkflows = res;
                return this.paginatedWorkflows.items;
            }),
            debounceTime(300),
        ).toPromise();
    }

    remove(element: any): Promise<any>{
        return this.workflowsClient.deleteWorkflow(element.id,new DeleteWorkflowCommand({id: element.id})).toPromise();
    }

    update(element: any, values: any): Promise<any>{
        return this.workflowsClient.updateWorkflow(element.id,
            new UpdateWorkflowDetailsCommand({
                id: element.id,
                name: values.name,
                description: values.description,
                workflowType: parseInt(values.type),
            })).toPromise();
    }


     prepend(element: any) : Promise<any> {
        return this.workflowsClient.createWorkflow(new CreateWorkflowCommand({
            name: element.name,
            description: element.description,
            workflowType: parseInt(element.workflowType),
            setupId: element.setupId,
        })).toPromise();
    } 



}
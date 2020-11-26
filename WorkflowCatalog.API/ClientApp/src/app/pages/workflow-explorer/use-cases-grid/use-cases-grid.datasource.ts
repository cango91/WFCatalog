import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { CreateUseCaseCommand, DeleteUseCaseCommand, PaginatedListOfUseCaseDto, UpdateUseCaseDetailsCommand, UseCasesClient } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';

export class UseCasesGridDataSource extends LocalDataSource{
    
    private _page: number = 1;
    private _pageSize: number = 5;
    private _sorts = '';
    private _filters = '';

    paginatedUseCases: PaginatedListOfUseCaseDto;
    lastRequestCount: number;

    workflowId: string;

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
        return this.paginatedUseCases ? (this.paginatedUseCases.totalPages ? this.paginatedUseCases.totalPages : 0) : 0;
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
        return this.paginatedUseCases ? (this.paginatedUseCases.totalCount ? this.paginatedUseCases.totalCount : 0) : 0;
    }

    
    constructor(workflowId: string, private useCasesClient: UseCasesClient, config: PaginatedQueryConfig){
        super();

        this.page = config.page;
        this.pageSize = config.pageSize;

        if(config.filters){
            this.filterConf = config.filters;
        }

        if(config.sorts){
            this.sortConf = config.sorts;
        }
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
                    if (fieldConf['field'] === 'id' || fieldConf['field'] === 'status' || fieldConf['field'] === 'workflowId') {
                        condition = '==';
                    }
                    filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
                }
            });
            filter += `${filter.length > 0 ? ',' : ''}workflowId==${this.workflowId}`;
            query.filters = filter;
        }

        return this.useCasesClient.getUseCases(query.filters,query.sorts,query.page,query.pageSize)
        .pipe(
            map(res => {
                this.lastRequestCount = +res.totalCount;
                this.paginatedUseCases = res;
                return this.paginatedUseCases.items;
            }),
            debounceTime(300),
        ).toPromise();
    }

    remove(element: any): Promise<any>{
        return this.useCasesClient.deleteUseCase(element.id,new DeleteUseCaseCommand({id: element.id})).toPromise();
    }

    update(element: any, values: any): Promise<any>{
        return this.useCasesClient.updateUseCaseDetails(element.id,
            new UpdateUseCaseDetailsCommand({
                id: element.id,
                name: values.name,
                description: values.description,
                actors: values.actors,
                preconditions: values.preconditions,
                postconditions: values.postconditions,
                normalCourse: values.normalCourse,
                altCourse: values.altCourse                
            })).toPromise();
    }


     prepend(element: any) : Promise<any> {
        return this.useCasesClient.createUseCase(new CreateUseCaseCommand({
            workflowId: this.workflowId,
            name: element.name,
            description: element.description,
            actors: element.actors,
            preconditions: element.preconditions,
            postconditions: element.postconditions,
            normalCourse: element.normalCourse,
            altCourse: element.altCourse,
        })).toPromise();
    } 
}
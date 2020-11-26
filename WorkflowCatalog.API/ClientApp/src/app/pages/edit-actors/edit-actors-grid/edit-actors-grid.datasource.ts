import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { ActorsClient, CreateActorCommand, DeleteActorCommand, PaginatedListOfUCActorDto, UCActorDto, UpdateActorCommand } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';


export class ActorsDataSource extends LocalDataSource {
    paginatedActors: PaginatedListOfUCActorDto;
    actors: UCActorDto[];
    setupId: string;


    protected _filters: string='';
    protected _sorts: string='';
    protected _page: number = 1;
    protected _pageSize: number = 5;

    lastRequestCount: number = 0;

    get filters(): string {
        return this._filters;
    }

    set filters(val:string){
        this._filters = val;
    }

    get sorts(): string {
        return this._sorts;
    }

    set sorts(val:string){
        this._sorts = val;
    }

    get pageCount(): number {
        return this.paginatedActors ? (this.paginatedActors.totalPages ? this.paginatedActors.totalPages : 0) : 0;
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
        return this.paginatedActors ? (this.paginatedActors.totalCount ? this.paginatedActors.totalCount : 0) : 0;
    }

    /**
     *
     */
    constructor(setup: string, protected actorsClient: ActorsClient, config: PaginatedQueryConfig = new PaginatedQueryConfig()) {
        super();
        this.setupId = setup;
        this.pageSize = config.pageSize;
        this.page = config.page;
        if(config.sorts){
            this.sortConf = config.sorts;
        }

        if(config.filters){
            this.filterConf = config.filters;
        }

/*         if (this.sortConf) {
            let sorting = '';
            this.sortConf.forEach((fieldConf) => {
                sorting = sorting + `${fieldConf.direction.toUpperCase() === 'DESC' ? '-' : ''}${fieldConf.field},`;
            });
            this.sorts = sorting;
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
            filter += `${filter.length > 0 ? ',' : ''}setup==${setup}`;
            this.filters = filter;
        } */

/*         this.actorsClient.getActors(this.filters,this.sorts,this.page,this.pageSize).subscribe(x => {
            this.paginatedActors = x;
        }); */
    }

    

    getElements(): Promise<any>{
        //console.log('getElements()');
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

/*         if (this.pagingConf && this.pagingConf['page'] && this.pagingConf['perPage']) {
            query.page = this.pagingConf['page'];
            query.pageSize = this.pagingConf['perPage'];
        } */

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
            filter += `${filter.length > 0 ? ',' : ''}setup==${this.setupId}`;
            query.filters = filter;
        }

        return this.actorsClient.getActors(query.filters,query.sorts,query.page,query.pageSize)
        .pipe(
            map(res => {
                this.lastRequestCount = +res.totalCount;
                this.paginatedActors = res;
                return this.paginatedActors.items;
            }),
            debounceTime(300),
        ).toPromise();
    }

    prepend(element: any) : Promise<any> {
        return this.actorsClient.createActor(this.setupId,new CreateActorCommand({
            setupId: this.setupId,
            name: element.name,
            description: element.description
        })).toPromise();
    }

    remove(element: any): Promise<any>{
        return this.actorsClient.deleteActor(element.id,new DeleteActorCommand({id: element.id})).toPromise();
    }

    update(element: any, values: any): Promise<any>{
        //console.log(values);
        return this.actorsClient.updateActor(element.id,
            new UpdateActorCommand({
                id: element.id,
                name: values.name,
                description: values.description,
            })).toPromise();
    }





}
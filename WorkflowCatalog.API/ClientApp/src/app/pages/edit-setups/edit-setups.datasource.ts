import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { isNil } from 'lodash';
import { CreateSetupCommand, DeleteSetupCommand, EnumsClient, PaginatedListOfSetupsDto, SetupsClient, SetupsDto, SetupStatus, UpdateSetupDetailsCommand } from 'src/app/web-api-client';
import { PaginatedQueryConfig } from 'src/app/_models/paginated-query-config.model';

export class SetupsDataSource extends LocalDataSource{

    //setups : SetupsDto[];
    paginatedSetups: PaginatedListOfSetupsDto;
    setupStatuses;
    lastRequestCount : number = 0;

    protected _pageSize: number = 5;
    protected _page: number = 1;
    protected _filters: string = '';
    protected _sorts: string = '';

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
        return this.paginatedSetups ? (this.paginatedSetups.totalPages ? this.paginatedSetups.totalPages : 0) : 0;
    }
    get pageSize(): number {
        return this._pageSize;
    };
    set pageSize(val: number){
        this._pageSize = val;
       /*  super.setPaging(this._page,this._pageSize); */
    }

    get page():number {
        return this._page;
    }

    set page(val:number){
        this._page = val;
/*         super.setPage(val);
        this.refresh(); */
    }

    get itemCount(): number{
        return this.paginatedSetups ? (this.paginatedSetups.totalCount ? this.paginatedSetups.totalCount : 0) : 0;
    }

    constructor(private setupsClient:SetupsClient, private enumsClient: EnumsClient, config: PaginatedQueryConfig = new PaginatedQueryConfig()) {
        super();

        this.page = config.page;
        this.pageSize = config.pageSize;
        if(config.filters)
        {this.filterConf = config.filters;}
        if(config.sorts)
       { this.sortConf  = config.sorts;}
/* 
        if (this.sortConf) {
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
            this.filters = filter;
        } */
/*         setupsClient.getSetups(this.filters,this.sorts,this.page,this.pageSize).subscribe(x => {
            this.paginatedSetups = x;
        }) */
        enumsClient.getEnumsInfo().subscribe(x => {
            this.setupStatuses = x.setupStatuses;
        })
    }

 
    getTotalCount(): number {
        if(isNil(this.paginatedSetups.totalCount)){
            return 0;
        }
        return this.paginatedSetups.totalCount;
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
                    if (fieldConf['field'] === 'id' || fieldConf['field'] === 'status') {
                        condition = '==';
                    }
                    filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
                }
            });
            query.filters = filter;
        }

        return this.setupsClient.getSetups(query.filters,query.sorts,query.page,query.pageSize)
        .pipe(
            map(res => {
                this.lastRequestCount = +res.totalCount;
                this.paginatedSetups = res;
                return this.paginatedSetups.items;
            }),
            debounceTime(300),
        ).toPromise();
    }

    remove(element: any): Promise<any>{
        return this.setupsClient.deleteSetup(element.id,new DeleteSetupCommand({id: element.id})).toPromise();
    }

    update(element: any, values: any): Promise<any>{
        //console.log(values);
        return this.setupsClient.updateSetup(element.id,
            new UpdateSetupDetailsCommand({
                id: element.id,
                name: values.name,
                shortName: values.shortName,
                description: values.description,
                status: parseInt(values.status)
            })).toPromise();
    }
    


     prepend(element: any) : Promise<any> {
        return this.setupsClient.createSetup(new CreateSetupCommand({
            name: element.name,
            shortName: element.shortName,
            description: element.description
        })).toPromise();
    } 

}
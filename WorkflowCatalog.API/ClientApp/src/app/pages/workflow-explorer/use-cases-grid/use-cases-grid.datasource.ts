import { LocalDataSource } from 'ng2-smart-table';
import { debounceTime, map } from 'rxjs/operators';
import { CreateUseCaseCommand, DeleteUseCaseCommand, PaginatedListOfUseCaseDto, UpdateUseCaseDetailsCommand, UseCaseDto, UseCasesClient } from 'src/app/web-api-client';
import { isNil } from 'lodash';
import { BehaviorSubject, of } from 'rxjs';

export class UseCasesGridDataSource extends LocalDataSource {

paging: BehaviorSubject<{ pageSize: number, itemsCount: number }> = new BehaviorSubject({ pageSize: 5, itemsCount: 0 });

  workflowId: string;

  

  constructor(workflowId: string, private useCasesClient: UseCasesClient) {
    super();
    this.workflowId = workflowId;

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
          if (fieldConf['field'] === 'id' || fieldConf['field'] === 'status' || fieldConf['field'] === 'workflowId') {
            condition = '==';
          }
          filter = filter + `${filter.length > 0 ? ',' : ''}${fieldConf['field']}${condition}${fieldConf['search']}`;
        }
      });
      filter += `${filter.length > 0 ? ',' : ''}workflow.id==${this.workflowId}`;
      query.filters = filter;
    }

    return this.useCasesClient.getUseCases(query.filters, query.sorts, query.page, query.pageSize)
      .pipe(
        map(res => {
          this.paging.next({pageSize: query.pageSize, itemsCount: res.totalCount})
          this.data = res.items;
          return this.data;
        }),
        debounceTime(300),
      ).toPromise();
  }

  find(element: UseCaseDto): Promise<any> {
    const found = this.data.find(el => el.id === element.id);
    if (found) {
        return Promise.resolve(found);
    }

    return Promise.reject(new Error('Element was not found in the dataset'));
}

update(element: UseCaseDto, values) {
    return new Promise((resolve, reject) => {
        this.useCasesClient.updateUseCaseDetails(element.id, new UpdateUseCaseDetailsCommand(
            {
                id: element.id,
                name: values.name,
                description: values.description,
                altCourse: values.altCourse,
                normalCourse: values.normalCourse,
                preconditions: values.preconditions,
                postconditions: values.postconditions,
                actors: values.actors,
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
        this.useCasesClient.deleteUseCase(element.id, new DeleteUseCaseCommand({ id: element.id })).subscribe(res => {
            super.remove(element).then(() => resolve()).catch(err => reject(err));
        }, err => {
            reject(err);
        });
    })
}

add(element) {
    return new Promise((resolve, reject) => {
        this.useCasesClient.createUseCase(new CreateUseCaseCommand(element)).subscribe(res => {
            super.add(element).then(() => resolve()).catch(err => reject(err));
        }, err => {
            reject(err);
        })
    })
}

setPage(page, doEmit) {
    return this;
}

  
}

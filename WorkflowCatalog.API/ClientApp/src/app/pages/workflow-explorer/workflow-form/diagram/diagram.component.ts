import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { DiagramsClient, DiagramsMetaDto, PaginatedListOfDiagramsMetaDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-diagram',
  templateUrl: './diagram.component.html',
  styleUrls: ['./diagram.component.scss']
})
export class DiagramComponent implements OnInit {

  @Input()
  workflowId : string;

  @Input()
  currentPrimaryDiagramId: string ='';

  @Output()
  onPrimaryWorkflowSelected: EventEmitter<any> = new EventEmitter();

  @ViewChild(NgbPagination) pagination: NgbPagination;

  paging: BehaviorSubject<{page:number, pageSize:number}> = new BehaviorSubject({page:1,pageSize:5})
  itemsCount : number = 0;
  pageSize=5;
  page = 1;
  


  uploadedFiles: DiagramsMetaDto[] = []

  editMode: boolean = false;

  tableItems: {id:string,name:string,isPrimary:boolean}[] = []



  constructor(protected diagramService: DiagramsClient) { }

  ngOnInit(): void {
    if(this.workflowId){
      this.editMode = true;
      //console.log(this.workflowId);
      this.paging.subscribe(res => {
        this.refreshItems();
      })

    }
    
  }

  refreshItems(){
    this.tableItems = [];
    this.diagramService.getDiagramsMetaData('workflowId=='+ this.workflowId,null,this.pagination ? this.pagination.page : 1,this.pageSize).subscribe(res => {
      this.itemsCount = res.totalCount;
      //debugger;
      res.items.map(x => {
        this.tableItems.push({id:x.id, name: x.name, isPrimary: this.currentPrimaryDiagramId === x.id});
      })
    })
  }


  handlePageChange(event){
    this.paging.next({page: event,pageSize: this.pageSize});
  }

  toggleRow(event){
    //console.log(event);
  }

}

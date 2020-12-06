import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { DefaultFilter } from 'ng2-smart-table';
import { filter, map, mergeMap } from 'rxjs/operators';
import { ActorsClient, UCActorDto } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';

@Component({
  selector: 'app-actors-filter',
  templateUrl: './actors-filter.component.html',
  styleUrls: ['./actors-filter.component.scss']
})
export class ActorsFilterComponent extends DefaultFilter implements OnInit, OnChanges {

  actorsList : UCActorDto[];
  items: Array<any>;
  filteredActors: Array<UCActorDto> = new Array<UCActorDto>();

  settings = {
    singleSelection: false,
                idField: 'id',
                textField: 'text',
                selectAllText: 'Select All',
                unSelectAllText: 'UnSelect All',
                itemsShowLimit: 1,
                allowSearchFilter: true
  }

  @Input()
  setupId: string;
  constructor(private actorsClient: ActorsClient, private setupService: SetupService) { 
    super();
  }
    ngOnChanges(changes: SimpleChanges): void {
        // throw new Error("Method not implemented.");
    }

  ngOnInit(): void {
    this.setupService.currentSetupId.subscribe(x => {
      this.setupId = x
      if (this.setupId) {
        this.refreshActorsList();
      }
    })
  }

  setSetupId(id: string){
    this.setupId = id;
    this.refreshActorsList();
  }

  refreshValue(data: any){
    console.log('refreshed');
  }

  refreshActorsList(){
    this.actorsClient.getActors(`setup.id==${this.setupId}`,'name',null,null)
    .pipe(
      map(x => x.items))
    .subscribe(x => {
      this.actorsList = x;
      this.items = this.actorsList.map(a => ({id:a.id,text:a.name}));
    });
    console.log('refresh called');
  }

  selected(data: any){
    console.log('item selected');
    this.filteredActors.push(this.actorsList.find(x => x.id === data.id));
  }

  removed(data: any){
    this.filteredActors.splice(this.filteredActors.findIndex(x => x.id === data.id));
  }

}

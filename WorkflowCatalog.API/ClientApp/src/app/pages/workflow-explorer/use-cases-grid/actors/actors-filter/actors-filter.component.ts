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
    //console.log('filter inited');
    this.setupService.currentSetupId.subscribe(x => {
      //console.log('new setupid received')
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

  refreshActorsList(){
    this.actorsClient.getActors(`setup.id==${this.setupId}`,'name',null,null)
    .pipe(
      map(x => x.items))
    .subscribe(x => {
      this.actorsList = x;
      this.items = this.actorsList.map(a => ({id:a.id,text:a.name}));
    });
    //console.log('refresh called');
  }

  refreshFilterValue(data){
    this.query = data.map(s => s.id).join('|');
    this.setFilter();
  }

}

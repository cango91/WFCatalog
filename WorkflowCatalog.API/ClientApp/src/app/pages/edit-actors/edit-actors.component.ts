import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NbDialogService } from '@nebular/theme';
import { ActorsClient, SetupsClient, SetupsDto } from 'src/app/web-api-client';
import { EditActorsGridComponent} from './edit-actors-grid/edit-actors-grid.component';

@Component({
  selector: 'app-edit-actors',
  templateUrl: './edit-actors.component.html',
  styleUrls: ['./edit-actors.component.scss']
})
export class EditActorsComponent implements OnInit {

  @ViewChild(EditActorsGridComponent) actorsGrid: EditActorsGridComponent;

  setupId: string;
  get setupName(): string {
    return this.setups.filter(x => {
      return x.id === this.setupId;
    }).map(x => {
      return x.name;
    }).pop();
  }
  get setupAbbreviation(): string {
    return this.setups.filter(x => {
      return x.id === this.setupId;
    }).map(x => {
      return x.shortName;
    }).pop();
  }
  
  setups: SetupsDto[];
  constructor(protected activatedRoute: ActivatedRoute, private setupsClient: SetupsClient,private actorsClient: ActorsClient) { }

  ngOnInit(): void {
    this.setupsClient.getSetups(null,null,1,null).subscribe(x => {
      this.setups = x.items;
    })
  }

  handleSetupChange(event: any){
    this.setupId = event;
  }



}
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SetupsClient, SingleSetupDto, SingleWorkflowDto } from 'src/app/web-api-client';
import { SetupService } from 'src/app/_providers/setup.service';

@Component({
  selector: 'app-workflow-explorer',
  templateUrl: './workflow-explorer.component.html',
  styleUrls: ['./workflow-explorer.component.scss']
})
export class WorkflowExplorerComponent implements OnInit {
  setupId: string;
  workflowId: string;
  setup: SingleSetupDto;
  workflowName: string;
  
  constructor(protected activatedRoute: ActivatedRoute,protected setupsClient: SetupsClient, @Inject(AppComponent) protected parent: AppComponent, private setupService: SetupService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(data => {
      this.setupId = data.id;
      console.log(`Nexting ${this.setupId}`);
      this.setupService.currentSetupId.next(this.setupId);
      this.setupsClient.getSetupById(this.setupId).subscribe( x => {
        this.setup = x;
      });
    });
  }

  handleWorkflowSelect(event: any){
    this.workflowId = event.id;
    this.workflowName = event.name;
  }

}

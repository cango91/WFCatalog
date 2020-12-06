import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NbMenuItem, NB_TIME_PICKER_CONFIG } from '@nebular/theme';
import { PaginatedListOfSetupsDto, SetupsClient } from './web-api-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Workflow Catalog';
  preventAbuse = false;

  items : NbMenuItem[] =[];

  products: NbMenuItem[] = [
    {
      title: 'Cerebral Plus',
      link : '/pages/fc',
    },
    {title: 'Team',
    link : '/pages/fc',
  },
    {title: 'MyTicket',
    link : '/pages/fc',}
  ]

  /**
   *
   */
  constructor(private http: HttpClient, private setupsClient: SetupsClient) {
  }

  testHttp(){
    this.preventAbuse = true;
    this.http.get("/api/Setups").subscribe(res =>
      {
        console.log(res);
        setTimeout(() => {
          this.preventAbuse = false;
        }, 800)
      })
  }

  ngOnInit(){
    this.setupsClient.getSetups('status==1',null,null,null).subscribe(res =>{
      this.updateSetups(res.items);
    })
  }

  public refresh(){
    this.setupsClient.getSetups('status==1',null,1,100).subscribe(res =>{
      this.updateSetups(res.items);
    })
  }
  private updateSetups(res:any){
    let children: NbMenuItem[] = [];
    res.forEach(element => {
        if(element.status === 1){
        children.push({
          title: element.shortName.toLocaleUpperCase(),
          link: 'pages/wf/' + element.id,
        badge: {
          text: element.workflowCount.toString(),
          status: 'info'
        }});
      }
    });
    this.items = [{
      title: 'Function Catalog',
      expanded: false,
      children: this.products
    },
    {
      title: 'Workflow Catalog',
      expanded: true,
      children: children
    }];
  }
}

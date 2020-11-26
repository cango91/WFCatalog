import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditActorsComponent } from './edit-actors/edit-actors.component';
import { EditSetupsComponent } from './edit-setups/edit-setups.component';
import { FunctionExplorerComponent } from './function-explorer/function-explorer.component';
import { PagesComponent } from './pages.component';
import { WorkflowExplorerComponent } from './workflow-explorer/workflow-explorer.component';




const routes : Routes = [{
    path: '',
    component: PagesComponent,
    children: [
        {
            path: 'editactors',
            component: EditActorsComponent,
        },
        {
            path: 'editsetups',
            component: EditSetupsComponent,
        },
        {
            path: 'fc',
            component: FunctionExplorerComponent,
        },
        {
            path: 'wf/:id',
            component: WorkflowExplorerComponent,
        },
        {
            path: '',
            redirectTo: 'fc',
            pathMatch: 'full'
        }
    ]
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PagesRoutingModule {

}
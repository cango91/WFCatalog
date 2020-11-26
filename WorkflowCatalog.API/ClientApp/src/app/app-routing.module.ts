import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions } from '@angular/router';
import { AppComponent } from './app.component';
import { PagesComponent } from './pages/pages.component';

/* const routes: Routes = [
  {
    path: '',
    component: PagesComponent,
    children: [
    {
      path: 'functions/:id',
      component: AppComponent
    },
    {
      path: 'workflows/:id',
      redirectTo: 'functions/:id'
    }
    ]
  }
]; */

const routes: Routes = [
  {
    path: 'pages',
    loadChildren: () => import('./pages/pages.module')
    .then(m => m.PagesModule),
  },
  {path: '', redirectTo: 'pages', pathMatch:'full'},
  {path: '**', redirectTo: 'pages'},
];

const config: ExtraOptions = {
  useHash: false,
}

@NgModule({
  imports: [RouterModule.forRoot(routes,config)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

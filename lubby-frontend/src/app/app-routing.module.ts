import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";

import { CtLoginComponent } from './template-user/container-template/ct-login/ct-login.component';
import { AuthGuard } from './core/auth/auth.guard';

const routes: Routes = [
  { path: '', component: CtLoginComponent },
  { path: 'dash', canActivate: [AuthGuard],
    loadChildren: './template-dash/container-actions/container-dash.module#ContainerDashModule'},
]

@NgModule({
  imports: [ RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'}) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }

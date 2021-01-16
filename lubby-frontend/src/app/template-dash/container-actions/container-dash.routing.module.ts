import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from '../../core/auth/auth.guard';
import { ContainerDashRaizComponent } from './container-dash.raiz.component';
import { ContentComponent } from '../content/content.component';

import { CdUsuarioComponent } from './cd-usuario/list/cd-usuario.component';
import { CdUsuarioEditComponent } from './cd-usuario/edit/cd-usuario-edit.component';
import { CdUsuarioNewComponent } from './cd-usuario/new/cd-usuario-new.component';

import { CdProjetoComponent } from './cd-projetos/list/cd-projetos.component';
import { CdProjetoEditComponent } from './cd-projetos/edit/cd-projetos-edit.component';
import { CdProjetoNewComponent } from './cd-projetos/new/cd-projetos-new.component';
import { CdHorasComponent } from './cd-horas/list/cd-horas.component';
import { CdHorasEditComponent } from './cd-horas/edit/cd-horas-edit.component';
import { CdHorasNewComponent } from './cd-horas/new/cd-horas-new.component';


const routes: Routes = [
  { path: '',
    component: ContainerDashRaizComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component:  ContentComponent},

      { path: 'horas', component:  CdHorasComponent},
      { path: 'horas/edit/:id', component:  CdHorasEditComponent},
      { path: 'horas/new', component:  CdHorasNewComponent},      
      
      { path: 'usuarios', component:  CdUsuarioComponent},
      { path: 'usuarios/edit/:id', component:  CdUsuarioEditComponent},
      { path: 'usuarios/new', component:  CdUsuarioNewComponent},      
      
      { path: 'projetos', component:  CdProjetoComponent},
      { path: 'projetos/edit/:id', component:  CdProjetoEditComponent},
      { path: 'projetos/new', component:  CdProjetoNewComponent},
    ]
  }
]

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ContainerDashRoutingModule { }

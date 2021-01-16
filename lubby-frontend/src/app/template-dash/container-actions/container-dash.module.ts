import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';

// Other imports for animations
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ContainerDashRaizComponent } from './container-dash.raiz.component';
import { ContainerDashRoutingModule } from './container-dash.routing.module';
import { LayoutDashModule } from '../layout/layout.module';
import { UsuarioModule } from './cd-usuario/cd-usuario.module';
import { ProjetoModule } from './cd-projetos/cd-projetos.module';
import { HorasModule } from './cd-horas/cd-horas.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    LayoutDashModule,
    ContainerDashRoutingModule,
    HttpClientModule,
    UsuarioModule,
    ProjetoModule,
    HorasModule,
],
declarations: [
    ContainerDashRaizComponent,
  ],
  providers: [
  ],
  exports: [
      ContainerDashRaizComponent,
  ]
})
export class ContainerDashModule { }

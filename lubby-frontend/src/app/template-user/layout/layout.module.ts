import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutUserComponent } from './layout.component';
import { ContentUserComponent } from '../content/content.component';
import { HttpClientModule } from '@angular/common/http';
import { ContainerTemplateComponent } from '../container-template/ct-template/container-template.component';
import { Container } from '@angular/compiler/src/i18n/i18n_ast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CtLoginComponent } from '../container-template/ct-login/ct-login.component';
import { CtMenuComponent } from '../container-template/ct-menu/ct-menu.component';
import { LayoutDashModule } from '../../template-dash/layout/layout.module';
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    HttpClientModule,
    BrowserAnimationsModule,
    LayoutDashModule,
    RouterModule
  ],
  declarations: [
    LayoutUserComponent, 
    ContentUserComponent, 
    ContainerTemplateComponent,
    CtLoginComponent,
    CtMenuComponent
  ],
  providers: [],
  exports: [ LayoutUserComponent, ContainerTemplateComponent ]
})
export class LayoutUserModule { }

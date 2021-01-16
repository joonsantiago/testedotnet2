import { NgModule } from '@angular/core';

import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { LayoutDashComponent } from './layout.component';
import { ContentComponent } from '../content/content.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
  ],
  declarations: [
    LayoutDashComponent, NavbarComponent, SidebarComponent, ContentComponent
  ],
  providers: [],
  exports: [ LayoutDashComponent ]
})
export class LayoutDashModule { }

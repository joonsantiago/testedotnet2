import { NgModule } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CdHorasComponent } from './list/cd-horas.component';
import { CdHorasEditComponent } from './edit/cd-horas-edit.component';
import { CdHorasNewComponent } from './new/cd-horas-new.component';


@NgModule({
imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxPaginationModule,

],
declarations: [
    CdHorasComponent,
    CdHorasEditComponent,
    CdHorasNewComponent
  ],
providers: [ ],
exports: [  ]
})
export class HorasModule { }

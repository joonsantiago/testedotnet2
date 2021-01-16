import { NgModule } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CdProjetoComponent } from './list/cd-projetos.component';
import { CdProjetoEditComponent } from './edit/cd-projetos-edit.component';
import { CdProjetoNewComponent } from './new/cd-projetos-new.component';


@NgModule({
imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxPaginationModule
],
declarations: [
    CdProjetoComponent,
    CdProjetoEditComponent,
    CdProjetoNewComponent
  ],
providers: [ ],
exports: [  ]
})
export class ProjetoModule { }

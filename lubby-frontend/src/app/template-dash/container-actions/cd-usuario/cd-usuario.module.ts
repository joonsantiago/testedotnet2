import { NgModule } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CdUsuarioComponent } from './list/cd-usuario.component';
import { CdUsuarioEditComponent } from './edit/cd-usuario-edit.component';
import { CdUsuarioNewComponent } from './new/cd-usuario-new.component';



@NgModule({
imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgxPaginationModule
],
declarations: [
    CdUsuarioComponent,
    CdUsuarioEditComponent,
    CdUsuarioNewComponent,
  ],
providers: [ ],
exports: [  ]
})
export class UsuarioModule { }

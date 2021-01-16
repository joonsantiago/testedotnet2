import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'cd-usuario',
  templateUrl: 'cd-usuario.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdUsuarioComponent implements OnInit  {
    
    listagemUsuario: any = [];

    @Input() id: string;
    currP: number = 0;
    page: number = 0;
    listSize: number = 10
    @Input() maxSize: number;
    @Output() pageChange: EventEmitter<number> = new EventEmitter();

    constructor(
        private baseService: BaseService,
        private router: Router
        ) {
        
    }

    onPageStateChangeEvent(page) {
        this.currP = page
        this.page = (page - 1);
        this.listarUsuario()
    }
    
    ngOnInit() {
        this.listarUsuario();
    } 

    listarUsuario(){
        this.baseService.get(`/users?page=${this.page}&size=${this.listSize}`).subscribe(
            (response: any) => {
                this.listagemUsuario = response;          
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }
    
    confirmDelete(id) {
        
        swal(
            {
                title: 'Finalizar',
                text: 'Tudo conferido? Deseja continuar?',
                icon: 'info',
                buttons: ["Cancelar", "Enviar"],
            }
        ).then((next) => {
            if(next){
                this.excluir(id);
            }
          });
      }

    excluir(id: number){
        this.baseService.delete('/users/' + id).subscribe(
            (response: any) => {
                if(response.success){
                    swal("Excluído", "Usuário excluído com sucesso", "success");
                    this.listarUsuario();
                }
            }, 
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }
  
  }
  
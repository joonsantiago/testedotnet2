import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'cd-horas',
  templateUrl: 'cd-horas.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdHorasComponent implements OnInit  {
    
    listagemHora: any = [];

    @Input() id: string;
    currP: number = 0;
    page: number = 0;
    listSize: number = 10;
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
        this.listarHoras();
    }
    
    ngOnInit() {
        this.listarHoras();
    } 

    listarHoras(){
        this.baseService.get(`/work-hours?page=${this.page}&size=${this.listSize}`).subscribe(
            (response: any) => {
                this.listagemHora = response;                           
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
        this.baseService.delete('/work-hours/' + id).subscribe(
            (response: any) => {
                if(response.success){
                    swal("Excluído", "Horas excluída com sucesso", "success");
                    this.listarHoras();
                }
            }, 
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }
  
  }
  
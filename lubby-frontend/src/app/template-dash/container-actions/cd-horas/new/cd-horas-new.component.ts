import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-horas-new',
  templateUrl: 'cd-horas-new.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdHorasNewComponent implements OnInit  {
   
    idHoras: number = 0;
    dadosHoras: any = {
        projectId: '',
        userId: '',
        finishedAt: '',
        createdAt: ''
    };
    listagemProjetos: any = [];
    listagemUsuario: any = [];
    
    constructor(
        private baseService: BaseService,
        private router: Router
        ) { }
    
    ngOnInit() {
        this.buscarUsuarios()    
    } 

    
    buscarProjetos(userId){

        this.baseService.get(`/projects-user/${userId}/projects` ).subscribe(
           (response: any) => {
               this.listagemProjetos = response.data;          
           },
           (error) => {
               this.baseService.tratarError(error);
           }
       );
   }

   buscarUsuarios(){

        this.baseService.get(`/users` ).subscribe(
           (response: any) => {
               this.listagemUsuario = response.items;          
           },
           (error) => {
               this.baseService.tratarError(error);
           }
       );
   }
    
    finalizar() {
        swal(
            {
                title: 'Finalizar',
                text: 'Tudo conferido? Deseja continuar?',
                icon: 'info',
                buttons: ["Cancelar", "Enviar"],
            }
        ).then((next) => {
            if (next) {
                this.save();
            } else { }
          });
      }

    voltar(){
        this.router.navigate(["dash/horas"]);
    }

    save(){
        var dadosSalvar = {
            id: this.dadosHoras.id,
            projectId: this.dadosHoras.projectId,
            userId: this.dadosHoras.userId,
            createdAt: this.dadosHoras.createdAt
        }

        if(this.dadosHoras.finishedAt){
            dadosSalvar['finishedAt'] = this.dadosHoras.finishedAt;
        }

        this.baseService.post('/work-hours', JSON.stringify(dadosSalvar))
            .subscribe(
                (response: any) => { 
                    if(response.success){
                        swal("Salvo", "Horas registradas com sucesso!", "success");
                        this.router.navigate(['dash/horas']);                        
                    }
                 }, 
                (error) => {
                    this.baseService.tratarError(error);
                 }
            );     
    }    

    setProjectValue(val) {
        if(val > 0){
            this.dadosHoras.projectId = parseInt(val);
        }
    }

    setUserValue(val) {
        if(val > 0){
            this.dadosHoras.userId = parseInt(val);
            this.buscarProjetos(val);
        }
    }
  
  }
  
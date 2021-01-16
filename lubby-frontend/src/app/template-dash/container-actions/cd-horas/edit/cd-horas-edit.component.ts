import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-horas-edit',
  templateUrl: 'cd-horas-edit.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdHorasEditComponent implements OnInit  {
   
    horasId: number = 0;
    listagemProjetos: any = [];
    listagemUsuario: any = [];

    dadosHoras: any = {
        projectId: '',
        userId: '',
        finishedAt: '',
        createdAt: ''
    };

    projeto = 0;
    user = 0;

    constructor(
        private baseService: BaseService,
        private route: ActivatedRoute,
        private router: Router
        ) { }
    
    ngOnInit() {
        this.buscarItem();
    } 

    buscarItem(){

        this.route.params.subscribe(params => {
            this.horasId = +params['id']; 
         });

         this.baseService.get('/work-hours/' + this.horasId ).subscribe(
            (response: any) => {
                this.dadosHoras = response.data;
                this.projeto = this.dadosHoras.projectId;          
                this.user = this.dadosHoras.userId;          
                this.buscarProjetos(this.user);
                this.buscarUsuarios();
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        );
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
            createdAt: this.dadosHoras.createdAt,
            finishedAt: this.dadosHoras.finishedAt
        }
        this.baseService.patch('/work-hours', JSON.stringify(dadosSalvar))
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
        }
    }
  
  }
  
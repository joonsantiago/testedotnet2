import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-projetos-edit',
  templateUrl: 'cd-projetos-edit.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdProjetoEditComponent implements OnInit  {
   
    projectId: number = 0;
    dadosProjeto: any = {
        projectId: '',
        name: '',
        description: ''
    };

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
            this.projectId = +params['id']; 
         });

         this.baseService.get('/projects/' + this.projectId ).subscribe(
            (response: any) => {
                this.dadosProjeto = response.data;          
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
        this.router.navigate(["dash/projetos"]);
    }

    save(){
        this.baseService.patch('/projects', JSON.stringify(this.dadosProjeto))
            .subscribe(
                (response: any) => { 
                    if(response.success){
                        swal("Salvo", "Assunto salvo com sucesso!", "success");
                        this.router.navigate(['dash/projetos']);                        
                    }
                 }, 
                (error) => {
                    this.baseService.tratarError(error);
                 }
            );     
    }    
  
  }
  
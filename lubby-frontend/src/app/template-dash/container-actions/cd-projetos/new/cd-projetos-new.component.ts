import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-projetos-new',
  templateUrl: 'cd-projetos-new.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdProjetoNewComponent implements OnInit  {
   
    idProject: number = 0;
    dadosProjeto: any = {
        name: '',
        description: ''
    };
    
    constructor(
        private baseService: BaseService,
        private router: Router
        ) { }
    
    ngOnInit() {    
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
        this.baseService.post('/projects', JSON.stringify(this.dadosProjeto))
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
  
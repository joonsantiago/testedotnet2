import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'cd-projetos',
  templateUrl: 'cd-projetos.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdProjetoComponent implements OnInit  {
    
    listagemProjeto: any = [];
    listagemProjetoUser: any = [];
    listagemProjetoUserNaoVinculados: any = [];

    dadosProjectUser: any = {
        projectId: 0,
        userId: 0
    }

    @Input() id: string;
    currP: number = 0;
    currPU: number = 0;
    page: number = 0;
    pagePU: number = 0;
    listSize: number = 10;
    @Input() maxSize: number;
    @Output() pageChange: EventEmitter<number> = new EventEmitter();

    constructor(
        private baseService: BaseService,
        private router: Router
        ) {
        
    }

    onPageStateChangeEvent(page) {
        /**
         * Move the pagination, if on total list in page
         */
        /** */
        this.currP = page
        this.page = (page - 1);
        this.listarProjetos();
    }
    
    ngOnInit() {
        this.listarProjetos();
    } 

    listarProjetos(){
        this.baseService.get(`/projects?page=${this.page}&size=${this.listSize}`).subscribe(
            (response: any) => {
                this.listagemProjeto = response;                           
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }

    listarProjetoUsuario(id) {
        
        this.dadosProjectUser.projectId = id;
        this.listarProjetoUsuarioLivres(id);
        this.baseService.get(`/projects-user/${id}/users`).subscribe(
            (response: any) => {
                this.listagemProjetoUser = response.data;
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }

    listarProjetoUsuarioLivres(id) {
        this.baseService.get(`/projects-user/${id}/habled-users`).subscribe(
            (response: any) => {
                this.listagemProjetoUserNaoVinculados = response.data;
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        )
        console.log(id);
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
    
      confirmDeleteUserProject(id) {
        
        swal(
            {
                title: 'Finalizar',
                text: 'Tudo conferido? Deseja continuar?',
                icon: 'info',
                buttons: ["Cancelar", "Enviar"],
            }
        ).then((next) => {
            if(next){
                this.excluirProjetoUsuario(id);
            }
          });
      }

    excluir(id: number){
        this.baseService.delete('/projects/' + id).subscribe(
            (response: any) => {
                if(response.success){
                    swal("Excluído", "Assunto excluída com sucesso", "success");
                    this.listarProjetos();
                }
            }, 
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }

    excluirProjetoUsuario(id: number){
        var item = this.listagemProjetoUser.find(x => x.id == id);
        this.baseService.delete('/projects-user/' + id).subscribe(
            (response: any) => {
                if(response.success){
                    swal("Excluído", "Usuário removido com sucesso", "success");
                    this.listarProjetoUsuario(item.projectId);
                }
            }, 
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }

    setSelectValue(val){
        if(val > 0){
            this.dadosProjectUser.userId = val;
        }
    }

    
    saveProjetoUsuario(){
        
        let dadosSalvar = {
            userId: parseInt(this.dadosProjectUser.userId),
            projectId: parseInt(this.dadosProjectUser.projectId),
        };

        this.baseService.post('/projects-user', JSON.stringify(dadosSalvar))
            .subscribe(
                (response: any) => { 
                    if(response.success){
                        swal("Salvo", "Usuário vinculado com sucesso!", "success");
                        this.listarProjetoUsuario(dadosSalvar.projectId);
                    }
                 }, 
                (error) => {
                    this.baseService.tratarError(error);
                 }
            );     
    }
  
  }
  
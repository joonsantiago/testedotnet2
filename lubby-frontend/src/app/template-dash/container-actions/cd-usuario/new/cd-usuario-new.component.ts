import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-usuario-new',
  templateUrl: 'cd-usuario-new.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdUsuarioNewComponent implements OnInit  {
   
    idUsuario: number = 0;
    dadosUsuario: any = {
        id: '',
        name: '',
        cpf: '',
        login: '',
        password: '',
        role: ''
    };
    listagemPerfil: any = [
        {
            id: 1,
            name: "Admnistrador"
        },
        {
            id: 2,
            name: "Desenvolvedor"
        }
    ];
    confirmarSenha = '';

    constructor(
        private baseService: BaseService,
        private router: Router
        ) { }
    
    ngOnInit() {
    } 
    
    confirmFinalizar(){
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


    finalizar() {        
        var next = false;
        var passOk = false;

        if(this.dadosUsuario.name != '' && this.dadosUsuario.name != undefined && 
            this.dadosUsuario.email != '' && this.dadosUsuario.email != undefined && 
            this.dadosUsuario.role != '' && this.dadosUsuario.role != undefined &&
            this.dadosUsuario.login != '' && this.dadosUsuario.login != undefined &&
            this.dadosUsuario.password != '' && this.dadosUsuario.password != undefined){

                next = true;
                passOk  = this.confirmarSenha === this.dadosUsuario.password ? true : false;
        }

        if(next){
            if(passOk){
                this.confirmFinalizar();
            }else{
                swal("Senha Divergente", "A senha digitada não confere com a confirmação", "info");
            }
        }else{
            swal("Campos Obrigatório", "Campos obrigatório não preenchidos", "info");
        }

    }

    voltar(){
        this.router.navigate(["dash/usuarios"]);
    }

    save(){
        
        let dadosSalvar = {
            role: parseInt(this.dadosUsuario.role),
            cpf: this.dadosUsuario.cpf,
            email: this.dadosUsuario.email,
            name: this.dadosUsuario.name,
            login: this.dadosUsuario.login,
            password: this.dadosUsuario.password
        };

        this.baseService.post('/users', JSON.stringify(dadosSalvar))
            .subscribe(
                (response: any) => { 
                    if(response.success){
                        swal("Salvo", "Usuário salvo com sucesso!", "success");
                        this.router.navigate(['dash/usuarios']);                        
                    }
                 }, 
                (error) => {
                    this.baseService.tratarError(error);
                 }
            );     
    }

    setSelectValue(val){
        if(val > 0){
            this.dadosUsuario.role = val;
        }
    }
  
  }
  
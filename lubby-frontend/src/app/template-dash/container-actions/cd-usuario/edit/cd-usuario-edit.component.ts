import { BaseService } from '../../../../core/services/base.service';
import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import swal from 'sweetalert';

@Component({
  selector: 'cd-usuario-edit',
  templateUrl: 'cd-usuario-edit.component.html',
  styleUrls: [
      '../../../../../assets/css/style.css',
      '../../assets/css/cd-style.css',
    ]
})
export class CdUsuarioEditComponent implements OnInit  {
   
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
    perfilUser = 0;

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
            this.idUsuario = +params['id']; 
         });

         this.baseService.get('/users/' + this.idUsuario ).subscribe(
            (response: any) => {
                this.dadosUsuario = response.data;   
                this.perfilUser = this.dadosUsuario.role; 
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        );
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
            this.dadosUsuario.login != '' && this.dadosUsuario.login != undefined
            )
        {
                next = true;

                if(this.dadosUsuario.password != '' && this.dadosUsuario.password != undefined){
                    passOk  = this.confirmarSenha === this.dadosUsuario.password ? true : false;
                }else{
                    passOk = true;
                }
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
            id: parseInt(this.dadosUsuario.id),
            role: parseInt(this.dadosUsuario.role),
            cpf: this.dadosUsuario.cpf,
            email: this.dadosUsuario.email,
            name: this.dadosUsuario.name,
            login: this.dadosUsuario.login
        };

        if(this.confirmarSenha){
            dadosSalvar['password'] = this.dadosUsuario.password;
        }

        this.baseService.patch('/users', JSON.stringify(dadosSalvar))
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
  
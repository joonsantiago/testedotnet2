import { Component, OnInit } from '@angular/core';
import {trigger,state,style,animate,transition} from '@angular/animations';
import { TokenService } from '../../../core/token/token.service';
import { LoginService } from '../../../core/services/login.service';
import swal from 'sweetalert';
import { BaseService } from '../../../core/services/base.service';
import { Router } from '@angular/router';

@Component({
  selector: 'container-login',
  templateUrl: 'ct-login.component.html',
  styleUrls: [ 
    './css/l1.css'
   ],
})
export class CtLoginComponent implements OnInit  {
  
    usuario: string = "";
    senha: string = "";

    constructor(
        private tokenService : TokenService, private loginService: LoginService, private baseService: BaseService, private router: Router
        ) 
    {   }

    ngOnInit() {
        document.body.style.background = "#0f385a";

        if(this.loginService.taLogado()){
            this.router.navigate(['dash/home']);
        }
    }    

    logar() {

        var j = {
            login: this.usuario,
            password: this.senha
        }

        if(j.login != "" && j.password != ""){
            
            var dataValidate = JSON.stringify(j);
            this.loginService.postJSON('/auth/login', dataValidate)
            .subscribe(
               (data : any) => {
                   console.log('data', data);                   
                   if(data.token){
                       this.loginService.setToken(data.token);
                       //this.router.navigate(['dash/home']);
                       window.location.href = "dash/home";
                   }
               },
               (error : any) => {                   
                   swal("Erro", error.error.message, "error");                   
               },
               () => { 
                   //finally block 
               }
            );
        }else {
            swal("Erro", "Informe usuário e senha");
        }
        
    }
    
  
  }
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenService } from '../token/token.service';
import { Router } from '@angular/router';
import swal from 'sweetalert';


@Injectable({
    providedIn: 'root'
})
export class BaseService {

    API_URL = "https://localhost:44316/v1";
    _seed;


    constructor(private http: HttpClient, private tokenService : TokenService, private router: Router) { }

    getAPI_URL() { return this.API_URL;}

    
    postFormData(url, form_data){
        return this.http.post(this.API_URL + url, form_data);
    }

    post(url, data){
        var token = null;
        if(this.taLogado()){
            token = this.tokenService.recuperaToken();
        }

        if(token != null){
            return this.http.post(this.API_URL + url, data, {
                headers: new HttpHeaders()
                .set('Authorization', 'Bearer ' + token)
                .set('Content-Type', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('observe', 'response')
                .set('Access-Control-Allow-Headers', 'Content-Type')
            });
        }else{
            return this.http.post(this.API_URL + url, data);
        }
    }

    postJSON(url, data){
        return this.http.post(this.API_URL + url, data,
            {
                headers: new HttpHeaders()
                  .set('Content-Type', 'application/json')
              });
    }  

    get(url){
        var token = null;
        if(this.taLogado()){
            token = this.tokenService.recuperaToken();
        }

        if(token != null){
            return this.http.get(this.API_URL + url, {
                headers: new HttpHeaders()
                .set('Authorization', 'Bearer ' + token)
                .set('Content-Type', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('observe', 'response')
                .set('Access-Control-Allow-Headers', 'Content-Type')
            });
        }else{
            return this.http.get(this.API_URL + url);
        }
    }
    
    delete(url){
        var token = null;
        if(this.taLogado()){
            token = this.tokenService.recuperaToken();
        }

        if(token != null){
            return this.http.delete(this.API_URL + url, {
                headers: new HttpHeaders()
                .set('Authorization', 'Bearer ' + token)
                .set('Content-Type', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('observe', 'response')
                .set('Access-Control-Allow-Headers', 'Content-Type')
            });
        }else{
            return this.http.delete(this.API_URL + url);
        }
    }

    patch(url, data){
        var token = null;
        if(this.taLogado()){
            token = this.tokenService.recuperaToken();
        }

        if(token != null){
            return this.http.patch(this.API_URL + url, data, {
                headers: new HttpHeaders()
                .set('Authorization', 'Bearer ' + token)
                .set('Content-Type', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('observe', 'response')
                .set('Access-Control-Allow-Headers', 'Content-Type')
            });
        }else{
            return this.http.patch(this.API_URL + url, data);
        }
    }
    put(url, data){
        var token = null;
        if(this.taLogado()){
            token = this.tokenService.recuperaToken();
        }

        if(token != null){
            return this.http.put(this.API_URL + url, data, {
                headers: new HttpHeaders()
                .set('Authorization', 'Bearer ' + token)
                .set('Content-Type', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('observe', 'response')
                .set('Access-Control-Allow-Headers', 'Content-Type')
            });
        }else{
            return this.http.put(this.API_URL + url, data);
        }
    }
    
    taLogado() {
        return this.tokenService.temToken();
    }

    getData(funcao){
        var valor = null;
        var v1 = null;
        funcao.subscribe(
              (response) => { 
                  valor = response['$values'];
                  v1 = valor;
              } ,
              (error) => {
                  //console.log(error.message);
              }
          );
      return v1;
    }

    tratarError(error: any){        
        if(error.status == 401){
            this.tokenService.removeToken();
            swal("Erro", "Sessão do usuário expirada", "warning");
            this.router.navigate(['']);
        }else if(error.error.messages != undefined && error.error.messages.length > 0){
            var msg = "";
             error.error.messages.forEach(element => {
                msg = element + "\n";
            });
            swal("Erro", msg, "warning");
        }else{
            swal("Erro", "Entre em contato com o suporte.", "error");
        }
    }
    
    srand(seed) {
        this._seed = seed;
    }

    rand(min, max) {
        this.srand(Date.now());
        var seed = this._seed;
        min = min === undefined ? 0 : min;
        max = max === undefined ? 1 : max;
        this._seed = (seed * 9301 + 49297) % 233280;
        return min + (this._seed / 233280) * (max - min);
    }

    IsNullOrEmpty(vlr) {
        var saida = true;
        if(vlr != undefined || vlr.length > 0 || vlr != ''){
            saida = false;
        }
        return saida;
    }

    finalizar(titulo, msg, icon){
        var saida = false;
        var titulo = titulo != null ? titulo : 'Finalizar';
        var msg = msg != null ? msg : 'Tudo conferido? Deseja continuar?';
        var icone = icon != null ? icon : 'info';

        swal(
            {
                title: titulo,
                text: msg,
                icon: icone,
                buttons: ["Cancelar", "Enviar"],
            }
        ).then((next) => {
            saida = next;
          });

        return saida;
    }

}
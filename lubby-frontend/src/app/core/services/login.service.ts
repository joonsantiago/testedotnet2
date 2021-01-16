import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenService } from '../token/token.service';
import { BaseService } from './base.service';
import { User } from '../../template-user/container-template/ct-login/user';
import { BehaviorSubject } from 'rxjs';
import * as jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

    private userSubject = new BehaviorSubject<User>(null);

    constructor(private http: HttpClient, private tokenService : TokenService, private base : BaseService) {
        if(this.tokenService.temToken()){
            this.decodeAndPublish();
        }
     }
  
    API_URL = this.base.getAPI_URL();

    postFormData(url_path, form_data){
        return this.http.post(this.API_URL + url_path, form_data);
    }

    post(url, data){
        return this.http.post(this.API_URL + url, data);
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

    put(url, data){
        return this.http.put(this.API_URL + url, data);
    }

    taLogado() {
        return this.tokenService.temToken();
    }

    deslogar() {
        this.tokenService.removeToken();
    }

    getUser() {
        return this.userSubject.asObservable();
    }

    setToken(token: string) {
        this.tokenService.guardaToken(token);
        this.decodeAndPublish();
    }
    
    private decodeAndPublish() {
        const token = this.tokenService.recuperaToken();
        const user = jwt_decode(token) as User;

        this.userSubject.next(user);
    }

}
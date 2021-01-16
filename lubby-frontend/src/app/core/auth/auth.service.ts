import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { tap } from "rxjs/operators";
import { LoginService } from '../services/login.service';

const API_ULR = 'https://localhost:44316/v1/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private loginService: LoginService
  ) { }

}

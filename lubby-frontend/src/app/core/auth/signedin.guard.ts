import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { LoginService } from "../services/login.service";

@Injectable({ providedIn: 'root' })
export class SignedInGuard implements CanActivate {

  constructor(
    private loginService: LoginService,
    private router: Router
  ){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) : boolean | Observable<boolean> | Promise<boolean>{

    if (this.loginService.taLogado()) {
        return true;
    }else{
        console.log('nao logado');        
    }

    this.router.navigate(['']);
    return false;
  }

}

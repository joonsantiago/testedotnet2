import { Component, OnInit, ElementRef, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../core/services/login.service';

@Component({
  selector: 'cdb-navbar',
  templateUrl: 'navbar.component.html',
  styleUrls: ['../../../assets/css/style.css']
})
export class NavbarComponent implements OnInit  {
    
    constructor(private router: Router, private loginService: LoginService) {   }

    sair(){
        this.loginService.deslogar();
    } 

    ngOnInit() {
     
    }       

  
  }
  
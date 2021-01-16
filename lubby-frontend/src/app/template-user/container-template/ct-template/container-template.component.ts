import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { WINDOW } from '../../../core/services/window.service';
import { DOCUMENT } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'container-template',
  templateUrl: 'container-template.component.html',
  styleUrls: [
      '../assets/css/cont-style.css',
      '../assets/css/bootstrap.min.css',
      '../assets/css/creative.css'
    ]
})
export class ContainerTemplateComponent implements OnInit  {
    
    public navIsFixed: boolean = false;

    constructor(
        @Inject(DOCUMENT) private document: Document,
        @Inject(WINDOW) private window: Window,
        private router: Router
        ) {        
    }
  
    ngOnInit() {
        
    }    

    @HostListener('window:scroll', ['$event'])
    onElementScroll($event){
        //Event > path > Window > pageYOffset
        let number = this.window.pageYOffset || this.document.documentElement.scrollTop || this.document.body.scrollTop || 0;
        if (number > 100) {
            this.navIsFixed = true;
        } else if (this.navIsFixed && number < 10) {
            this.navIsFixed = false;
        }        
    }
  
  }
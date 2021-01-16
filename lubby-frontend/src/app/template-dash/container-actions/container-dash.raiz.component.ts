import { Component, OnInit } from '@angular/core';

@Component({
  template:  `
    <cdb-layout-dash>
        <router-outlet>
            <ng-content></ng-content>
        </router-outlet>
    </cdb-layout-dash>
  `
})
export class ContainerDashRaizComponent implements OnInit {
    ngOnInit(){
        var navInicial = document.getElementById("mainNav");
        navInicial.remove();
    }
 }

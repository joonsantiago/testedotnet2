import { Component, OnInit, ElementRef } from '@angular/core';
import { BaseService } from '../../core/services/base.service';

@Component({
  selector: 'cdb-content',
  templateUrl: 'content.component.html',
  styleUrls: ['../../../assets/css/style.css']
})
export class ContentComponent implements OnInit  {

    constructor(private baseService: BaseService) { }

    top_five_devs: any = [];

    ngOnInit() { 
        this.buscarDadosDash();
    }    

    buscarDadosDash() {
        this.baseService.get('/work-hours/dev-top5-week').subscribe(
            (response: any) => {
                this.top_five_devs = response.data;
                console.log(this.top_five_devs);
            },
            (error) => {
                this.baseService.tratarError(error);
            }
        )
    }
  
  }
  
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LayoutDashModule } from './template-dash/layout/layout.module';
import { LayoutUserModule } from './template-user/layout/layout.module';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { WINDOW_PROVIDERS } from './core/services/window.service';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    LayoutDashModule,
    LayoutUserModule,
    AppRoutingModule,
    HttpClientModule,
    
  ],
  providers: [
    WINDOW_PROVIDERS
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { LayoutComponent } from './navegacao/layout/layout.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteRoutingModule } from './site-routing.module';
import { HeaderComponent } from './navegacao/header/header.component';
import { MenuComponent } from './navegacao/menu/menu.component';
import { FooterComponent } from './navegacao/footer/footer.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    HeaderComponent,
    MenuComponent,
    FooterComponent,
    LayoutComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    SiteRoutingModule
  ],
  exports: [
    HeaderComponent,
    MenuComponent,
    FooterComponent,
    LayoutComponent,
    HomeComponent
  ]
})
export class SiteModule { }

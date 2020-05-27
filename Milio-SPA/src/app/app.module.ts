import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HeroComponent } from './hero/hero.component';
import { CarouselModule } from 'ngx-bootstrap/carousel'
import { FooterComponent } from './footer/footer.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HeroComponent,
      FooterComponent
   ],
   imports: [
      BrowserModule,
      CarouselModule.forRoot()
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from '../app.component';
import { HomePageComponent } from '../components/home-page/home-page.component';
import { FoodDictionaryPageComponent } from '../components/food-dictionary-page/food-dictionary-page.component';
import { FooterComponent } from '../components/footer/footer.component';
import { MentorsPageComponent } from '../components/mentors-page/mentors-page.component';
import { RecipesPageComponent } from '../components/recipes-page/recipes-page.component';
import { SignInComponent } from '../components/sign-in/sign-in.component';
import { HeroComponent } from '../components/hero/hero.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from '../components/nav-bar/nav-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    FoodDictionaryPageComponent,
    FooterComponent,
    MentorsPageComponent,
    RecipesPageComponent,
    SignInComponent,
    HeroComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

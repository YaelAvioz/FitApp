import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from '../app.component';
import { HomePageComponent } from '../components/home-page/home-page.component';
import { FoodDictionaryPageComponent } from '../components/food-dictionary-page/food-dictionary-page.component';
import { FooterComponent } from '../components/footer/footer.component';
import { MentorsPageComponent } from '../components/mentors-page/mentors-page.component';
import { NavbarComponent } from '../components/navbar/navbar.component';
import { RecipesPageComponent } from '../components/recipes-page/recipes-page.component';
import { SignInComponent } from '../components/sign-in/sign-in.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    FoodDictionaryPageComponent,
    FooterComponent,
    MentorsPageComponent,
    NavbarComponent,
    RecipesPageComponent,
    SignInComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

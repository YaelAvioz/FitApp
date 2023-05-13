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
import { OurMentorsComponent } from '../components/our-mentors/our-mentors.component';
import {MatCardModule} from '@angular/material/card';
import { MentorCardComponent } from '../components/mentor-card/mentor-card.component';
import { HomePageEffects } from '../effects/homePageEffect';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { homePageReducer } from '../store/home-page/homePageReducer';
import { HttpClientModule } from '@angular/common/http';
import { FoodDictionaryEffects } from '../effects/foodDictionaryPageEffect';
import { foodDictionaryPageReducer } from '../store/food-dictionary-page/foodDictionaryPageReducer';
import { FoodItemCardComponent } from '../components/food-item-card/food-item-card.component';
import { FoodItemModalComponent } from '../components/food-item-modal/food-item-modal.component';
import { RecipesEffects } from '../effects/recipesPageEffect';
import { recipesPageReducer } from '../store/recipes-page/recipesPageReducer';
import { SingleRecipePageComponent } from '../components/single-recipe-page/single-recipe-page.component';
import { RecipeCardComponent } from '../components/recipe-card/recipe-card.component';
import { MentorsPageEffects } from '../effects/mentorsPageEffect';
import { mentorPageReducer } from '../store/mentors-page/mentorPageReducer';
import { singleRecipePageReducer } from '../store/single-recipe-page/singleRecipePageReducer';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MentorFlipCardComponent } from '../components/mentor-flip-card/mentor-flip-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChatComponent } from '../components/chat/chat.component';
import { chatEffects } from '../effects/chatEffect';
import { chatReducer } from '../store/chat/chatReducer';
import { userReducer } from '../store/user/userReducer';
import { userEffects } from '../effects/userEffect';

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
    NavbarComponent,
    OurMentorsComponent,
    MentorCardComponent,
    FoodItemCardComponent,
    FoodItemModalComponent,
    SingleRecipePageComponent,
    RecipeCardComponent,
    MentorFlipCardComponent,
    ChatComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatPaginatorModule,
    EffectsModule.forRoot([HomePageEffects, FoodDictionaryEffects, RecipesEffects, MentorsPageEffects, chatEffects, userEffects, ]),
    StoreModule.forRoot({homePageReducer, foodDictionaryPageReducer, recipesPageReducer, mentorPageReducer, singleRecipePageReducer, chatReducer, userReducer}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

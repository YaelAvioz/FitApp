import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FoodDictionaryPageComponent } from '../components/food-dictionary-page/food-dictionary-page.component';
import { HomePageComponent } from '../components/home-page/home-page.component';
import { MentorsPageComponent } from '../components/mentors-page/mentors-page.component';
import { RecipesPageComponent } from '../components/recipes-page/recipes-page.component';
import { SingleRecipePageComponent } from '../components/single-recipe-page/single-recipe-page.component';

const routes: Routes = [
  { path: 'home-page', component: HomePageComponent },
  { path: 'food-dictionary-page', component: FoodDictionaryPageComponent},
  { path: 'recipes-page', component: RecipesPageComponent},
  { path: 'mentors-page', component: MentorsPageComponent},
  { path: 'single-recipe/:id', component: SingleRecipePageComponent},
  { path: '**', redirectTo: 'home-page'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FoodDictionaryPageComponent } from '../components/food-dictionary-page/food-dictionary-page.component';
import { HomePageComponent } from '../components/home-page/home-page.component';
import { MentorsPageComponent } from '../components/mentors-page/mentors-page.component';
import { RecipesPageComponent } from '../components/recipes-page/recipes-page.component';
import { SingleRecipePageComponent } from '../components/single-recipe-page/single-recipe-page.component';
import { SignUpPageComponent } from '../components/sign-up-page/sign-up-page.component';
import { ProfilePageComponent } from '../components/profile-page/profile-page.component';

const routes: Routes = [
  { path: 'home-page', component: HomePageComponent, data: { displayNavbar: true } },
  { path: 'food-dictionary-page', component: FoodDictionaryPageComponent, data: { displayNavbar: true } },
  { path: 'recipes-page', component: RecipesPageComponent, data: { displayNavbar: true } },
  { path: 'mentors-page', component: MentorsPageComponent, data: { displayNavbar: true } },
  { path: 'sign-up-page', component: SignUpPageComponent, data: { displayNavbar: false }},
  { path: 'profile-page', component: ProfilePageComponent, data: { displayNavbar: true } },
  { path: 'single-recipe/:id', component: SingleRecipePageComponent, data: { displayNavbar: true } },
  { path: '**', redirectTo: 'home-page', data: { displayNavbar: true } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

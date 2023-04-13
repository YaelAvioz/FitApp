import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/interfaces/recipe';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { singleRecipePageState } from 'src/app/store/single-recipe-page/singleRecipePageReducer';
import { loadSingleRecipe } from 'src/app/store/single-recipe-page/singleRecipePageAction';


@Component({
  selector: 'app-single-recipe-page',
  templateUrl: './single-recipe-page.component.html',
  styleUrls: ['./single-recipe-page.component.scss']
})
export class SingleRecipePageComponent {
  recipe$: Observable<any>;
  recipeToShow: Recipe | undefined;
  id !: string | null;
  recipe!: Recipe;

  public constructor(private _activatedRoute: ActivatedRoute, private store: Store<{ singleRecipePageReducer: singleRecipePageState }>) {
    this.id = this._activatedRoute.snapshot.paramMap.get("id");
    this.recipe$ = this.store.select((state) => {
      return state.singleRecipePageReducer.recipe;
    })
  }

  ngOnInit() {
    if (this.id != null) {
      this.store.dispatch(loadSingleRecipe({ recipeName: this.id }));
      this.recipe$.subscribe(recipeToShow => {
        return this.recipe = recipeToShow[0];
      })    
    }
  }
}

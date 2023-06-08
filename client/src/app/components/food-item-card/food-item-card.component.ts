import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { SessionService } from 'src/app/service/sessionService';
import { addFoodItem } from 'src/app/store/user/userAction';
import { UserState } from 'src/app/store/user/userReducer';
import { FoodItem } from 'src/interfaces/foodItem';
import { User } from 'src/interfaces/user';

@Component({
  selector: 'app-food-item-card',
  templateUrl: './food-item-card.component.html',
  styleUrls: ['./food-item-card.component.scss']
})
export class FoodItemCardComponent {
  @Input() foodItem !: FoodItem;
  isModalVisible: boolean = false;
  user !: User;
  addFoodItem$: any;
  amount!:number;
  input!: string;

  constructor(private sessionService: SessionService, private store: Store<{ userReducer: UserState }>,) {
    this.addFoodItem$ = this.store.select((state) => {
      return state.userReducer.currentUser;
    });
  }

  ngOnInit() {
    this.user = this.sessionService.getUserFromSession();
  }

  addItem(foodItem: string) {
    // const amountInput = (<HTMLInputElement>document.getElementById("amountInput")).value;
    // this.amount = parseFloat(amountInput);
    let amountInput = (document.getElementById('amountInput_' + this.foodItem.id) as HTMLInputElement).value;
    this.amount = parseFloat(amountInput);
    this.input = '';
    this.user = this.sessionService.getUserFromSession();
    console.log(this.amount);
    this.store.dispatch(addFoodItem({ userId: "647f83fb2f0139eb06605840", foodItemId: this.foodItem.id, amount: this.amount }));
  }
}
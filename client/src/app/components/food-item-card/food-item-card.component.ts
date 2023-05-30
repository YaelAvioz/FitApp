import { Component, Input } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
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

  constructor(private sessionService: SessionService) { }

  ngOnInit() {
    this.user = this.sessionService.getUserFromSession();
  }

  addItem(foodItem: string) {
    // alert(foodItem);
    this.user = this.sessionService.getUserFromSession();
    alert(this.user.firstname);
  }
}

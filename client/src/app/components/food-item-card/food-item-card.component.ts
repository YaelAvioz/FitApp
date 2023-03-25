import { Component, Input } from '@angular/core';
import { FoodItem } from 'src/interfaces/foodItem';

@Component({
  selector: 'app-food-item-card',
  templateUrl: './food-item-card.component.html',
  styleUrls: ['./food-item-card.component.scss']
})
export class FoodItemCardComponent {
  @Input() foodItem !: FoodItem;
  isModalVisible : boolean = false;
}

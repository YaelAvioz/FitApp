import { Component, Input } from '@angular/core';
import { FoodItem } from 'src/interfaces/foodItem';
import { FoodItemCardComponent } from '../food-item-card/food-item-card.component';

@Component({
  selector: 'app-food-item-modal',
  templateUrl: './food-item-modal.component.html',
  styleUrls: ['./food-item-modal.component.scss']
})
export class FoodItemModalComponent {
  @Input() foodItem !: FoodItem;

  constructor(private parent: FoodItemCardComponent) {
  }

  isModalVisible() {
    this.parent.isModalVisible = !this.parent.isModalVisible;
  }
}
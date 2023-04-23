import { Component, Input } from '@angular/core';
import { Mentor } from 'src/interfaces/mentor';
import { trigger, state, style, transition, animate } from '@angular/animations';


@Component({
  selector: 'app-mentor-flip-card',
  templateUrl: './mentor-flip-card.component.html',
  styleUrls: ['./mentor-flip-card.component.scss'],
  animations: [
    trigger('flipState', [
      state('active', style({
        transform: 'rotateY(179deg)'
      })),
      state('inactive', style({
        transform: 'rotateY(0)'
      })),
      transition('active => inactive', animate('500ms ease-out')),
      transition('inactive => active', animate('500ms ease-in'))
    ])
  ]
})
export class MentorFlipCardComponent {
  @Input() mentor !: Mentor;

  flip: string = 'inactive';

  toggleFlip() {
    this.flip = (this.flip == 'inactive') ? 'active' : 'inactive';
  }

}
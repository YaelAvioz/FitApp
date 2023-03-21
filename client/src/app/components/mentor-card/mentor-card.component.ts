import { Component, Input } from '@angular/core';
import { Mentor } from 'src/interfaces/mentor';

@Component({
  selector: 'app-mentor-card',
  templateUrl: './mentor-card.component.html',
  styleUrls: ['./mentor-card.component.scss']
})
export class MentorCardComponent {
  @Input() mentor !: Mentor;
}

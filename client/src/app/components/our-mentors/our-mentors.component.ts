import { Component, Input } from '@angular/core';
import { Mentor } from 'src/interfaces/mentor';

@Component({
  selector: 'app-our-mentors',
  templateUrl: './our-mentors.component.html',
  styleUrls: ['./our-mentors.component.scss']
})
export class OurMentorsComponent {
  @Input() mentors !: Mentor[];
}

import { Component } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
  user !: User;

  constructor(private sessionService: SessionService) { 
    this.user = this.sessionService.getUserFromSession();
  }
}

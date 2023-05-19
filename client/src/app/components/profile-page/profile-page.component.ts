import { Component } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  user !: User;

  constructor(private sessionService: SessionService) { }

  ngOnInit(){
    this.user = this.sessionService.getUserFromSession();
  }
  
}

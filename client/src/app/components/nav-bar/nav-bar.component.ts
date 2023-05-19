import { Component } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavbarComponent {
  isSignInVisible : boolean = false;
  user !: User;

  constructor(private sessionService: SessionService) { }

  ngOnInit(){
    this.user = this.sessionService.getUserFromSession();
  }
}

import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  
  constructor(private parent: NavbarComponent){}

  isSignInVisible()
  {
    this.parent.isSignInVisible = !this.parent.isSignInVisible;
  }
}

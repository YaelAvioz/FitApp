import { Component } from '@angular/core';
import { NavbarComponent } from '../nav-bar/nav-bar.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserState } from 'src/app/store/user/userReducer';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Login } from 'src/interfaces/user';
import { login } from 'src/app/store/user/userAction';
import { AppComponent } from '../app/app.component';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  signInForm: FormGroup;
  LoginUser$: Observable<Login | null>;
  LoginUserSubscriber: any;
  successMessage: string = '';

  constructor(private navBarParent: NavbarComponent, private appComponentParent:AppComponent,
    private formBuilder: FormBuilder, private store: Store<{ userReducer: UserState }>,) {

    this.LoginUser$ = this.store.select((state) => {
      return state.userReducer.currentUser;
    });

    this.signInForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (!this.signInForm || !this.signInForm.valid) return;
    var loginData: Login = {
      username: this.signInForm.value['username'],
      password: this.signInForm.value['password'],
    };

    this.store.dispatch(login({ loginData }));

    this.LoginUser$.subscribe((loginUser: any) => {
      this.LoginUserSubscriber = loginUser;

      if (loginUser) {
        this.saveUserInSession(loginUser);
        this.navBarParent.user = loginUser;
        this.appComponentParent.user = loginUser;
      }
    });
    this.isSignInVisible();
  }

  saveUserInSession(user: any) {
    sessionStorage.setItem('currentUser', JSON.stringify(user));
  }

  isSignInVisible() {
    this.navBarParent.isSignInVisible = !this.navBarParent.isSignInVisible;
  }
}
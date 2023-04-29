import { Component } from '@angular/core';
import { NavbarComponent } from '../nav-bar/nav-bar.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  signInForm: FormGroup;

  constructor(private parent: NavbarComponent,
    private formBuilder: FormBuilder,) {

    this.signInForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

   onSubmit() {
  //   if (!this.signInForm || !this.signInForm.valid) return;
  //   var LoginDTO: LoginDTO = {
  //     username: this.signInForm.value['username'],
  //     password: this.signInForm.value['password'],
  //   };

  //   this.store.dispatch(loginUser({ loginDTOsend: LoginDTO }));

  //   this.LoginUser$.subscribe((loginUser) => {
  //     this.LoginUserSubscriber = loginUser;

  //     console.log(this.LoginUserSubscriber);
  //     if (loginUser) {
  //       this.successMessage = 'Login successful, redirect to homepage';
  //       this.storeCurrentUser.dispatch(
  //         loadCurrentUser({ username: loginUser.username })
  //       );
  //       alert(this.successMessage);
  //       this.router.navigate(['/']);
  //     }
  //   });
     this.isSignInVisible();
   }

  isSignInVisible() {
    console.log("sign in clicked");
    
    this.parent.isSignInVisible = !this.parent.isSignInVisible;
  }
}
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { UserState } from 'src/app/store/user/userReducer';
import { Register } from 'src/interfaces/user';
import { register } from 'src/app/store/user/userAction';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {

  signUpForm: FormGroup;
  RegisterUser$: Observable<Register| null>;
  RegisterUserSubscriber: Register | null = null

  constructor(private formBuilder: FormBuilder, private router: Router, private store: Store<{ userReducer: UserState }>) {
    this.RegisterUser$ = this.store.select((state) => {
      return state.userReducer.newUser;
    })

    this.signUpForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      age: ['', Validators.required],
      height: ['', Validators.required],
      weight: ['', Validators.required],
      gender: ['', Validators.required],
      //email: ['', [Validators.required, Validators.email]],
    });


  }

  onSubmit() {
  
    if (!this.signUpForm || !this.signUpForm.valid) return;
    var registerData: Register = {
      username: this.signUpForm.value['username'],
      password: this.signUpForm.value['password'],
      firstname: this.signUpForm.value['firstName'],
      lastname: this.signUpForm.value['lastName'],
      age: this.signUpForm.value['age'],
      height: this.signUpForm.value['height'],
      weight: this.signUpForm.value['weight'],
      gender: this.signUpForm.value['gender'],
      goal: '',
      mentor: '',
      tags: []
    }

    this.store.dispatch(register({ registerData }));
     this.RegisterUser$.subscribe(() => {
       this.signInComplete()
     })
  }

  signInComplete() {
    const message = 'Sign in complete. Redirecting to home page in 2 seconds...';
    alert(message);
    setTimeout(() => {
      this.router.navigate(['/']);
    }, 2000);
  }
}
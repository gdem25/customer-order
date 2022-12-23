import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SignupRequest } from 'src/app/types/signupRequest';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    let signupRequest: SignupRequest = {
      userName: this.form.controls['userName'].value,
      email: this.form.controls['email'].value,
      password: this.form.controls['password'].value,
    };
    this.authService.signup(signupRequest).subscribe({
      next: (result) => {
        if (result.success) {
          this.router.navigate(['/']);
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}

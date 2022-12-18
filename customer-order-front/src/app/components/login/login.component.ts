import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginRequest } from 'src/app/types/loginRequest';
import { LoginResponse } from 'src/app/types/loginResponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  form!: UntypedFormGroup;
  loginResult!: LoginResponse;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    let loginRequest: LoginRequest = {
      userName: this.form.controls['userName'].value,
      password: this.form.controls['password'].value,
    };
    this.authService.login(loginRequest).subscribe({
      next: (result) => {
        this.loginResult = result;
        if (result.success) {
          this.router.navigate(['/']);
        }
      },
      error: (error) => {
        if (error.status == 401) {
          loginRequest = error.error;
        }
      },
    });
  }
}

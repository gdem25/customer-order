import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginRequest } from '../types/loginRequest';
import { LoginResponse } from '../types/loginResponse';
import { SignupRequest } from '../types/signupRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  tokenKey: string = 'jwt-token';
  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();

  constructor(protected http: HttpClient) {}

  init() {
    if (this.isLoggedIn()) {
      this.setAuthStatus(true);
    }
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    const url = `${environment.baseUrl}/Account`;
    return this.http.post<LoginResponse>(url, request).pipe(
      tap((loginResult: LoginResponse): void => {
        if (loginResult.success && loginResult.token) {
          localStorage.setItem(this.tokenKey, loginResult.token);
          this.setAuthStatus(true);
        }
      })
    );
  }

  signup(request: SignupRequest): Observable<LoginResponse> {
    const url = `${environment.baseUrl}/Account/Signup`;
    return this.http.post<LoginResponse>(url, request).pipe(
      tap((signupResult: LoginResponse): void => {
        if (signupResult.success && signupResult.token) {
          localStorage.setItem(this.tokenKey, signupResult.token);
          this.setAuthStatus(true);
        }
      })
    );
  }

  setAuthStatus(status: boolean) {
    this._authStatus.next(status);
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }
}

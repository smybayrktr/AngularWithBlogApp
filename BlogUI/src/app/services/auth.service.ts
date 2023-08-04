import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AuthLogin } from '../models/auth-login';
import { AuthRegister } from '../models/auth-register';
import { ApiDataResponse } from '../models/api-data-response';
import { AccessToken } from '../models/access-token';
import { Subject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenSubject: Subject<AccessToken | null> = new Subject<AccessToken | null>();

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string
  ) { }

  login(userLogin: AuthLogin) {
    return this.httpClient.post<ApiDataResponse<AccessToken>>(
      `${this.baseUrl}/api/auth/login`,
      userLogin,
      { observe: 'response' }
    ).pipe(map(response => {
      if (!response.body?.success) {
        return;
      }
      localStorage.setItem('jwt', JSON.stringify(response.body.data));
      this.tokenSubject.next(response.body.data);
      return response.body;
    }));
  }

  register(userRegister: AuthRegister) {
    return this.httpClient.post<ApiDataResponse<AccessToken>>(
      `${this.baseUrl}/api/auth/register`,
      userRegister,
      { observe: 'response' }
    ).pipe(map(response => {
      if (!response.body?.success) {
        return;
      }
      localStorage.setItem('jwt', JSON.stringify(response.body.data));
      this.tokenSubject.next(response.body.data);
      return response.body;
    }));;
  }

  logout() {
    localStorage.removeItem("jwt");
    this.tokenSubject.next(null);
  }
  
  getToken() {
    return this.tokenSubject.asObservable();
  }
}

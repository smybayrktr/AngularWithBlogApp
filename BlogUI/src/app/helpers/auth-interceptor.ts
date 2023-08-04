import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router, private authService: AuthService) { }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var token = localStorage.getItem('jwt');
    if (token != '' && token != null) {
      try {
        var accessToken = JSON.parse(token);
        request = request.clone({
          setHeaders: { Authorization: `Bearer ${accessToken.token}` },
        });
      } catch {
        this.authService.logout()
      }
    }
    return next.handle(request).pipe(catchError(x => this.handleAuthError(x)));;
  }

  private handleAuthError(err: HttpErrorResponse): Observable<any> {
    if (err.status === 401 || err.status === 403) {
      this.authService.logout()
      this.router.navigateByUrl(`/auth-login`);
    }
    return throwError(err);
  }
}


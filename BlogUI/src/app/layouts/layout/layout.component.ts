import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccessToken } from 'src/app/models/access-token';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, OnDestroy {
  token!: AccessToken | null;
  userSub!: Subscription;

  constructor(private authService: AuthService,private router: Router) {}

  ngOnInit(): void {
    this.userSub = this.authService.getToken().subscribe((data) => {
      this.token=data;
    });
    var token = localStorage.getItem("jwt");
    if(token!="" && token != null){
      var accessToken = JSON.parse(token);
      this.token=accessToken;
    }
  }
  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }
  logout(){
    this.authService.logout();
    this.router.navigateByUrl("/auth-login");
  }
}

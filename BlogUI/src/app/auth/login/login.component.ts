import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthLogin } from 'src/app/models/auth-login';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

    loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  })
  constructor(private authService:AuthService, private router:Router){ }
  ngOnInit(): void {
  }

  login(){
   this.authService.login(this.loginForm.value as AuthLogin).subscribe(x=>{
      if(x?.success){
        this.router.navigateByUrl("/");
      }
      else{
        alert(x?.message)
      }
   });

}
}
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRegister } from 'src/app/models/auth-register';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm = new FormGroup({
    name: new FormControl(''),
    lastname: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
  });
  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {}

  register() {
    this.authService
      .register(this.registerForm.value as AuthRegister)
      .subscribe((x) => {
        if (x?.success) {
          this.router.navigateByUrl('/');
        } else {
          alert(x?.message);
        }
      });
  }
}

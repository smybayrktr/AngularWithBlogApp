import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BlogAddComponent } from './components/blog-add/blog-add.component';
import { BlogListComponent } from './components/blog-list/blog-list.component';
import { BlogSavedComponent } from './components/blog-saved/blog-saved.component';
import { MyBlogComponent } from './components/my-blog/my-blog.component';
import { BlogUpdatedComponent } from './components/blog-updated/blog-updated.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { LayoutComponent } from './layouts/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { AuthInterceptor } from './helpers/auth-interceptor';
import { BlogDetailComponent } from './components/blog-detail/blog-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    BlogAddComponent,
    BlogListComponent,
    BlogSavedComponent,
    MyBlogComponent,
    BlogUpdatedComponent,
    LoginComponent,
    RegisterComponent,
    LayoutComponent,
    HomeComponent,
    BlogDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: 'BASE_API_URL', useValue: 'https://localhost:7227' },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

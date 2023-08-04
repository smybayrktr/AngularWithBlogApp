import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlogListComponent } from './components/blog-list/blog-list.component';
import { BlogAddComponent } from './components/blog-add/blog-add.component';
import { BlogUpdatedComponent } from './components/blog-updated/blog-updated.component';
import { BlogSavedComponent } from './components/blog-saved/blog-saved.component';
import { MyBlogComponent } from './components/my-blog/my-blog.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { authGuard } from './guards/auth/auth.guard';
import { BlogDetailComponent } from './components/blog-detail/blog-detail.component';

const routes: Routes = [
  {path:'',redirectTo:'/blogs', pathMatch:'full'},
  {path:'blogs',component:BlogListComponent},
  {path:'blogs-detail/:url',component:BlogDetailComponent},
  {path:'blogs-add',component:BlogAddComponent,canActivate: [authGuard]},
  {path:'blogs-saved',component:BlogSavedComponent,canActivate: [authGuard]},
  {path:'blogs-my-blog',component:MyBlogComponent, canActivate: [authGuard]},
  {path:'blogs/create',component:BlogAddComponent, canActivate: [authGuard]},
  {path:'blogs-update/:id',component: BlogUpdatedComponent,canActivate: [authGuard]},
  {path:'login',component:LoginComponent},
  {path:'register',component: RegisterComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

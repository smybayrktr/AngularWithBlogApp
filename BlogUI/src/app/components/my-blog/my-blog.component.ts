import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/models/blog';
import { BlogUpdate } from 'src/app/models/blog-update';
import { BlogService } from 'src/app/services/blog.service';

@Component({
  selector: 'app-my-blog',
  templateUrl: './my-blog.component.html',
  styleUrls: ['./my-blog.component.css']
})
export class MyBlogComponent implements OnInit {
  myBlog:Blog[] = [];

  constructor(private blogService:BlogService, private router: Router){ }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.blogService.getUserBlog().subscribe(response=>{
      this.myBlog = response.data
    });
  }

  delete(id: number) {
    this.blogService.delete(id).subscribe((x) => {
      if (x == true) {
        this.load();
      } else {
        alert('Blog Silinemedi!');
      }
    });
  }
}

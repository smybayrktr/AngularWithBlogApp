import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/models/blog';
import { BlogService } from 'src/app/services/blog.service';

@Component({
  selector: 'app-blog-saved',
  templateUrl: './blog-saved.component.html',
  styleUrls: ['./blog-saved.component.css']
})
export class BlogSavedComponent implements OnInit {

  savedBlog:Blog[] = [];

  constructor(private blogService:BlogService){ }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.blogService.getSavedBlog().subscribe(response=>{
      this.savedBlog = response.data
    });
  }
}

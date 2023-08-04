import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/models/blog';
import { BlogService } from 'src/app/services/blog.service';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css'],
})
export class BlogListComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number[] = [];
  blogList: Blog[] = [];

  constructor(private blogService: BlogService) {}

  ngOnInit(): void {
    this.loadBlogs();
    this.loadPages();
  }

  loadPages(){
    this.blogService.getBlogsCount().subscribe(response=>{
      if(!response.body?.success){
        alert(response.body?.message)
      }
      this.totalPages = Array.from({ length: response.body!.data.totalPage }, (_, index) => index + 1);
    })
  }

  loadBlogs() {
    this.blogService.getAll(this.currentPage).subscribe((response) => {
      this.blogList = response.data
    });
  }


  onPageChange(page: number) {
    this.currentPage = page;
    this.loadBlogs();
  }

  saveAction(blog:Blog){
    this.blogService.action(blog.id).subscribe(response=>{
      blog.bookmarkImage=response.data.bookmarkImage
    })
  }
}

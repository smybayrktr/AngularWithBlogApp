import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog } from 'src/app/models/blog';
import { BlogService } from 'src/app/services/blog.service';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  blogDetail: Blog | null = null;
  isLoading = false;

  constructor(private route: ActivatedRoute, private blogService: BlogService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const url = params.get('url');
      if (url) {
        this.getBlogDetail(url);
      }
    });
  }

  getBlogDetail(url: string): void {
    this.isLoading = true;
    this.blogService.getBlogByUrl(url).subscribe(response => {
      this.blogDetail = response.data;
      this.isLoading = false;
    }, error => {
      console.error(error);
      this.isLoading = false;
    });
  }
}
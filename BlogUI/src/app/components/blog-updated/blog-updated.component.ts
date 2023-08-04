import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogUpdate } from 'src/app/models/blog-update';
import { Category } from 'src/app/models/category';
import { BlogService } from 'src/app/services/blog.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-blog-updated',
  templateUrl: './blog-updated.component.html',
  styleUrls: ['./blog-updated.component.css']
})
export class BlogUpdatedComponent implements OnInit {
  categoryList: Category[] = [];
  title: string = "";
  blog!: BlogUpdate
  selectedFile: File | null = null;

  blogForm = new FormGroup({
    title: new FormControl(''),
    body: new FormControl(''),
    image: new FormControl(''),
    categoryId: new FormControl(0)
  })

  constructor(private blogService: BlogService, private categoryService: CategoryService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (!id) {
        alert("Blog bulunamadı!")
        this.router.navigateByUrl("/")
        return
      }
      this.getBlog(parseInt(id))
    });
    this.getCategory();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  getCategory() {
    this.categoryService.getAll().subscribe(response => {
      this.categoryList = response.data
    });
  }

  update() {
    if (this.selectedFile != null) {
      const formData = new FormData();
      formData.append('formFile', this.selectedFile!, this.selectedFile!.name);
      var blogUpdate = this.blogForm.value as BlogUpdate;
      this.blogService.uploadImage(formData).subscribe(uploadResponse => {
        if (!uploadResponse.body?.success) {
          alert(uploadResponse.body?.message)
          return
        }
        blogUpdate.image = uploadResponse.body.data
        blogUpdate.id = this.blog.id
        this.blogService.update(blogUpdate).subscribe(x => {
          if (!x.body?.success) {
            alert(x.body?.message)
          }
          else {
            this.router.navigateByUrl("/blogs-my-blog");
          }
        })
      })
    } else {
      var blogUpdate = this.blogForm.value as BlogUpdate;
      blogUpdate.image = this.blog.image;
      blogUpdate.id = this.blog.id
      this.blogService.update(blogUpdate).subscribe(x => {
        if (!x.body?.success) {
          alert(x.body?.message)
        }
        else {
          this.router.navigateByUrl("/blogs-my-blog");
        }
      })
    }
  }

  getBlog(id: number) {
    this.blogService.get(id).subscribe(response => {
      if (!response.success) {
        alert(response.message)
        this.router.navigateByUrl("/")
      }
      this.blog = response.data
      console.log(this.blog)
      this.blogForm.setValue({
        title: response.data.title,
        body: response.data.body,
        image: response.data.image,
        categoryId: response.data.categoryId
      })
    })
  }

}

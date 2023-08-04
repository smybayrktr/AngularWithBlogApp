import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BlogAdd } from 'src/app/models/blog-add';
import { Category } from 'src/app/models/category';
import { BlogService } from 'src/app/services/blog.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-blog-add',
  templateUrl: './blog-add.component.html',
  styleUrls: ['./blog-add.component.css']
})
export class BlogAddComponent implements OnInit {
  categoryList: Category[] = [];
  title: string = "";
  selectedFile: File | null = null;

  blogForm = new FormGroup({
    title: new FormControl(''),
    body: new FormControl(''),
    image: new FormControl(''),
    categoryId: new FormControl(0)
  })

  constructor(private blogService: BlogService, private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
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

  add() {
    const formData = new FormData();
    formData.append('formFile', this.selectedFile!, this.selectedFile!.name);
    var blogAdd = this.blogForm.value as BlogAdd;
    this.blogService.uploadImage(formData).subscribe(uploadResponse => {
      if(!uploadResponse.body?.success){
        alert(uploadResponse.body?.message)
        return
      }
      blogAdd.image = uploadResponse.body.data;
      this.blogService.add(blogAdd).subscribe(x => {
        if (!x.body?.success) {
          alert(x.body?.message)
        }
        else {
          this.router.navigateByUrl("/blogs-my-blog");
        }
      })
    })
  }

}


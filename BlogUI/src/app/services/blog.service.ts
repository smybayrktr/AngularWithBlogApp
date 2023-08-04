import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Blog } from '../models/blog';
import { Observable, map } from 'rxjs';
import { BlogUpdate } from '../models/blog-update';
import { ApiResponse } from "../models/api-response";
import { ApiDataResponse } from '../models/api-data-response';
import { BlogAdd } from '../models/blog-add';
import { BlogPagination } from '../models/blog-pagination';
import { SavedBlog } from '../models/saved-blog';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private httpClient: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  getAll(page: number) {
    return this.httpClient.get<ApiDataResponse<Blog[]>>(`${this.baseUrl}/api/blogs/get-all?page=${page}`)
  }

  getSavedBlog() {
    return this.httpClient.get<ApiDataResponse<Blog[]>>(`${this.baseUrl}/api/blogs/get-saved-blogs`)
  }

  getUserBlog() {
    return this.httpClient.get<ApiDataResponse<Blog[]>>(`${this.baseUrl}/api/blogs/get-user-blogs`)
  }

  get(id: number) {
    return this.httpClient.get<ApiDataResponse<BlogUpdate>>(`${this.baseUrl}/api/blogs/get-by-id/${id}`)
  }

  getBlogByUrl(url: string): Observable<ApiDataResponse<Blog>> {
    return this.httpClient.get<ApiDataResponse<Blog>>(`${this.baseUrl}/blog/${url}`);
  }

  add(blog: BlogAdd) {
    return this.httpClient.post<ApiResponse>(`${this.baseUrl}/api/blogs/create`, blog,
      { observe: 'response' })
  }

  delete(id: number) {
    return this.httpClient.delete<ApiResponse>(`${this.baseUrl}/api/blogs/delete/${id}`, { observe: 'response' })
      .pipe(map((response) => response.status == 200))
  }

  update(blog: BlogUpdate) {
    return this.httpClient.put<ApiResponse>(`${this.baseUrl}/api/blogs/update/${blog.id}`, blog, { observe: 'response' })
  }

  uploadImage(file: FormData) {
    return this.httpClient.post<ApiDataResponse<string>>(`${this.baseUrl}/api/blogs/upload-image`, file, { observe: 'response' })
  }

  getBlogsCount() {
    return this.httpClient.get<ApiDataResponse<BlogPagination>>(`${this.baseUrl}/api/blogs/blogs-count`, { observe: 'response' })
  }

  action(blogId:number){
    return this.httpClient.post<ApiDataResponse<SavedBlog>>(`${this.baseUrl}/api/saved-blogs/action?blogId=${blogId}`, { observe: 'response' })
  }
}

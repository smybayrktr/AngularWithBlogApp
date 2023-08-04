import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiDataResponse } from '../models/api-data-response';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private httpClient: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  getAll() {
    return this.httpClient.get<ApiDataResponse<Category[]>>(`${this.baseUrl}/api/categories/get-all`)
  }

}

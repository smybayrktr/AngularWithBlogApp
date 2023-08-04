import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogAddComponent } from './blog-add.component';

describe('BlogAddComponent', () => {
  let component: BlogAddComponent;
  let fixture: ComponentFixture<BlogAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogAddComponent]
    });
    fixture = TestBed.createComponent(BlogAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

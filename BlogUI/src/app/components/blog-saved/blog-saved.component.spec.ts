import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogSavedComponent } from './blog-saved.component';

describe('BlogSavedComponent', () => {
  let component: BlogSavedComponent;
  let fixture: ComponentFixture<BlogSavedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogSavedComponent]
    });
    fixture = TestBed.createComponent(BlogSavedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

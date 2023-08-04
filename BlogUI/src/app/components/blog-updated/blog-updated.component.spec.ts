import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogUpdatedComponent } from './blog-updated.component';

describe('BlogUpdatedComponent', () => {
  let component: BlogUpdatedComponent;
  let fixture: ComponentFixture<BlogUpdatedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogUpdatedComponent]
    });
    fixture = TestBed.createComponent(BlogUpdatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceTablesComponent } from './invoice-tables.component';

describe('InvoiceTablesComponent', () => {
  let component: InvoiceTablesComponent;
  let fixture: ComponentFixture<InvoiceTablesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceTablesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceTablesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Customer } from 'src/app/types/Customer';
import { CustomerService } from 'src/app/services/customer.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css'],
})
export class CustomerEditComponent implements OnInit {
  title?: string;

  customer!: Customer;

  id?: number;

  form!: FormGroup;

  constructor(
    private customerService: CustomerService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });
    this.loadData();
  }

  loadData() {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    if (this.id) {
      this.customerService.getCustomer(this.id).subscribe({
        next: (result) => {
          this.customer = result;
          this.title = 'Edit - ' + this.customer.firstName + `'s profile`;
          this.form.patchValue(this.customer);
        },
        error: (error) => console.log(error),
      });
    } else {
      this.title = 'Add a new Customer';
    }
  }

  onSubmit() {
    let customer = this.id ? this.customer : <Customer>{};
    customer.firstName = this.form.controls['firstName'].value;
    customer.lastName = this.form.controls['lastName'].value;
    if (this.id) {
      this.customerService.editCustomer(this.id, customer).subscribe({
        next: () => {
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          console.log(error);
        },
      });
    } else {
      this.customerService.addCustomer(customer).subscribe({
        next: () => {
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }
}

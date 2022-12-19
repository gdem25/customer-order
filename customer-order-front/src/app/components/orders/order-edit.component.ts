import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from 'src/app/services/customer.service';
import { OrderService } from 'src/app/services/order.service';
import { Customer, Order } from 'src/app/types/Customer';

@Component({
  selector: 'app-order-edit',
  templateUrl: './order-edit.component.html',
  styleUrls: ['./order-edit.component.css'],
})
export class OrderEditComponent implements OnInit {
  title?: string;

  customers?: Customer[];

  order!: Order;

  id?: number;

  form!: FormGroup;

  constructor(
    private orderService: OrderService,
    private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      orderName: ['', Validators.required],
      price: ['', Validators.required],
      customerId: ['', Validators.required],
    });
    this.loadOrder();
  }
  loadOrder() {
    this.loadCustomers();
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    if (this.id) {
      this.orderService.getOrder(this.id).subscribe({
        next: (result) => {
          this.order = result;
          this.title = `Edit - ${this.order.orderName} Order`;
          this.form.patchValue(this.order);
        },
        error: (error) => {
          console.log(error);
        },
      });
    } else {
      this.title = 'Create a new Order';
    }
  }
  loadCustomers() {
    this.customerService.getCustomers().subscribe({
      next: (result) => {
        this.customers = result;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  onSubmit() {
    let order = this.id ? this.order : <Order>{};
    order.orderName = this.form.controls['orderName'].value;
    order.price = this.form.controls['price'].value;
    order.customerId = this.form.controls['customerId'].value;
    if (this.id) {
      this.orderService.editOrder(this.id, order).subscribe({
        next: () => {
          this.router.navigate([`customer-orders/${order.customerId}`]);
        },
        error: (error) => {
          console.log(error);
        },
      });
    } else {
      this.orderService.addOrder(order).subscribe({
        next: () => {
          this.router.navigate([`customer-orders/${order.customerId}`]);
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }
}

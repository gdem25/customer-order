import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { CustomerOrders, OrderTotal } from '../../types/Customer';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  id!: number;
  orders!: CustomerOrders;
  faPencilAlt = faPencilAlt;
  orderTotal?: OrderTotal;

  constructor(
    private customerService: CustomerService,
    private activatedRoute: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    this.customerService.getCustomerOrders(this.id).subscribe({
      next: (result) => {
        this.orders = result;
        this.customerService.getOrderTotal(this.id).subscribe({
          next: (result) => {
            this.orderTotal = result;
          },
          error: (error) => {
            console.log(error);
          },
        });
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  onDelete(id: number) {
    this.orderService.deleteOrder(id).subscribe({
      next: () => {
        console.log('deleted');
        this.customerService.getCustomerOrders(this.id).subscribe({
          next: (result) => {
            this.orders = result;
          },
          error: (error) => {
            console.log(error);
          },
        });
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}

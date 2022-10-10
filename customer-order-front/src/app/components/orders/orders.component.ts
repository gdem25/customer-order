import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../types/Order';
import { Customer } from '../../types/Customer';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  customers: Customer[] = [];
  orders: Order[] = [];
  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService
      .getCustomers()
      .subscribe((customers) => (this.customers = customers));
    this.orderService.getOrders().subscribe((orders) => (this.orders = orders));
  }
}

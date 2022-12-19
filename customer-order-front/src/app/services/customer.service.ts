import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Customer, CustomerOrders, OrderTotal } from '../types/Customer';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private apiUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/Customers`);
  }
  getCustomerOrders(id: number): Observable<CustomerOrders> {
    return this.http.get<CustomerOrders>(`${this.apiUrl}/Customers/${id}`);
  }
  getCustomer(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/Customers/Customer/${id}`);
  }
  editCustomer(id: number, customer: Customer): Observable<any> {
    return this.http.put(`${this.apiUrl}/Customers/${id}`, customer);
  }
  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.apiUrl}/Customers`, customer);
  }
  deleteCustomer(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Customers/${id}`);
  }
  getOrderTotal(id: number): Observable<OrderTotal> {
    return this.http.get<OrderTotal>(`${this.apiUrl}/Customers/Total/${id}`);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Customer, CustomerOrders } from '../types/Customer';

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
}

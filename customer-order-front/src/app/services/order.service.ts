import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../types/Customer';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getOrder(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/Orders/order/${id}`);
  }

  editOrder(id: number, order: Order): Observable<any> {
    return this.http.put(`${this.apiUrl}/Orders/${id}`, order);
  }

  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/Orders`, order);
  }

  deleteOrder(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Orders/${id}`);
  }
}

import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../types/Customer';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
})
export class CustomersComponent implements OnInit {
  customers!: Customer[];

  constructor(private customerService: CustomerService) {}
  faPencilAlt = faPencilAlt;

  ngOnInit(): void {
    this.customerService
      .getCustomers()
      .subscribe((customers) => (this.customers = customers));
  }

  onDelete(id: number) {
    this.customerService.deleteCustomer(id).subscribe({
      next: () => {
        this.customerService.getCustomers().subscribe({
          next: (result) => {
            this.customers = result;
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

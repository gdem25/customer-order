export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
}

interface Order {
  id: number;
  orderName: string;
  price: number;
  customerId: number;
}

export interface CustomerOrders {
  firstName: string;
  lastName: string;
  orders: Order[];
}

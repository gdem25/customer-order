export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
}

export interface Order {
  id: number;
  orderName: string;
  price: number;
  customerId: number;
}

export interface OrderTotal {
  id: number;
  total: number;
}

export interface CustomerOrders {
  firstName: string;
  lastName: string;
  orders: Order[];
}

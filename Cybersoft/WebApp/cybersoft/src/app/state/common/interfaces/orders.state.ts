import { Order } from "src/app/models/order/order";
import { StatusEnums } from "../enums/StatusEnums";


export interface IOrdersState {
    isLoading: boolean | null;
    orders: Order[];
    error: string | null;
    status: StatusEnums;
  }

import { EntityState } from "@ngrx/entity";
import { CartItem } from "src/app/models/cart/cart-item";
import { StatusEnums } from "../enums/StatusEnums";


export interface ICartState extends EntityState<CartItem> {
    isLoading: boolean | null;
    error: string | null;
    status: StatusEnums;
  }
  
  // cart: CartItem[];
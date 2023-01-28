import { Product } from "src/app/models/product/product";
import { StatusEnums } from "../enums/StatusEnums";


export interface IProductsState {
    isLoading: boolean | null;
    products: Product[];
    error: string | null;
    status: StatusEnums;
  }

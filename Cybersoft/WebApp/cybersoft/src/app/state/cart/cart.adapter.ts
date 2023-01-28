import { EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { CartItem } from "src/app/models/cart/cart-item";

export const cartAdapter: EntityAdapter<CartItem> =
  createEntityAdapter<CartItem>();

// // export const cartAdapter : EntityAdapter<CartItem> = 
// //    createCartEntityAdapter<CartItem>({
// //        sortComparer: sortBySeqNo
// //    });

// export const cartAdapter: EntityAdapter<CartItem> = createEntityAdapter<CartItem>({
//     sortComparer: false //sortByProductName
//  }); 

// export function sortByProductName(ob1: CartItem, ob2: CartItem): number {
//     return ob1.productName.localeCompare(ob2.productName);
//  }

// export function sortBySeqNo(e1: CartItem, e2: CartItem) {
//     return e1.id! - e2.id!;
// }

//  // to leave it unsorted, pass sortComparer: false


// from docs - 
// export function selectUserId(a: User): string {
//     //In this case this would be optional since primary key is id
//     return a.id;
//   }
   
//   export function sortByName(a: User, b: User): number {
//     return a.name.localeCompare(b.name);
//   }
   
//   export const adapter: EntityAdapter<User> = createEntityAdapter<User>({
//     selectId: selectUserId,
//     sortComparer: sortByName,
//   });
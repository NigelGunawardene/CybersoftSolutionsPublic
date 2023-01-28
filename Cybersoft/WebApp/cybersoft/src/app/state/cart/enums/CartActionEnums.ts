export enum CartActionEnums {
  //   GET_CART = '[Cart] Get Cart',
  //   GET_CART_SUCCESS = '[Cart] Get Cart Success',
  //   GET_CART_FAILURE = '[Cart] Get Cart Failure',
  //   ADD_TO_CART = '[Cart] Add To Cart',
  //   ADD_TO_CART_SUCCESS = '[Cart] Add To Cart Success',
  //   ADD_TO_CART_FAILURE = '[Cart] Add To Cart Failure',
  //   REMOVE_FROM_CART = '[Cart] Remove From Cart',
  //   REMOVE_FROM_CART_SUCCESS = '[Cart] Remove From Cart Success',
  //   REMOVE_FROM_CART_FAILURE = '[Cart] Remove From Cart Failure',

  // COMMANDS
  LOAD_CART_ITEMS_COMMAND = '[Cart] Load Cart Items Command',
  ADD_TO_CART_COMMAND = '[Cart] Add To Cart Command',
  DELETE_FROM_CART_COMMAND = '[Cart] Delete From Cart Command',

  // ERRORS
  CART_OPERATION_FAILURE = '[Cart] Cart Operation Failure',

  // ACTIONS
  LOAD_CART_ITEMS = '[Cart] Load Cart Items',
  SET_CART_ITEM = '[Cart] Set Cart Item',
  SET_CART_ITEMS = '[Cart] Set Cart Items',
  ADD_CART_ITEM = '[Cart] Add Cart Item',
  ADD_CART_ITEMS = '[Cart] Add Cart Items',
  UPSERT_CART_ITEM = '[Cart] Upsert Cart Item',
  UPSERT_CART_ITEMS = '[Cart] Upsert Cart Items',
  UPDATE_CART_ITEM = '[Cart] Update Cart Item',
  UPDATE_CART_ITEMS = '[Cart] Update Cart Items',
  MAP_CART_ITEM = '[Cart] Map Cart Item',
  MAP_CART_ITEMS = '[Cart] Map Cart Items',
  DELETE_CART_ITEM = '[Cart] Delete Cart Item',
  DELETE_CART_ITEMS = '[Cart] Delete Cart Items',
  DELETE_CART_ITEMS_BY_PREDICATE = '[Cart] Delete Cart Items By Predicate',
  CLEAR_CART_ITEMS = '[Cart] Clear Cart Items',
}

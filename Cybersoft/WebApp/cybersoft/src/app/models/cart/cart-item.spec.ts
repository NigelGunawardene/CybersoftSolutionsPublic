import { CartItem } from './cart-item';

describe('Cart', () => {
  it('should create an instance', () => {
    expect(new CartItem(1, 1, 1, 'testName', 1, 100, new Date())).toBeTruthy();
  });
});

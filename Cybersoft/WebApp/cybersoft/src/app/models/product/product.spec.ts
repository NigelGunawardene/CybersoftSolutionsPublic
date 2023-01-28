import { Product } from './product';

describe('Product', () => {
  it('should create an instance', () => {
    expect(new Product(1, 'testName', 'testDesc', 100, 'assets/test.png', new Date())).toBeTruthy();
  });
});

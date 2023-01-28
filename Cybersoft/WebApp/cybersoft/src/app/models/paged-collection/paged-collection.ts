export class PagedCollection<T> {
  public items: T[];
  public currentPage: number;
  public totalPages: number;
  public pageSize: number;
  public totalCount: number;
  public hasPrevious: boolean;
  public hasNext: boolean;

  constructor(items: T[], currentPage: number, totalPages: number, pageSize: number, totalCount: number, hasPrevious: boolean, hasNext: boolean) {
    this.items = items;
    this.currentPage = currentPage;
    this.totalPages = totalPages;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.hasPrevious = hasPrevious;
    this.hasNext = hasNext;

  }
}

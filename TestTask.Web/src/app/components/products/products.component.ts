import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ProductListRequest, ProductDto } from 'src/app/models/product';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { CategoryDto } from 'src/app/models/category';
import { ProductService } from 'src/app/services/product.service';
import { CategoryService } from 'src/app/services/category.service';
import { Router } from '@angular/router';
import { ProductModalComponent } from './product-modal/product-modal.component';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  

  ajaxRequest: boolean = true;
  request: ProductListRequest;
  showFilter: boolean = false;
  productsData = new MatTableDataSource();
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  products: ProductDto[] = [];
  productsCount = 0;
  pageSize = 10;
  categoriesAutocomplete: CategoryDto[];
  displayedColumns = ['title', 'price', 'categoryTitle', 'createdDate', 'actions'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  categoryFilter: any;

  constructor(private productService: ProductService,
    private categoryService: CategoryService,
    public dialog: MatDialog,
    public router: Router,) {
    this.request = new ProductListRequest();
    this.request.page = 0;
  }

  ngOnInit() {

    this.productsData.sort = this.sort;
    this.update();
    this.paginator.page.subscribe(
      () => {
        this.update();
      }
    )
  }

  public update(): void {
    this.ajaxRequest = true;
    this.request.page = this.paginator.pageIndex;
    this.productService.getList(this.request).subscribe(res => {
      this.products = res.values;
      this.productsCount = res.total;
      this.productsData.data = this.products;
      this.ajaxRequest = false;
    },
      err => {
        console.error(err);
        this.ajaxRequest = false;
      });
  }

  search() {
    this.paginator.pageIndex = 0;
    this.update();
  }

  clearFilters(): void {
    this.paginator.pageIndex = 0;
    this.request.categoryFilterId = "";
    this.request.term = "";
    this.categoryFilter = null;
    this.update();
  }

  toEdit(id: string): void {
    this.ajaxRequest = true;
    this.productService.getEditProductDto(id).subscribe(res => {
      this.openModal(res);
      this.ajaxRequest = false;
    },
      err => {
        console.error(err);
        this.ajaxRequest = false;
      });
  }

  toCreation(): void {
    this.openModal(new ProductDto());
  }

  openModal(dto: ProductDto) {
    let dialogRef = this.dialog.open(ProductModalComponent, {
      width: '1920px',
      maxWidth: '90vw',
      data: { productDto: dto},
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('The dialog was closed');
      this.update();
    });
  }

  remove(id: string): void {
    if (confirm('You’re going to delete product. Are you sure?')) {
      this.ajaxRequest = true;
      this.productService.remove(id).subscribe(res => {
        this.update();
        this.ajaxRequest = false;
      },
        err => {
          console.error(err);
          this.ajaxRequest = false;
        });
    }
  }

  displayEntityRefFn(entity?: any): string | undefined {
    if (!entity)
      return undefined;

    if (entity.title)
      return entity.title;

    return entity;
  }

  onCategoryFilterSelected(event: MatAutocompleteSelectedEvent): void {
    this.request.categoryFilterId = event.option.value.id;
    this.update();
  }
  

  searchCategory(term: string): void {
    this.categoryService.autoComplete(term).subscribe(res => {
      this.categoriesAutocomplete = res;
    },
      err => {
        console.error(err);
      });
  };
}



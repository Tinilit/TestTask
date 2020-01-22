import { Component, OnInit, Input, Inject, ViewChild } from '@angular/core';
import { ProductDto } from 'src/app/models/product';
import { ProductService } from "src/app/services/product.service";
import { CategoryService } from "src/app/services/category.service";
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef, MatAutocompleteSelectedEvent } from '@angular/material';
import { CategoryDto } from 'src/app/models/category';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.scss']
})
export class ProductModalComponent implements OnInit {

  pageTitle: string;
  productTitle: string;
  productPrice: number;
  productId: string = "";
  product: ProductDto = new ProductDto();
  submitButtonText: string;
  categories: CategoryDto[];
  categoriesAutocomplete: CategoryDto[];
  category: CategoryDto = new CategoryDto();
  @ViewChild("editProductForm", { static: true }) editProductForm: NgForm;
  @Input('productDto') productDto: ProductDto;

  constructor(private productService: ProductService,
    @Inject(MAT_DIALOG_DATA) public data: ProductDto,
    private categoryService: CategoryService,
    public dialogRef: MatDialogRef<ProductModalComponent>,
  ) {}

  ngOnInit() {

    if (this.data["productDto"].title != undefined) {
      this.submitButtonText = "Save";
      this.pageTitle = "Update " + this.data["productDto"].title;
    } else {
      this.submitButtonText = "Create";
      this.pageTitle = "Create new product";
    }
    debugger;
    this.productTitle = this.data["productDto"].title;
    this.productPrice = this.data["productDto"].price;
    this.productId = this.data["productDto"].id;

    this.category = new CategoryDto();
    this.category.id = this.data["productDto"].categoryId;
    this.category.title = this.data["productDto"].categoryTitle;
  }

  formSubmit() {
    if (this.editProductForm.valid) {
      this.product.id = this.productId;
      this.product.categoryId = this.category.id;
      this.product.price = Number(this.productPrice);
      this.product.title = this.productTitle;
      this.productService.save(this.product).subscribe(result => {
        console.log(result);
        this.dialogRef.close();
      });
    }
  }

  onCancelClick() {
    this.dialogRef.close();
  }

  onCategoryFilterSelected(event: MatAutocompleteSelectedEvent): void {
    this.category = event.option.value;
  }

  searchCategory(term: string): void {
    this.categoryService.autoComplete(term).subscribe(res => {
      this.categoriesAutocomplete = res;
    },
      err => {
        console.error(err);
      });
  };

  displayEntityRefFn(entity?: any): string | undefined {
    if (!entity)
      return undefined;

    if (entity.title)
      return entity.title;

    return entity;
  }
}

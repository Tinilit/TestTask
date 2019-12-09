import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductsComponent } from './components/products/products.component';
import { LayoutComponent } from './shared/layout/layout.component';

import {
  MatInputModule,
  MatListModule,
  MatIconModule,
  MatFormFieldModule,
  MatButtonModule,
  MatSidenavModule,
  MatGridListModule,
  MatCardModule,
  MatSelectModule,
  MatRadioModule,
  MatBadgeModule,
  MatMenuModule,
  MatPaginatorModule,
  MatSortModule,
  MatAutocompleteModule,
  MatTableModule,
  MatTooltipModule,
  MatProgressSpinnerModule,
  MatDialogModule
} from '@angular/material';
import { ProductService } from './services/product.service';
import { CategoryService } from './services/category.service';
import { ProductModalComponent } from './components/products/product-modal/product-modal.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    LayoutComponent,
    ProductModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatInputModule,
    MatListModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSidenavModule,
    MatGridListModule,
    MatCardModule,
    MatSelectModule,
    MatRadioModule,
    MatBadgeModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule,
    MatAutocompleteModule,
    MatTableModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatDialogModule
  ],
  entryComponents:[
    ProductModalComponent
  ],
  providers: [
    ProductService,
    CategoryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

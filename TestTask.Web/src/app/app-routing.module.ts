import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { LayoutComponent } from './shared/layout/layout.component';


const routes: Routes = [
  {
    path: '', component: LayoutComponent, 
    children:[
      {
        path: '', component:ProductsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

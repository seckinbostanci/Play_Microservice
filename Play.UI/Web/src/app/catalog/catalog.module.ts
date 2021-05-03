import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogListComponent } from './catalog-list/catalog-list.component';
import { CatalogCreateComponent } from './catalog-create/catalog-create.component';
import { CatalogUpdateComponent } from './catalog-update/catalog-update.component';
import { CatalogDetailComponent } from './catalog-detail/catalog-detail.component';
import { CatalogComponent } from './catalog.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: CatalogListComponent
  },
  {
    path: 'add',
    component: CatalogCreateComponent
  },
  {
    path: ':id',
    component: CatalogComponent,
    children: [
      {
        path: 'detail',
        component: CatalogDetailComponent
      },
      {
        path: 'edit',
        component: CatalogUpdateComponent
      },
      {
        path: '**',
        redirectTo: 'detail',
        pathMatch: 'full'
      }
    ]
  },
];

@NgModule({
  declarations: [
    CatalogComponent,
    CatalogListComponent,
    CatalogCreateComponent,
    CatalogUpdateComponent,
    CatalogDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CatalogModule { }

import { NgModule } from '@angular/core';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatToolbarModule,
  MatIconModule,
  MatMenuModule,
  MatSidenavModule,
  MatExpansionModule,
  MatListModule,
  MatDividerModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatSnackBarModule
} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [],
  imports: [
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatSidenavModule,
    MatExpansionModule,
    MatListModule,
    MatDividerModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    FlexLayoutModule
  ],
  exports: [
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatSidenavModule,
    MatExpansionModule,
    MatListModule,
    MatDividerModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    FlexLayoutModule
  ]
})
export class SharedUIModule { }

import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ImageUploadDialogComponent } from '@app/components/image-upload/image-upload-dialog/image-upload-dialog.component';
import { SnackService } from '@app/services/snack.service';
import { SpinnerOverlayService } from '@app/services/spinner-overlay.service';
import { ImageService } from '@app/services/image.service';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.scss']
})
export class ImageUploadComponent implements OnInit {

  constructor(
    private dialog: MatDialog,
    private snackService: SnackService,
    private spinnerService: SpinnerOverlayService,
    private imageService: ImageService
  ) { }

  ngOnInit() {
  }

  public openUploadDialog() {
    const dialogRef = this.dialog.open(ImageUploadDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (typeof (result) == 'boolean' && !result) {
        return;
      }
      
      this.uploadImage(result);
    });
  }

  private uploadImage(imageData): boolean{
    if (!imageData || !imageData["image"]) {
      return false;
    }

    this.spinnerService.show("Uploading image...");
    this.imageService.uploadImage(imageData["image"], imageData["title"]).subscribe(result => {
      this.spinnerService.hide();
      this.snackService.showBar("Image uploaded successfuly!");
      return true;
    }, error => {
      this.spinnerService.hide();
      this.snackService.showBar("Image upload failed!");
      return false;
    })
  }

}

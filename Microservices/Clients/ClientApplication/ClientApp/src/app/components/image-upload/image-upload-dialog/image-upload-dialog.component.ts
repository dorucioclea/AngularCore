import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { ImageService } from '@app/services/image.service';
import { SpinnerOverlayService } from '@app/services/spinner-overlay.service';

@Component({
  selector: 'app-image-upload-dialog',
  templateUrl: './image-upload-dialog.component.html',
  styleUrls: ['./image-upload-dialog.component.scss']
})
export class ImageUploadDialogComponent implements OnInit {

  public title: string;
  public imgURL: string;
  public imgHeight = 200;

  private image: any;

  constructor(
    public dialogRef: MatDialogRef<ImageUploadDialogComponent>
  ) { }

  ngOnInit() {
  }

  previewImage(imageInput: any) {
    if (imageInput.length === 0) {
      return;
    }

    this.image = imageInput[0];
    var reader = new FileReader();
    reader.readAsDataURL(this.image);
    reader.onload = (_event) => {
      this.imgURL = reader.result.toString();
    }
  }

  returnData() {
    return {
      "image": this.image,
      "title": this.title
    };
  }
}

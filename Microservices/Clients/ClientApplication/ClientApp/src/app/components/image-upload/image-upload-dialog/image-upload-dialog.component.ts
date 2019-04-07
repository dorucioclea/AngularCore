import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-image-upload-dialog',
  templateUrl: './image-upload-dialog.component.html',
  styleUrls: ['./image-upload-dialog.component.scss']
})
export class ImageUploadDialogComponent implements OnInit {

  public title: string;
  public imgURL: string;
  public fileName: string;
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
    this.fileName = this.image.name;
    var reader = new FileReader();
    reader.readAsDataURL(this.image);
    reader.onload = (_event) => {
      this.imgURL = reader.result.toString();
    }
  }

  returnData() {
    return {
      "image": this.image,
      "title": this.title,
      "fileName": this.fileName
    };
  }
}

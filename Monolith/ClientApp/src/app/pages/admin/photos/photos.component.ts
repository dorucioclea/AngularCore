import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Image } from '@app/models/image';
import { ImageService } from '@app/services/image.service';
import { SnackService } from '@app/services/snack.service';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.scss']
})
export class PhotosComponent implements OnInit {

  public imagesList$: Observable<Image[]>;

  constructor(
    private imageService: ImageService,
    private snackService: SnackService
  ) { }

  ngOnInit() {
    this.imagesList$ = this.imageService.getAllImages();
  }

  public deleteImage(imageId) {
    this.imageService.deleteImage(imageId).subscribe(success => {
      this.snackService.showBar("Image deleted successfuly!");
      this.imagesList$ = this.imageService.getAllImages();
    }, error => {
      this.snackService.showBar("Error occurred while deleting image! Sorry!");
    })
  }

}

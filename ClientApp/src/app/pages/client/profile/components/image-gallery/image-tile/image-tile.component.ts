import { Component, OnInit, Input } from '@angular/core';
import { Image } from '@app/models/image';
import { MatDialog } from '@angular/material';
import { ImageViewComponent } from '@app/pages/client/profile/components/image-gallery/image-tile/image-view/image-view.component';

@Component({
  selector: 'app-image-tile',
  templateUrl: './image-tile.component.html',
  styleUrls: ['./image-tile.component.scss']
})
export class ImageTileComponent implements OnInit {

  @Input() image: Image;
  @Input() currentUser: boolean = false;

  constructor( private dialog: MatDialog ) { }

  ngOnInit() {
  }

  viewImage() {
    const dialogRef = this.dialog.open(ImageViewComponent, {
      data: { image: this.image, currentUser: this.currentUser }
    });
  }

}

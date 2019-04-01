import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Image } from '@app/models/image';

@Component({
  selector: 'app-image-gallery',
  templateUrl: './image-gallery.component.html',
  styleUrls: ['./image-gallery.component.scss']
})
export class ImageGalleryComponent implements OnInit, OnChanges {

  @Input() images: Image[];
  @Input() currentUser: boolean = false;

  constructor() { }

  ngOnInit() {
    if (this.images) {
      this.images = this.images.sort(this.sortByDateFunc());
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.images && changes.images.currentValue) {
      this.images = changes.images.currentValue.sort(this.sortByDateFunc());
    }
  }

  private sortByDateFunc() {
    return (i1: Image, i2: Image) => {
      return new Date(i2.createdAt).getTime() - new Date(i1.createdAt).getTime();
    }
  }

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Image } from '@app/models/image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private imagesUrl = '/api/v1/images/';

  constructor(private http: HttpClient) { }

  public uploadImage(imageData, title: string) {
    let formData = new FormData();
    formData.append("image", imageData);
    formData.append("title", title);

    return this.http.post(this.imagesUrl, formData)
  }

  public getAllImages() {
    return this.http.get<Image[]>(this.imagesUrl);
  }

  public deleteImage(imageId) {
    return this.http.delete(this.imagesUrl + imageId);
  }
}

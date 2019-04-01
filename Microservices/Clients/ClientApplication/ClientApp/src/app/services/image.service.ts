import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Image } from '@app/models/image';
import { AuthService } from '@app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private imagesUrl = 'http://localhost:16000/api/images/';

  constructor(private http: HttpClient,
                private authService: AuthService) { }

  public uploadImage(imageData, title: string) {
    let formData = new FormData();
    formData.append("image", imageData);
    formData.append("title", title);
    formData.append("authorId", this.authService.loggedUserValue.id);
    return this.http.post(this.imagesUrl, formData)
  }

  public getAllImages() {
    return this.http.get<Image[]>(this.imagesUrl);
  }

  public deleteImage(imageId) {
    return this.http.delete(this.imagesUrl + imageId);
  }
}

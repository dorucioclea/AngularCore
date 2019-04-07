import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Image } from '@app/models/image';
import { AuthService } from '@app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private imagesUrl = 'http://localhost:16001/api/images/';

  constructor(private http: HttpClient,
                private authService: AuthService) { }

  public uploadImage(imageData, title: string, fileName: string) {
    let formData = new FormData();
    formData.append("image", imageData);
    formData.append("title", title);
    formData.append("authorId", this.authService.loggedUserValue.id);
    formData.append("fileName", fileName);
    return this.http.post(this.imagesUrl, formData)
  }

  public getAllImages() {
    return this.http.get<Image[]>(this.imagesUrl);
  }

  public deleteImage(imageId) {
    return this.http.delete(this.imagesUrl + imageId);
  }

  public getUserProfilePicture(userId) {
    return this.http.get<Image>(this.imagesUrl + "profile/" + userId);
  }

  public setUserProfilePicture(imageId: string) {
    return this.http.patch(this.imagesUrl + "profile/" + imageId, {});
  }

  public getUserImages(userId) {
    return this.http.get<Image[]>(this.imagesUrl + "user/" + userId);
  }
}

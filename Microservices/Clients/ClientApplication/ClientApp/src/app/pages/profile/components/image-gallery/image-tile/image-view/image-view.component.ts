import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Image } from '@app/models/image';
import { AuthService } from '@app/services/auth.service';
import { UserService } from '@app/services/user.service';
import { SnackService } from '@app/services/snack.service';
import { ImageService } from '../../../../../../services/image.service';

@Component({
  selector: 'app-image-view',
  templateUrl: './image-view.component.html',
  styleUrls: ['./image-view.component.scss']
})
export class ImageViewComponent implements OnInit {

  public currentUser: boolean;
  public imageData: Image;

  constructor(
    public imageService: ImageService,
    public dialogRef: MatDialogRef<ImageViewComponent>,
    public userService: UserService,
    public snackService: SnackService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.imageData = this.data.image;
    this.currentUser = this.data.currentUser;
    console.log(this.imageData);
  }

  public setProfilePicture() {
    this.imageService.setUserProfilePicture(this.imageData.id).subscribe(result => {
      this.snackService.showBar("Profile picture changed successfuly!");
      location.reload();
    }, error => {
      this.snackService.showBar("Error occured while changing picture!");
    });
  }

}

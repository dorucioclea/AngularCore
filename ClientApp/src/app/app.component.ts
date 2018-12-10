import { SpinnerOverlayService } from './services/spinner-overlay.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Social Media Application';

  constructor(public spinner: SpinnerOverlayService ){ }
}

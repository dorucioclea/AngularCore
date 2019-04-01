import { SpinnerOverlayService } from './services/spinner-overlay.service';
import { Component } from '@angular/core';
import { Event, Router, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Social Media Application';

  public isSpinnerShown: boolean = true;

  constructor(
    private spinner: SpinnerOverlayService,
    private authService: AuthService,
    private router: Router
  ) {
    this.router.events.subscribe((event: Event) => {
      switch (true) {
        case event instanceof NavigationStart: {
          this.spinner.show();
          this.isSpinnerShown = true;
          break;
        }

        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.spinner.hide();
          this.isSpinnerShown = false;
          break;
        }
      }
    });
  }
}

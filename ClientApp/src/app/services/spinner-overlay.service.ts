import { Injectable } from '@angular/core';

@Injectable()
export class SpinnerOverlayService {

  spinnerMessage: string = null;
  isVisible: boolean = false;

  constructor() { }

  public show(message?: string) {
    if(message){
      this.spinnerMessage = message;
    }
    this.isVisible = true;
  }

  public hide() {
    this.spinnerMessage = null;
    this.isVisible = false;
  }

}

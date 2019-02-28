import { MatSnackBar } from '@angular/material';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SnackService {

  private defaultAction: string = "close";
  private defaultDuration: number = 2000;

  constructor(
    private snack: MatSnackBar
  ) { }

  public showBar(message: string, action?: string, duration?: number) {
    this.snack.open(message, action ? action : this.defaultAction, {
      duration: duration ? duration : this.defaultDuration,
    });
  }
}

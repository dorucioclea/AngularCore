import { Pipe, PipeTransform } from '@angular/core';
import * as moment from "moment";

@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {

  transform(value: any, args?: string[]): any {
    if (value) {
      var date = moment(value, "DD.MM.YYYY HH:mm:ss");
      return date.format("MMM D YYYY");
    }
  }

}

import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public cities: { name: string, id: string }[] = [];
  title = 'Web';

  constructor(private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.showSpinner();
    this.cities = [{ name: 'London', id: 'UK01' }, { name: 'Sofia', id: 'BG01' }];
  }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

  showSpinner() {
    this.spinner.show();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinner.hide();
    }, 5000);
  }
}

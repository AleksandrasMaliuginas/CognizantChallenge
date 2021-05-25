import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-upload-code',
  templateUrl: './upload-code.component.html',
  styleUrls: ['./upload-code.component.css']
})
export class UploadCodeComponent {
  private http: HttpClient;
  private baseUrl: string;

  public result: TaskRecord;
  public errorMessage: string;
  public uploading: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    console.log(form.valid);

    if (form.valid) {
      this.uploading = true;
      this.http.post<TaskRecord>(this.baseUrl + 'TaskRecords', form.value).subscribe(
        result => {
          console.log(result);
          this.result = result;
          this.uploading = false;
        },
        error => {
          console.error(error);
          this.errorMessage = "Something went wrong while uploading your code.";
          this.uploading = false;
        }
      );
    }
  }
}

interface TaskRecord {
  id: number,
  name: string,
  taskId: number,
  description: string,
  sourceCode: string,

  output: string,
  memory: number,
  cpuTime: number
}

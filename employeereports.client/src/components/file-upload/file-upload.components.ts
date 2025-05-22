import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import EmployeePairResult from '../../models/EmployeePairResult';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {
  selectedFile: File | null = null;
  uploading: boolean = false;
  errorMessage: string | null = null;

  @Output() uploadResult = new EventEmitter<EmployeePairResult[]>();

  constructor(private http: HttpClient) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0] || null;

    if (file && file.name.endsWith('.csv')) {
      this.selectedFile = file;
      this.errorMessage = null;
    } else {
      this.errorMessage = 'Please select a valid .csv file.';
      this.selectedFile = null;
    }
  }

  upload(): void {
    if (!this.selectedFile) return;
  
    const formData = new FormData();
    formData.append('file', this.selectedFile);
  
    this.uploading = true;
    const url = '/api/fileupload';
    this.http.post<EmployeePairResult[]>(url, formData)
      .subscribe({
        next: (res: EmployeePairResult[]) => {
          this.uploadResult.emit(res);
          this.uploading = false;
        },
        error: (err) => {
          this.errorMessage = 'Upload failed.';
          this.uploading = false;
        }
      });
  }
  
}

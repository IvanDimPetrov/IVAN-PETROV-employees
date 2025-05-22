import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FileUploadComponent } from '../components/file-upload/file-upload.components';
import { EmployeePairsComponent } from '../components/employees-pair/employee-pairs.component';
import EmployeePairResult from '../models/EmployeePairResult';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  imports: [FileUploadComponent, EmployeePairsComponent],
  standalone: true,
  styleUrl: './app.component.css'
})
export class AppComponent {
  employeePairResult: EmployeePairResult[] = [];

  onResult(data: EmployeePairResult[]) {
    this.employeePairResult = data;
  }

  title = 'Employees Reports';
}

import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import EmployeePairResult from '../../models/EmployeePairResult';

@Component({
    selector: 'app-employee-pairs',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './employee-pairs.component.html',
    styleUrls: ['./employee-pairs.component.css']
})
export class EmployeePairsComponent {
    @Input() data: EmployeePairResult[] = [];
}
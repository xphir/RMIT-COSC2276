import { Component } from "@angular/core";
import { Http } from "@angular/http";
import { Router } from "@angular/router";
import { EmployeeService } from "../../services/employee.service";

@Component({
  selector: "app-fetch-employee",
  templateUrl: "./fetch-employee.component.html",
  styleUrls: ["./fetch-employee.component.css"]
})
export class FetchEmployeeComponent {
  empList: EmployeeData[];

  constructor(public http: Http, private _router: Router, private _employeeService: EmployeeService)
  {
    this.getEmployees();
  }

  getEmployees()
  {
    this._employeeService.getEmployees().subscribe(data => this.empList = data);
  }

  delete(employeeID)
  {
    const ans = confirm("Do you want to delete customer with Id: " + employeeID);
    if(ans)
    {
      this._employeeService.deleteEmployee(employeeID).subscribe((data) =>
        {
          this.getEmployees();
        },
        error => console.error(error));
    }
  }
}

interface EmployeeData {
  employeeId: number;
  name: string;
  gender: string;
  city: string;
  department: string;
}

import {SelectionModel} from '@angular/cdk/collections';
import {Component} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';

export interface PeriodicElement {
  position: number,
  wellName: string, 
  projectName: string, 
  country: string, 
  reservoir: string, 
  pad:string,
  api:string, 
  field:string, 
  wellType:string,
  customer:string 
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, wellName: 'Well P1 1.1', projectName: 'Project1', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 2, wellName: 'Well P1 1.2', projectName: 'Project2', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 3, wellName: 'Well P1 1.3', projectName: 'Project3', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 4, wellName: 'Well P1 1.4', projectName: 'Project4', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 5, wellName: 'Well P1 1.5', projectName: 'Project5', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 6, wellName: 'Well P1 1.5', projectName: 'Project6', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 7, wellName: 'Well P1 2.1', projectName: 'Project7', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 8, wellName: 'Well P1 2.2', projectName: 'Project8', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 9, wellName: 'Well P1 2.3', projectName: 'Project9', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 10, wellName: 'Well P1 2.4', projectName: 'Project10', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 11, wellName: 'Well P1 3.1', projectName: 'Project11', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
  {position: 12, wellName: 'Well P1 3.2', projectName: 'Project12', country: 'USA', reservoir: 'Rezervoir z', pad:'Pad 1', api:'', field:'', wellType:'', customer:'' },
];

/**
 * @title Table with selection
 */
@Component({
  selector: 'table-selection-example',
  styleUrls: ['./projects.component.css'],
  templateUrl: './projects.component.html',
})
export class ProjectsComponent {
  displayedColumns: string[] = ['select', 'position', 'wellName', 'projectName', 'country', 'reservoir', 'pad', 'api', 'field', 'wellType', 'customer'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  selection = new SelectionModel<PeriodicElement>(true, []);

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: PeriodicElement): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  onClickbutton(event){
    console.log("View button click");
  }
}

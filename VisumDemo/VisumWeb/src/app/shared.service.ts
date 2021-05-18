import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { Router } from '@angular/router';
import { Project } from './projects/projects.component';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  

  readonly AppUrl = "http://localhost:5000/api";

  constructor(
    private http: HttpClient,
    public router : Router
  ){}

  getProjectList() : Observable<any> {
   return this.http.get(this.AppUrl + "/projects")
  }

  getWellList(projectId)  : Observable<any> {
    return this.http.get(this.AppUrl + "/well/wells/"+ projectId )
  }
 
}
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
 

  readonly AppUrl = "";

  constructor(private http: HttpClient) { }

 /*public loginUser = (route: string, body: UserAuthData)=> {
   return this.http.post(this.createCompleteRoute(route, this.AppUrl+"/Auth/Login").body);
 }*/
}

export interface UserAuthData{
  username:string
  password: string
}

export interface AuthResponse {
  isAuthSuccessful: boolean
  errorMessage: string
  token: string
}



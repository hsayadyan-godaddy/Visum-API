import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AngularMaterialModule } from './angular-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedService} from './shared.service'
import { HttpClientModule } from '@angular/common/http';
import { ProjectsComponent } from './projects/projects.component';
import { RoutingModule } from './routing/routing.module';
import { HomeComponent } from './home/home.component';
import { ViewComponent } from './view/view.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProjectsComponent,
    HomeComponent,
    ViewComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/', pathMatch : 'full'},
      {path: 'login', component: LoginComponent},
      {path: 'projects', component: ProjectsComponent}
    ]),
    RoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
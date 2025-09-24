import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashBoardComponent } from './pages/dash-board/dash-board.component';
import { RegistersComponent } from './pages/registers/registers.component';
import { ReportComponent } from './pages/report/report.component';
import { HistoryComponent } from './pages/history/history.component';

const routes: Routes = [
  { path: 'auth', loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule) },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  { path: 'dash', component: DashBoardComponent },
  { path: 'registers', component: RegistersComponent },
  { path: 'report', component: ReportComponent },
  { path: 'history', component: HistoryComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

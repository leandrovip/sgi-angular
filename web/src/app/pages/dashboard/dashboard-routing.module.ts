import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from 'src/app/components/layout/main/main.component';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { DashboardComponent } from './dashboard.component';

const dashboardRoutes: Routes = [
	{
		path: 'dashboard',
		component: MainComponent,
		canActivate: [AuthGuard],
		children: [{ path: '', component: DashboardComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(dashboardRoutes)],
	exports: [RouterModule],
})
export class DashboardRoutingModule {}

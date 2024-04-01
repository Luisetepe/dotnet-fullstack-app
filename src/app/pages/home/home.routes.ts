import { DashboardComponent } from '@/app/pages/home/dashboard/dashboard.component'
import { dashboardResolver } from '@/app/pages/home/dashboard/dashboard.resolver'
import { HomeComponent } from '@/app/pages/home/home.component'
import { Routes } from '@angular/router'

export const HOME_ROUTES: Routes = [
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'dashboard'
	},
	{
		path: '',
		component: HomeComponent,
		children: [
			{
				path: 'dashboard',
				component: DashboardComponent,
				resolve: {
					pageData: dashboardResolver
				}
			},
			{
				path: 'assets',
				loadChildren: () =>
					import('@/app/pages/home/assets/assets.routes').then((m) => m.ASSETS_ROUTES)
			}
		]
	},
	{
		path: '**',
		redirectTo: 'dashboard'
	}
]

export const BREADCRUMS = [
	{
		path: 'dashboard',
		label: 'Dashboard',
		icon: 'dashboard'
	},
	{
		path: 'locations',
		label: 'Locations',
		icon: 'environment'
	},
	{
		path: 'plants',
		label: 'Plants',
		icon: 'appstore'
	}
]

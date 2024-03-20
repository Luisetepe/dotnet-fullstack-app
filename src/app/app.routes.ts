import { LoginComponent } from '@/app/pages/login/login.component'
import { Routes } from '@angular/router'

export const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: '/home' },
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: 'home',
		loadChildren: () => import('@/app/pages/home/home.routes').then((m) => m.HOME_ROUTES)
	},
	{
		path: '**',
		redirectTo: 'home'
	}
]

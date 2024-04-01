import { LocationsComponent } from '@/app/pages/home/assets/locations/locations.component'
import { PlantsComponent } from '@/app/pages/home/assets/plants/plants.component'
import { Routes } from '@angular/router'
import { locationsResolver } from './locations/locations.resolver'
import { plantsResolver } from './plants/plants.resolver'

export const ASSETS_ROUTES: Routes = [
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'plants'
	},
	{
		path: 'locations',
		component: LocationsComponent,
		resolve: {
			pageData: locationsResolver
		}
	},
	{
		path: 'plants',
		component: PlantsComponent,
		resolve: {
			pageData: plantsResolver
		}
	},
	{
		path: '**',
		redirectTo: 'plants'
	}
]

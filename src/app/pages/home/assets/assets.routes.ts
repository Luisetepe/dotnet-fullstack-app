import { Routes } from '@angular/router'
import { LocationsComponent } from '@/app/pages/home/assets/locations/locations.component'
import { PlantsComponent } from '@/app/pages/home/assets/plants/plants.component'

export const ASSETS_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'plants',
  },
  {
    path: 'locations',
    component: LocationsComponent,
  },
  {
    path: 'plants',
    component: PlantsComponent,
  },
  {
    path: '**',
    redirectTo: 'plants',
  },
]

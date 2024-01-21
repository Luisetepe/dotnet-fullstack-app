import { Routes } from '@angular/router'
import { DashboardComponent } from '@/app/pages/home/dashboard/dashboard.component'
import { LocationsComponent } from '@/app/pages/home/assets/locations/locations.component'
import { LoginComponent } from '@/app/pages/login/login.component'

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/home' },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'home',
    loadChildren: () => import('@/app/pages/home/home.routes').then(m => m.HOME_ROUTES),
  },
  {
    path: '**',
    redirectTo: 'home',
  },
]

import { DashboardData, DashboardService } from '@/app/pages/home/dashboard/dashboard.service'
import { environment } from '@/environments/environment'
import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize } from 'rxjs'

export type DashboardResolverData = DashboardData

export const dashboardResolver: ResolveFn<DashboardResolverData> = (
	route: ActivatedRouteSnapshot,
	state: RouterStateSnapshot
) => {
	const dashboardService = inject(DashboardService)
	const notification = inject(NzNotificationService)
	const appStore = inject(AppStore)

	appStore.startRouteLoading('Loading dashboard data...')

	return dashboardService.getDashboardData().pipe(
		delay(environment.artificialNavigationDelay),
		finalize(() => {
			appStore.finishRouteLoading()
		}),
		catchError((error) => {
			appStore.finishRouteLoading()
			notification.error(
				'Error fetching dashboard data',
				'An error occurred while fetching dashboard data. Please try again later.'
			)
			return EMPTY
		})
	)
}

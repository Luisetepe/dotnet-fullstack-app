import { DashboardData, DashboardService } from '@/app/pages/home/dashboard/dashboard.service'
import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize } from 'rxjs'

export const dashboardResolver: ResolveFn<DashboardData> = (
	route: ActivatedRouteSnapshot,
	state: RouterStateSnapshot
) => {
	const dashboardService = inject(DashboardService)
	const notification = inject(NzNotificationService)
	const appStore = inject(AppStore)

	appStore.startRouteLoading('Loading dashboard data...')

	return dashboardService.getDashboardData().pipe(
		delay(500),
		finalize(() => {
			appStore.finishRouteLoading()
		}),
		catchError((error) => {
			console.error('Error fetching dashboard data:', error)
			appStore.finishRouteLoading()
			notification.error(
				'Error fetching dashboard data',
				'An error occurred while fetching dashboard data. Please try again later.'
			)
			return EMPTY
		})
	)
}

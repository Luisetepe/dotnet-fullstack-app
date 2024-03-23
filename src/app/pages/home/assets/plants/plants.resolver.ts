import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize } from 'rxjs'
import { PlantDataDto, PlantsService } from './plants.service'

export const plantsResolver: ResolveFn<PlantDataDto> = (
	route: ActivatedRouteSnapshot,
	state: RouterStateSnapshot
) => {
	const plantsService = inject(PlantsService)
	const notification = inject(NzNotificationService)
	const appStore = inject(AppStore)

	appStore.startRouteLoading('Loading plants data...')

	return plantsService
		.getPlantsList({
			pageNumber: 1,
			pageSize: 5
		})
		.pipe(
			delay(500),
			finalize(() => {
				appStore.finishRouteLoading()
			}),
			catchError((error) => {
				console.error('Error fetching plants data:', error)
				appStore.finishRouteLoading()
				notification.error(
					'Error fetching plants data',
					'An error occurred while fetching plants data. Please try again later.'
				)
				return EMPTY
			})
		)
}

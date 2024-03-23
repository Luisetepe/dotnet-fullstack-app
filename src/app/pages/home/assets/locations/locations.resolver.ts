import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize } from 'rxjs'
import { DEFAULT_LOCATIONS_PAGE_SIZE, LocationDataDto, LocationsService } from './locations.service'

export const locationsResolver: ResolveFn<LocationDataDto> = (
	route: ActivatedRouteSnapshot,
	state: RouterStateSnapshot
) => {
	const plantsService = inject(LocationsService)
	const notification = inject(NzNotificationService)
	const appStore = inject(AppStore)

	appStore.startRouteLoading('Loading locations data...')

	return plantsService
		.getLocationsList({
			pageNumber: 1,
			pageSize: DEFAULT_LOCATIONS_PAGE_SIZE
		})
		.pipe(
			delay(500),
			finalize(() => {
				appStore.finishRouteLoading()
			}),
			catchError((error) => {
				console.error('Error fetching locations data:', error)
				appStore.finishRouteLoading()
				notification.error(
					'Error fetching locations data',
					'An error occurred while fetching locations data. Please try again later.'
				)
				return EMPTY
			})
		)
}

import { environment } from '@/environments/environment'
import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize, forkJoin, map, pipe } from 'rxjs'
import { PlantDependenciesDto, PlantGridDataDto, PlantsService } from './plants.service'

export type PlantsResolverData = PlantGridDataDto

export const plantsResolver: ResolveFn<PlantsResolverData> = (
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
			pageSize: environment.defaultGridPageSize
		})
		.pipe(
			delay(environment.artificialApiDelay),
			pipe(
				map((plantsResult) => ({
					plants: plantsResult.plants,
					pagination: plantsResult.pagination
				}))
			),
			finalize(() => {
				appStore.finishRouteLoading()
			}),
			catchError((error) => {
				appStore.finishRouteLoading()
				notification.error(
					'Error fetching plants data',
					'An error occurred while fetching plants data. Please try again later.'
				)
				return EMPTY
			})
		)
}

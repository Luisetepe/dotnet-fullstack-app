import { AppStore } from '@/lib/stores/app.store'
import { inject } from '@angular/core'
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { EMPTY, catchError, delay, finalize, forkJoin, map, pipe } from 'rxjs'
import { PlantDependenciesDto, PlantGridDataDto, PlantsService } from './plants.service'

export type PlantsResolverData = PlantGridDataDto & { dependencies: PlantDependenciesDto }

export const plantsResolver: ResolveFn<PlantsResolverData> = (
	route: ActivatedRouteSnapshot,
	state: RouterStateSnapshot
) => {
	const plantsService = inject(PlantsService)
	const notification = inject(NzNotificationService)
	const appStore = inject(AppStore)

	appStore.startRouteLoading('Loading plants data...')

	const $plants = plantsService.getPlantsList({
		pageNumber: 1,
		pageSize: 5
	})
	const $dependencies = plantsService.getPlantCreationDependencies()

	return forkJoin([$plants, $dependencies]).pipe(
		delay(500),
		pipe(
			map(([plantsResult, dependenciesResult]) => ({
				plants: plantsResult.plants,
				dependencies: dependenciesResult,
				pagination: plantsResult.pagination
			}))
		),
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

import { environment } from '@/environments/environment'
import type { PaginationInfo, SearchRequest } from '@/lib/services/backend-api.service'
import { CommonModule } from '@angular/common'
import { Component, type OnDestroy, type OnInit, inject } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'
import { NzBadgeModule } from 'ng-zorro-antd/badge'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzDrawerModule } from 'ng-zorro-antd/drawer'
import { NzFormModule } from 'ng-zorro-antd/form'
import { NzGridModule } from 'ng-zorro-antd/grid'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzInputModule } from 'ng-zorro-antd/input'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header'
import { NzSelectModule } from 'ng-zorro-antd/select'
import { NzTableModule, type NzTableQueryParams } from 'ng-zorro-antd/table'
import { Subject, debounceTime, delay, finalize } from 'rxjs'
import { PlantDrawerComponent } from './plant-drawer.component'
import type { PlantsResolverData } from './plants.resolver'
import { type PlantByIdDto, type PlantGridDataDto, PlantsService } from './plants.service'

type PlantRow = PlantGridDataDto['plants'][0]

@Component({
	selector: 'app-plants',
	standalone: true,
	imports: [
		CommonModule,
		NzTableModule,
		NzPageHeaderModule,
		NzButtonModule,
		NzIconModule,
		NzBadgeModule,
		NzInputModule,
		NzSelectModule,
		NzDrawerModule,
		NzGridModule,
		NzFormModule,
		FormsModule,
		PlantDrawerComponent
	],
	templateUrl: './plants.component.html'
})
export class PlantsComponent implements OnInit, OnDestroy {
	private readonly activatedRoute = inject(ActivatedRoute)
	private readonly plantsService = inject(PlantsService)
	private readonly notification = inject(NzNotificationService)
	private readonly searchSubject = new Subject<string>()
	private readonly debounceTimeMs = 350

	searchText = ''
	plants: PlantRow[] = []
	paginartionInfo: PaginationInfo
	drawerPlant: PlantByIdDto

	// This flag is used to prevent the grid from reloading when pagination is set manually
	gridManuallyChanged = false

	loading = false
	drawerVisible = false

	ngOnInit() {
		// biome-ignore lint/style/noNonNullAssertion: it is safe to assume the data is present
		const data = this.activatedRoute.snapshot.data['pageData']! as PlantsResolverData

		this.plants = data.plants
		this.paginartionInfo = data.pagination
		this.gridManuallyChanged = true

		this.searchSubject.pipe(debounceTime(this.debounceTimeMs)).subscribe((searchValue) => {
			this.performSearch(searchValue)
		})
	}

	ngOnDestroy() {
		this.searchSubject.complete()
	}

	onSearch(searchEvent: Event) {
		if (!(searchEvent instanceof InputEvent)) return

		this.searchSubject.next(this.searchText)
	}

	performSearch(searchValue: string) {
		this.loading = true
		this.loadPlants(
			{
				search: searchValue,
				pageNumber: 1,
				pageSize: this.paginartionInfo.currentPageSize
			},
			true
		)
	}

	onQueryParamsChange(params: NzTableQueryParams) {
		// This check is used to prevent the grid from reloading when pagination is set manually
		if (this.gridManuallyChanged) {
			this.gridManuallyChanged = false
			return
		}

		const { pageSize, pageIndex, sort } = params
		const currentSort = sort.find((item) => item.value !== null)
		const sortField = currentSort?.key || undefined
		const sortOrder = currentSort?.value || undefined

		this.paginartionInfo.currentPageNumber = pageIndex
		this.paginartionInfo.currentPageSize = pageSize
		this.loading = true

		this.loadPlants({
			search: this.searchText,
			pageNumber: pageIndex,
			pageSize: pageSize,
			order: sortOrder === 'ascend' ? 'asc' : 'desc',
			orderBy: sortField
		})
	}

	onRowEdit(row: PlantRow) {
		this.loading = true
		this.plantsService
			.getPlantById(row.id)
			.pipe(
				delay(environment.artificialApiDelay),
				finalize(() => {
					this.loading = false
				})
			)
			.subscribe({
				next: (plant) => {
					this.drawerPlant = plant
					this.drawerVisible = true
				},
				error: () => {
					this.notification.error(
						'Error fetching plant data',
						'An error occurred while fetching plant data. Please try again later.'
					)
				}
			})
	}

	onCancelDrawer() {
		this.drawerVisible = false
	}

	private loadPlants(params: SearchRequest, resetPagination = false) {
		this.plantsService
			.getPlantsList(params)
			.pipe(
				delay(environment.artificialApiDelay),
				finalize(() => {
					this.loading = false
				})
			)
			.subscribe({
				next: (data) => {
					this.plants = data.plants

					if (resetPagination) {
						this.paginartionInfo = data.pagination
					}
				},
				error: () => {
					this.notification.error(
						'Error fetching plants data',
						'An error occurred while fetching plants data. Please try again later.'
					)
				}
			})
	}
}

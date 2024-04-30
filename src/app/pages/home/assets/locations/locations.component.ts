import { environment } from '@/environments/environment'
import { PaginationInfo, SearchRequest } from '@/lib/services/backend-api.service'
import { CommonModule } from '@angular/common'
import { Component, inject } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'
import { NzBadgeModule } from 'ng-zorro-antd/badge'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzInputModule } from 'ng-zorro-antd/input'
import { NzNotificationService } from 'ng-zorro-antd/notification'
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header'
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table'
import { Subject, debounceTime, delay, finalize } from 'rxjs'
import { LocationsResolverData } from './locations.resolver'
import { LocationDataDto, LocationsService } from './locations.service'

@Component({
	selector: 'app-locations',
	standalone: true,
	imports: [
		CommonModule,
		NzTableModule,
		NzPageHeaderModule,
		NzButtonModule,
		NzIconModule,
		NzBadgeModule,
		NzInputModule,
		FormsModule
	],
	templateUrl: './locations.component.html'
})
export class LocationsComponent {
	private readonly activatedRoute = inject(ActivatedRoute)
	private readonly plantsService = inject(LocationsService)
	private readonly notification = inject(NzNotificationService)
	private readonly searchSubject = new Subject<string>()
	private readonly debounceTimeMs = 350

	// This flag is used to prevent the grid from reloading when pagination is set manually
	gridManuallyChanged = false

	searchText = ''
	loading = false
	locations: LocationDataDto['locations'] = []
	paginartionInfo: PaginationInfo

	ngOnInit() {
		// biome-ignore lint/style/noNonNullAssertion: it is safe to assume the data is present
		const data = this.activatedRoute.snapshot.data['pageData']! as LocationsResolverData

		this.locations = data.locations
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

		this.paginartionInfo.currentPageSize = pageSize
		this.paginartionInfo.currentPageNumber = pageIndex
		this.loading = true

		this.loadPlants({
			search: this.searchText,
			pageNumber: pageIndex,
			pageSize: pageSize,
			order: sortOrder === 'ascend' ? 'asc' : 'desc',
			orderBy: sortField
		})
	}

	private loadPlants(params: SearchRequest, resetPagination = false) {
		this.plantsService
			.getLocationsList(params)
			.pipe(
				delay(environment.artificialApiDelay),
				finalize(() => {
					this.loading = false
				})
			)
			.subscribe({
				next: (data) => {
					this.locations = data.locations

					if (resetPagination) {
						this.paginartionInfo = data.pagination
						this.gridManuallyChanged = true
					}
				},
				error: (error) => {
					this.notification.error(
						'Error fetching locations data',
						'An error occurred while fetching locations data. Please try again later.'
					)
				}
			})
	}
}

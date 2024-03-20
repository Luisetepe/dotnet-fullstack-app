import { CommonModule } from '@angular/common'
import { Component, OnDestroy, OnInit, inject } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'
import { NzBadgeModule } from 'ng-zorro-antd/badge'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzInputModule } from 'ng-zorro-antd/input'
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header'
import {
	NzTableFilterFn,
	NzTableFilterList,
	NzTableModule,
	NzTableSortFn,
	NzTableSortOrder
} from 'ng-zorro-antd/table'
import { Subject, debounceTime, delay } from 'rxjs'
import { PlantData, PlantsService } from './plants.service'

type ColumnItem = {
	name: string
	sortOrder?: NzTableSortOrder
	sortFn?: NzTableSortFn<PlantData>
	listOfFilter?: NzTableFilterList
	filterFn?: NzTableFilterFn<PlantData>
	filterMultiple?: boolean
	sortDirections?: NzTableSortOrder[]
}

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
		FormsModule
	],
	templateUrl: './plants.component.html'
})
export class PlantsComponent implements OnInit, OnDestroy {
	private activatedRoute = inject(ActivatedRoute)
	private plantsService = inject(PlantsService)
	private searchSubject = new Subject<string>()
	private readonly debounceTimeMs = 400

	searchText = ''
	loading = false

	columnsDef: ColumnItem[] = [
		{
			name: 'Name'
		},
		{
			name: 'Plant ID'
		},
		{
			name: 'Utility Company'
		},
		{
			name: 'Status'
		},
		{
			name: 'Tags'
		},
		{
			name: 'Capacity DC'
		},
		{
			name: 'Portfolios'
		}
	]

	plants: PlantData['plants'] = []

	ngOnInit() {
		// biome-ignore lint/style/noNonNullAssertion: it is safe to assume the data is present
		const data = this.activatedRoute.snapshot.data['plantsData']! as PlantData
		this.plants = data.plants
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
		this.plantsService
			.getPlantsList({ search: searchValue })
			.pipe(delay(1000))
			.subscribe((response) => {
				this.plants = response.plants
				this.loading = false
			})
	}
}

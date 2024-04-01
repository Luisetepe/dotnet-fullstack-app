import { DashboardData } from '@/app/pages/home/dashboard/dashboard.service'
import { DecimalPipe, formatNumber } from '@angular/common'
import { Component, Input, LOCALE_ID, OnInit, inject } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { Chart } from 'chart.js/auto'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzNotificationModule } from 'ng-zorro-antd/notification'
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton'
import { DashboardResolverData } from './dashboard.resolver'

@Component({
	selector: 'app-dashboard-card',
	standalone: true,
	template: `
	<div [class]="'flex items-center w-80 h-24 border rounded-lg ' + this.colorClass">
		<div class="flex flex-row justify-between items-center text-white w-full h-full">
			<span class="text-[40px] ml-6" nz-icon [nzType]="this.icon" nzTheme="outline"></span>
			<div class="text-xl mr-5 font-bold justify-between">
				<div>{{this.label}}</div>
				<span class="flex justify-end">{{ this.value }}</span>
			</div>
		</div>
	</div>`,
	imports: [DecimalPipe, NzIconModule]
})
export class DashboardCardComponent {
	@Input() label: string
	@Input() colorClass: string
	@Input() value: string
	@Input() icon: string
}

@Component({
	selector: 'app-dashboard',
	standalone: true,
	imports: [
		NzIconModule,
		NzSkeletonModule,
		NzNotificationModule,
		DashboardCardComponent,
		DecimalPipe
	],
	templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
	private activatedRoute = inject(ActivatedRoute)
	private locale = inject(LOCALE_ID)

	dashboardData: DashboardData
	totalCapacity: string

	ngOnInit() {
		// biome-ignore lint/style/noNonNullAssertion: it is safe to assume the data is present
		this.dashboardData = this.activatedRoute.snapshot.data['pageData']! as DashboardResolverData
		this.totalCapacity = formatNumber(
			this.dashboardData.solarCapacity + this.dashboardData.storageCapacity,
			this.locale
		)

		this.LoadCapacityChart()
	}

	private LoadCapacityChart() {
		// We need to use setTimeout to ensure the chart is rendered after the view is initialized
		setTimeout(
			() =>
				new Chart('PowerChart', {
					type: 'doughnut',
					data: {
						labels: [
							`Solar (DC) MW ${formatNumber(this.dashboardData.solarCapacity, this.locale)}`,
							`Storage (DC) MW ${formatNumber(this.dashboardData.storageCapacity, this.locale)}`
						],
						datasets: [
							{
								data: [this.dashboardData.solarCapacity, this.dashboardData.storageCapacity],
								backgroundColor: ['rgb(245, 197, 66)', 'rgb(132, 53, 212)']
							}
						]
					},
					options: {
						plugins: {
							legend: {
								position: 'bottom',
								fullSize: true,
								labels: {
									font: {
										size: 18
									}
								}
							},
							title: {
								display: true,
								text: `${this.totalCapacity} MW TOTAL`,
								font: {
									size: 24
								},
								position: 'bottom',
								align: 'center'
							}
						}
					}
				}),
			0
		)
	}
}

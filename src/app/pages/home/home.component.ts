import { BREADCRUMS } from '@/app/pages/home/home.routes'
import { AppStore } from '@/lib/stores/app.store'
import { Location } from '@angular/common'
import { Component, OnInit, inject } from '@angular/core'
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router'
import { NzAvatarComponent } from 'ng-zorro-antd/avatar'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzIconDirective } from 'ng-zorro-antd/icon'
import {
	NzContentComponent,
	NzHeaderComponent,
	NzLayoutComponent,
	NzSiderComponent
} from 'ng-zorro-antd/layout'
import { NzMenuDirective, NzMenuItemComponent, NzSubMenuComponent } from 'ng-zorro-antd/menu'
import { NzPopoverModule } from 'ng-zorro-antd/popover'
import { NzSpinComponent } from 'ng-zorro-antd/spin'
import { filter } from 'rxjs/operators'

type MenuRoute = {
	path?: string
	label: string
	icon: string
	isOpen?: boolean
	children?: {
		path: string
		label: string
		icon: string
	}[]
}

type BreadcrumSegment = {
	label: string
	icon: string
}

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [
		NzAvatarComponent,
		NzContentComponent,
		NzHeaderComponent,
		NzIconDirective,
		NzLayoutComponent,
		NzMenuDirective,
		NzMenuItemComponent,
		NzSiderComponent,
		NzSubMenuComponent,
		NzButtonModule,
		NzPopoverModule,
		RouterLink,
		RouterOutlet,
		NzSpinComponent
	],
	templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
	private location = inject(Location)

	private route = inject(Router)

	appStore = inject(AppStore)
	currentBreadcrumb: BreadcrumSegment
	routes: MenuRoute[]

	ngOnInit(): void {
		// Build the menu and breadcrum at first render
		this.buildBreadcrum()
		this.routes = [
			{
				path: 'dashboard',
				label: 'Dashboard',
				icon: 'dashboard'
			},
			{
				label: 'Assets',
				isOpen: this.location.path().includes('/assets'),
				children: [
					{
						path: 'assets/locations',
						label: 'Locations',
						icon: 'environment'
					},
					{
						path: 'assets/plants',
						label: 'Plants',
						icon: 'appstore'
					}
				],
				icon: 'table'
			}
		]

		// Update breadcrum on route change
		this.route.events
			.pipe(filter((e) => e instanceof NavigationEnd))
			.subscribe((_) => this.buildBreadcrum())
	}

	private buildBreadcrum(): void {
		const currentRouteSegment = this.location
			.path()
			.split('/')
			.filter((r) => r !== '')
			.pop()

		const route = BREADCRUMS.find((r) => r.path === currentRouteSegment) || {
			label: 'Not found',
			icon: 'question'
		}

		this.currentBreadcrumb = {
			label: route.label,
			icon: route.icon
		}
	}
}

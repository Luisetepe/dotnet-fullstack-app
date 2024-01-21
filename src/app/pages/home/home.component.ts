import { Component, inject, OnInit } from '@angular/core'
import { NzAvatarComponent } from 'ng-zorro-antd/avatar'
import {
  NzContentComponent,
  NzHeaderComponent,
  NzLayoutComponent,
  NzSiderComponent,
} from 'ng-zorro-antd/layout'
import { NzIconDirective } from 'ng-zorro-antd/icon'
import { NzMenuDirective, NzMenuItemComponent, NzSubMenuComponent } from 'ng-zorro-antd/menu'
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router'
import { Location } from '@angular/common'
import { filter } from 'rxjs/operators'
import { BREADCRUMS } from '@/app/pages/home/home.routes'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzPopoverModule } from 'ng-zorro-antd/popover'

type MenuRoute = {
  path?: string
  label: string
  icon: string
  isOpen?: boolean
  children?: { path: string; label: string; icon: string }[]
}

type BreadcrumSegment = {
  path: string
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
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.less',
})
export class HomeComponent implements OnInit {
  private location = inject(Location)
  private route = inject(Router)

  currentBreadcrumb: BreadcrumSegment[]
  routes: MenuRoute[]

  ngOnInit(): void {
    // Build the menu and breadcrum at first render
    this.buildBreadcrum()
    this.routes = [
      {
        path: 'dashboard',
        label: 'Dashboard',
        icon: 'dashboard',
      },
      {
        label: 'Assets',
        isOpen: this.location.path().includes('/assets'),
        children: [
          {
            path: 'assets/locations',
            label: 'Locations',
            icon: 'environment',
          },
          {
            path: 'assets/plants',
            label: 'Plants',
            icon: 'appstore',
          },
        ],
        icon: 'table',
      },
    ]

    // Update breadcrum on route change
    this.route.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(_ => this.buildBreadcrum())
  }

  private buildBreadcrum(): void {
    const currentRouteSegments = this.location
      .path()
      .split('/')
      .filter(r => r !== '')

    this.currentBreadcrumb = currentRouteSegments.map((segment, index) => {
      const route = BREADCRUMS.find(r => r.path === segment)!
      return {
        path: route.path,
        label: route.label,
        icon: route.icon,
      }
    })
  }
}

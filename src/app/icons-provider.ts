import { EnvironmentProviders, importProvidersFrom } from '@angular/core'
import {
	DashboardOutline,
	FormOutline,
	MenuFoldOutline,
	MenuUnfoldOutline
} from '@ant-design/icons-angular/icons'
import { NzIconModule } from 'ng-zorro-antd/icon'

const icons = [MenuFoldOutline, MenuUnfoldOutline, DashboardOutline, FormOutline]

export function provideNzIcons(): EnvironmentProviders {
	return importProvidersFrom(NzIconModule.forRoot(icons))
}

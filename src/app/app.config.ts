import { registerLocaleData } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'
import es from '@angular/common/locales/es'
import { ApplicationConfig, LOCALE_ID, importProvidersFrom } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { provideAnimations } from '@angular/platform-browser/animations'
import { provideRouter } from '@angular/router'
import { NzConfig, provideNzConfig } from 'ng-zorro-antd/core/config'
import { es_ES, provideNzI18n } from 'ng-zorro-antd/i18n'
import { routes } from './app.routes'
import { provideNzIcons } from './icons-provider'

registerLocaleData(es)

const ngZorroConfig: NzConfig = {
	notification: { nzTop: 75 },
	message: { nzTop: 75 }
}
export const appConfig: ApplicationConfig = {
	providers: [
		{ provide: LOCALE_ID, useValue: 'es' },
		provideRouter(routes),
		provideNzConfig(ngZorroConfig),
		provideNzIcons(),
		provideNzI18n(es_ES),
		importProvidersFrom(FormsModule),
		importProvidersFrom(HttpClientModule),
		provideAnimations()
	]
}

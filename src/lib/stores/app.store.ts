import { Injectable, inject, signal } from '@angular/core'
import { NzMessageService } from 'ng-zorro-antd/message'

@Injectable({ providedIn: 'root' })
export class AppStore {
	private messageService = inject(NzMessageService)

	private loadingMessageId = ''

	private routeLoading = signal(false)

	get isRouteLoading() {
		return this.routeLoading()
	}

	startRouteLoading(loadingMessage?: string) {
		this.loadingMessageId = this.messageService.loading(loadingMessage ?? 'Loading...', {
			nzDuration: 0
		}).messageId
		this.routeLoading.set(true)
	}

	finishRouteLoading() {
		this.messageService.remove(this.loadingMessageId)
		this.routeLoading.set(false)
	}
}

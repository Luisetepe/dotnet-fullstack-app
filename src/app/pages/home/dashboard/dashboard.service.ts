import { BackendApiService } from '@/lib/services/backend-api.service'
import { Injectable, inject } from '@angular/core'
import { Observable, map } from 'rxjs'
import * as z from 'zod'

const dashboarDataSchema = z.object({
	plants: z.number(),
	locations: z.number(),
	solarCapacity: z.number(),
	storageCapacity: z.number()
})

export type DashboardData = z.infer<typeof dashboarDataSchema>

@Injectable({ providedIn: 'root' })
export class DashboardService {
	private backendApiService = inject(BackendApiService)

	getDashboardData(): Observable<DashboardData> {
		return this.backendApiService.get<DashboardData>('dashboard/getDashboardWidgetsData').pipe(
			map((data) => {
				const result = dashboarDataSchema.safeParse(data)
				if (!result.success) {
					throw new Error('Invalid dashboard data')
				}
				return result.data
			})
		)
	}
}

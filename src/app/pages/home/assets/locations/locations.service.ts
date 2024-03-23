import {
	BackendApiService,
	SearchRequest,
	paginationInfoSchema,
	searchRequestSchema
} from '@/lib/services/backend-api.service'
import { createHttpParams } from '@/lib/utils/http.utils'
import { Injectable, inject } from '@angular/core'
import { Observable, map } from 'rxjs'
import * as z from 'zod'

export const DEFAULT_LOCATIONS_PAGE_SIZE = 10

const locationDataDtoSchema = z.object({
	locations: z.array(
		z.object({
			id: z.string(),
			name: z.string(),
			longitude: z.number(),
			latitude: z.number()
		})
	),
	pagination: paginationInfoSchema
})
export type LocationDataDto = z.infer<typeof locationDataDtoSchema>

@Injectable({ providedIn: 'root' })
export class LocationsService {
	private backendApiService = inject(BackendApiService)

	getLocationsList(params?: SearchRequest): Observable<LocationDataDto> {
		const searchParams = searchRequestSchema.parse(params)

		return this.backendApiService
			.get<LocationDataDto>('locations/getLocationsList', createHttpParams(searchParams))
			.pipe(
				map((data) => {
					const result = locationDataDtoSchema.safeParse(data)
					if (!result.success) {
						throw new Error('Invalid location data')
					}
					return result.data
				})
			)
	}
}

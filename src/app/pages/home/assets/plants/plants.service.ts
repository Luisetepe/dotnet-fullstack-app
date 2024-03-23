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

const plantDataDtoSchema = z.object({
	plants: z.array(
		z.object({
			id: z.string(),
			name: z.string(),
			plantId: z.string(),
			utilityCompany: z.string(),
			status: z.string(),
			tags: z.array(z.string()),
			capacityDc: z.number(),
			portfolios: z.array(z.string())
		})
	),
	pagination: paginationInfoSchema
})
export type PlantDataDto = z.infer<typeof plantDataDtoSchema>

@Injectable({ providedIn: 'root' })
export class PlantsService {
	private backendApiService = inject(BackendApiService)

	getPlantsList(params?: SearchRequest): Observable<PlantDataDto> {
		const searchParams = searchRequestSchema.parse(params)

		return this.backendApiService
			.get<PlantDataDto>('plants/getPlantsList', createHttpParams(searchParams))
			.pipe(
				map((data) => {
					const result = plantDataDtoSchema.safeParse(data)
					if (!result.success) {
						throw new Error('Invalid plant data')
					}
					return result.data
				})
			)
	}
}

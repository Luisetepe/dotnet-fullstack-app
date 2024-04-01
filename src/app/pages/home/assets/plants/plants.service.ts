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

const plantDependenciesDtoSchema = z.object({
	plantStatuses: z.array(z.object({ id: z.string(), name: z.string() })),
	locations: z.array(z.object({ id: z.string(), name: z.string() })),
	plantTypes: z.array(z.object({ id: z.string(), name: z.string() })),
	resourceTypes: z.array(z.object({ id: z.string(), name: z.string() })),
	portfolios: z.array(z.object({ id: z.string(), name: z.string() }))
})

const dependencySchema = z.object({
	id: z.string(),
	name: z.string()
})
const getPlantByIdDtoSchema = z.object({
	id: z.string(),
	name: z.string(),
	plantId: z.string(),
	capacityDc: z.number(),
	capacityAc: z.number(),
	storageCapacity: z.number(),
	projectCompany: z.string(),
	utilityCompany: z.string(),
	voltage: z.number(),
	assetManager: z.string(),
	tags: z.string(),
	notes: z.string().optional(),
	plantType: dependencySchema,
	resourceType: dependencySchema,
	status: dependencySchema,
	location: dependencySchema,
	portfolios: z.array(dependencySchema)
})

export type PlantGridDataDto = z.infer<typeof plantDataDtoSchema>
export type PlantDependenciesDto = z.infer<typeof plantDependenciesDtoSchema>
export type PlantByIdDto = z.infer<typeof getPlantByIdDtoSchema>

@Injectable({ providedIn: 'root' })
export class PlantsService {
	private backendApiService = inject(BackendApiService)

	getPlantsList(params?: SearchRequest): Observable<PlantGridDataDto> {
		const searchParams = searchRequestSchema.parse(params)

		return this.backendApiService
			.get<PlantGridDataDto>('plants/getPlantsList', createHttpParams(searchParams))
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

	getPlantCreationDependencies(): Observable<PlantDependenciesDto> {
		return this.backendApiService
			.get<PlantDependenciesDto>('plants/getPlantCreationDependencies')
			.pipe(
				map((data) => {
					const result = plantDependenciesDtoSchema.safeParse(data)
					if (!result.success) {
						throw new Error('Invalid plant dependencies data')
					}
					return result.data
				})
			)
	}

	getPlantById(id: string): Observable<PlantByIdDto> {
		return this.backendApiService
			.get<PlantByIdDto>('plants/getPlantById', createHttpParams({ id }))
			.pipe(
				map((data) => {
					const result = getPlantByIdDtoSchema.safeParse(data)
					if (!result.success) {
						throw new Error('Invalid plant data')
					}
					return result.data
				})
			)
	}
}

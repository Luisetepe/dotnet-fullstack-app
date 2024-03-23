import { environment } from '@/environments/environment'
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http'
import { Injectable, inject } from '@angular/core'
import { Observable, catchError, map, throwError } from 'rxjs'
import * as z from 'zod'

export enum StatusCode {
	Success = 0,
	Error = 1
}

export type BaseResponse<T> = {
	status: StatusCode
	message?: string
	result?: T
}

export const paginationInfoSchema = z.object({
	currentPageNumber: z.number(),
	currentPageSize: z.number(),
	totalRows: z.number(),
	totalPages: z.number()
})

export type PaginationInfo = z.infer<typeof paginationInfoSchema>

export const searchRequestSchema = z
	.object({
		search: z.string().optional(),
		pageNumber: z.number().optional(),
		pageSize: z.number().optional(),
		order: z.enum(['asc', 'desc']).optional(),
		orderBy: z.string().optional()
	})
	.optional()

export type SearchRequest = z.infer<typeof searchRequestSchema>

@Injectable({ providedIn: 'root' })
export class BackendApiService {
	private http = inject(HttpClient)
	private baseUrl = environment.backendBaseUrl

	get<T>(url: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
		const options = { params, headers, observe: 'response' } as const
		return this.http.get<BaseResponse<T>>(`${this.baseUrl}/${url}`, options).pipe(
			map((response) => {
				// biome-ignore lint/style/noNonNullAssertion: It is safe to assume a body is present
				return response.body?.result!
			}),
			catchError(handleApiError)
		)
	}

	// biome-ignore lint/suspicious/noExplicitAny: This method is intentionally generic
	post<T>(url: string, body: any, headers?: HttpHeaders): Observable<T> {
		const options = { headers, observe: 'response' } as const
		return this.http.post<BaseResponse<T>>(`${this.baseUrl}/${url}`, body, options).pipe(
			map((response) => {
				// biome-ignore lint/style/noNonNullAssertion: It is safe to assume a body is present
				return response.body?.result!
			}),
			catchError(handleApiError)
		)
	}
}

function handleApiError(error: HttpErrorResponse) {
	let errorMessage = ''
	if (error.error instanceof ErrorEvent) {
		// A client-side or network error occurred. Handle it accordingly.

		errorMessage = `A client-side error occurred: ${error.error.message}`
	} else {
		// The backend returned an unsuccessful response code.
		// The response body may contain clues as to what went wrong,

		const backendError = error.error as BaseResponse<null>
		errorMessage = backendError.message ?? 'An error occurred on the server side.'
	}

	console.error(errorMessage)
	return throwError(() => new Error(errorMessage))
}

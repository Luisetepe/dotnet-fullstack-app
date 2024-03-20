import { HttpParams } from '@angular/common/http'

export function createHttpParams<
	T extends
		| Record<string, string | number | boolean | readonly (string | number | boolean)[]>
		| undefined
>(obj: T): HttpParams {
	let params: T = {} as T
	if (obj) {
		params = Object.fromEntries(Object.entries(obj).filter(([, v]) => !!v)) as T
	}
	return new HttpParams({
		fromObject: params ?? {}
	})
}

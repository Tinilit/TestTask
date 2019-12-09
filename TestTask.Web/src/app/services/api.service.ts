import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiService {

    private readonly apiUrl = environment.apiUrl;

    constructor(
        private injector: Injector) {
    }

    private buildHeaders(options: any): HttpHeaders {
        const headersConfig: any = {
            'Accept': 'application/json'
        };

        // can't set 'multipart/form-data' because boundary
        if (options.contentType != 'multipart/form-data')
            headersConfig['Content-Type'] = options.contentType || 'application/json';

        return new HttpHeaders(headersConfig);
    }

    //removed typing, cause we don't use it and it makes problems in AngularV8
    private makeRequest<T>(method: string, url: string, queryParameters: any, request: any, options: any = {}): Observable<any> {
        const path = this.apiUrl + '/api/' + url;
        let body = request;
        if (request && !(request instanceof FormData))
            body = JSON.stringify(request);

        let params = new HttpParams();
        if (queryParameters) {
            for (let key of Object.keys(queryParameters)) {
                if (queryParameters[key] instanceof Array) {
                    queryParameters[key].forEach(val => {
                        params = params.append(key, val);
                    });
                } else {
                    params = params.set(key, queryParameters[key]);
                }

            }
        }

        return this.getHttp().request(method, path, {
            body: body,
            headers: this.buildHeaders(options),
            params: params,
            observe: 'response'
        }).pipe(
            map(response => {
                console.log(response);
                if (response.status === 204) {
                    return undefined;
                } else {
                    return response.body || {};
                }
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    private getHttp(): HttpClient {
        return this.injector.get(HttpClient);
    }

    protected get<T>(url: string, request: any, options: any = {}): Observable<T> {
        return this.makeRequest<T>("GET", url, request, null, options);
    }

    protected post<T>(url: string, request: any, options: any = {}): Observable<T> {
        return this.makeRequest<T>("POST", url, null, request, options);
    }

    protected delete<T>(url: string, request: any, options: any = {}): Observable<T> {
        return this.makeRequest<T>("DELETE", url, request, null, options);
    }
}
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators';

import { BaseService } from './base.service';
import { Contact } from '../models';
import { IListResponse, IQueryParameters } from '../interfaces';

@Injectable({
    providedIn: 'root',
})
export class ContactService extends BaseService {

    constructor(
        private httpClient: HttpClient
    ) {
        super();
    }

    public search(params: IQueryParameters = {}): Observable<IListResponse<Contact>> {
        return this.httpClient
            .get(
                `${this.baseUrl}/contact/search`,
                {
                    params: (params as any)
                }
            )
            .pipe(
                map((response: any) => {
                    const contactList: IListResponse<Contact> = {
                        result: response.result.map(contact => new Contact(contact)),
                        page: response.page,
                    };

                    return contactList;
                })
            );
    }
}

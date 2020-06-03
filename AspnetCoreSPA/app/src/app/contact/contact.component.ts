import { ViewChild, Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { MatTableDataSource, MatSort } from '@angular/material';

import { Subject } from "rxjs";
import { takeUntil, debounceTime, switchMap } from 'rxjs/internal/operators';

import { ContactService } from '../core/services';
import { Contact } from '../core/models';
import { IQueryParameters, IPaging, IListResponse } from '../core/interfaces';

@Component({
    selector: 'app-contact',
    templateUrl: './contact.component.html',
    styleUrls: ['./contact.component.scss'],
})
export class ContactComponent implements OnInit, OnDestroy {

    public queryParams: IQueryParameters = {
        page: 0,
        size: 10,
    };

    public displayedColumns = [
        'firstName',
        'lastName',
        'email',
        'phoneNumber1'
    ];

    public dataSource = new MatTableDataSource<Contact>([]);
    public totalElements = 0;
    public pageSizeOptions = [ 10 ];

    public currentUserId: string;
    public searchForm: FormGroup;

    private contactSubject$ = new Subject<IQueryParameters>();
    private destroyed$ = new Subject<void>();

    @ViewChild(MatSort, { static: true }) private sort: MatSort;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private contactService: ContactService
    ) { }

    ngOnInit() {
        this.createForm();
        this.initInternalParamsFromUrl();
        this.initContactSearchSubject();
        this.contactSubject$.next();
        this.sortChanged();
        this.formChanged();
    }

    ngOnDestroy() {
        this.contactSubject$.complete();
        this.destroyed$.next();
        this.destroyed$.complete();
    }

    public pageChange(event: IPaging): void {
        this.queryParams.page = event.pageIndex;
        this.queryParams.size = event.pageSize;
        this.contactSubject$.next();
    }

    private createForm() {
        this.searchForm = new FormGroup({
            searchText: new FormControl()
        });
    }

    private initInternalParamsFromUrl(): void {
        const params: any = this.activatedRoute.snapshot.queryParams;
        if (params.size) {
            this.queryParams.size = params.size;
        }
        if (params.page) {
            this.queryParams.page = params.page - 1;
        }
        if (params.pattern) {
            this.queryParams.pattern = params.pattern;

            this.searchForm.patchValue({
                searchText: params.pattern
            });
        }
    }

    private initContactSearchSubject(): void {
        this.contactSubject$
            .pipe(
                debounceTime(250),
                switchMap(() => {
                    return this.contactService.search(this.queryParams);
                }),
                takeUntil(this.destroyed$)
            )
            .subscribe({
                next: (contactList: IListResponse<Contact>) => {
                    this.dataSource.data = contactList.result.slice();
                    this.totalElements = contactList.page.totalElements;


                    const params = Object.assign({}, this.queryParams);
                    params.page++;
                    this.router.navigate(['/'], { queryParams: params });
                },
                error: () => {
                    // TODO: handle error here
                },
                complete: () => {
                    // TODO: do something here
                }
            });
    }

    private sortChanged(): void {
        this.sort.sortChange
            .pipe(
                debounceTime(350)
            )
            .subscribe((event) => {
                this.queryParams.sort = `${event.active},${event.direction}`;
                this.queryParams.page = 0;
                this.queryParams.size = 10;
                this.contactSubject$.next();
            });
    }


    private formChanged() {
        this.searchForm.get('searchText').valueChanges.subscribe((data) => {
            this.queryParams.pattern = data;
            this.queryParams.page = 0;
            this.queryParams.size = 10;
            this.contactSubject$.next();
        });
    }


}

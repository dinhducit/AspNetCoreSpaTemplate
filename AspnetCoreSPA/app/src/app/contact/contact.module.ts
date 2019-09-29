import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ContactComponent } from './contact.component';


@NgModule({
    imports: [
        SharedModule,
    ],
    declarations: [
        ContactComponent
    ],
    providers: [],
    entryComponents: [
    ]
})

export class ContactModule {
}

import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { ContactModule } from "./contact/contact.module";

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        SharedModule,
        CoreModule,
        AppRoutingModule,
        ContactModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }

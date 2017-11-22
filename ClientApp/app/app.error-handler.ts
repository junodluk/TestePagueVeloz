import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { ToastyService } from "ng2-toasty";

export class AppErrorHandler implements ErrorHandler {
    constructor(@Inject(NgZone) private ngZone: NgZone,
                @Inject(ToastyService) private toastyService: ToastyService) {}

    handleError(err: any) {
        this.ngZone.run(() => {
            this.toastyService.error({
                title: `${err.status} - ${err.statusText}`,
                msg:'Erro inesperado.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        });
    }
}
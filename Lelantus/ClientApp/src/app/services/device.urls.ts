import { Injectable, Inject } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class DeviceUrls {

    private baseUrl = 'api/devices';

    constructor(@Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = `${baseUrl}${this.baseUrl}`;
    }

    getAll(): string {
        return `${this.baseUrl}`;
    }
}

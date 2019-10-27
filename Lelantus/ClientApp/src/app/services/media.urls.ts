import { Injectable, Inject } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class MediaUrls {

    private baseUrl = 'api/media';

    constructor(@Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = `${baseUrl}${this.baseUrl}`;
    }

    getAllByDevice(deviceId: string): string {
        return `${this.baseUrl}/${deviceId}`;
    }

    getAll(): string {
        return `${this.baseUrl}`;
    }
}

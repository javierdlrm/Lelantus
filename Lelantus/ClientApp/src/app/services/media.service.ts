import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MediaUrls } from './media.urls';

@Injectable({
    providedIn: 'root',
})
export class MediaService {

    constructor(private http: HttpClient, private urls: MediaUrls) { }

    getAllByDevice(deviceId: string): Observable<Media[]> {
        return this.http.get<Media[]>(this.urls.getAllByDevice(deviceId));
    }

    getAll(): Observable<Media[]>{
        return this.http.get<Media[]>(this.urls.getAll());
    }
}

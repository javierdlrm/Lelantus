import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeviceUrls } from './device.urls';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {

  constructor(private http: HttpClient, private urls: DeviceUrls) { }

  getAll(): Observable<Device[]> {
    return this.http.get<Device[]>(this.urls.getAll());
  }
}

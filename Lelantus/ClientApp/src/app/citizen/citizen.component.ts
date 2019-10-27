import { Component, OnInit } from '@angular/core';
import { MediaService } from '../services/media.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-citizen',
  templateUrl: './citizen.component.html',
  styleUrls: ['./citizen.component.scss']
})
export class CitizenComponent implements OnInit {

  readonly deviceId = '4918ae98-2c60-4536-aae4-a471b0bfc962';

  media: Media[];
  selectedMedia: Media[] = [];
  collapsed = false;
  canSave = false;

  // Archive
  archive_columns = [
    { name: 'Type', prop: 'type', sortable: true },
    { name: 'Name', prop: 'name', sortable: true },
    { name: 'Date', prop: 'date', sortable: true },
    { name: 'View', prop: 'url'},
    { name: 'Visible', prop: 'visible', sortable: true }
  ];
  loadingIndicator = true;
  reorderable = true;

  constructor(private mediaService: MediaService, private httpClient: HttpClient) {
  }

  toggle() {
    this.collapsed = !this.collapsed;
  }

  async ngOnInit(): Promise<void> {
    this.loadingIndicator = true;
    this.media = await this.mediaService.getAllByDevice(this.deviceId).toPromise();
    this.selectedMedia = this.media.filter(m => m.visible);
    this.loadingIndicator = false;
    return Promise.resolve();
  }
}

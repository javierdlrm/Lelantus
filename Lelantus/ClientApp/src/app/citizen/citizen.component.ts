import { Component, OnInit } from '@angular/core';
import { MediaService } from '../services/media.service';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-citizen',
  templateUrl: './citizen.component.html',
  styleUrls: ['./citizen.component.scss']
})
export class CitizenComponent implements OnInit {

  readonly deviceId = '4918ae98-2c60-4536-aae4-a471b0bfc962';
  hubConnection: HubConnection;

  media: Media[];
  selectedMedia: Media[] = [];
  collapsed = false;
  canSave = false;

  // Archive
  archive_columns = [
    { name: 'Type', prop: 'type', sortable: true },
    { name: 'Name', prop: 'name', sortable: true },
    { name: 'Date', prop: 'date', sortable: true },
    { name: 'View', prop: 'url' },
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
    await this.loadData();

    if (!this.hubConnection) {
      this.hubConnection = new HubConnectionBuilder().withUrl('https://lelantus.azurewebsites.net/mediahub').build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started!'))
        .catch(err => console.log('Error while establishing connection :('));

      this.hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
        console.log("Receiving broadcast");
        console.log(payload);
        this.loadData();
      });
    }

    return Promise.resolve();
  }

  private async loadData(): Promise<void> {
    this.loadingIndicator = true;
    this.media = await this.mediaService.getAllByDevice(this.deviceId).toPromise();
    this.selectedMedia = this.media.filter(m => m.visible);
    this.loadingIndicator = false;
  }
}

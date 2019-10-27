import { Component, OnInit } from '@angular/core';
import { DeviceService } from '../services/device.service';
import { MediaService } from '../services/media.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-security-force',
  templateUrl: './security-force.component.html',
  styleUrls: ['./security-force.component.scss']
})
export class SecurityForceComponent implements OnInit {

  hubConnection: HubConnection;

  collapsed = false;
  media: Media[];
  devices: Device[];
  selectedDevice: Device;

  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  // Histogram
  public barChartLabels: string[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  public barChartType = 'bar';
  public barChartLegend = true;
  public barChartData: any[] = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' }
  ];

  // History
  history_columns = [
    { name: 'Device id', prop: 'deviceId', sortable: true },
    { name: 'Age', prop: 'age' },
    { name: 'Location', sortable: false },
    { name: 'Phone number', prop: 'phoneNumber', sortable: false }
  ];

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

  constructor(private deviceService: DeviceService, private mediaService: MediaService) {
  }

  async ngOnInit(): Promise<void> {
    this.devices = await this.deviceService.getAll().toPromise();
    this.media = await this.mediaService.getAll().toPromise();
    this.loadingIndicator = false;

    this.hubConnection = new HubConnectionBuilder().withUrl('https://localhost:44366/mediahub').build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
      console.log("Receiving broadcast!!!");
    });

    return Promise.resolve();
  }

async onSelect({ selected }): Promise < void> {
  this.loadingIndicator = true;
  this.selectedDevice = selected[0];
  this.media = await this.mediaService.getAllByDevice(this.selectedDevice.deviceId).toPromise();
  this.loadingIndicator = false;
  return Promise.resolve();
}

toggle() {
  this.collapsed = !this.collapsed;
}

}

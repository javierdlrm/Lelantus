import { NgModule } from '@angular/core';
import { DeviceService } from './device.service';
import { MediaService } from './media.service';
@NgModule({
  imports: [],
  providers: [DeviceService, MediaService]
})
export class ServicesModule { }

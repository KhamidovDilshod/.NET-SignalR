import {AfterViewInit, Component, OnInit} from '@angular/core';
import {HubService} from "./service/hub.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit,OnInit {
  latitude: string = '';
  longitude: string = '';
  private locationSubscription$: any;

  constructor(private hub: HubService) {
  }

  ngAfterViewInit(): void {
    this.hub.connect();
    this.locationSubscription$ = this.hub.locationCoordinates$.subscribe(loc => {
      this.latitude = loc.latitude;
      this.longitude = loc.longitude;
    })
  }

  ngOnInit(): void {
    this.hub.connect();
    this.locationSubscription$ = this.hub.locationCoordinates$.subscribe(loc => {
      console.log(loc);
      this.latitude = loc.latitude;
      this.longitude = loc.longitude;
    })
  }


}

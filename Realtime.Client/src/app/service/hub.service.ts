import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr'
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Subject} from "rxjs";
import {Coordinate} from "../models/coordinate";
import {CONFIGURATION} from "../constants/configuration";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class HubService {
  private connection!: HubConnection;
  connectionEstablished$ = new Subject<Boolean>();
  locationCoordinates$ = new Subject<Coordinate>()

  constructor(private http: HttpClient) {
  }

  public connect(): void {
    this.connection = new HubConnectionBuilder().withUrl(CONFIGURATION.baseUrls.server).build();
    this.connection.start().then(() => {
      console.log('Connection started')
      this.connectionEstablished$.next(true);
    }).catch(err => console.log(err));

    this.connection.on('GetLocation', (res) => {
      console.log(res.latitude);
      console.log(`Received  ${res.latitude}-${res.longitude}`);
      this.locationCoordinates$.next({latitude: res.latitude, longitude: res.longitude})
    })
  }

  public disconnect() {
    if (this.connection) {
      this.connection.stop().finally();
      // @ts-ignore
      this.connection = null;
    }
  }
}

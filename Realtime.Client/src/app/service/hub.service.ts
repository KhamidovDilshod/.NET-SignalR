import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr'
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Subject} from "rxjs";
import {Coordinate} from "../models/coordinate";
import {CONFIGURATION} from "../constants/configuration";

@Injectable({
  providedIn: 'root'
})
export class HubService {
  private connection!: HubConnection;
  connectionEstablished$ = new Subject<Boolean>();
  locationCoordinates$ = new Subject<Coordinate>()

  constructor() {
  }

  public connect(): void {
    if (this.connection) {
      this.connection = new HubConnectionBuilder().withUrl(CONFIGURATION.baseUrls.server).build();
      this.connection.start().then(() => {
        console.log('Connection started')
        this.connectionEstablished$.next(true);
      }).catch(err => console.log(err));

      this.connection.on('GetLocation', (latitude, longitude) => {
        console.log(`Received  ${latitude}-${longitude}`);
        this.locationCoordinates$.next({latitude, longitude})
      })
    }
  }

  public disconnect() {
    if (this.connection) {
      this.connection.stop().finally();
      // @ts-ignore
      this.connection = null;
    }
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class SecretSantaApiService {

  constructor(private client: HttpClient) { }

  async fetchGroup() {
    var result = await this.client.get()
  }
}

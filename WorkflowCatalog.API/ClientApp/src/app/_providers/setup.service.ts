import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SetupService {

  constructor() { }

  currentSetupId: BehaviorSubject<string> = new BehaviorSubject(null);

}

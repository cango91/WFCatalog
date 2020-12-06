import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkflowService {

  constructor() { }

  selectedWorkflowId: BehaviorSubject<string> = new BehaviorSubject(null);
}

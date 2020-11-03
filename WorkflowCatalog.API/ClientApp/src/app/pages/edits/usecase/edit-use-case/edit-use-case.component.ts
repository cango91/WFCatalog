import { Component, OnInit, Input } from '@angular/core';
import { UseCasesDto, WorkflowsDto } from 'src/app/web-api-client';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-edit-use-case',
  templateUrl: './edit-use-case.component.html',
  styleUrls: ['./edit-use-case.component.scss']
})
export class EditUseCaseComponent implements OnInit {

  @Input()
  initialData: UseCasesDto;

  @Input()
  editAuthority: boolean = false;

  asd : UseCasesDto = null;


  constructor(public activeModal : NgbActiveModal) { }

  ngOnInit(): void {
   
  }

}

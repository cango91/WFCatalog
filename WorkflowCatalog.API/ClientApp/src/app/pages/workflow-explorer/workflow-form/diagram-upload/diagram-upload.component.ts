import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { DiagramUploadItem } from './diagram-upload-item';
import { isEmpty } from 'lodash';
import { Guid } from 'src/app/helpers/guid';
import { DiagramsClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-diagram-upload',
  templateUrl: './diagram-upload.component.html',
  styleUrls: ['./diagram-upload.component.scss']
})
export class DiagramUploadComponent implements OnInit {

  @Input() files: DiagramUploadItem[] = [];
  @Output() filesChange: EventEmitter<DiagramUploadItem[]> = new EventEmitter();

  @Input() deleteFiles: DiagramUploadItem[] = [];
  @Output() deleteFilesChange: EventEmitter<DiagramUploadItem[]> = new EventEmitter();

  @Input() addFiles: DiagramUploadItem[] = [];
  @Output() addFilesChange: EventEmitter<DiagramUploadItem[]> = new EventEmitter();

  @Input() defaultDiagram: string = null;
  @Output() defaultDiagramChange: EventEmitter<string> = new EventEmitter();

  @ViewChild('fileInput') fileInput: ElementRef;

  constructor(protected cdr: ChangeDetectorRef,protected diagramService:DiagramsClient) { }

  ngOnInit(): void {
  }

  delete(item: DiagramUploadItem) {
    if (isEmpty(item.id)) {
      this.addFiles.splice(this.addFiles.findIndex(a => a.generatedId === item.generatedId),1);
      this.addFilesChange.emit(this.addFiles);
    } else {
      this.deleteFiles.push(item);
      this.deleteFilesChange.emit(this.deleteFiles);
    }

    this.files.splice(this.files.findIndex(a => a.generatedId === item.generatedId),1);
    this.filesChange.emit(this.files);

    this.cdr.detectChanges();
  }

  makeDefault(item: DiagramUploadItem) {
    this.defaultDiagram = item.id;
    this.defaultDiagramChange.emit(this.defaultDiagram);
  }

  add(files: FileList) {
    const tmpFiles = this.files;
    const tmpAddFiles = this.addFiles;

    for(let i = 0; i < files.length;i++){
      const file = files.item(i);
      const item: DiagramUploadItem = {
        generatedId: Guid.newGuid(),
        data: file,
        name: file.name
      };
 
      tmpFiles.push(item);
      tmpAddFiles.push(item);
    }
    
    this.files = tmpFiles;
    this.addFiles = tmpAddFiles;

    this.filesChange.emit(this.files);
    this.addFilesChange.emit(this.addFiles);

    this.fileInput.nativeElement.value = '';

    this.cdr.detectChanges();
  }

  downloadFile(file){
    if(file.id){
      this.diagramService.getDiagramById(file.id).subscribe((res)=>{
        var a = document.createElement("a");
        a.href = URL.createObjectURL(res.data);
        a.download = res.fileName;
        a.click();
        a.remove();
      });

    }
  }

}

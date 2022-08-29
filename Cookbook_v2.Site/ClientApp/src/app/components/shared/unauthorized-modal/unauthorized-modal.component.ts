import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalWindowComponent } from '../modal-window/modal-window.component';
import { ModalWindowService } from '../modal-window/modal-window.service';

@Component({
  selector: 'app-unauthorized-modal',
  templateUrl: './unauthorized-modal.component.html',
  styleUrls: ['./unauthorized-modal.component.css']
})
export class UnauthorizedModalComponent implements OnInit {
  @ViewChild(ModalWindowComponent) modal: ModalWindowComponent;

  constructor(
    private _modalService: ModalWindowService
  ) { }

  ngOnInit(): void {
  }

  openModal(id: string): void {
    this.close();
    this._modalService.open(id);
  }

  close(): void {
    this.modal.close();
  }
}

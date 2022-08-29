import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalWindowService } from './modal-window.service';

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.css'],
})
export class ModalWindowComponent implements OnInit {
  @Input() id: string;
  @Output() modalClosed = new EventEmitter();
  private _element: any;

  constructor(
    private _modalService: ModalWindowService,
    el: ElementRef
  ) {
    this._element = el.nativeElement;
  }

  ngOnInit(): void {
    if (!this.id) {
      console.error('Invalid modal window id');
      return;
    }

    document.body.appendChild(this._element);

    this._element.addEventListener("click", el => {
      if (el.target.className === "modal-background") {
        this.close();
      }
    });

    this._modalService.add(this);
  }

  ngOnDestroy(): void {
    this._modalService.remove(this.id);
    this._element.remove();
  }

  open(): void {
    this._element.style.display = "block";
    document.body.classList.add("modal_open");
  }

  close(): void {
    this._element.style.display = "none";
    document.body.classList.remove("modal_open");
    this.modalClosed.emit();
  }
}

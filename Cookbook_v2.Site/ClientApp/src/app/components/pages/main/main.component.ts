import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';

import { CategoriesService } from '../../../services/categories.service';
import { Category } from '../../../interfaces/category';
import { ModalWindowService } from '../../shared/modal-window/modal-window.service';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  categories?: Category[] = [];
  searchForm = this._formBuilder.group({
    searchText: '',
  });
  isLoggedIn: boolean = false;

  constructor(
    private _categoriesService: CategoriesService,
    private _formBuilder: UntypedFormBuilder,
    private _modalService: ModalWindowService,
    private _accountService: AccountService,
    private _router: Router
  ) {
    this.isLoggedIn = _accountService.userValue != null;
  }

  ngOnInit() {
    this.loadCategories();
  }

  onSubmit(): void {}

  openModal(id: string) {
    this._modalService.open(id);
  }

  goToRecipeCreationPage(): void {
    if (this._accountService.userValue) {
      this._router.navigate(['/create-recipe']);
    } else {
      this.openModal('unauthorized-modal');
    }
  }

  loadCategories(): void {
    this._categoriesService.getCategories().subscribe((response) => {
      this.categories = response;
    });
  }
}

import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LocalStorageUtils } from "src/app/utils/localstorage";

@Component({
    selector: 'app-admin-header',
    templateUrl: './admin-header.component.html'
  })
export class AdminHeaderComponent {

  localStorageUtil = new LocalStorageUtils();

  constructor(private router: Router) {}

  logOut() {
    this.localStorageUtil.limparDadosLocaisUsuario();
    this.router.navigate(['/admin/login']);
  }

}
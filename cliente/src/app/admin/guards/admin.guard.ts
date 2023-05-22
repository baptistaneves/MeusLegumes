import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { BaseGuard } from 'src/app/services/base.guard';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard extends BaseGuard implements CanActivate {

  constructor(protected override router: Router){ super(router); }

  canActivate(routeAc: ActivatedRouteSnapshot) {
    return super.validarRoles(routeAc);
  }
  
}

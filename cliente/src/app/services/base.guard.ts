import { Router, ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';
import { LocalStorageUtils } from 'src/app/utils/localstorage';

export abstract class BaseGuard { 

    private localStorageUtils = new LocalStorageUtils();
    result: boolean = false;

    constructor(protected router: Router){}

    protected validarRoles(routeAc: ActivatedRouteSnapshot) : boolean {

        if(!this.localStorageUtils.obterTokenUsuario()) {
            this.router.navigate(['/admin/login/'], { queryParams: { returnUrl: this.router.url }});
        }

        let roles = routeAc.data['role'] as Array<string>;

        if(roles) {

            if(this.roleMatch(roles)) {
                this.result = true;
            }
            else {
                this.result = false;
            }
        }

        return this.result;
    }

  private decodefyToken(token:any) {
    return atob(token.split('.')[1]);
  }

  private roleMatch(roles: any[]) : boolean
  {
    let isMatch: boolean = false;
    let payload = JSON.parse(this.decodefyToken(this.localStorageUtils.obterTokenUsuario()));
    
    roles.forEach(element => {
      if(payload.role == element) {
        isMatch = true;
      }
    });

    return isMatch;
  }

}
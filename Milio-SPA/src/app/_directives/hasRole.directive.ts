import { Directive, OnInit, Input, ViewContainerRef, TemplateRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;

  constructor(private viewContainerRef: ViewContainerRef,
              private templateref: TemplateRef<any>,
              private authService: AuthService) { }
  ngOnInit(): void {
    const userRoles = this.authService.decodedToken.role as Array<string>;
    //si no hay roles limpiar viewCOntainerRef
    if (!userRoles) {
      this.viewContainerRef.clear();
    }

    //si el suario tiene un rol necesario se renderizara el elemento
    if (this.authService.roleMatch(this.appHasRole)) {
      if(!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templateref);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    }

  }

}

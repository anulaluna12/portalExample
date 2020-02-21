import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { UserListComponent } from "./user/user-list/user-list.component";
import { LikesComponent } from "./likes/likes.component";
import { MessegesComponent } from "./messeges/messeges.component";
import { AuthGuard } from "./_guards/auth.guard";
import { UserDetailComponent } from "./user/user-detail/user-detail.component";
import { UserDetailResolver } from "./_resolvers/user-detail.resolver";
import { UserListResolver } from "./_resolvers/user-list.resolver";
import { UserEditComponent } from "./user/user-edit/user-edit.component";
import { UserEditResolver } from "./_resolvers/user-edit.resolver";
import { PreventUnsavedChanges } from "./_guards/prevent-unsaved-changes.guard";

export const appRoutes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {
        path: "users",
        component: UserListComponent,
        resolve: {
          users: UserListResolver // Uzywając resolve ładujemy dane przed samą aktywacją rutingu , zanim zostanie  użyty ten komponent
        }
      },
      {
        path: "users/:id",
        component: UserDetailComponent,
        resolve: {
          user: UserDetailResolver // Uzywając resolve ładujemy dane przed samą aktywacją rutingu , zanim zostanie  użyty ten komponent
        }
      },
      {
        path: "likes",
        component: LikesComponent
      },
      {
        path: "messeges",
        component: MessegesComponent
      },
      {
        path: "users/edit",
        component: UserEditComponent,
        resolve: {
          user: UserEditResolver // Uzywając resolve ładujemy dane przed samą aktywacją rutingu , zanim zostanie  użyty ten komponent
        },
        canDeactivate: [PreventUnsavedChanges]
      }
    ]
  },
  {
    path: "**",
    redirectTo: "home",
    pathMatch: "full"
  }
];

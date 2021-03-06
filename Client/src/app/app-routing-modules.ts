import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminPanelComponent } from 'src/app/admin-panel/admin-panel.component';
import { HomeComponent } from 'src/app/home/home.component';
import { AuthGuard } from 'src/app/_guards/auth-guard.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { RegisterComponent } from './registration/register/register.component';
import { SuccesfullRegisterComponent } from './registration/succesfull-register/succesfull-register.component';

const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'admin', component: AdminPanelComponent},

        ]
    },
     {path: 'not-found', component: NotFoundComponent},
     {path: 'error', component: ServerErrorComponent},
     {path: 'register', component: RegisterComponent},
     {path: 'succesfull-register', component: SuccesfullRegisterComponent},
     {path: '**', component: NotFoundComponent, pathMatch: 'full'} // kiedy nie znajdzie zadnej drogi

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}

import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { UserCompound, UserCompoundRequest } from '../../core/models/user/userModel';
import { Observable } from 'rxjs';
import { selectAllUsersCompound } from '../../store/user-compound/user-compound.selectors';
import { fetchUsersCompound } from '../../store/user-compound/user-compound.actions';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import * as AuthActions from '../../store/auth/auth.actions';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-compound-user',
  standalone: true,
  imports:[CommonModule, FormsModule,  RouterModule, RouterOutlet],
  templateUrl: './compound-user.component.html',
  styleUrl: './compound-user.component.css'
})
export class CompoundUserComponent {
  users$:Observable<UserCompound[]> = new Observable<UserCompound[]>(); 

  constructor(
    private store: Store,
    private router: Router 
  ) 
  {    
    this.store.pipe(select(selectAllUsersCompound));
  }
  ngOnInit() {    
    this.users$ = this.store.select(selectAllUsersCompound);
  }
  getUsersData(searchText:string)
  {
    let usersCompound :UserCompoundRequest= {
      firstName : searchText,
      lastName:""
    }   
    this.store.dispatch(fetchUsersCompound({usersCompound}));   

    setTimeout(() => {
      this.users$ = this.store.select(selectAllUsersCompound);      
    }, 1000); 
  }
  logout() {
    localStorage.setItem('isAuthenticated', 'false');
    this.store.dispatch(
      AuthActions.setAuthenticated({ isAuthenticated: false })
    );
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}

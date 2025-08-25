import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/memebers/member-list/member-list';
import { MemberDetailed } from '../features/memebers/member-detailed/member-detailed';
import { Messages } from '../features/messages/messages';
import { authGuard } from '../core/guards/auth-guard';
import { TestErrors } from '../features/test-errors/test-errors';
import { NotFound } from '../shared/errors/not-found/not-found';
import { ServerError } from '../shared/errors/server-error/server-error';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [{ path: 'members', component: MemberList, canActivate: [authGuard] },
    { path: 'members/:id', component: MemberDetailed, canActivate: [authGuard] },
    { path: 'messages', component: Messages, canActivate: [authGuard] },
    ]
  },
  {
    path: 'errors', component: TestErrors
  },
  {
    path: 'server-error', component: ServerError
  },
  { path: '**', component: NotFound }
];

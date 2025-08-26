import { Component, computed, inject, OnInit, Signal, signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AsyncPipe, DatePipe } from '@angular/common';
import { filter, map, Observable } from 'rxjs';
import { Member } from '../../../types/member';
import { AgePipe } from '../../../core/pipes/age-pipe';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-member-detailed',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, RouterOutlet, AgePipe],
  templateUrl: './member-detailed.html',
  styleUrl: './member-detailed.css'
})
export class MemberDetailed implements OnInit {

  private route = inject(ActivatedRoute);
  private accountService = inject(AccountService);
  protected memberService = inject(MemberService);
  private router = inject(Router);
  protected title = signal('Profile');
  protected isCurrentUser = computed(() => {
    return this.accountService.currentUser()?.id === this.route.snapshot.paramMap.get('id');
  }
  );


  ngOnInit(): void {

    this.title.set(this.route.firstChild?.snapshot?.title || 'Profile');
    this.router.events.pipe(
      filter(event => event instanceof RouterLinkActive)
    ).subscribe({
      next: () => {
        this.title.set(this.route.firstChild?.snapshot?.title || 'Profile');
      }
    })




  }



}

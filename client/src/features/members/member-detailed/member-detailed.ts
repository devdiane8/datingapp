import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AsyncPipe, DatePipe } from '@angular/common';
import { filter, map, Observable } from 'rxjs';
import { Member } from '../../../types/member';
import { AgePipe } from '../../../core/pipes/age-pipe';

@Component({
  selector: 'app-member-detailed',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, RouterOutlet, AgePipe],
  templateUrl: './member-detailed.html',
  styleUrl: './member-detailed.css'
})
export class MemberDetailed implements OnInit {

  private membserService = inject(MemberService);
  private route = inject(ActivatedRoute);
  protected member = signal<Member | undefined>(undefined);
  private router = inject(Router);
  protected title = signal('Profile');


  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.member.set(data['member']);
      }
    })
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

import { Component, HostListener, inject, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EditableMember, Member } from '../../../types/member';
import { DatePipe } from '@angular/common';
import { MemberService } from '../../../core/services/member-service';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastService } from '../../../core/services/toast-service';
import { AccountService } from '../../../core/services/account-service';
import { TimeAgoPipe } from '../../../core/pipes/time-ago-pipe';

@Component({
  selector: 'app-member-profile',
  imports: [DatePipe, FormsModule,TimeAgoPipe],
  templateUrl: './member-profile.html',
  styleUrl: './member-profile.css'
})
export class MemberProfile implements OnInit, OnDestroy {

  constructor() {

  }

  @ViewChild('editForm') editForm?: NgForm;
  protected router = inject(Router);

  protected memberService = inject(MemberService);
  protected editableMember: EditableMember = {
    displayName: '',
    description: '',
    city: '',
    country: ''
  };
  private toast = inject(ToastService);
  private accountService = inject(AccountService);
  @HostListener('window:beforeunload', ['$event'])
  notify($event: BeforeUnloadEvent) {
    if (this.editForm?.dirty) {
      $event.preventDefault();
    }
  }



  ngOnInit(): void {

    this.editableMember = {
      displayName: this.memberService.member()?.displayName || '',
      description: this.memberService.member()?.description || '',
      city: this.memberService.member()?.city || '',
      country: this.memberService.member()?.country || ''
    }
  }
  updateProfile() {

    if (!this.memberService.member()) return;
    const updatedMember = {
      ...this.memberService.member(),
      ...this.editableMember
    }

    this.memberService.updateMember(updatedMember).subscribe({
      next: () => {
        const currentUser = this.accountService.currentUser();
        if (currentUser && currentUser.id === updatedMember.id) {
          currentUser.displayName = updatedMember.displayName;
          this.accountService.setCurrentUser(currentUser);
        }
        this.toast.success('Profile updated successfully');
        this.memberService.editMode.set(false);
        this.memberService.member.set(updatedMember as Member);
        this.editForm?.reset(updatedMember);

      },
      error: error => {
        this.toast.error(error.error);
      }
    })
    this.toast.success('Profile updated successfully');
    this.memberService.editMode.set(false);
  }
  ngOnDestroy(): void {
    if (this.memberService.editMode())
      this.memberService.editMode.set(false);
  }

}

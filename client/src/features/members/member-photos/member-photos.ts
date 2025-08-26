import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Photo } from '../../../types/member';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-member-photos',
  imports: [AsyncPipe],
  templateUrl: './member-photos.html',
  styleUrl: './member-photos.css'
})
export class MemberPhotos {

  private memberService = inject(MemberService);
  private route = inject(ActivatedRoute);
  protected photos$?: Observable<Photo[]>;
  constructor() {
    this.photos$ = this.loadPhotos();
  }

  loadPhotos() {
    const id = this.route.parent?.snapshot.paramMap.get('id');
    if (!id) return;
    return this.memberService.getMemberPhotos(id);
  }
  get photoMocks() {
    return Array.from({ length: 20 }, (_, i) => ({
      url: '/user.png'
    }))
  }

}

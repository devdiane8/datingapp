import { Component, inject, OnInit, signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Member, Photo } from '../../../types/member';
import { ImageUpload } from '../../../shared/image-upload/image-upload';
import { AccountService } from '../../../core/services/account-service';
import { User } from '../../../types/user';
import { StarButton } from '../../../shared/star-button/star-button';
import { DeleteButton } from '../../../shared/delete-button/delete-button';

@Component({
  selector: 'app-member-photos',
  imports: [ImageUpload, StarButton, DeleteButton],
  templateUrl: './member-photos.html',
  styleUrl: './member-photos.css'
})
export class MemberPhotos implements OnInit {

  protected memberService = inject(MemberService);
  protected accountService = inject(AccountService);
  private route = inject(ActivatedRoute);
  protected photos = signal<Photo[]>([]);
  protected loading = signal<boolean>(false);


  ngOnInit(): void {
    this.loadPhotos();
  }

  loadPhotos() {
    const id = this.route.parent?.snapshot.paramMap.get('id');
    if (!id) return;
    return this.memberService.getMemberPhotos(id).subscribe({
      next: photos => {
        this.photos.set(photos);
      }
    })
  }
  onUploadImage(file: File) {
    this.loading.set(true);
    this.memberService.uploadPhoto(file).subscribe({
      next: photo => {
        this.memberService.editMode.set(false);
        this.loading.set(false);
        this.photos.update(photos => [...photos, photo]);
        if (!this.memberService.member()?.imageUrl) {
          this.setMainLocalPhoto(photo);
        }

      },
      error: error => {
        console.log("error on photot upload");
        this.loading.set(false);
      }
    })
  }
  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo).subscribe({
      next: () => {
        const currentUser = this.accountService.currentUser();
        if (currentUser && currentUser.id === this.memberService.member()?.id) {
          currentUser.imageUrl = photo.url;
          this.accountService.setCurrentUser(currentUser as User);
        }
        this.memberService.member.update(member => ({
          ...member,
          imageUrl: photo.url

        }) as Member);
      },
      error: error => {
        console.log("error on photot upload");
        this.loading.set(false);
      }
    })
  }
  private setMainLocalPhoto(photo: Photo) {
    const currentUser = this.accountService.currentUser();
    if (currentUser && currentUser.id === this.memberService.member()?.id) {
      currentUser.imageUrl = photo.url;
      this.accountService.setCurrentUser(currentUser as User);
    }
    this.memberService.member.update(member => ({
      ...member,
      imageUrl: photo.url

    }) as Member);
  }
  deletePhoto(photoId: number) {
    const photo = this.photos().find(x => x.id === photoId);
    if (!photo) return;
    this.memberService.deletePhoto(photo.id).subscribe({
      next: () => {
        this.memberService.editMode.set(false);
        this.photos.update(photos => [...photos.filter(x => x.id !== photo.id)]);
      },
      error: error => {
        console.log("error on photot upload");
        this.loading.set(false);
      }
    })
  }
}

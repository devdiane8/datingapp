export type Member = {
  id: string;
  dateOfBirth: string;
  displayName: string;
  gender: string;
  city: string;
  country: string;
  description: string;
  imageUrl?: string;
  created: string;
  lastActive: string;
}

export type Photo = {
  id: number;
  url: string;
  publicId?: string;
  memberId: string;
}

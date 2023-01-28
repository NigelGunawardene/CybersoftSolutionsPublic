export class RefreshTokenModel {
  jwtToken: string;
  refreshToken: string;

  constructor(jwtToken: string, refreshToken: string) {
    this.jwtToken = jwtToken;
    this.refreshToken = refreshToken;
  }
}

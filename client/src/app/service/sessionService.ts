export class SessionService {
    
    getUserFromSession() {
      const user = sessionStorage.getItem('currentUser');
      if (user) {
        return JSON.parse(user);
      }
      return null;
    }
  }
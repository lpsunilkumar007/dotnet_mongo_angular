export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  mobileNumber: string;
  remainingDays: number;
  isRequestedToDelete:boolean;
  isDeleted:boolean;
}

export interface UserCompound { 
  firstName: string;
  lastName: string;  
  email: string;
  mobileNumber: string;
}
export interface UserCompoundRequest { 
  firstName: string;
  lastName: string; 
}

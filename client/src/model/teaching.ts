export interface SimpleUser {
    id: string;
    username: string;
    firstName: string;
    lastName: string;
}

export interface Teaching {
    id: string;
    professor: SimpleUser;
    examDate: number;
    subject: Subject;
}

export interface Subject {
    id: string;
    subject: string;
    description: string;
}

export interface Enrollment {
    id: string;
    student: SimpleUser;
    teaching: Teaching;
    grade: number;
    review: string;
}
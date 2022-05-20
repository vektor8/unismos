import { PayloadAction } from "@reduxjs/toolkit";
import Enrollments from "../../features/student-dashboard/components/Enrollments";
import { Enrollment, Teaching } from "../../model/teaching";
import { ProfessorState } from "./state";

export const refreshTeachingsCaseReducer = (
    state: ProfessorState, action: PayloadAction<Teaching[]>
) => {
    state.teaching = action.payload;
};


export const addEnrollmentsToTeachingsCaseReducer = (
    state: ProfessorState, action: PayloadAction<{ id: string, enrollments: Enrollment[] }>
) => {
    state.enrollments[action.payload.id] = action.payload.enrollments;
};

export const updateEnrollmentCaseReducer = (
    state: ProfessorState, action: PayloadAction<{ id: string, enrollmentId: string, enrollment: Enrollment }>
) => {
    const enrollments = state.enrollments[action.payload.id];
    if (enrollments) {
        const enrollment = enrollments.find(e => e.id === action.payload.enrollmentId);
        if (enrollment) {
            enrollment.grade = action.payload.enrollment.grade;
            enrollment.review = action.payload.enrollment.review;
        }
    }
}
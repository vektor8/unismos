import { PayloadAction } from "@reduxjs/toolkit";
import { Enrollment, Teaching } from "../../model/teaching";
import { StudentState } from "./state";


export const refreshEnrollmentsCaseReducer = (
        state: StudentState, action: PayloadAction<Enrollment[]>
    ) => {
        state.enrollments = action.payload;
    };
    

export const addEnrollmentCaseReducer = (
    state: StudentState, action: PayloadAction<Enrollment>
) => {
    state.enrollments.push(action.payload);
};
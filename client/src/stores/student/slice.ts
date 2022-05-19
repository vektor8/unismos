import { createSlice } from "@reduxjs/toolkit";
import Enrollments from "../../features/student-dashboard/components/Enrollments";
import { addEnrollmentCaseReducer, refreshEnrollmentsCaseReducer } from "./reducers";
import { studentInitialState } from "./state";

const StudentReducerSlice = createSlice({
  name: 'student',
  initialState: studentInitialState,
  reducers: {
    refreshEnrollments: refreshEnrollmentsCaseReducer,
    enroll: addEnrollmentCaseReducer
  },
});

export const { refreshEnrollments, enroll } = StudentReducerSlice.actions;
export const StudentReducer = StudentReducerSlice.reducer;
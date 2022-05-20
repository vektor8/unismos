import { createSlice } from "@reduxjs/toolkit";
import { addEnrollmentsToTeachingsCaseReducer, refreshTeachingsCaseReducer, updateEnrollmentCaseReducer } from "./reducers";
import { ProfessorInitialState } from "./state";

const ProfessorReducerSlice = createSlice({
  name: 'professor',
  initialState: ProfessorInitialState,
  reducers: {
    refreshTeachings: refreshTeachingsCaseReducer,
    addEnrollmentsToTeachings: addEnrollmentsToTeachingsCaseReducer,
    updateEnrollment : updateEnrollmentCaseReducer
  },
});

export const { refreshTeachings, addEnrollmentsToTeachings, updateEnrollment } = ProfessorReducerSlice.actions;
export const ProfessorReducer = ProfessorReducerSlice.reducer;
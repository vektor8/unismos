import { createSlice } from "@reduxjs/toolkit";
import { addEnrollmentsToTeachingsCaseReducer, refreshTeachingsCaseReducer, updateEnrollmentGradeCaseReducer } from "./reducers";
import { ProfessorInitialState } from "./state";

const ProfessorReducerSlice = createSlice({
  name: 'professor',
  initialState: ProfessorInitialState,
  reducers: {
    refreshTeachings: refreshTeachingsCaseReducer,
    addEnrollmentsToTeachings: addEnrollmentsToTeachingsCaseReducer,
    updateEnrollmentGrade : updateEnrollmentGradeCaseReducer
  },
});

export const { refreshTeachings, addEnrollmentsToTeachings, updateEnrollmentGrade } = ProfessorReducerSlice.actions;
export const ProfessorReducer = ProfessorReducerSlice.reducer;
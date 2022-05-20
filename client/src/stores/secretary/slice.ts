import { createSlice } from "@reduxjs/toolkit";
import { addNewProfessorCaseReducer, addNewSubjectCaseReducer, addTeachingCaseReducer, refreshProfessorsCaseReducer, refreshSubjectsCaseReducer, refreshTeachingsCaseReducer, updateTeachingDateCaseReducer } from "./reducers";
import { SecretaryInitialState } from "./state";

const SecretaryReducerSlice = createSlice({
  name: 'professor',
  initialState: SecretaryInitialState,
  reducers: {
    refreshTeachings: refreshTeachingsCaseReducer,
    updateTeachingDate: updateTeachingDateCaseReducer,
    addNewTeaching: addTeachingCaseReducer,
    addNewProfessor: addNewProfessorCaseReducer,
    addNewSubject: addNewSubjectCaseReducer,
    refreshProfessors: refreshProfessorsCaseReducer,
    refreshSubjects: refreshSubjectsCaseReducer,
  },
});

export const { refreshTeachings, updateTeachingDate, addNewTeaching, addNewProfessor, addNewSubject, refreshProfessors, refreshSubjects } = SecretaryReducerSlice.actions;
export const SecretaryReducer = SecretaryReducerSlice.reducer;
import { createSlice } from "@reduxjs/toolkit";
import { addTeachingCaseReducer, refreshTeachingsCaseReducer, updateTeachingDateCaseReducer } from "./reducers";
import { SecretaryInitialState } from "./state";

const SecretaryReducerSlice = createSlice({
  name: 'professor',
  initialState: SecretaryInitialState,
  reducers: {
    refreshTeachings: refreshTeachingsCaseReducer,
    updateTeachingDate: updateTeachingDateCaseReducer,
    addNewTeaching: addTeachingCaseReducer,
  },
});

export const { refreshTeachings, updateTeachingDate, addNewTeaching } = SecretaryReducerSlice.actions;
export const SecretaryReducer = SecretaryReducerSlice.reducer;
import { PayloadAction } from "@reduxjs/toolkit";
import { SimpleUser, Subject, Teaching } from "../../model/teaching";
import { SecretaryState } from "./state";

export const refreshTeachingsCaseReducer = (
    state: SecretaryState, action: PayloadAction<Teaching[]>
) => {
    state.teaching = action.payload;
};

export const updateTeachingDateCaseReducer = (
    state: SecretaryState, action: PayloadAction<{ id: string, date: number }>
) => {
    const teaching = state.teaching.find(t => t.id === action.payload.id);
    if (teaching)
        teaching.examDate = action.payload.date;
}

export const addTeachingCaseReducer = (
    state: SecretaryState, action: PayloadAction<Teaching>
) => {
    state.teaching.push(action.payload);
};

export const addNewProfessorCaseReducer = (
    state: SecretaryState, action: PayloadAction<SimpleUser>
) => {
    state.professors.push(action.payload);
}

export const addNewSubjectCaseReducer = (
    state: SecretaryState, action: PayloadAction<Subject>
) => {
    state.subjects.push(action.payload);
}

export const refreshProfessorsCaseReducer = (
    state: SecretaryState, action: PayloadAction<SimpleUser[]>
) => {
    state.professors = action.payload;
}

export const refreshSubjectsCaseReducer = (
    state: SecretaryState, action: PayloadAction<Subject[]>
) => {
    state.subjects = action.payload;
}



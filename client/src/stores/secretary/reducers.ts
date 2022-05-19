import { PayloadAction } from "@reduxjs/toolkit";
import { Teaching } from "../../model/teaching";
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
    console.log("plm,", teaching);
}

export const addTeachingCaseReducer = (
    state: SecretaryState, action: PayloadAction<Teaching>
) => {
    state.teaching.push(action.payload);
};

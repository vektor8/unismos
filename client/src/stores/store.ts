import { combineReducers, configureStore, Reducer } from "@reduxjs/toolkit";
import thunk from "redux-thunk";
import { UserReducer } from "./user/slice";
import { UserState } from "./user/state";
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { StudentReducer } from "./student/slice";
import { StudentState } from "./student/state";
import { ProfessorState } from "./professor/state";
import { ProfessorReducer } from "./professor/slice";
import { SecretaryState } from "./secretary/state";
import { SecretaryReducer } from "./secretary/slice";

export interface RootState {
  user: UserState;
  student: StudentState;
  professor: ProfessorState;
  secretary: SecretaryState
}

const userPersistConfig = {
  key: 'user',
  storage,
};

const studentPersistConfig = {
  key: 'student',
  storage,
};

const professorPersistConfig = {
  key: 'professor',
  storage
};

const secretaryPersistConfig = {
  key: 'secretary',
  storage
};

const store = configureStore({
  reducer: combineReducers<RootState>({
    user: persistReducer(userPersistConfig, UserReducer as Reducer),
    student: persistReducer(studentPersistConfig, StudentReducer as Reducer),
    professor: persistReducer(professorPersistConfig, ProfessorReducer as Reducer),
    secretary: persistReducer(secretaryPersistConfig, SecretaryReducer as Reducer),
  }),
  middleware: [thunk],
});

const persistor = persistStore(store);

export { store, persistor };

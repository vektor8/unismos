import { combineReducers, configureStore, Reducer } from "@reduxjs/toolkit";
import thunk from "redux-thunk";
import { UserReducer } from "./user/slice";
import { UserState } from "./user/state";
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { StudentReducer } from "./student/slice";
import { StudentState } from "./student/state";

export interface RootState {
  user: UserState;
  student: StudentState;
}

const userPersistConfig = {
  key: 'user',
  storage,
};

const studentPersistConfig = {
  key: 'student',
  storage,
};

const store = configureStore({
  reducer: combineReducers<RootState>({
    user: persistReducer(userPersistConfig, UserReducer as Reducer),
    student: persistReducer(studentPersistConfig, StudentReducer as Reducer),
  }),
  middleware: [thunk],
});

const persistor = persistStore(store);

export { store, persistor };

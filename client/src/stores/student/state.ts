import { Enrollment} from "../../model/teaching";

export interface StudentState {
    enrollments: Enrollment[]
}

export const studentInitialState: StudentState = {
    enrollments: []
};
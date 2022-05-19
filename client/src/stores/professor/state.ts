import { Enrollment, Teaching} from "../../model/teaching";

export interface ProfessorState {
    enrollments: Record<string, Enrollment[]>
    teaching: Teaching[]
}

export const ProfessorInitialState: ProfessorState = {
    enrollments: {},
    teaching: []
};
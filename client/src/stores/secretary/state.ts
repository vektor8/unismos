import { SimpleUser, Subject, Teaching} from "../../model/teaching";

export interface SecretaryState {
    teaching: Teaching[],
    professors: SimpleUser[],
    subjects: Subject[],
}

export const SecretaryInitialState: SecretaryState = {
    teaching: [],
    professors: [],
    subjects: [],
};
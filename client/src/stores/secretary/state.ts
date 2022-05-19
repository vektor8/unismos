import { Teaching} from "../../model/teaching";

export interface SecretaryState {
    teaching: Teaching[]
}

export const SecretaryInitialState: SecretaryState = {
    teaching: []
};
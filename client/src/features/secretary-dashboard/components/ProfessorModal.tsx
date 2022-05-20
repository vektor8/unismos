import {
    Alert,
    Box,
    Button,
    Grid,
    Modal,
    Snackbar,
    TextField,
    Typography,
} from "@mui/material";
import React from "react";
import { useDispatch } from "react-redux";
import { Axios } from "../../../api/api";
import { addNewProfessor } from "../../../stores/secretary/slice";

type Props = {
    isOpen: boolean;
    onClose: () => void;
};


const ProfessorModal = (props: Props) => {
    const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
    const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);
    const dispatch = useDispatch();
    const submitProfessor = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        Axios.post("/professors", {
            firstName: data.get('firstName'),
            lastName: data.get('lastName'),
            username: data.get('username'),
            password: data.get('password')
        }).then((response) => {
            dispatch(addNewProfessor(response.data));
            setOpenSnackBarSuccess(true);
            props.onClose();
        }).catch(() => {
            setOpenSnackBarFail(true);
        })
    };

    return (
        <>
            <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
                <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
                    Created new professor
                </Alert>
            </Snackbar>
            <Snackbar open={openSnackBarFail} autoHideDuration={5000} onClose={() => setOpenSnackBarFail(false)}>
                <Alert onClose={() => setOpenSnackBarFail(false)} severity="error" sx={{ width: '100%' }}>
                    Invalid data
                </Alert>
            </Snackbar>
            <Modal
                style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                }}
                open={props.isOpen}
                onClose={props.onClose}
            >
                <Box component="form" noValidate onSubmit={submitProfessor} sx={{ mt: 3 }}>
                    <Typography sx={{ marginBottom: 2 }} variant="h5" component="h2" gutterBottom>
                        Add a new professor
                    </Typography>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                autoComplete="given-name"
                                name="firstName"
                                required
                                fullWidth
                                id="firstName"
                                label="First Name"
                                autoFocus
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                required
                                fullWidth
                                id="lastName"
                                label="Last Name"
                                name="lastName"
                                autoComplete="family-name"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                id="username"
                                label="Username"
                                name="username"
                                autoComplete="username"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                            />
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Submit
                    </Button>
                </Box>
            </Modal >
        </>
    );
};

export default ProfessorModal;
import {
    Alert,
    Box,
    Button,
    createTheme,
    FormControl,
    InputLabel,
    MenuItem,
    Modal,
    Select,
    Snackbar,
    TextField,
    ThemeProvider,
    Typography,
} from "@mui/material";
import React from "react";
import { Axios } from "../../../api/api";

type Props = {
    isOpen: boolean;
    onClose: () => void;
};


const SubjectModal = (props: Props) => {
    const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
    const submitSubject = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        Axios.post("/subjects", {
            name: data.get('name'),
            description: data.get('description')
        }).then((response) => {
            console.log(response.data);
            props.onClose();
        }).catch(() => {
        })
    };

    return (
        <>
            <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
                <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
                    Created new subject
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
                <Box component="form" noValidate onSubmit={submitSubject}
                    sx={{
                        maxWidth: 500,
                        backgroundColor: "grey",
                        borderRadius: 2 / 1,
                        padding: 4,
                    }}
                >
                    <Typography sx={{ marginBottom: 2 }} variant="h5" component="h2">
                        Add a new subject
                    </Typography>
                    <TextField sx={{ marginBottom: 2 }}
                        margin="normal"
                        required
                        fullWidth
                        id="name"
                        label="Subject"
                        name="name"
                        autoFocus />
                    <TextField
                        sx={{ marginBottom: 2 }}
                        margin="normal"
                        required
                        fullWidth
                        id="description"
                        label="Description"
                        name="description"
                        autoComplete="description"
                        autoFocus
                    />
                    <br></br>
                    <Button
                        sx={{ marginTop: 2, textTransform: "none" }}
                        variant="contained"
                        type="submit"
                    >
                        Submit
                    </Button>
                </Box>
            </Modal >
        </>
    );
};

export default SubjectModal;
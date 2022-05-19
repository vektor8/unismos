import {
    Alert,
    Box,
    Button,
    MenuItem,
    Modal,
    Select,
    Snackbar,
    Typography,
} from "@mui/material";
import React from "react";
import { useDispatch } from "react-redux";
import { Axios } from "../../../api/api";
import { SimpleUser, Subject } from "../../../model/teaching";
import { addNewTeaching } from "../../../stores/secretary/slice";
import Title from "./Title";

type Props = {
    isOpen: boolean;
    onClose: () => void;
};

const TeachingModal = (props: Props) => {
    const [professorId, setProfessorId] = React.useState("");
    const [subjectId, setSubjectId] = React.useState("");
    const [professors, setProfessors] = React.useState<SimpleUser[]>([]);
    const [subjects, setSubjects] = React.useState<Subject[]>([]);
    const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
    const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);
    const dispatch = useDispatch();

    React.useEffect(() => {
        Axios.get("/professors").then(res => {
            setProfessors(res.data);
            setProfessorId(professors[0].id);
        });
        Axios.get("/subjects").then(res => {
            setSubjects(res.data);
            setSubjectId(subjects[0].id);
        });
    }, []);


    const handleStatusProf = (e: any) => {
        setProfessorId(e.target.value);
    };

    const handleStatusSub = (e: any) => {
        setSubjectId(e.target.value);
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        Axios.post("/teachings", {
            professorId: professorId,
            subjectId: subjectId,
        }).then((response) => {
            dispatch(addNewTeaching(response.data));
            props.onClose();
            setOpenSnackBarSuccess(true);
        }).catch(() => setOpenSnackBarFail(true))
    };

    return (
        <>
            <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
                <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
                    Created new class
                </Alert>
            </Snackbar>
            <Snackbar open={openSnackBarFail} autoHideDuration={5000} onClose={() => setOpenSnackBarFail(false)}>
                <Alert onClose={() => setOpenSnackBarFail(false)} severity="error" sx={{ width: '100%' }}>
                    This class already exists
                </Alert>
            </Snackbar>
            <Modal
                style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    backdropFilter: "blur(5px)",
                }}
                open={props.isOpen}
                onClose={props.onClose}
            >
                <Box component="form" noValidate onSubmit={handleSubmit}
                    sx={{
                        maxWidth: 500,
                        backgroundColor: "grey",
                        borderRadius: 2 / 1,
                        padding: 4,
                    }}
                >
                    <Title>Create a new class</Title>
                    <Typography component="h2" variant="h6" color="primary" gutterBottom>
                        Pick a professor
                    </Typography>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={professorId}
                        label="Status"
                        onChange={handleStatusProf}
                    >
                        {professors.map((e) => <MenuItem value={e.id}>{e.firstName + " " + e.lastName}</MenuItem>)}
                    </Select>
                    <Typography component="h2" variant="h6" color="primary" gutterBottom>
                        Pick a subject
                    </Typography>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={subjectId}
                        label="Status"
                        onChange={handleStatusSub}
                    >
                        {subjects.map((e) => <MenuItem value={e.id}>{e.subject + " - " + e.description}</MenuItem>)}
                    </Select>
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

export default TeachingModal;
import {
    Alert,
    Box,
    Button,
    MenuItem,
    Modal,
    Select,
    Snackbar,
} from "@mui/material";
import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Axios } from "../../../api/api";
import { Teaching } from "../../../model/teaching";
import { User } from "../../../model/user";
import { RootState } from "../../../stores/store";
import { enroll } from "../../../stores/student/slice";
import Title from "./Title";

type Props = {
    isOpen: boolean;
    onClose: () => void;
};

const EnrollmentModal = (props: Props) => {
    const [teachings, setTeachings]: [Teaching[], any] = React.useState([]);
    const [teachingId, setTeachingId] = React.useState("");
    const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
    const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);
    const user = useSelector<RootState>(state => state.user.userData) as User;
    const dispatch = useDispatch();

    const handleStatus = (e: any) => {
        setTeachingId(e.target.value);
        console.log(teachingId);
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        Axios.post("/enrollments", {
            studentId: user.id,
            teachingId: teachingId,
        }).then((response) => {
            props.onClose();
            dispatch(enroll(response.data));
            setOpenSnackBarSuccess(true);
        }).catch(() => setOpenSnackBarFail(true))
    };


    React.useEffect(() => {
        Axios.get("/teachings").then(res => {
            setTeachings(res.data)
            setTeachingId(teachings[0].id);
        });
    }, []);

    return (
        <>
            <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
                <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
                    Enrolled successfully into {teachings.find(t => t.id === teachingId)?.subject.subject}
                </Alert>
            </Snackbar>
            <Snackbar open={openSnackBarFail} autoHideDuration={5000} onClose={() => setOpenSnackBarFail(false)}>
                <Alert onClose={() => setOpenSnackBarFail(false)} severity="error" sx={{ width: '100%' }}>
                    You are already enrolled into this class
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
                    <Title>Enroll into a new class</Title>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={teachingId}
                        label="Status"
                        onChange={handleStatus}
                    >
                        {teachings.map((e) => <MenuItem value={e.id}>{e.subject.subject + " - " + e.professor.firstName + " " + e.professor.lastName}</MenuItem>)}
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

export default EnrollmentModal;
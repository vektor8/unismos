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
    review: string;
};

const ReviewModal = (props: Props) => {
    return (<>
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
            <Box
                sx={{
                    maxWidth: 500,
                    backgroundColor: "grey",
                    borderRadius: 2 / 1,
                    padding: 4,
                }}
            >
                {props.review}
            </Box>
        </Modal >
    </>
    );
};

export default ReviewModal;
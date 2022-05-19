import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from './Title';
import { Enrollment } from '../../../model/teaching';
import { Alert, Box, Button, Collapse, IconButton, Snackbar, TextField } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Axios } from '../../../api/api';

function Row(props: { row: Enrollment }) {
  const { row } = props;
  const [submitted, setSubmitted] = React.useState(row.review !== '' || row.grade > 0);
  const [rowData, setRowData] = React.useState(row);
  const [open, setOpen] = React.useState(false);
  const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
  const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);

  const submitReview = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log(data.get('review')?.toString());
    Axios.put(`enrollments/review/${row.id}`, {
      review: data.get('review')
    }).then((resp) => {
      setSubmitted(true);
      setRowData(resp.data);
      setOpenSnackBarSuccess(true);
    }).catch(() => setOpenSnackBarFail(true));
  };
  return (
    <React.Fragment>
      <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
        <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
          Review added successfully
        </Alert>
      </Snackbar>
      <Snackbar open={openSnackBarFail} autoHideDuration={5000} onClose={() => setOpenSnackBarFail(false)}>
        <Alert onClose={() => setOpenSnackBarFail(false)} severity="error" sx={{ width: '100%' }}>
          Review failed to add as it cannot be empty
        </Alert>
      </Snackbar>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell>{rowData.teaching.subject.subject}</TableCell>
        <TableCell>{rowData.teaching.professor.firstName + " " + rowData.teaching.professor.lastName}</TableCell>
        <TableCell>{new Date(rowData.teaching.examDate * 1000).toUTCString()}</TableCell>
        <TableCell>{rowData.grade > 0 ? rowData.grade : "Not graded"}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1, display: 'flex', flexDirection: 'column', alignItems: 'center' }}
              component="form" noValidate onSubmit={submitReview}>
              <Title>Your thoughts on this class</Title>
              <TextField
                id="standard-multiline-static"
                name='review'
                label="Review"
                multiline
                rows={4}
                disabled={submitted}
                defaultValue={row.review === ""
                  ? row.grade > 0 ? "You can't review after receiving the grade" : "Your thoughts"
                  : row.review
                }
                variant="standard"
                sx={{ width: "80%" }}
              />
              {!submitted &&
                <Button
                  sx={{ marginTop: 2, textTransform: "none", width: "25%" }}
                  variant="contained"
                  type="submit"
                >
                  Review
                </Button>}
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}


export default function Enrollments(props: { enrollments: Enrollment[] }) {
  const { enrollments } = props;
  return (
    <React.Fragment>
      <Title>All classes</Title>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell padding="checkbox" />
            <TableCell>Subject</TableCell>
            <TableCell>Professor</TableCell>
            <TableCell>Exam date</TableCell>
            <TableCell>Your grade</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {enrollments.map(row => <Row key={row.id} row={row} />)}
        </TableBody>
      </Table>
    </React.Fragment>
  );
}

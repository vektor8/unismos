import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from './Title';
import { Enrollment, Teaching } from '../../../model/teaching';
import { Alert, Box, Button, Collapse, IconButton, Snackbar, TextField } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Axios } from '../../../api/api';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../../stores/store';
import { addEnrollmentsToTeachings, updateEnrollmentGrade } from '../../../stores/professor/slice';
import SaveIcon from '@mui/icons-material/Save';
import ReviewModal from './ReviewModal';

function Student(props: { enrollment: Enrollment }) {
  const { enrollment } = props;
  const [grade, setGrade] = React.useState(enrollment.grade);
  const [openReviewModal, setOpenReviewModal] = React.useState(false);
  const dispatch = useDispatch();

  const submitGrade = () => {
    Axios.put(`enrollments/grade/${enrollment.id}`, { grade: grade }).then((resp) => {
      const parsedResponse = resp.data as Enrollment;
      dispatch(updateEnrollmentGrade({ id: parsedResponse.teaching.id, enrollmentId: parsedResponse.id, grade: parsedResponse.grade }))
    });
  }

  return (<>
    <ReviewModal
      isOpen={openReviewModal}
      onClose={() => setOpenReviewModal(false)}
      review={enrollment.review}
    // onSubmit={submitRestaurant}
    />
    <TableRow key={enrollment.id}>
      <TableCell>{enrollment.student.firstName + " " + enrollment.student.lastName}</TableCell>
      <TableCell>{enrollment.grade == 0 ?
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <TextField sx={{ marginBottom: 2, width: '10%' }}
            margin="normal"
            required
            id="grade"
            name="grade"
            InputProps={{
              inputProps: {
                max: 10, min: 1, step: 1
              }
            }}
            value={grade}
            onChange={(e) => setGrade(+e.target.value)}
            autoFocus />
          <IconButton type="submit">
            <SaveIcon onClick={submitGrade} />
          </IconButton>
        </Box>
        :
        <>
          {enrollment.grade}
        </>}
      </TableCell>
      <TableCell>
        {enrollment.review !== '' ?
          <Button variant="contained" sx={{ marginTop: 2, textTransform: "none" }}
            onClick={() => setOpenReviewModal(true)}>See review</Button>
          : "No review"}
      </TableCell>
    </TableRow>
  </>)
}


function Row(props: { row: Teaching }) {
  const { row } = props;
  const dispatch = useDispatch();
  const [rowData, setRowData] = React.useState(row);
  const [open, setOpen] = React.useState(false);
  const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
  const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);
  const enrollments = useSelector((state: RootState) => state.professor.enrollments[row.id]);

  React.useEffect(() => {
    Axios.get(`/enrollments/teaching/${row.id}`).then((resp) => {
      dispatch(addEnrollmentsToTeachings({ id: row.id, enrollments: resp.data }));
    })
  }, []);


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
        <TableCell>{rowData.subject.subject}</TableCell>
        <TableCell>{new Date(rowData.examDate * 1000).toUTCString()}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
              <Title>Students</Title>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Student</TableCell>
                    <TableCell>Grade</TableCell>
                    <TableCell>Review</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {enrollments !== undefined && enrollments.map((enrollment: Enrollment) => <Student enrollment={enrollment} />)}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}


export default function Teachings(props: { teachings: Teaching[] }) {
  const { teachings } = props;
  return (
    <React.Fragment>
      <Title>All classes</Title>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell padding="checkbox" />
            <TableCell>Subject</TableCell>
            <TableCell>Exam date</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {teachings.map(row => <Row key={row.id} row={row} />)}
        </TableBody>
      </Table>
    </React.Fragment>
  );
}

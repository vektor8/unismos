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
import SaveIcon from '@mui/icons-material/Save';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { useDispatch } from 'react-redux';
import { updateTeachingDate } from '../../../stores/secretary/slice';


function Row(props: { row: Teaching }) {
  const { row } = props;
  const [value, setValue] = React.useState(row.examDate === 0 ? new Date() : new Date(row.examDate));
  const dispatch = useDispatch();
  const [openSnackBarSuccess, setOpenSnackBarSuccess] = React.useState(false);
  const [openSnackBarFail, setOpenSnackBarFail] = React.useState(false);

  const submitExamDate = () => {
    Axios.put(`/teachings/${row.id}`, { examDate: value.getTime() / 1000 }).then((resp) => {
      const parsedResponse = resp.data as Teaching;
      dispatch(updateTeachingDate({ id: parsedResponse.id, date: parsedResponse.examDate }));
      setOpenSnackBarSuccess(true);
    }).catch(err => setOpenSnackBarFail(true));
  }
  return (
    <React.Fragment>
      <Snackbar open={openSnackBarSuccess} autoHideDuration={5000} onClose={() => setOpenSnackBarSuccess(false)}>
        <Alert onClose={() => setOpenSnackBarSuccess(false)} severity="success" sx={{ width: '100%' }}>
          Scheduled exam
        </Alert>
      </Snackbar>
      <Snackbar open={openSnackBarFail} autoHideDuration={5000} onClose={() => setOpenSnackBarFail(false)}>
        <Alert onClose={() => setOpenSnackBarFail(false)} severity="error" sx={{ width: '100%' }}>
          Cannot schedule exam in the past
        </Alert>
      </Snackbar>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>{row.subject.subject}</TableCell>
        <TableCell>{row.professor.firstName + " " + row.professor.lastName}</TableCell>
        <TableCell>{row.examDate == 0 ?
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
              <DatePicker
                value={value}
                onChange={(newValue) => {
                  console.log(newValue);
                  if (newValue)
                    setValue(newValue);
                }}
                renderInput={(params) => <TextField {...params} />}
              />
            </LocalizationProvider>
            <IconButton type="submit">
              <SaveIcon onClick={submitExamDate} />
            </IconButton>
          </Box>
          : new Date(row.examDate * 1000).toUTCString()}</TableCell>
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
            <TableCell>Subject</TableCell>
            <TableCell>Professor</TableCell>
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

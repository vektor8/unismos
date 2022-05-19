import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from './Title';
import { Teaching } from "../../../model/teaching";
import { Typography } from '@mui/material';

export default function ExamSchedule(props: { teachings: Teaching[] }) {
  const { teachings } = props;
  return (
    <React.Fragment>
      <Title>Upcoming exams</Title>
      {teachings.length !== 0 ?
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox" />
              <TableCell>Subject</TableCell>
              <TableCell>Professor</TableCell>
              <TableCell>Exam date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {teachings.map((teaching, index) => (
              <TableRow key={index}>
                <TableCell padding="checkbox" />
                <TableCell>{teaching.subject.subject}</TableCell>
                <TableCell>{teaching.professor.firstName + " " + teaching.professor.lastName}</TableCell>
                <TableCell>{new Date(teaching.examDate * 1000).toUTCString()}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
        :
        <Typography component="h2" variant="h6" color="primary" gutterBottom>
          Lucky you, no upcoming exams!
        </Typography>
      }
    </React.Fragment >
  );
}
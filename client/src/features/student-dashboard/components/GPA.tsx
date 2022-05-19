import * as React from 'react';
import Typography from '@mui/material/Typography';
import Title from './Title';

export default function GPA(props: { gpa: number }) {
  return (
    <React.Fragment>
      <Title>GPA</Title>
      <Typography component="p" variant="h4">
        {props.gpa === 0 ? 'No grades' : props.gpa}
      </Typography>
      <Typography color="text.secondary" sx={{ flex: 1 }}>
        on {new Date().toDateString()}
      </Typography>
    </React.Fragment>
  );
}

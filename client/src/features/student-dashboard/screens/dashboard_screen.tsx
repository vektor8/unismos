import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Link from '@mui/material/Link';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../../stores/store';
import { User } from '../../../model/user';
import { logout } from '../../../stores/user/slice';
import LogoutIcon from '@mui/icons-material/Logout';
import { Axios } from '../../../api/api';
import { Enrollment } from '../../../model/teaching';
import Enrollments from '../components/Enrollments';
import GPA from '../components/GPA';
import ExamSchedule from '../components/Schedule';
import { Alert, ListItemButton, ListItemIcon, ListItemText, Snackbar } from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import EnrollmentModal from '../components/EnrollmentModal';
import { refreshEnrollments } from '../../../stores/student/slice';

function Copyright(props: any) {
  return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

const drawerWidth: number = 240;

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9),
        },
      }),
    },
  }),
);

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#c30101',
    },
    secondary: {
      main: '#c30101',
    },
  },
});

function DashboardContent() {
  const [openModal, setOpenModal] = React.useState(false);
  const user: User = useSelector<RootState>(state => state.user.userData) as User;
  const enrollments: Enrollment[] = useSelector<RootState>(state => state.student.enrollments) as Enrollment[];
  const gradedEnrollments = enrollments.filter(enrollment => enrollment.grade > 0);
  const upcomingExams = enrollments.map(e => e.teaching).filter(e => e.examDate * 1000 > Date.now());
  const dispatch = useDispatch();

  React.useEffect(() => {
    Axios.get(`/enrollments/student/${user.id}`).then(res => {
      dispatch(refreshEnrollments(res.data));
      console.log(res.data)
      console.log(enrollments)
    });
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <EnrollmentModal
        isOpen={openModal}
        onClose={() => setOpenModal(false)}
      />
      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        <AppBar>
          <Toolbar
          >
            <Typography
              component="h1"
              variant="h6"
              color="inherit"
              noWrap
              sx={{ flexGrow: 1 }}
            >
              Hello {user.firstName + " " + user.lastName}
            </Typography>
            <IconButton onClick={() => dispatch(logout())}>
              <LogoutIcon />
            </IconButton>
          </Toolbar>
        </AppBar>
        <Drawer variant="permanent" open={true}>
          <Toolbar
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'flex-end',
              px: [1],
            }}
          >
          </Toolbar>
          <Divider />
          <List component="nav">
            <ListItemButton onClick={() => setOpenModal(true)}>
              <ListItemIcon>
                <DashboardIcon />
              </ListItemIcon>
              <ListItemText primary="Enroll into class" />
            </ListItemButton>
            <Divider sx={{ my: 1 }} />
          </List>
        </Drawer>
        <Box
          component="main"
          sx={{
            backgroundColor: (theme) =>
              theme.palette.mode === 'light'
                ? theme.palette.grey[100]
                : theme.palette.grey[900],
            flexGrow: 1,
            height: '100vh',
            overflow: 'auto',
          }}
        >
          <Toolbar />
          <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} md={8} lg={9}>
                <Paper
                  sx={{
                    p: 2,
                    display: 'flex',
                    flexDirection: 'column',
                  }}
                >
                  <ExamSchedule teachings={upcomingExams} />
                </Paper>
              </Grid>
              <Grid item xs={12} md={4} lg={3}>
                <Paper
                  sx={{
                    p: 2,
                    display: 'flex',
                    flexDirection: 'column',
                  }}
                >
                  <GPA gpa={gradedEnrollments.reduce((a, b) => a + b.grade, 0) / gradedEnrollments.length || 0} />
                </Paper>
              </Grid>
              <Grid item xs={12}>
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                  <Enrollments enrollments={enrollments} />
                </Paper>
              </Grid>
            </Grid>
            <Copyright sx={{ pt: 4 }} />
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

export default function StudentDashboardScreen() {
  return <DashboardContent />;
}


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
import { Teaching } from '../../../model/teaching';
import { Alert, ListItemButton, ListItemIcon, ListItemText, Snackbar } from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import Teachings from '../components/Teachings';
import { refreshTeachings } from '../../../stores/secretary/slice';
import TeachingModal from '../components/TeachingModal';
import SchoolIcon from '@mui/icons-material/School';
import SubjectModal from '../components/SubjectModal';
import ProfessorModal from '../components/ProfessorModal';
import SubjectIcon from '@mui/icons-material/Subject';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

function Copyright(props: any) {
  return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Unismos
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

const drawerWidth: number = 260;

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
  const [openTeachingModal, setOpenTeachingModal] = React.useState(false);
  const [openSubjectModal, setOpenSubjectModal] = React.useState(false);
  const [openProfessorModal, setOpenProfessorModal] = React.useState(false);
  const user: User = useSelector<RootState>(state => state.user.userData) as User;
  const teachings = useSelector<RootState>(state => state.secretary.teaching) as Teaching[];
  const dispatch = useDispatch();

  React.useEffect(() => {
    Axios.get('/teachings').then(res => {
      dispatch(refreshTeachings(res.data));
    });
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <TeachingModal
        isOpen={openTeachingModal}
        onClose={() => setOpenTeachingModal(false)}
      />
      <SubjectModal
        isOpen={openSubjectModal}
        onClose={() => setOpenSubjectModal(false)}
      />
      <ProfessorModal
        isOpen={openProfessorModal}
        onClose={() => setOpenProfessorModal(false)}
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
            <ListItemButton onClick={() => setOpenTeachingModal(true)}>
              <ListItemIcon>
                <SchoolIcon />
              </ListItemIcon>
              <ListItemText primary="Create new class" />
            </ListItemButton>
            <ListItemButton onClick={() => setOpenSubjectModal(true)}>
              <ListItemIcon>
                <SubjectIcon />
              </ListItemIcon>
              <ListItemText primary="Create a new subject" />
            </ListItemButton>
            <ListItemButton onClick={() => setOpenProfessorModal(true)}>
              <ListItemIcon>
                <PersonAddIcon />
              </ListItemIcon>
              <ListItemText primary="Create a new professor" />
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
              <Grid item xs={12}>
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                  <Teachings teachings={teachings} />
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

export default function SecretaryDashboardScreen() {
  return <DashboardContent />;
}


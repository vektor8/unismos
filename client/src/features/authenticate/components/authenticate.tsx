import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { RootState } from '../../../stores/store';


function AuthenticationSwitch(props: any) {
  const isAuthenticated = useSelector<RootState>(state => state.user.isLoggedIn);
  const userType = useSelector<RootState>(state => state.user.userData?.type);
  const { to, redirect } = props;
  if (!isAuthenticated) {
    return (redirect ?? <Navigate to="/login" />);
  }
  console.log(userType);
  switch (userType) {
    case 'student':
      return to ?? <Navigate to="/student-dashboard" />;
    // case 'professor':
    //   return to ?? <Navigate to="/professor-dashboard" />;
    // case 'secretary':
    //   return to ?? <Navigate to="/secretary-dashboard" />;
    default:
      return redirect ?? <Navigate to={redirect} />;
  }
}

export default AuthenticationSwitch

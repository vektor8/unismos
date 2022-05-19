import { Route, Routes } from 'react-router-dom'
import AuthenticationSwitch from '../../authenticate/components/authenticate'
import LoginScreen from '../../login/screens/login_screen'
import ProfessorDashboardScreen from '../../professor-dashboard/screens/dashboard_screen'
import RegisterScreen from '../../register/screens/register_screen'
import SecretaryDashboardScreen from '../../secretary-dashboard/screens/dashboard_screen'
import StudentDashboardScreen from '../../student-dashboard/screens/dashboard_screen'


interface Props {}

function MainRouter(props: Props) {
  return (
    <Routes>
      <Route path="/" element={<AuthenticationSwitch/>}/>
      <Route path="/login" element={<AuthenticationSwitch redirect={<LoginScreen/>}/>}/>
      <Route path="/register" element={<AuthenticationSwitch redirect={<RegisterScreen/>}/>}/>
      <Route path="/student-dashboard" element={<AuthenticationSwitch to={<StudentDashboardScreen/>}/>}/>
      <Route path="/professor-dashboard" element={<AuthenticationSwitch to={<ProfessorDashboardScreen/>}/>}/>
      <Route path="/secretary-dashboard" element={<AuthenticationSwitch to={<SecretaryDashboardScreen/>}/>}/>
    </Routes>
    
  )
}



export default MainRouter

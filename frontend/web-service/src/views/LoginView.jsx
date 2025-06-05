import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useAuth } from 'hooks/use-auth';
import LoginForm from 'components/forms/login-form/LoginForm';
import './LoginView.css';


const LoginView = () => {

    const { isAuth } = useAuth();

    const navigate = useNavigate();

    useEffect(() => {
        if (isAuth) {
            console.log("Redirect autorizated user to main page...");
            navigate('/home');
        }
  });

    return(
        <div id='container'>
            <LoginForm />
        </div>
    )
}

export default LoginView;
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useAuth } from 'hooks/use-auth';


const MainView = () => {

    const { isAuth, login } = useAuth();

    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuth) {
            console.log("Redirect unautorizated user to login...");
            navigate('/login');
        }
  });


    return(
        <div>
            <h1>MainView</h1>
        </div>
    )
}

export default MainView;
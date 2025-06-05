import { Routes, Route, Link, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useAuth } from 'hooks/use-auth';

// Pages
import HomePage from './pages/home-page/HomePage'; 
import SchedulePage from './pages/schedule-page/SchedulePage'; 
import AccountPage from './pages/account-page/AccountPage'; 
import TemplateCreatorPage from './pages/template-creator-page/TemplateCreatorPage'; 
import TemplateEditPage from './pages/template-creator-page/template-edit-page/TemplateEditPage'; 

import NavBar from 'components/nav-bar/NavBar'

import './MainView.css';

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
        <div className='wrapper'>
            <div className='content'>
                <Routes>
                    <Route path="/home" element={<HomePage />} />
                    <Route path="/schedule" element={<SchedulePage />} />
                    <Route path="/account" element={<AccountPage />} />
                    <Route path="/template_creator" element={<TemplateCreatorPage />} />
                    <Route path='/template_edit/:templateid?' element={<TemplateEditPage />} />
                </Routes>
            </div>
            <NavBar className='nav-bar' />
        </div>
    )
}

export default MainView;
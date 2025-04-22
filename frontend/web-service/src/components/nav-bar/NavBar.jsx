import React from 'react';
import { useNavigate, useLocation  } from 'react-router-dom';
import NavButton from './nav-bar-buttons/NavButton';
import homeIco from 'assets/svg/home-ico.svg';
import scheduleIco from 'assets/svg/schedule-ico.svg';
import accountIco from 'assets/svg/account-ico.svg';
import templateCreatorIco from 'assets/svg/template-creator-ico.svg';

import './NavBar.css'



const NavBar = () => {
    
    const navigate = useNavigate();
    const location = useLocation();

    const isActivePage = (buttonRoute) => {
        if(location.pathname  === buttonRoute) {
            return 'isActive';
        }
        else {
            return 'isInactive';
        }
    }

    const navButtonHandler = (redirectRoute) => {
        navigate(redirectRoute);
    }


    return(
        <div className='nav-bar'>
            <NavButton id='home-nav-button' isActivePage={isActivePage} handle={navButtonHandler} redirectRoute='/home' srcSvg={homeIco}/>
            <NavButton id='schedule-nav-button' isActivePage={isActivePage} handle={navButtonHandler} redirectRoute='/schedule' srcSvg={scheduleIco}/>
            <NavButton id='account-nav-button' isActivePage={isActivePage} handle={navButtonHandler} redirectRoute='/account' srcSvg={accountIco}/>
            <NavButton id='template_creator-nav-button' isActivePage={isActivePage} handle={navButtonHandler} redirectRoute='/template_creator' srcSvg={templateCreatorIco}/>
        </div>
    )
}

export default NavBar;


import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {createRegistrationAttempt, loginUser} from 'store/user/userSlice';
import LoginInput from './login-input/LoginInput';
import ChangeModeButton from './change-mode-button/ChangeModeButton';
import './LoginForm.css';

const LoginForm = () => {

    const [formMode, setFormMode ] = React.useState("sing-in");

    const [singInEmail, setSingIpEmail] = React.useState("");
    const [singInPassword, setSingIpPassword] = React.useState("");

    const [singUpEmail, setSingUpEmail] = React.useState("");
    const [singUpLogin, setSingUpLogin] = React.useState("");
    const [singUpPassword, setSingUpPassword] = React.useState("");
    const [singUpConfirmPassword, setSingUpConfirmPassword] = React.useState("");

    const dispatch = useDispatch();

    const buttonSwitchModeHandle = (mode) => {
        setFormMode(mode);

        setSingIpEmail("");
        setSingIpPassword("");

        setSingUpEmail("");
        setSingUpLogin("");
        setSingUpPassword("");
        setSingUpConfirmPassword("");

        console.log(singInEmail);
    };

    const buttonSingUpHandle = () => {
        //сделать валидацию для регистрации здесь, сравнить два введенных пароля
        dispatch(createRegistrationAttempt({login: singUpLogin, emailAddress: singUpEmail, password: singUpPassword}));
        // показать ожидание ответа, ошибку, если не получилось сделать запрос 
        // и отчистить форму только при успешном выполнении запроса о регистрации
        setSingUpEmail("");
        setSingUpLogin("");
        setSingUpPassword("");
        setSingUpConfirmPassword("");

    };

    const buttonSingInHandle = () => {
        //сделать валидацию для регистрации здесь, сравнить два введенных пароля
        dispatch(loginUser({emailAddress: singInEmail, password: singInPassword}));
        // сделать ожидание выполнения запроса и только потом срабуотает редирект
        // если запрос будет выполнен не верно - показать ошибку
        // форму лучше не отчищать
    };


    switch(formMode) {
        case "sing-in":
            return (
                <div className='login-form-container'>
                    <div className='change-mode-buttons-container'>
                        <div className='active-mode-button-container mode-button-container'>
                            <ChangeModeButton id="button-singin" text="вход" handle={buttonSwitchModeHandle} setedMode="sing-in" activeMode = {formMode} />
                        </div>
                        <div className='inactive-mode-button-container mode-button-container'>
                            <ChangeModeButton id="button-singup" text="регистрация" handle={buttonSwitchModeHandle} setedMode="sing-up" activeMode = {formMode} />
                        </div>
                    </div>
                    <div className='input-login-form login-mode'>
                        <LoginInput id="input-login-email" type="email" placeholder="Е-маил" value={singInEmail} setValue={setSingIpEmail}/>
                        <LoginInput id="input-login-password" type="password" placeholder="Пароль" value={singInPassword} setValue={setSingIpPassword}/>
                    </div>
                    <div id="login-mode-action-container" className='login-form-action-container'>
                        <button className="login-form-action-button" onClick={buttonSingInHandle}>Войти</button>
                    </div>
                </div>
            );

        case "sing-up":
            return (
                <div className='login-form-container'>
                    <div className='change-mode-buttons-container'>
                        <div className='inactive-mode-button-container mode-button-container'>
                            <ChangeModeButton id="button-singin" text="вход" handle={buttonSwitchModeHandle} setedMode="sing-in" activeMode = {formMode} />
                        </div>
                        <div className='active-mode-button-container mode-button-container'>
                            <ChangeModeButton id="button-singup" text="регистрация" handle={buttonSwitchModeHandle} setedMode="sing-up" activeMode = {formMode} />
                        </div>
                    </div>
                    <div className='input-login-form register-mode'>
                        <LoginInput id="input-login-email" type="email" placeholder="Е-маил" value={singUpEmail} setValue={setSingUpEmail} />
                        <LoginInput id="input-login-login" type="text" placeholder="Логин" value={singUpLogin} setValue={setSingUpLogin} />
                        <LoginInput id="input-login-password" type="password" placeholder="Пароль" value={singUpPassword} setValue={setSingUpPassword} />
                        <LoginInput id="input-login-confirm-pasword" type="password" placeholder="Подтвердите пароль" value={singUpConfirmPassword} setValue={setSingUpConfirmPassword} />
                    </div>
                    <div id="register-mode-action-container" className='login-form-action-container'>
                        <button className="login-form-action-button" onClick={buttonSingUpHandle}>Создать аккаунт</button>
                    </div>
                </div>
            );

    }
}

export default LoginForm;
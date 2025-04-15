import React from 'react'
import LoginInput from './login-input/LoginInput'
import ChangeModeButton from './change-mode-button/ChangeModeButton'
import './LoginForm.css'

const LoginForm = () => {

    const [formMode, setFormMode ] = React.useState("sing-in");

    const [singInEmail, setSingIpEmail] = React.useState("");
    const [singInPassword, setSingIpPassword] = React.useState("");

    const [singUpEmail, setSingUpEmail] = React.useState("");
    const [singUpLogin, setSingUpLogin] = React.useState("");
    const [singUpPassword, setSingUpPassword] = React.useState("");
    const [singUpConfirmPassword, setSingUpConfirmPassword] = React.useState("");

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

    const buttonActionHandle = () => {
        console.log("use action");
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
                        <button className="login-form-action-button" onClick={buttonActionHandle}>Войти</button>
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
                        <button className="login-form-action-button" onClick={buttonActionHandle}>Создать аккаунт</button>
                    </div>
                </div>
            );

    }
}

export default LoginForm;
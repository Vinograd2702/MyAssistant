import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import {getNotificationSettingsForCurrentUser, updateNotificationSettingsForCurrentUser} from 'store/user/userSettingsSlice';
import {exitUser, updateUserInfoById} from 'store/user/userSlice';
import {useUserInfo} from 'hooks/use-user-Info';
import {useUserSettings} from 'hooks/use-user-settings';
import { useAuth } from 'hooks/use-auth';
import './AccountPage.css';

const AccountPage = () => {
    
    const dispatch = useDispatch();

    React.useState( async () => {
        await dispatch(getNotificationSettingsForCurrentUser());
    }, []);

    const userInfo = useUserInfo();
    const userSettings = useUserSettings();

    const [userLocalData, setFormData] = React.useState({ 
        userFirstName: (userInfo.firstName == null? "" : userInfo.firstName),
        userLastName: (userInfo.lastName == null? "" : userInfo.lastName),
        userPatronymic: (userInfo.patronymic == null? "" : userInfo.patronymic),
        userPhoneNumber: (userInfo.phoneNumber == null? "" : userInfo.phoneNumber),
        useEmailNotificate: userSettings.isAcceptEmailNotification,
        usePushNotificate: userSettings.isAcceptPushNotification
    });

    const [isChangeUserData, setIsChangeUserData] = React.useState(false);

    const [isChangeUserNotificationSettings, setIsChangeUserNotificationSettings] = React.useState(false);

    const handleInputUserDataChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({ ...prevData, [name]: value }));
        console.log(value);
        setIsChangeUserData(true);
    };

    const handleInputUserNotificationChange = (e) => {
        const { name, checked } = e.target;
        setFormData((prevData) => ({ ...prevData, [name]: checked }));
        setIsChangeUserNotificationSettings(true);
    };

    const { id, isAuth } = useAuth();
    const navigate = useNavigate();

    const buttonExitHandle = async () => {
        await dispatch(exitUser());
        console.log("Redirect unautorizated user to login...");
        navigate('/login');
    };

    const buttonSaveChangesUserDataandSettings = async () => {
        if(isChangeUserData) {
            await dispatch(updateUserInfoById({id: id, 
            firstName: (userLocalData.userFirstName == ""? null : userLocalData.userFirstName),
            lastName:  (userLocalData.userLastName == ""? null : userLocalData.userLastName),
            patronymic:  (userLocalData.userPatronymic == ""? null : userLocalData.userPatronymic), 
            phoneNumber:  (userLocalData.userPhoneNumber == ""? null : userLocalData.userPhoneNumber),
            }));
            setIsChangeUserData(false);
            navigate('/account');
        }

        if(isChangeUserNotificationSettings) {
            await dispatch(updateNotificationSettingsForCurrentUser({id: id, 
            isUseEmail: userLocalData.useEmailNotificate,
            isUsePush: userLocalData.usePushNotificate
            }));

            setIsChangeUserNotificationSettings(false);
            navigate('/account');
        }
    }


    return(
        <div className="page">
            <div className="head">
                <h1 className="user-login">{userInfo.login}</h1>
                <button className="exit-button" onClick={buttonExitHandle}>Выход</button>
            </div>
            <div className="page-content">
                <h2>Имя</h2>
                <input className='user-first-name' name="userFirstName" value={userLocalData.userFirstName} onChange={handleInputUserDataChange}></input>
                <h2>Отчество</h2>
                <input className='user-last-name' name="userLastName" value={userLocalData.userLastName} onChange={handleInputUserDataChange}></input>
                <h2>Фамилия</h2>
                <input className='user-patronymic' name="userPatronymic" value={userLocalData.userPatronymic} onChange={handleInputUserDataChange}></input>
                <h2>Телефон</h2>
                <input className='user-phone-number' name="userPhoneNumber" value={userLocalData.userPhoneNumber} onChange={handleInputUserDataChange}></input>
                <h2>Уведомления</h2>
                <div className='notify-container'>
                    <span>email</span>
                    <input type='checkbox' className='user-settings-email-notify' name="useEmailNotificate" checked={userLocalData.useEmailNotificate} onChange={handleInputUserNotificationChange}></input>
                </div>
                <div className='notify-container'>
                    <span>push</span>
                    <input type='checkbox' className='user-settings-push-notify' name="usePushNotificate" checked={userLocalData.usePushNotificate} onChange={handleInputUserNotificationChange}></input>
                </div>
                <div className="save-user-settings-button-container">
                    <button className="save-user-settings-button" hidden={!(isChangeUserData || isChangeUserNotificationSettings)} onClick={buttonSaveChangesUserDataandSettings}>Сохранить изменения</button>
                </div>
            </div>
        </div>
    )
}

export default AccountPage;
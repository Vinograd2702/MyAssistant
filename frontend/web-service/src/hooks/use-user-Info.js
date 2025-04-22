import { useSelector } from "react-redux";

export function useUserInfo() {
    const {status, error, id, login, firstName, lastName, patronymic, phoneNumber} = useSelector(state => state.user);

    return {
        status,
        error,
        id,
        login,
        firstName,
        lastName,
        patronymic, 
        phoneNumber
    };
}
import { useSelector } from "react-redux";

export function useAuth() {
    const {id, login, firstName, lastName, patronymic, phoneNumber} = useSelector(state => state.user);

    return {
        isAuth: !!login,
        id,
        login,
        firstName,
        lastName,
        patronymic,
        phoneNumber
    };
}
import { useSelector } from "react-redux";

export function useAuth() {
    const {id, login} = useSelector(state => state.user);

    return {
        isAuth: (document.cookie.indexOf(process.env.REACT_APP_AUTH_COOKIE) == 0),
        id,
        login
    };
}
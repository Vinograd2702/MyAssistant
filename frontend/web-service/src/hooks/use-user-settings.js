import { useSelector } from "react-redux";

export function useUserSettings() {
    const {isAcceptEmailNotification, isAcceptPushNotification} = useSelector(state => state.userSettings);

    return {
        isAcceptEmailNotification,
        isAcceptPushNotification
    };
}
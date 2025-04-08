import {configureStore} from '@reduxjs/toolkit';
import userReduser from './user/userSlice';


export default configureStore({
    reducer: {
        user: userReduser,
    }
});
import {createSlice, createAsyncThunk, createReducer} from '@reduxjs/toolkit';

export const getNotificationSettingsForCurrentUser = createAsyncThunk(
    'auth/User/GetNotificationSettingsForMyUser',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/GetNotificationSettingsForMyUser', {
                    mode: 'cors',
                    method: 'GET',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    }
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t load user settings. Server error.');
            }

            const userSettingsData = await response.json();

            return userSettingsData;


        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateNotificationSettingsForCurrentUser = createAsyncThunk(
    'auth/User/UpdateNotificationUserSettings',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            let userSettingsData = {
                id: data.id,
                isUseEmail: data.isUseEmail,
                isUsePush: data.isUsePush
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/UpdateNotificationUserSettings', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(userSettingsData)
                }
            );

            if(response.status === 401) {
                throw new Error('Can\'t  post user settings. Unautorized.');
            }

            if(response.status === 404) {
                throw new Error('Can\'t  post user settings. NotFound.');
            }


            if(!response.ok) {
                throw new Error('Can\'t post user settings. Server error.');
            }

            response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/GetNotificationSettingsForMyUser', {
                    mode: 'cors',
                    method: 'GET',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    }
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t load user settings. Server error.');
            }

            userSettingsData = await response.json();

            return userSettingsData;


        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    status: null,
    error: null,
    isAcceptEmailNotification: null,
    isAcceptPushNotification: null,
};

const userSettingsSlice = createSlice({
    name: "userSettings",
    initialState,
    reducers: {
        setUserSettings(state, action) {
            state.isAcceptEmailNotification = action.payload.isAcceptEmailNotification;
            state.isAcceptPushNotification = action.payload.isAcceptPushNotification;
        },
        removeUser(state) {
            state.isAcceptEmailNotification = null;
            state.isAcceptPushNotification = null;
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getNotificationSettingsForCurrentUser.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getNotificationSettingsForCurrentUser.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.isAcceptEmailNotification = action.payload.isAcceptEmailNotification;
            state.isAcceptPushNotification = action.payload.isAcceptPushNotification;
        })
        .addCase(getNotificationSettingsForCurrentUser.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
        .addCase(updateNotificationSettingsForCurrentUser.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(updateNotificationSettingsForCurrentUser.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.isAcceptEmailNotification = action.payload.isAcceptEmailNotification;
            state.isAcceptPushNotification = action.payload.isAcceptPushNotification;
        })
        .addCase(updateNotificationSettingsForCurrentUser.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    
        // использовать остальные кейсы для остальных запросов

    }
});

export const {setUserSettings, removeUser} = userSettingsSlice.actions;

export default userSettingsSlice.reducer;
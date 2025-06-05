import {createSlice, createAsyncThunk, createReducer} from '@reduxjs/toolkit';

export const createRegistrationAttempt = createAsyncThunk(
    'auth/RegistrationAttempt/CreateRegistrationAttempt',
    async (data, {rejectWithValue, dispatch}) => {
        
        try {
            const registrationAttempt = {

                login: data.login,
                emailAddress: data.emailAddress,
                password: data.password
            };

            const response = await fetch(process.env.REACT_APP_API_URL + 
                'auth/RegistrationAttempt/CreateRegistrationAttempt', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(registrationAttempt)
                }
            );

            if(response.status === 400) {
                throw new Error('Can\'t create new Register Attempt. Bad request.');
            }
            
            if(!response.ok) {
                throw new Error('Can\'t create new Register Attempt. Server error.');
            }

        } catch (error) {
            return rejectWithValue(error.message);
        }
        
        //return codeResponse;
    }
);

export const loginUser = createAsyncThunk(
    'auth/User/LoginUser',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const loginInfo = {
                emailAdress: data.emailAddress,
                password: data.password
            };

            let response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/LoginUser', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(loginInfo)
                }
            );

            if(response.status === 400) {
                throw new Error('Can\'t login user. Bad request.');
            }

            if(response.status === 404) {
                throw new Error('Can\'t login user. Incorrect login data. Not found user.');
            }
            
            if(!response.ok) {
                throw new Error('Can\'t login user. Server error.');
            }

            response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/GetMyUserInfo', {
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
                throw new Error('Can\'t load user info. Server error.');
            }

            const userInfoData = await response.json();
            
            document.cookie = process.env.REACT_APP_AUTH_COOKIE + '=' + 'is_active';

            return userInfoData;


        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const exitUser = createAsyncThunk(
    'auth/User/ExitUser',
    async (data, {rejectWithValue, dispatch}) => {
        
        try {
            const response = await fetch(process.env.REACT_APP_API_URL + 
                'auth/User/ExitUser', {
                    mode: 'cors',
                    method: 'DELETE',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    }
                }
            );
            
            if(!response.ok) {
                throw new Error('Can\'t create new Register Attempt. Server error.');
            }

        } catch (error) {
            return rejectWithValue(error.message);
        }
        
        document.cookie = process.env.REACT_APP_AUTH_COOKIE + '=; Max-Age=-1;';
    }
);
//
export const updateUserInfoById = createAsyncThunk(
    'auth/User/UpdateUserInfoById',
    async (data, {rejectWithValue, dispatch}) => {
        
        try {
            let userInfoData = {
                id: data.id,
                firstName: data.firstName,
                lastName: data.lastName,
                patronymic: data.patronymic,
                phoneNumber: data.phoneNumber
            };

            let response = await fetch(process.env.REACT_APP_API_URL + 
                'auth/User/UpdateUserInfoById', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(userInfoData)
                }
            );


            if(response.status === 401) {
                throw new Error('Can\'t  update User Info. Unautorized.');
            }

            if(response.status === 404) {
                throw new Error('Can\'t  update User Info. NotFound.');
            }
            
            if(!response.ok) {
                throw new Error('Can\'t update User Info. Server error.');
            }

            response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/GetMyUserInfo', {
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
                throw new Error('Can\'t load user info. Server error.');
            }

            userInfoData = await response.json();
            
            document.cookie = process.env.REACT_APP_AUTH_COOKIE + '=' + 'is_active';

            return userInfoData;

        } catch (error) {
            return rejectWithValue(error.message);
        }
        
    }
);

export const getUserInfoById = createAsyncThunk(
    'auth/User/GetUserInfoById',
    async (data, {rejectWithValue, dispatch}) => {
        
        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'auth/User/GetMyUserInfo', {
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
                throw new Error('Can\'t load user info. Server error.');
            }

            const userInfoData = await response.json();

            return userInfoData;

        } catch (error) {
            return rejectWithValue(error.message);
        }
        
    }
);
//
const initialState = {
    status: null,
    error: null,
    id: null,
    login: null,
    firstName: null,
    lastName: null,
    patronymic: null,
    phoneNumber: null
};

const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        setUser(state, action) {
            state.id = action.payload.id;
            state.login = action.payload.login;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.patronymic = action.payload.patronymic;
            state.phoneNumber = action.payload.phoneNumber;
        },
        removeUser(state) {
            state.id = null;
            state.login = null;
            state.firstName = null;
            state.lastName = null;
            state.patronymic = null;
            state.phoneNumber = null;
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(loginUser.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(loginUser.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.login = action.payload.login;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.patronymic = action.payload.patronymic;
            state.phoneNumber = action.payload.phoneNumber;
        })
        .addCase(loginUser.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
        .addCase(exitUser.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(exitUser.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.error = null;
            state.id = null;
            state.login = null;
            state.firstName = null;
            state.lastName = null;
            state.patronymic = null;
            state.phoneNumber = null;
        })
        .addCase(exitUser.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
        .addCase(updateUserInfoById.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(updateUserInfoById.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.login = action.payload.login;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.patronymic = action.payload.patronymic;
            state.phoneNumber = action.payload.phoneNumber;
        })
        .addCase(updateUserInfoById.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
        .addCase(getUserInfoById.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getUserInfoById.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.login = action.payload.login;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.patronymic = action.payload.patronymic;
            state.phoneNumber = action.payload.phoneNumber;
        })
        .addCase(getUserInfoById.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    
        // использовать остальные кейсы для остальных запросов
    
    }
});

export const {setUser, removeUser} = userSlice.actions;

export default userSlice.reducer;
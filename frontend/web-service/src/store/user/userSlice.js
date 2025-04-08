import {createSlice} from '@reduxjs/toolkit';


const initialState = {
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
            
            console.log(state); // log
            console.log(action);

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
    }
});

export const {setUser, removeUser} = userSlice.actions;

export default userSlice.reducer;
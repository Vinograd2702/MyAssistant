import {createSlice, createAsyncThunk, createReducer} from '@reduxjs/toolkit';

export const getExerciseGroup = createAsyncThunk(
    'sports/Exercises/GetExerciseGroupVm',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL + 
                'sports/Exercises/GetExerciseGroupVm?Id=' + data.groupId, {
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
                throw new Error('Can\'t load user exercise group by id. Server error.');
            }

            const group = await response.json();

            return group;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const getExerciseGroupList = createAsyncThunk(
    'sports/Exercises/GetExerciseGroupVmList',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/GetExerciseGroupVmList', {
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
                throw new Error('Can\'t load user exercise groups. Server error.');
            }

            const groups = await response.json();

            return groups;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const createExerciseGroup = createAsyncThunk(
    'sports/Exercises/CreateExercisesGroup',
    async (data, {rejectWithValue, dispatch}) => {
        try {
            const newGroup = {
                parentGroupId: data.parentGroupId,
                name: data.name
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/CreateExercisesGroup', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(newGroup)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t new group. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateNameExerciseGroup = createAsyncThunk(
    'sports/Exercises/UpdateNameExercisesGroup',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const updatetGroupInfo = {
                id: data.id,
                name: data.name
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/UpdateNameExercisesGroup', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(updatetGroupInfo)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t post group. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);

export const deleteExerciseGroup = createAsyncThunk(
    'sports/Exercises/DeleteExercisesGroup',
    async (data, {rejectWithValue, dispatch}) => {
        try {
            const deleteGroup = {
                id: data.id
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/DeleteExercisesGroup', {
                    mode: 'cors',
                    method: 'DELETE',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(deleteGroup)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t delete group. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
); 

export const getExerciseType = createAsyncThunk(
    'sports/Exercises/GetExersiseTypeVm',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL + 
                'sports/Exercises/GetExersiseTypeVm?Id=' + data.exerciseId, {
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
                throw new Error('Can\'t load user exercise type by id. Server error.');
            }

            const type = await response.json();

            return type;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const getExerciseTypeList = createAsyncThunk(
    'sports/Exercises/GetExersiseTypeVmList',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/GetExersiseTypeVmList', {
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
                throw new Error('Can\'t load user exercise types. Server error.');
            }

            const types = await response.json();

            return types;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const createExerciseType = createAsyncThunk(
    'sports/Exercises/CreateExerciseType',
    async (data, {rejectWithValue, dispatch}) => {
        try {
            const newExerciseType = {
                exerciseGroupId: data.exerciseGroupId,
                name: data.name,
                description: data.description
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/CreateExerciseType', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(newExerciseType)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t new Exercise Type. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateInfoExerciseType = createAsyncThunk(
    'sports/Exercises/UpdateInfoExerciseType',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const updatetExerciseTypeInfo = {
                id: data.id,
                name: data.name,
                description: data.description
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/UpdateInfoExerciseType', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(updatetExerciseTypeInfo)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t post Exercise Type. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }

);

export const deleteExerciseType = createAsyncThunk(
    'sports/Exercises/DeleteExerciseType',
    async (data, {rejectWithValue, dispatch}) => {
        try {
            const deleteExerciseType = {
                id: data.id
            };

            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Exercises/DeleteExerciseType', {
                    mode: 'cors',
                    method: 'DELETE',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(deleteExerciseType)
                }
            );

            if(!response.ok) {
                throw new Error('Can\'t delete Exercise Type. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }

);


const initialExerciseGroupLookupList = {
    status: null,
    error: null,
    ExerciseGroupList: []
}

const groupsLookupSlice = createSlice({
    name: "groupsLookup",
    initialState: initialExerciseGroupLookupList,
    reducers: {
        setGroupsLookup(state, action) {
            state.ExerciseGroupList = action.payload/*.groups */;/*проверить, приходит массив элементов */
        },
        removeGroupsLookup(state, action) {
            state.ExerciseGroupList = [];
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getExerciseGroupList.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getExerciseGroupList.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.ExerciseGroupList = action.payload/*.groups */;/*проверить, приходит массив элементов */

        })
        .addCase(getExerciseGroupList.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});

const initialExerciseGroupDetails = {
    status: null,
    error: null,
    id: null,
    parentGroupId: null,
    name: null
}

const groupDetailsSlice = createSlice({
    name: "groupDetails",
    initialState: initialExerciseGroupDetails,
    reducers: {
        setGroupDetails(state, action) {
            state.id = action.payload.id;
            state.parentGroupId = action.payload.parentGroupId;
            state.name = action.payload.name;
        },
        removeGroupDetails(state, action) {
            state.id = null;
            state.parentGroupId = null;
            state.name = null;
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getExerciseGroup.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getExerciseGroup.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.parentGroupId = action.payload.parentGroupId;
            state.name = action.payload.name;
        })
        .addCase(getExerciseGroup.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});

const initialExerciseTypeLookupList = {
    status: null,
    error: null,
    ExerciseTypeList: []
}

const typesLookupSlice = createSlice({
    name: "typesLookup",
    initialState: initialExerciseTypeLookupList,
    reducers: {
        setTypesLookup(state, action) {
            state.ExerciseTypeList = action.payload/*.types */;/*проверить, приходит массив элементов */
        },
        removeTypesLookup(state, action) {
            state.ExerciseTypeList = [];
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getExerciseTypeList.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getExerciseTypeList.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.ExerciseTypeList = action.payload/*.types */;/*проверить, приходит массив элементов */

        })
        .addCase(getExerciseTypeList.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});

const initialExerciseTypeDetails = {
    status: null,
    error: null,
    id: null,
    exerciseGroupId: null,
    name: null,
    description: null
}

const typeDetailsSlice = createSlice({
    name: "typeDetails",
    initialState: initialExerciseTypeDetails,
    reducers: {
        setTypeDetails(state, action) {
            state.id = action.payload.id;
            state.exerciseGroupId = action.payload.exerciseGroupId;
            state.name = action.payload.name;
            state.description = action.payload.description;
        },
        removeTypeDetails(state, action) {
            state.ExerciseTypeList = [];
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getExerciseType.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getExerciseType.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.exerciseGroupId = action.payload.exerciseGroupId;
            state.name = action.payload.name;
            state.description = action.payload.description;
        })
        .addCase(getExerciseType.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});

export const {setTypeDetails, removeTypeDetails} = typeDetailsSlice.actions;
export const {setGroupDetails, removeGroupDetails} = groupDetailsSlice.actions;

export const groupsLookupReduser = groupsLookupSlice.reducer;
export const groupDetailsReduser = groupDetailsSlice.reducer;
export const typesLookupReduser = typesLookupSlice.reducer;
export const typeDetailsReduser = typeDetailsSlice.reducer;

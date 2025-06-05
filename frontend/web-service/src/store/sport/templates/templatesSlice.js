import {createSlice, createAsyncThunk, createReducer} from '@reduxjs/toolkit';

export const getTemplateById = createAsyncThunk(
    'sports/Templates/GetTemplateWorkoutById',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Templates/GetTemplateWorkoutById?Id=' + data.templateId, {
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
                throw new Error('Can\'t load user template by id. Server error.');
            }

            const template = await response.json();

            return template;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const getTempleteList = createAsyncThunk(
    'sports/Templates/GetTemplateWorkoutList',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Templates/GetTemplateWorkoutList', {
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
                throw new Error('Can\'t load user templates. Server error.');
            }

            const templates = await response.json();

            return templates;

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);



export const createTemplate = createAsyncThunk(
    '/sports/Templates/CreateTemplateWorkout',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Templates/CreateTemplateWorkout', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body:  JSON.stringify(data)
                }
            ); 

            if(!response.ok) {
                throw new Error('Can\'t create new template. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);


export const updateTemplate = createAsyncThunk(
    '/sports/Templates/UpdateTemplateWorkout',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Templates/UpdateTemplateWorkout', {
                    mode: 'cors',
                    method: 'POST',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                }
            ); 

            if(!response.ok) {
                throw new Error('Can\'t update template. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);

export const deleteTemplate = createAsyncThunk(
    '/sports/Templates/DeleteTemplateWorkout',
    async (data, {rejectWithValue, dispatch}) => {

        try {
            const response = await fetch(process.env.REACT_APP_API_URL +
                'sports/Templates/DeleteTemplateWorkout', {
                    mode: 'cors',
                    method: 'DELETE',
                    credentials: "include",
                    headers: {
                        'Accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                }
            ); 

            if(!response.ok) {
                throw new Error('Can\'t delete template. Server error.');
            }

        } catch(error) {
            return rejectWithValue(error.message);
        }
    }
);


const initnalTemplatesLookupList = {
    status: null,
    error: null,
    templateLookupDto: []
};

const templatesLookupSlice = createSlice({
    name: "templatesLookup",
    initialState: initnalTemplatesLookupList,
    reducers: {
        setTemplatesLookup(state, action) {
            state.templateLookupDto = action.payload.templates;
        },
        removeTemplatesLookup(state, action) {
            state.templateLookupDto = [];
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getTempleteList.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getTempleteList.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.templateLookupDto = action.payload.templates;

        })
        .addCase(getTempleteList.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});

const initnalTemplateDetails = {
    status: null,
    error: null,
    id: null,
    name: null,
    description: null,
    templatesBlockCardio: [],
    templatesBlockStrenght: [],
    templatesBlockSplit: [],
    templatesBlockWarmUp: []
    // остальные поля темплейта
}

const templateDetailsSlice = createSlice({
    name: "templateDetails",
    initialState: initnalTemplateDetails,
    reducers: {
        setTemplateDetails(state, action) {
            state.id = action.payload.id;
            state.name = action.payload.name;
            state.description = action.payload.description;
            state.templatesBlockCardio = action.payload.templatesBlockCardio;
            state.templatesBlockStrenght = action.payload.templatesBlockStrenght;
            state.templatesBlockSplit = action.payload.templatesBlockSplit;
            state.templatesBlockWarmUp = action.payload.templatesBlockWarmUp;
        },
        removeTemplateDetails(state, action) {
            state.id = null;
            state.name = null;
            state.description = null;
            state.templatesBlockCardio = [];
            state.templatesBlockStrenght = [];
            state.templatesBlockSplit = [];
            state.templatesBlockWarmUp = [];
        }
    },
    extraReducers: (builder) => {
        builder
        .addCase(getTemplateById.pending, (state, action) => {
            state.status = 'loading';
            state.error = null;
        })
        .addCase(getTemplateById.fulfilled, (state, action) => {
            state.status = 'resolved';
            state.id = action.payload.id;
            state.name = action.payload.name;
            state.description = action.payload.description;
            state.templatesBlockCardio = action.payload.templatesBlockCardio;
            state.templatesBlockStrenght = action.payload.templatesBlockStrenght;
            state.templatesBlockSplit = action.payload.templatesBlockSplit;
            state.templatesBlockWarmUp = action.payload.templatesBlockWarmUp;

        })
        .addCase(getTemplateById.rejected, (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        })
    }
});


export const templatesLookupReduser = templatesLookupSlice.reducer;
export const templateDetailsReduser = templateDetailsSlice.reducer;
export const {removeTemplateDetails} = templateDetailsSlice.actions;
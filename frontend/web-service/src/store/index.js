import {configureStore, combineReducers} from '@reduxjs/toolkit';
import { 
    persistStore, 
    persistReducer,
    FLUSH,
    REHYDRATE,
    PAUSE,
    PERSIST,
    PURGE,
    REGISTER,
} from 'redux-persist'
import storage from 'redux-persist/lib/storage'
import userReduser from './user/userSlice';
import userSettingsReduser from './user/userSettingsSlice';
import {templatesLookupReduser, templateDetailsReduser} from './sport/templates/templatesSlice';
import {groupsLookupReduser, groupDetailsReduser, typesLookupReduser, typeDetailsReduser} from './sport/execises/execisesSlice'

const rootReduser = combineReducers({
    user: userReduser,
    userSettings: userSettingsReduser,

    templatesLookup: templatesLookupReduser,
    templateDetails: templateDetailsReduser,

    groupsLookup: groupsLookupReduser,
    groupDetails: groupDetailsReduser,
    typesLookup: typesLookupReduser,
    typeDetails: typeDetailsReduser,
});

const persistConfig = {
    key: 'root',
    storage : storage,
}

const persistedReducer = persistReducer(persistConfig, rootReduser);

const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
        serializableCheck: {
            ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
        }
    })  
});

export const persistor = persistStore(store);
export default store;
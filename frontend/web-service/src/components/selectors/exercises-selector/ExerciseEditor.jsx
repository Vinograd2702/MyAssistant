import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getExerciseGroupList, createExerciseGroup, updateNameExerciseGroup, deleteExerciseGroup,
         getExerciseTypeList, createExerciseType, updateInfoExerciseType, deleteExerciseType} from 'store/sport/execises/execisesSlice'

import './ExerciseEditor.css';

const ExerciseEditor = ({elementType, id, groupId}) => {
    const dispatch = useDispatch();

    const selectorDataGroupDetailsStatus = useSelector(state=>state.groupDetails.status);
    const selectorDataGroupName = useSelector(state=>state.groupDetails.name);

    const selectorDataExrciseDetailsStatus = useSelector(state=>state.typeDetails.status);
    const selectorDataExerciseName = useSelector(state=>state.typeDetails.name);
    const selectorDataExerciseDescription = useSelector(state=>state.typeDetails.description);

    const [editLocalData, setFormData] = React.useState({
        editGroupName: "",
        editExerciseName: "",
        editExerciseDescription: ""
    });

    const [createMode, setCreateMode] = React.useState("new-exercise");

    const [isDataChanged, setIsDataChanged] = React.useState(false);

    React.useEffect(() => {
        if((id != null)&&(elementType === "group")) {
            if (selectorDataGroupDetailsStatus === "resolved") {
                
                setFormData((prevData) => ({ ...prevData, 'editGroupName': selectorDataGroupName }));
            }
        }
        else if ((id != null)&&(elementType === "exersise")) {
            if (selectorDataExrciseDetailsStatus === "resolved") {
                
                setFormData((prevData) => ({ ...prevData, 'editExerciseName': selectorDataExerciseName }));
                setFormData((prevData) => ({ ...prevData, 'editExerciseDescription': selectorDataExerciseDescription }));
            }
        }
    }, [selectorDataGroupDetailsStatus, selectorDataExrciseDetailsStatus]);

    React.useEffect(() => {
        if((elementType === "new-element")) {
            setFormData((prevData) => ({ ...prevData, 'editGroupName': "" }));
            setFormData((prevData) => ({ ...prevData, 'editExerciseName': "" }));
            setFormData((prevData) => ({ ...prevData, 'editExerciseDescription': "" }));
        }
    }, []);

    const handleInputDataChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({ ...prevData, [name]: value }));
        setIsDataChanged(true);
    };

    const handleCanseledButton = async() => {
        await dispatch(getExerciseGroupList());
        await dispatch(getExerciseTypeList());
    }

    const handleSaveElementButton = async () => {
        if (elementType === "group") {
            await dispatch(updateNameExerciseGroup({
                id: id,
                name: editLocalData.editGroupName
            }));
    
            await dispatch(getExerciseGroupList());
        }
        else if (elementType === "exersise") {
            await dispatch(updateInfoExerciseType({
                id: id,
                name: editLocalData.editExerciseName,
                description: editLocalData.editExerciseDescription
            }));
    
            await dispatch(getExerciseTypeList());
        }
        else if (elementType === "new-element") {
            if (createMode === "new-group") {
                await dispatch(createExerciseGroup({
                    parentGroupId: (groupId!=undefined?groupId:null),
                    name: editLocalData.editGroupName
                }));

                await dispatch(getExerciseGroupList());
        }
            else if (createMode === "new-exercise") {
                await dispatch(createExerciseType({
                    exerciseGroupId: (groupId!=undefined?groupId:null),
                    name: editLocalData.editExerciseName,
                    description: editLocalData.editExerciseDescription
                }));

                await dispatch(getExerciseTypeList());
            }
        }
    }

    const handleDeleteElementButton = async () => {
        if (elementType === "group") {
            await dispatch(deleteExerciseGroup({
                id: id
            }));
    
            await dispatch(getExerciseGroupList());
        }
        else if (elementType === "exersise") {
            await dispatch(deleteExerciseType({
                id: id
            }));
    
            await dispatch(getExerciseTypeList());
        }
    }

    const handleSwitchCreateMode = (mode) => {
        setFormData((prevData) => ({ ...prevData, 'editGroupName': "" }));
        setFormData((prevData) => ({ ...prevData, 'editExerciseName': "" }));
        setFormData((prevData) => ({ ...prevData, 'editExerciseDescription': "" }));
        setIsDataChanged(false);
        setCreateMode(mode);
    }

    if((id != null)&&(elementType === "group")) {

        if(selectorDataGroupDetailsStatus === "loading") {
            return (
                <div className='container-exercise-editor'>
                    <h2>Загрузка</h2>
                </div>
            )
        } 
        else {
            //редактор существующей группы
            return(
                <div className='container-exercise-editor'>
                    <span className='span-block-exercise-editor'>Изменить группу</span>
                    <div className='content-container-exercise-editor'>
                        <span className='span-exercise-editor'>Название:</span>
                        <input
                        id='edit-exercise-group-name'
                        className='input-exercise-editor'
                        type='text'
                        maxLength='50'
                        placeholder='Название группы'
                        name='editGroupName'
                        value={editLocalData.editGroupName}
                        onChange={handleInputDataChange}/>
                    </div>
                    
                    <div className='button-container-exercise-editor'>
                        <div className='plug-container-exercise-editor' hidden={isDataChanged}/>
                        <button id='save-button-exercise-editor' className='button-exercise-editor' onClick={handleSaveElementButton} hidden={!isDataChanged}>Сохранить</button>
                        <button id='delete-button-exercise-editor' className='button-exercise-editor' onClick={handleDeleteElementButton}>Удалить</button>
                        <button id='cancel-button-exercise-editor'className='button-exercise-editor' onClick={handleCanseledButton}>Отменить</button>
                    </div>  
                </div>
                
            )
        }
    }
    else if ((id != null)&&(elementType === "exersise")) {
        if(selectorDataExrciseDetailsStatus === "loading") {
            return (
                <h2>Загрузка</h2>
            )
        } 
        else {
            //редактор существующего упражнения
            return(
                <div className='container-exercise-editor'>
                    <span className='span-block-exercise-editor'>Изменить упражнение</span>
                    <div className='content-container-exercise-editor'>
                        <span className='span-exercise-editor'>Название:</span>
                        <input
                        id='edit-exercise-type-name'
                        className='input-exercise-editor'
                        type='text'
                        maxLength='50'
                        placeholder='Название упражнения'
                        name='editExerciseName'
                        value={editLocalData.editExerciseName}
                        onChange={handleInputDataChange}/>
                        <span className='span-exercise-editor'>Описание:</span>
                        <input
                        id='edit-exercise-type-description'
                        className='input-exercise-editor'
                        type='text'
                        maxLength='250'
                        placeholder='Описание упражнения'
                        name='editExerciseDescription'
                        value={editLocalData.editExerciseDescription}
                        onChange={handleInputDataChange}/>
                    </div>
                    <div className='button-container-exercise-editor'>
                        <div className='plug-container-exercise-editor' hidden={isDataChanged}/>
                        <button id='save-button-exercise-editor' className='button-exercise-editor' onClick={handleSaveElementButton} hidden={!isDataChanged}>Сохранить</button>
                        <button id='delete-button-exercise-editor' className='button-exercise-editor' onClick={handleDeleteElementButton}>Удалить</button>
                        <button id='cancel-button-exercise-editor'className='button-exercise-editor' onClick={handleCanseledButton}>Отменить</button>
                    </div>  
                </div>
            )
        }
    }

    else if (elementType === "new-element") {
        //создание нового упражнения или группы
        return(
            <div className='container-exercise-editor'>
                <span className='span-exercise-editor'>Добавить</span>
                <button className='switch-mode-button-exercise-editor'
                disabled={createMode==="new-exercise"}
                onClick={() => handleSwitchCreateMode("new-exercise")}>упражнение</button>
                <button className='switch-mode-button-exercise-editor'
                disabled={createMode==="new-group"}
                onClick={() => handleSwitchCreateMode("new-group")}>группу</button>
                <div className='content-container-exercise-editor' hidden={createMode!="new-exercise"}>
                    <span className='span-exercise-editor'>Название:</span>
                    <input
                    id='edit-exercise-type-name'
                    className='input-exercise-editor'
                    type='text'
                    maxLength='50'
                    name='editExerciseName'
                    value={editLocalData.editExerciseName}
                    onChange={handleInputDataChange}/>
                    <span className='span-exercise-editor'>Описание:</span>
                    <input
                    id='edit-exercise-type-description'
                    className='input-exercise-editor'
                    type='text'
                    maxLength='250'
                    name='editExerciseDescription'
                    value={editLocalData.editExerciseDescription}
                    onChange={handleInputDataChange}/>
                </div>
                <div className='content-container-exercise-editor' hidden={createMode!="new-group"}>
                    <span className='span-exercise-editor'>Название:</span>
                    <input
                    id='edit-exercise-group-name'
                    className='input-exercise-editor'
                    type='text'
                    maxLength='50'
                    name='editGroupName'
                    value={editLocalData.editGroupName}
                    onChange={handleInputDataChange}/>
                </div>
                <div className='button-container-exercise-editor'>
                    <div className='plug-container-exercise-editor' hidden={isDataChanged}/>
                    <button id='save-button-exercise-editor' className='button-exercise-editor' onClick={handleSaveElementButton} hidden={!isDataChanged}>Сохранить</button>
                    <div className='plug-container-exercise-editor'/>
                    <button id='cancel-button-exercise-editor'className='button-exercise-editor' onClick={handleCanseledButton}>Отменить</button>
                </div>  
            </div>
        )
    }
}

export default ExerciseEditor;
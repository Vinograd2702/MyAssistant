import React from 'react';
import { useParams } from 'react-router';
import { useNavigate, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { getTemplateById, removeTemplateDetails, createTemplate, updateTemplate, deleteTemplate } from 'store/sport/templates/templatesSlice';
import { getExerciseGroupList, getExerciseTypeList } from 'store/sport/execises/execisesSlice'

import CardioTemplateBlock from 'components/blocks/templat-lookup-block/template-training-block/template-block-cardio/CardioTemplateBlock';
import StreightTemplateBlock from 'components/blocks/templat-lookup-block/template-training-block/template-block-streight/StreightTemplateBlock';
import SplitTemplateBlock from 'components/blocks/templat-lookup-block/template-training-block/template-block-split/SplitTemplateBlock';
import WarmUpTemplateBlock from 'components/blocks/templat-lookup-block/template-training-block/template-block-warmup/WarmUpTemplateBlock';

import './TemplateEditPage.css';

const TemplateEditPage = () => {

    const [isChangesMade, setIsChangesMade] = React.useState(false);

    const navigate = useNavigate();

    const dispatch = useDispatch();
    
    const params = useParams();
    const templateid = params.templateid;

    const [editTemplateData, setEditTemplateData] =  React.useState({
            id: templateid != undefined? templateid : null,
            name: "",
            description: "",
            templatesBlockCardioList: [],
            templatesBlockStrenghtList: [],
            templatesBlockSplitList: [],
            templatesBlockWarmUpList: []
        }
    );

    const fetchTemplateData = useSelector(state => state.templateDetails);

    const fetchExerciseTypeList = useSelector(state => state.typesLookup.ExerciseTypeList);

    React.useEffect( () => {
        if (templateid!=undefined)
        {
            if (fetchTemplateData.status === 'resolved' && fetchTemplateData.id === templateid) {

                var initCardioBlockList = [];

                fetchTemplateData.templatesBlockCardio.forEach(element => {
                    initCardioBlockList.push(element);
                });
    
                var initStrenghtBlockList = [];
    
                fetchTemplateData.templatesBlockStrenght.forEach(element => {
                    initStrenghtBlockList.push(element);
                });
    
                var initSplitBlockList = [];
    
                fetchTemplateData.templatesBlockSplit.forEach(element => {
                    initSplitBlockList.push(element);
                });
    
                var initWarmUpBlockList = [];
    
                fetchTemplateData.templatesBlockWarmUp.forEach(element => {
                    initWarmUpBlockList.push(element);
                });
    
                setEditTemplateData({
                    id: editTemplateData.id,
                    name: fetchTemplateData.name,
                    description: fetchTemplateData.description,
                    templatesBlockCardioList: initCardioBlockList,
                    templatesBlockStrenghtList: initStrenghtBlockList,
                    templatesBlockSplitList: initSplitBlockList,
                    templatesBlockWarmUpList: initWarmUpBlockList
                });

                setIsChangesMade(false);
            }
           
        }
    }, [fetchTemplateData]);

    const moveBlockInTemplate = (oldNumberInTemplate, newNumberInTemplate) => {

        setIsChangesMade(true);

        let templateBlocks = [];
        //!допушить кардио блоки

        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockCardioList);
        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockStrenghtList);
        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockSplitList);
        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockWarmUpList);

        const newTemplatesBlockCardioList = [];
        const newTemplatesBlockStrenghtList = [];
        const newTemplatesBlockSplitList = [];
        const newTemplatesBlockWarmUpList = [];

        // ищу элементы и обновляю их позицию, если нашел по всем спискам, потом их передаю в изменяемый шаблон
        editTemplateData.templatesBlockCardioList.forEach(element => {
            if (element.numberInTemplate === oldNumberInTemplate) {
                const editetElement = {
                    id: element.id,
                    numberInTemplate: newNumberInTemplate,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    parametrValue: element.parametrValue,
                    parametrName: element.parametrName,
                    secondsOfDuration: element.secondsOfDuration,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockCardioList.push(editetElement);
            }

            else if (element.numberInTemplate === newNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: oldNumberInTemplate,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    parametrValue: element.parametrValue,
                    parametrName: element.parametrName,
                    secondsOfDuration: element.secondsOfDuration,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockCardioList.push(editetElement);
            }

            else {
                newTemplatesBlockCardioList.push(element);
            }
        });

        editTemplateData.templatesBlockStrenghtList.forEach(element => {
            if (element.numberInTemplate === oldNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: newNumberInTemplate,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    numberOfSets: element.numberOfSets,
                    sets: element.sets,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockStrenghtList.push(editetElement);
            }

            else if (element.numberInTemplate === newNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: oldNumberInTemplate,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    numberOfSets: element.numberOfSets,
                    sets: element.sets,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockStrenghtList.push(editetElement);
            }

            else {
                newTemplatesBlockStrenghtList.push(element);
            }
        });

        editTemplateData.templatesBlockSplitList.forEach(element => {
            if (element.numberInTemplate === oldNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: newNumberInTemplate,
                    numberOfCircles: element.numberOfCircles,
                    exercises: element.exercises,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockSplitList.push(editetElement);
            }

            else if (element.numberInTemplate === newNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: oldNumberInTemplate,
                    numberOfCircles: element.numberOfCircles,
                    exercises: element.exercises,
                    secondsToRest: element.secondsToRest
                };
                
                newTemplatesBlockSplitList.push(editetElement);
            }

            else {
                newTemplatesBlockSplitList.push(element);
            }
        });

        editTemplateData.templatesBlockWarmUpList.forEach(element => {
            if (element.numberInTemplate === oldNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: newNumberInTemplate,
                    exercises: element.exercises
                };
                
                newTemplatesBlockWarmUpList.push(editetElement);
            }

            else if (element.numberInTemplate === newNumberInTemplate) {

                const editetElement = {
                    id: element.id,
                    numberInTemplate: oldNumberInTemplate,
                    exercises: element.exercises
                };
                
                newTemplatesBlockWarmUpList.push(editetElement);
            }

            else {
                newTemplatesBlockWarmUpList.push(element);
            }
        });

        setEditTemplateData({
            name: editTemplateData.name,
            description: editTemplateData.description,
            templatesBlockCardioList: newTemplatesBlockCardioList,
            templatesBlockStrenghtList: newTemplatesBlockStrenghtList,
            templatesBlockSplitList: newTemplatesBlockSplitList,
            templatesBlockWarmUpList: newTemplatesBlockWarmUpList
        });
    }

    const changeCardioBlockExersiseId = (numberInTemplate, newExersiseId) => {

        setIsChangesMade(true);

        const editTemplatesBlockCardioList = [];

        editTemplateData.templatesBlockCardioList.forEach(element => {
            if (element.numberInTemplate === numberInTemplate) {
                const editetElement = {
                    id: element.id,
                    numberInTemplate: element.numberInTemplate,
                    exerciseTypeId: newExersiseId,
                    exerciseType: fetchExerciseTypeList.find(e => e.id ===newExersiseId).name,
                    parametrValue: element.parametrValue,
                    parametrName: element.parametrName,
                    secondsOfDuration: element.secondsOfDuration,
                    secondsToRest: element.secondsToRest
                }

                editTemplatesBlockCardioList.push(editetElement);
            }
            else {
                editTemplatesBlockCardioList.push(element);
            }
        });

        setEditTemplateData({
            name: editTemplateData.name,
            description: editTemplateData.description,
            templatesBlockCardioList: editTemplatesBlockCardioList,
            templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
            templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
            templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
        });
    }

    const changeStrengeBlockExersiseId = (numberInTemplate, newExersiseId) => {

        setIsChangesMade(true);

        const editTemplatesBlockStrenghtList = [];

        editTemplateData.templatesBlockStrenghtList.forEach(element => {
            if (element.numberInTemplate === numberInTemplate) {
                const editetElement = {
                    id: element.id,
                    numberInTemplate: element.numberInTemplate,
                    exerciseTypeId: newExersiseId,
                    exerciseType: fetchExerciseTypeList.find(e => e.id ===newExersiseId).name,
                    numberOfSets: element.numberOfSets,
                    sets: element.sets,
                    secondsToRest: element.secondsToRest
                }

                editTemplatesBlockStrenghtList.push(editetElement);
            }
            else {
                editTemplatesBlockStrenghtList.push(element);
            }
        });

        setEditTemplateData({
            name: editTemplateData.name,
            description: editTemplateData.description,
            templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
            templatesBlockStrenghtList: editTemplatesBlockStrenghtList,
            templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
            templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
        });
    }

    const changeBlockSecondToRest = (blockType, numberInTemplate, newSecondToRestValue) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 1:
                editTemplateData.templatesBlockCardioList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            parametrValue: element.parametrValue,
                            parametrName: element.parametrName,
                            secondsOfDuration: element.secondsOfDuration,
                            secondsToRest: newSecondToRestValue
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: newTemplatesBlockTypeList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            case 2:
                editTemplateData.templatesBlockStrenghtList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            numberOfSets: element.numberOfSets,
                            sets: element.sets,
                            secondsToRest: newSecondToRestValue
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: newTemplatesBlockTypeList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            case 3:
                editTemplateData.templatesBlockSplitList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            numberOfCircles: element.numberOfCircles,
                            exercises: element.exercises,
                            secondsToRest: newSecondToRestValue
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: newTemplatesBlockTypeList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                
                break;
            
            default:
                break;
        }
    }

    const changeParametrValue = (blockType, numberInTemplate, parametrValue) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 1:
                editTemplateData.templatesBlockCardioList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            parametrValue: parametrValue,
                            parametrName: element.parametrName,
                            secondsOfDuration: element.secondsOfDuration,
                            secondsToRest: element.secondsToRest
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: newTemplatesBlockTypeList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            default:
                break;
        }
    }

    const changeParametrName = (blockType, numberInTemplate, parametrName) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 1:
                editTemplateData.templatesBlockCardioList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            parametrValue: element.parametrValue,
                            parametrName: parametrName,
                            secondsOfDuration: element.secondsOfDuration,
                            secondsToRest: element.secondsToRest
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: newTemplatesBlockTypeList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            default:
                break;
        }
    }

    const changeSecondsOfDuration = (blockType, numberInTemplate, secondsOfDuration) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 1:
                editTemplateData.templatesBlockCardioList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            parametrValue: element.parametrValue,
                            parametrName: element.parametrName,
                            secondsOfDuration: secondsOfDuration,
                            secondsToRest: element.secondsToRest
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: newTemplatesBlockTypeList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            default:
                break;
        }
    }

    const changeBlockSetsOrExerciseList = (blockType, numberInTemplate, newSetOrExerciseList) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 2:
                editTemplateData.templatesBlockStrenghtList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            numberOfSets: newSetOrExerciseList.length,
                            sets: newSetOrExerciseList,
                            secondsToRest: element.secondsToRest
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: newTemplatesBlockTypeList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });

                break;
            case 3:
                editTemplateData.templatesBlockSplitList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            numberOfCircles: element.numberOfCircles,
                            exercises: newSetOrExerciseList,
                            secondsToRest: element.secondsToRest
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: newTemplatesBlockTypeList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                
                break;
            
            case 4:
                editTemplateData.templatesBlockWarmUpList.forEach(element => {
                    if (element.numberInTemplate === numberInTemplate) {
                        newTemplatesBlockTypeList.push({
                            id: element.id,
                            numberInTemplate: element.numberInTemplate,
                            exercises: newSetOrExerciseList
                        });
                    }
                    else {
                        newTemplatesBlockTypeList.push(element);
                    }
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: newTemplatesBlockTypeList
                });
                
                break;
            
            default:
                break;
        }

    }

    const handleCanseled = () => {
        setIsChangesMade(false);
        dispatch(removeTemplateDetails());
        navigate('/template_creator')
    }

    const handleDeleteTemplate = async () => {
        setIsChangesMade(false);
        await dispatch(deleteTemplate({
            id: templateid
        }));
        navigate('/template_creator')
    }

    const addBlockToTemplateHandler = (blockType) => {

        setIsChangesMade(true);

        const newTemplatesBlockTypeList = [];
        
        switch (blockType) {
            case 1:
                editTemplateData.templatesBlockCardioList.forEach(element => {
                    newTemplatesBlockTypeList.push(element);
                });

                newTemplatesBlockTypeList.push({
                    id: null,
                    numberInTemplate: editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length,
                    exerciseTypeId: null,
                    exerciseType: null,
                    parametrValue: 0,
                    parametrName: '',
                    secondsOfDuration: 0,
                    secondsToRest: 0
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: newTemplatesBlockTypeList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                break;
            case 2:
                editTemplateData.templatesBlockStrenghtList.forEach(element => {
                    newTemplatesBlockTypeList.push(element);
                });

                newTemplatesBlockTypeList.push({
                    id: null,
                    numberInTemplate: editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length,
                    exerciseTypeId: null,
                    exerciseType: null,
                    numberOfSets: 0,
                    sets: [],
                    secondsToRest: 0
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: newTemplatesBlockTypeList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                break;
            case 3:
                editTemplateData.templatesBlockSplitList.forEach(element => {
                    newTemplatesBlockTypeList.push(element);
                });

                newTemplatesBlockTypeList.push({
                    id: null,
                    numberInTemplate: editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length,
                    numberOfCircles: 0,
                    exercises: [],
                    secondsToRest: 0
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: newTemplatesBlockTypeList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                break;
            case 4:
                editTemplateData.templatesBlockWarmUpList.forEach(element => {
                    newTemplatesBlockTypeList.push(element);
                });

                newTemplatesBlockTypeList.push({
                    id: null,
                    numberInTemplate: editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length,
                    exercises: []
                });

                setEditTemplateData({
                    name: editTemplateData.name,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: newTemplatesBlockTypeList
                });
                break;
            default:
                break;
        }
    }

    const deleteBlockFromTemplateHandler = (numberInTemplate) => {

        setIsChangesMade(true);

        const newTemplatesBlockCardioList = [];

        editTemplateData.templatesBlockCardioList.forEach(element => {
            if (element.numberInTemplate < numberInTemplate) {
                newTemplatesBlockCardioList.push(element);
            }
            else if (element.numberInTemplate > numberInTemplate) {
                newTemplatesBlockCardioList.push({
                    id: element.id,
                    numberInTemplate: element.numberInTemplate-1,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    parametrValue: element.parametrValue,
                    parametrName: element.parametrName,
                    secondsOfDuration: element.secondsOfDuration,
                    secondsToRest: element.secondsToRest
                });
            }
        });

        const newTemplatesBlockStrenghtList = [];

        editTemplateData.templatesBlockStrenghtList.forEach(element => {
            if (element.numberInTemplate < numberInTemplate) {
                newTemplatesBlockStrenghtList.push(element);
            }
            else if (element.numberInTemplate > numberInTemplate) {
                newTemplatesBlockStrenghtList.push({
                    id: element.id,
                    numberInTemplate: element.numberInTemplate-1,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    numberOfSets: element.numberOfSets,
                    sets: element.sets,
                    secondsToRest: element.secondsToRest
                });
            }
        });

        const newTtemplatesBlockSplitList = [];

        editTemplateData.templatesBlockSplitList.forEach(element => {
            if (element.numberInTemplate < numberInTemplate) {
                newTtemplatesBlockSplitList.push(element);
            }
            else if (element.numberInTemplate > numberInTemplate) {
                newTtemplatesBlockSplitList.push({
                    id: element.id,
                    numberInTemplate: element.numberInTemplate-1,
                    numberOfCircles: element.numberOfCircles,
                    exercises: element.exercises,
                    secondsToRest: element.secondsToRest
                });
            }
        });

        const newTemplatesBlockWarmUpList = [];

        editTemplateData.templatesBlockWarmUpList.forEach(element => {
            if (element.numberInTemplate < numberInTemplate) {
                newTemplatesBlockWarmUpList.push(element);
            }
            else if (element.numberInTemplate > numberInTemplate) {
                newTemplatesBlockWarmUpList.push({
                    id: element.id,
                    numberInTemplate: element.numberInTemplate-1,
                    exercises: element.exercises
                });
            }
        });

        setEditTemplateData({
            name: editTemplateData.name,
            description: editTemplateData.description,
            templatesBlockCardioList: newTemplatesBlockCardioList,
            templatesBlockStrenghtList: newTemplatesBlockStrenghtList,
            templatesBlockSplitList: newTtemplatesBlockSplitList,
            templatesBlockWarmUpList: newTemplatesBlockWarmUpList
        });
    }

    const handleChangeTemplateData = (e) => {
        const { name, value } = e.target;

        setIsChangesMade(true);

        switch (name) {
            case "name":
                setEditTemplateData({
                    name: value,
                    description: editTemplateData.description,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                
                break;

            case "description":
                setEditTemplateData({
                    name: editTemplateData.name,
                    description: value,
                    templatesBlockCardioList: editTemplateData.templatesBlockCardioList,
                    templatesBlockStrenghtList: editTemplateData.templatesBlockStrenghtList,
                    templatesBlockSplitList: editTemplateData.templatesBlockSplitList,
                    templatesBlockWarmUpList: editTemplateData.templatesBlockWarmUpList
                });
                
                break;
        
            default:
                break;
        }


    } 

    const getSortedTemplateBlock = () => {
        let templateBlocks = [];
        
        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockCardioList.map((block) => (
                <CardioTemplateBlock 
                key={block.numberInTemplate}
                numberInTemplate={block.numberInTemplate}
                exerciseTypeName={block.exerciseType}
                setExerciseTypeId={changeCardioBlockExersiseId}
                parametrValue={block.parametrValue}
                setParametrValue={changeParametrValue}
                parametrName={block.parametrName}
                setParametrName={changeParametrName}
                secondsOfDuration={block.secondsOfDuration}
                setSecondsOfDuration={changeSecondsOfDuration}
                secondsToRest={block.secondsToRest}
                setSecondToRest={changeBlockSecondToRest}
                moveBlockHandler={moveBlockInTemplate}
                isEndTemplateBlock={ block.numberInTemplate===(editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length - 1)}
                deleteBlockHandler={deleteBlockFromTemplateHandler}
                {...block} />
            ))
        )

        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockStrenghtList.map((block) => (
                <StreightTemplateBlock
                key={block.numberInTemplate}
                numberInTemplate={block.numberInTemplate}
                exerciseTypeName={block.exerciseType}
                setExerciseTypeId={changeStrengeBlockExersiseId}
                numberOfSets={block.numberOfSets}
                setsList={block.sets}
                setSetsList={changeBlockSetsOrExerciseList}
                secondsToRest={block.secondsToRest}
                setSecondToRest={changeBlockSecondToRest}
                moveBlockHandler={moveBlockInTemplate}
                isEndTemplateBlock={
                    block.numberInTemplate===(editTemplateData.templatesBlockCardioList.length
                    + editTemplateData.templatesBlockStrenghtList.length
                    + editTemplateData.templatesBlockSplitList.length
                    + editTemplateData.templatesBlockWarmUpList.length - 1)}
                deleteBlockHandler={deleteBlockFromTemplateHandler}
                {...block} />
            ))
        )

        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockSplitList.map((block) => (
                <SplitTemplateBlock
                key={block.numberInTemplate}
                numberInTemplate={block.numberInTemplate}
                exercises={block.exercises}
                setExercises={changeBlockSetsOrExerciseList}
                secondsToRest={block.secondsToRest}
                setSecondToRest={changeBlockSecondToRest}
                moveBlockHandler={moveBlockInTemplate}
                isEndTemplateBlock={
                    block.numberInTemplate===(editTemplateData.templatesBlockCardioList.length
                        + editTemplateData.templatesBlockStrenghtList.length
                        + editTemplateData.templatesBlockSplitList.length
                        + editTemplateData.templatesBlockWarmUpList.length - 1)}
                deleteBlockHandler={deleteBlockFromTemplateHandler}
                {...block} />
            ))
        )

        templateBlocks = templateBlocks.concat(
            editTemplateData.templatesBlockWarmUpList.map((block) => (
              <WarmUpTemplateBlock 
              key={block.numberInTemplate}
              numberInTemplate={block.numberInTemplate}
              exercises={block.exercises}
              setExercises={changeBlockSetsOrExerciseList}
              moveBlockHandler={moveBlockInTemplate}
              isEndTemplateBlock={
                block.numberInTemplate===(editTemplateData.templatesBlockCardioList.length
                + editTemplateData.templatesBlockStrenghtList.length
                + editTemplateData.templatesBlockSplitList.length
                + editTemplateData.templatesBlockWarmUpList.length - 1)}
              deleteBlockHandler={deleteBlockFromTemplateHandler}
              {...block} />  
            ))
        )

        templateBlocks.sort(((a, b) => a.key > b.key ? 1 : -1
        ));

        return templateBlocks;
    }

    const handleSaveTemplateChanges = async () => {

        if (templateid === undefined) {

            const templateToCreateResponce = {
                name: editTemplateData.name,
                description: editTemplateData.description,
                templatesBlockCardioList: [],
                templatesBlockStrenghtList: [],
                templatesBlockSplitList: [],
                templatesBlockWarmUpList: []
            };

            editTemplateData.templatesBlockCardioList.forEach(block => {
                templateToCreateResponce.templatesBlockCardioList.push({
                    numberInTemplate: block.numberInTemplate,
                    exerciseTypeId: block.exerciseTypeId,
                    parametrValue: block.parametrValue,
                    parametrName: block.parametrName,
                    secondsOfDuration: block.secondsOfDuration,
                    secondsToRest: block.secondsToRest
                }); 
            });

            editTemplateData.templatesBlockStrenghtList.forEach(block => {

                const setsList = [];

                block.sets.forEach(set => {
                    setsList.push({
                        setNumber: set.setNumber,
                        weight: set.weight,
                        reps: set.reps
                    });
                });

                templateToCreateResponce.templatesBlockStrenghtList.push({
                    numberInTemplate: block.numberInTemplate,
                    exerciseTypeId: block.exerciseTypeId,
                    numberOfSets: block.numberOfSets,
                    setsList: setsList,
                    secondsToRest: block.secondsToRest
                });  
            });

            editTemplateData.templatesBlockSplitList.forEach(block => {

                const exercisesInSplitList = [];

                block.exercises.forEach(exercise => {
                    exercisesInSplitList.push({
                        numberInSplit: exercise.numberInSplit,
                        exerciseTypeId: exercise.exerciseTypeId,
                        weight: exercise.weight,
                        reps: exercise.reps
                    });
                });

                templateToCreateResponce.templatesBlockSplitList.push({
                    numberInTemplate: block.numberInTemplate,
                    numberOfCircles: block.numberOfCircles,
                    exercisesInSplitList: exercisesInSplitList,
                    secondsToRest: block.secondsToRest
                });
            });

            editTemplateData.templatesBlockWarmUpList.forEach(block => {

                const exercisesInWarmUpList = [];

                block.exercises.forEach(exercise => {
                    exercisesInWarmUpList.push({
                        numberInWarmUp: exercise.numberInWarmUp,
                        exerciseTypeId: exercise.exerciseTypeId
                    });
                });

                templateToCreateResponce.templatesBlockWarmUpList.push({
                    numberInTemplate: block.numberInTemplate,
                    exercisesInWarmUpList: exercisesInWarmUpList
                });
            });

            await dispatch(createTemplate(templateToCreateResponce));

            navigate('/template_creator')
        }

        else {

            const templateToUpdateResponce = {
                id: templateid,
                name: editTemplateData.name,
                description: editTemplateData.description,
                templatesBlockCardioList: [],
                templatesBlockStrenghtList: [],
                templatesBlockSplitList: [],
                templatesBlockWarmUpList: []
            };

            editTemplateData.templatesBlockCardioList.forEach(block => {
                templateToUpdateResponce.templatesBlockCardioList.push({
                    numberInTemplate: block.numberInTemplate,
                    exerciseTypeId: block.exerciseTypeId,
                    parametrValue: block.parametrValue,
                    parametrName: block.parametrName,
                    secondsOfDuration: block.secondsOfDuration,
                    secondsToRest: block.secondsToRest
                }); 
            });

            editTemplateData.templatesBlockStrenghtList.forEach(block => {

                const setsList = [];

                block.sets.forEach(set => {
                    setsList.push({
                        setNumber: set.setNumber,
                        weight: set.weight,
                        reps: set.reps
                    });
                });

                templateToUpdateResponce.templatesBlockStrenghtList.push({
                    numberInTemplate: block.numberInTemplate,
                    exerciseTypeId: block.exerciseTypeId,
                    numberOfSets: block.numberOfSets,
                    setsList: setsList,
                    secondsToRest: block.secondsToRest
                });  
            });

            editTemplateData.templatesBlockSplitList.forEach(block => {

                const exercisesInSplitList = [];

                block.exercises.forEach(exercise => {
                    exercisesInSplitList.push({
                        numberInSplit: exercise.numberInSplit,
                        exerciseTypeId: exercise.exerciseTypeId,
                        weight: exercise.weight,
                        reps: exercise.reps
                    });
                });

                templateToUpdateResponce.templatesBlockSplitList.push({
                    numberInTemplate: block.numberInTemplate,
                    numberOfCircles: block.numberOfCircles,
                    exercisesInSplitList: exercisesInSplitList,
                    secondsToRest: block.secondsToRest
                });
            });

            editTemplateData.templatesBlockWarmUpList.forEach(block => {

                const exercisesInWarmUpList = [];

                block.exercises.forEach(exercise => {
                    exercisesInWarmUpList.push({
                        numberInWarmUp: exercise.numberInWarmUp,
                        exerciseTypeId: exercise.exerciseTypeId
                    });
                });

                templateToUpdateResponce.templatesBlockWarmUpList.push({
                    numberInTemplate: block.numberInTemplate,
                    exercisesInWarmUpList: exercisesInWarmUpList
                });
            });

            await dispatch(updateTemplate(templateToUpdateResponce));

            navigate('/template_creator')
        }

        setIsChangesMade(false);
    }

    const AddBlockButton = ({addBlockToTemplateHandler}) => {

        const [isOpenDialog, setIsOpenDialog] = React.useState(false);

        const selectTypeOfBlockHandler = (blockType) => {
            setIsOpenDialog(false);
            setIsChangesMade(true);
            addBlockToTemplateHandler(blockType);
        }

        if (isOpenDialog) {
            return (
                <div className='container-addblocktotemplate-tempedit'>
                    <div className='modal-container-blocktype-addblocktotemplate-tempedit'>
                        <div className='container-title-modal-container-blocktype-addblocktotemplate-tempedit'>
                            <h2 className='title-modal-container-blocktype-addblocktotemplate-tempedit'>Добавить</h2>
                            <button className='close-modal-container-blocktype-addblocktotemplate-tempedit' onClick={() => setIsOpenDialog(false)} >&#9747;</button>
                        </div>
                        <div className='select-blocktype-addblocktotemplate-tempedit'>
                            <button className='button-addblockblocktype-tempedit' onClick={() => selectTypeOfBlockHandler(1)}>кардио</button>
                            <button className='button-addblockblocktype-tempedit' onClick={() => selectTypeOfBlockHandler(2)}>упражнение</button>
                            <button className='button-addblockblocktype-tempedit' onClick={() => selectTypeOfBlockHandler(3)}>сплит</button>
                            <button className='button-addblockblocktype-tempedit' onClick={() => selectTypeOfBlockHandler(4)}>разминка</button>
                        </div>
                    </div>
                </div>
            )
        }
        else {
            return (
                <div className='container-addblocktotemplate-tempedit'>
                    <button className='button-addblocktotemplate-tempedit' onClick={()=>setIsOpenDialog(true)}>+</button>
                </div>
            )
        }
    }

    return(
        <div className="page-tempedit">
            <div className="head-tempedit">
                <div className="head-data-tempedit">
                    <input className='template-name-tempedit' value={editTemplateData.name} name="name" onChange={handleChangeTemplateData} placeholder='Введите наименование шаблона'></input>
                    <input className='template-description-tempedit' value={editTemplateData.description} name="description" onChange={handleChangeTemplateData}  placeholder='Описание'></input>
                </div>
                <button className='button-close-tempedit' onClick={handleCanseled}>&#9747;</button>
            </div>
            <div className='page-content-tempedit'>
                <div className='templates-block-list-tempedit'>
                    {getSortedTemplateBlock()}
                </div>
                <div className='futter-buttons-container-tempedit'>
                    <AddBlockButton addBlockToTemplateHandler={addBlockToTemplateHandler}/>
                    <div className='action-button-container-tempedit'>
                        <button className='button-delete-tempedit' onClick={handleDeleteTemplate} >&#9747;</button>
                        <button className='button-save-tempedit' onClick={handleSaveTemplateChanges} hidden={!isChangesMade}>&#10004;</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default TemplateEditPage;
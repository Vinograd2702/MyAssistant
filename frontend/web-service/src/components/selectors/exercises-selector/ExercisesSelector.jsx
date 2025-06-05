import React from 'react';
import { useDispatch, useSelector, shallowEqual } from 'react-redux';
import {getExerciseGroup, getExerciseGroupList, getExerciseType, getExerciseTypeList, removeTypeDetails, removeGroupDetails} from 'store/sport/execises/execisesSlice'
import ExerciseEditor from './ExerciseEditor'
import './ExercisesSelector.css';

const ExercisesSelector = ({setExericeType}) =>
{
    const dispatch = useDispatch();

    React.useEffect(() => {
        removeTypeDetails();
        removeGroupDetails();
    }, []);

    const [selectedExerciseTypeId, setSelectedExerciseTypeId] = React.useState(null);


    const compareSelectorFunctionBuCount = (oldValue, newValue) => {
        console.log(oldValue.lenght != newValue.lenght)
        console.log(oldValue.lenght)
        console.log(newValue.lenght)
        return oldValue.lenght != newValue.lenght;
    }

    const exersiseTypeList = useSelector(state => state.typesLookup.ExerciseTypeList, shallowEqual);
    const exersiseGroupList = useSelector(state => state.groupsLookup.ExerciseGroupList, shallowEqual);

    //вызов модального окна
    const [callExerciseEditor, setCallExerciseEditor]= React.useState(false);

    //редактор существующей сущности
    const [editElementId, setEditElementId]= React.useState(null);

    //добавление элемента в группу
    const [groupId, setGroupId]= React.useState(null);

    const initExercseTree = () => {

        let exercseDataTree = [];

        exersiseGroupList.forEach(element => {
            exercseDataTree.push({
                elementType: "group",
                id: element.id,
                name: element.name,
                parentId: element.parentGroupId
            })
        });

        exersiseTypeList.forEach(element => {
            exercseDataTree.push({
                elementType: "exersise",
                id: element.id,
                name: element.name,
                description: element.description,
                parentId: element.exerciseGroupId,
                isSelectedExersise: element.id === selectedExerciseTypeId
            })
        });

        return exercseDataTree;
    }

    let exercseTree = initExercseTree();

    const getTreeElementsChilds = (parentId, isHideChild, isRoot) => {
        
        let childsData = [];

        exercseTree.forEach(element => {
            if(element.parentId === parentId) {
                childsData.push(element);
            }
        });
        
        return(
            <div className={`childs-container-tree-element-exercises-selector${isRoot?" root-exercises-selector":" no-root-exercises-selector"}`} parent-id={parentId}>
                {childsData.map((treeElementData) => (
                    <TreeElement
                    key={treeElementData.id}
                    elementType={treeElementData.elementType}
                    id={treeElementData.id}
                    parentId={treeElementData.parentId}
                    name={treeElementData.name}
                    description={treeElementData.description}
                    isHide={isHideChild}
                    isSelectedExersise={treeElementData.isSelectedExersise}
                    {...treeElementData} />
                ))}
            </div>

        )
    }

    const editButtonHandler = (elementType, id, groupId) => {  
        setSelectedExerciseTypeId(null);
        if (elementType === "new-element") {
            //создание нового упражнения или группы
            setEditElementId(null);
            setGroupId(groupId);
            setCallExerciseEditor(elementType);
        }
        else if ((elementType === "group")||(elementType === "exersise")) {
            //редактор существующей группы или упражнения
            setEditElementId(id);
            setGroupId(null);
            setCallExerciseEditor(elementType);
        }
    }

    const useExerciseEditor = (callExerciseEditor, id) => {
        if (callExerciseEditor != false) {
            if((id != null)&&(callExerciseEditor === "group"))
            {
                dispatch(getExerciseGroup({groupId: id}));
            }
            else if ((id != null)&&(callExerciseEditor === "exersise")) {
                dispatch(getExerciseType({exerciseId: id}));
            }
            return(
                <ExerciseEditor elementType={callExerciseEditor} id={editElementId} groupId={groupId}/>
            );
        }
    }

    React.useEffect(() => {
        setCallExerciseEditor(false);

    }, [exersiseTypeList, exersiseGroupList])

    const handleSelectExersiseType = (exersiseTypeId) => {
        setSelectedExerciseTypeId(exersiseTypeId);
    }

    const selectExeciseTypeButtonHandler = () => {
        setExericeType(selectedExerciseTypeId);
    }

    const TreeElement = ({elementType, id, parentId, name, description, isHide, isSelectedExersise}) => {

        const [isExpand, setIsExpand] = React.useState(false);

        const changeExpandHandler = (e) => {
            setIsExpand(!isExpand);
        }

        if(elementType==="group") {
            return (
                <div element-type={elementType} id={id} parent-id={parentId} hidden={isHide} className='tree-element-exercises-selector'>
                    <button className={`${isExpand?"collapse":"expand"}-tree-element-exercises-selector`} onClick={changeExpandHandler}>{isExpand?<>&#9660;</>:<>&#9658;</>}</button>
                    <span className='group-name-tree-element-exercises-selector'>{name}</span>
                    <button className='edit-tree-element-exercises-selector' hidden={callExerciseEditor} onClick={() => editButtonHandler(elementType, id)}>&#9998;</button>
                    <button className='add-element-exercises-selector' onClick={() => editButtonHandler("new-element", null, id)}>+</button>
                    {getTreeElementsChilds(id, !isExpand)}
                </div>
            )
        }

        else if (elementType==="exersise") {
            return (
                <div element-type={elementType} id={id} parent-id={parentId} hidden={isHide} className={'tree-element-exercises-selector' + (isSelectedExersise?" selected-item-exercises-selector":"")} onClick={()=>handleSelectExersiseType(id)}>
                    <span className='exersise-name-tree-element-exercises-selector'>{name}</span>
                    <button className='edit-tree-element-exercises-selector' hidden={callExerciseEditor} onClick={() => editButtonHandler(elementType, id)}>&#9998;</button>
                    <span className='exersise-description-tree-element-exercises-selector' >{description}</span>
                    {getTreeElementsChilds(id, !isExpand)}
                </div>
            )
        }

        else{
            return;
        }
    }

    return(
        <div className='container-exercises-selector'>
            <button className='add-element-exercises-selector' onClick={() => editButtonHandler("new-element")}>+</button>
            {getTreeElementsChilds(null, false, true)}
            <div className='selected-button-container-exercises-selector'>
                <button className='select-button-exercise-exercises-selector' hidden={selectedExerciseTypeId===null} onClick={selectExeciseTypeButtonHandler}>Выбор</button>
            </div>
            {useExerciseEditor(callExerciseEditor, editElementId)}
        </div>
    );
}


export default ExercisesSelector;
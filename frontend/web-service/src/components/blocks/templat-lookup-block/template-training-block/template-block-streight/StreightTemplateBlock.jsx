import React from 'react';

import ExercisesSelector from 'components/selectors/exercises-selector/ExercisesSelector'

import './StreightTemplateBlock.css'

const StreightTemplateBlock = ({numberInTemplate, 
    exerciseTypeName, setExerciseTypeId, 
    numberOfSets, setsList, setSetsList, 
    secondsToRest, setSecondToRest, 
    moveBlockHandler, isEndTemplateBlock, deleteBlockHandler}) =>
{
    const [isOpenExerciceTypeSelector, setIsOpenExerciceTypeSelector] = React.useState(false);

    const callExercisesSelector = () =>
    {
        setIsOpenExerciceTypeSelector(true);
    }

    const selectExersiseForBlock = (selectedExesiseType) => {
        setExerciseTypeId(numberInTemplate, selectedExesiseType);
        setIsOpenExerciceTypeSelector(false);

    }

    const changeValueSecondToRestHandler = (e) => {
        var numValue = Number(e.target.value);
        if(numValue<0) {numValue = numValue*(-1)}
        if(numValue>3600) {numValue = 3600}
        e.target.value = numValue;
        setSecondToRest(2, numberInTemplate, numValue);
    }

    const changeSetListHandle = (e) => {
        const { id, name } = e.target;

        const newSetList = [];

        var numValue = Number(e.target.value);
        if(numValue<0) {numValue = numValue*(-1)}
        if(numValue>500) {numValue = 500}

        setsList.forEach(element => {
            if(element.setNumber===Number(id)){
                switch (name) {
                    case "weight":
                        newSetList.push({
                            id: element.id,
                            reps: element.reps,
                            weight: numValue,
                            setNumber: element.setNumber
                        });
                        break;
        
                    case "reps":
                        newSetList.push({
                            id: element.id,
                            reps: numValue,
                            weight: element.weight,
                            setNumber: element.setNumber
                        });
                        break;
                
                    default:
                        break;

                }

            } else {
                newSetList.push(element);
            }
        });
        setSetsList(2, numberInTemplate, newSetList);
    }

    const addSetToSetList = () => {
        const newSetList = [];
        setsList.forEach(element => {
            newSetList.push(element);
        });

        newSetList.push({
            id: null,
            reps: 0,
            weight: 0,
            setNumber: numberOfSets
        });
        setSetsList(2, numberInTemplate, newSetList);
    }

    const removeLastSetFromSetList = () => {
        const newSetList = [];
        setsList.forEach(element => {
            newSetList.push(element);
        });

        newSetList.pop();

        setSetsList(2, numberInTemplate, newSetList);
    }

    const SetBlock = ({id, reps, setNumber, weight}) => {
        return(
            <div className='container-set-streight-template-block'>
                <div className='container-param-set-streight-template-block'>
                    <div className='container-property-set-streight-template-block'> 
                        <span>вес</span>
                    </div>
                    <input className="input-set-streight-template-block" type="number" min="0" step="5" value={weight} name="weight" id={setNumber} onChange={changeSetListHandle}/>
                </div>
                <div className='container-param-set-streight-template-block'>
                    <div className='container-property-set-streight-template-block'> 
                        <span>повторы</span>
                    </div>
                    <input className="input-set-streight-template-block" type="number" min="0" value={reps} name="reps" id={setNumber}  onChange={changeSetListHandle}/>
                </div>
            </div>
        )
    }

    return(
    <div className='container-streight-template-block'>
        <div className='container-title-streight-template-block'>
            <div className='container-move-block-buttons-streight-template-block'>
                <div className='plug-move-button-block-streight-template-block' hidden={numberInTemplate!=0}/>
                <button className='move-button-block-streight-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate-1)} hidden={numberInTemplate===0}>&#9650;</button>
                <button className='move-button-block-streight-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate+1)} hidden={isEndTemplateBlock}>&#9660;</button>
            </div>
            <h2 className='title-streight-template-block' onClick={callExercisesSelector}>{exerciseTypeName==null?"Выберите тип упражнения":exerciseTypeName}</h2>
            <button className='delete-button-block-streight-template-block' onClick={() => deleteBlockHandler(numberInTemplate)}>X</button>
        </div>
        {isOpenExerciceTypeSelector?<div>
            <hr className='hr-streight-template-block'></hr>
            <ExercisesSelector setExericeType={selectExersiseForBlock} setIsOpenExerciceTypeSelector={setIsOpenExerciceTypeSelector}/>
        </div>: null}
        <hr className='hr-streight-template-block'></hr>
        <div className='container-numberofsets-streight-template-block'>
            <div className='container-property-streight-template-block'>
                <span>подходы</span>
            </div>
            <span className='value-numberofsets-streight-template-block'>{numberOfSets}</span>
            <button className='add-button-numberofsets-streight-template-block' onClick={addSetToSetList}>+</button>
            <button className='reduce-button-numberofsets-streight-template-block' hidden={numberOfSets===0} onClick={removeLastSetFromSetList}>-</button>
            <div className='plug-numberofsets-streight-template-block'  hidden={numberOfSets!=0} />
            
        </div>
        <div className='container-sets-list-streight-template-block'>
            {setsList.map((set) => (
                <SetBlock
                key={set.setNumber}
                id={set.id}
                reps={set.reps}
                setNumber={set.setNumber}
                weight={set.weight}
                {...set}/>

            ))}
        </div>        
        <div className='container-resttime-streight-template-block'>
            <div className='container-property-streight-template-block'>
                <span>перерыв</span>
            </div>
            <input className='input-resttime-streight-template-block' type="number" value={secondsToRest} min="0" max="3600" step="10" onChange={changeValueSecondToRestHandler}></input>
            <span>сек</span>
        </div>
    </div>);
}

export default StreightTemplateBlock;
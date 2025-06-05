import React from 'react';

import ExercisesSelector from 'components/selectors/exercises-selector/ExercisesSelector'

import './CardioTemplateBlock.css'

const CardioTemplateBlock = ({numberInTemplate, 
    exerciseTypeName, setExerciseTypeId,
    parametrValue, setParametrValue,
    parametrName, setParametrName,
    secondsOfDuration, setSecondsOfDuration,
    secondsToRest, setSecondToRest, 
    moveBlockHandler, isEndTemplateBlock, deleteBlockHandler }) => 
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
        setSecondToRest(1, numberInTemplate, numValue);
    }

    const changeParametrValue = (e) => {
        setParametrValue(1, numberInTemplate, e.target.value);
    }

    const changeParametrName  = (e) => {
        setParametrName(1, numberInTemplate, e.target.value);
    }

    const changeSecondsOfDuration  = (e) => {
        var numValue = Number(e.target.value);
        if(numValue<0) {numValue = numValue*(-1)}
        if(numValue>3600) {numValue = 3600}
        e.target.value = numValue;
        setSecondsOfDuration(1, numberInTemplate, numValue);
    }

    return(
        <div className='container-cardio-template-block'>
            <div className='container-title-cardio-template-block'>
                <div className='container-move-block-buttons-cardio-template-block'>
                    <div className='plug-move-button-block-cardio-template-block' hidden={numberInTemplate!=0}/>
                    <button className='move-button-block-cardio-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate-1)} hidden={numberInTemplate===0}>&#9650;</button>
                    <button className='move-button-block-cardio-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate+1)} hidden={isEndTemplateBlock}>&#9660;</button>
                </div>
                <h2 className='title-cardio-template-block' onClick={callExercisesSelector}>{exerciseTypeName==null?"Выберите тип упражнения":exerciseTypeName}</h2>
                <button className='delete-button-block-cardio-template-block' onClick={() => deleteBlockHandler(numberInTemplate)}>X</button>
            </div>
            {isOpenExerciceTypeSelector?<div>
                <hr className='hr-cardio-template-block'></hr>
                <ExercisesSelector setExericeType={selectExersiseForBlock} setIsOpenExerciceTypeSelector={setIsOpenExerciceTypeSelector}/>
            </div>: null}
            <hr className='hr-cardio-template-block'></hr>
            <div className='container-parameter-cardio-template-block'>
                <div className='container-property-cardio-template-block'>
                    <span>параметр</span>
                </div>
                <input className='input-parametername-cardio-template-block' type="text" value={parametrName} onChange={changeParametrName}></input>
            </div>

            <div className='container-parameter-cardio-template-block'>
                <div className='container-property-cardio-template-block'>
                    <span>значение</span>
                </div>
                <input className='input-parametervalue-cardio-template-block' type="number" value={parametrValue} onChange={changeParametrValue}></input>
            </div>

            <div className='container-parameter-cardio-template-block'>
                <div className='container-property-cardio-template-block'>
                    <span>выполнение</span>
                </div>
                <input className='input-secondsofduration-cardio-template-block' type="number" value={secondsOfDuration} min="0" max="3600" step="10" onChange={changeSecondsOfDuration}></input>
                <span>сек</span>
            </div>


            <div className='container-parameter-cardio-template-block'>
                <div className='container-property-cardio-template-block'>
                    <span>перерыв</span>
                </div>
                <input className='input-resttime-cardio-template-block' type="number" value={secondsToRest} min="0" max="3600" step="10" onChange={changeValueSecondToRestHandler}></input>
                <span>сек</span>
            </div>
        </div>
    );
}

export default CardioTemplateBlock;
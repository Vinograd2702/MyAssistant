import React from 'react';
import { useSelector } from 'react-redux';

import ExercisesSelector from 'components/selectors/exercises-selector/ExercisesSelector'

import './SplitTemplateBlock.css'

const SplitTemplateBlock = ({numberInTemplate,
    exercises, setExercises,
    secondsToRest, setSecondToRest, 
    moveBlockHandler, isEndTemplateBlock, deleteBlockHandler}) =>
{

    const exersiseList = useSelector(state => state.typesLookup.ExerciseTypeList);
    // идентификатор позцици упражнения в котором вызвали селектор
    const [numberElementWhereOpenExerciceTypeSelector, setNumberElementWhereOpenExerciceTypeSelector] = React.useState(null);

    // обработчик выбора упражнения из селектора. через позицию выбранного блока и ИД упражнения заменяет
    // упражнение на упражнение с новым типом (в конце передает в родителя оновый массив упражнений)
    const selectExersiseTypeForItem = (numberInSplit, exerciseTypeId) => {
        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInSplit === numberInSplit) {
                updateExersiseList.push({
                    id: element.id,
                    numberInSplit: element.numberInSplit,
                    exerciseTypeId: exerciseTypeId,
                    exerciseType: exersiseList.find(e => e.id ===exerciseTypeId).name,
                    weight: element.weight,
                    reps: element.reps
                });
            }
            else {
                updateExersiseList.push(element);
            }
        });

        setExercises(3, numberInTemplate, updateExersiseList);
    }

    // перемешение упражнения в блоке
    const moveExerciseInBlockHandler = (oldNumberInSplit, newNumberInSplit) => {

        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInSplit === oldNumberInSplit)
            {
                updateExersiseList.push({
                    id: element.id,
                    numberInSplit: newNumberInSplit,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    weight: element.weight,
                    reps: element.reps
                });
            }
            else if (element.numberInSplit === newNumberInSplit) {
                updateExersiseList.push({
                    id: element.id,
                    numberInSplit: oldNumberInSplit,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    weight: element.weight,
                    reps: element.reps
                });
            }
            else {
                updateExersiseList.push(element);
            }
        });

        setExercises(3, numberInTemplate, updateExersiseList);
    }

    // добавление упражнения в блок
    const addExerciseInBlockHandler = () => {
        
        const updateExersiseList = [];

        exercises.forEach(element => {
            updateExersiseList.push(element);
        });

        updateExersiseList.push({
            id: null,
            numberInSplit: exercises.length,
            exerciseTypeId: null,
            exerciseType: null,
            weight: 0,
            reps: 0
        });

        setExercises(3, numberInTemplate, updateExersiseList);
    }

    //удаление упражнения из блока
    const deleteExercisefromBlockHandler = numberInSplit => {

        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInSplit < numberInSplit) {
                updateExersiseList.push(element);
            }
            else if (element.numberInSplit > numberInSplit) {
                updateExersiseList.push({
                    id: element.id,
                    numberInSplit: element.numberInSplit-1,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType,
                    weight: element.weight,
                    reps: element.reps
                });
            }
        });

        setExercises(3, numberInTemplate, updateExersiseList);
    }

    // изменение времени отдыха
    const changeValueSecondToRestHandler = (e) => {
        var numValue = Number(e.target.value);
        if(numValue<0) {numValue = numValue*(-1)}
        if(numValue>3600) {numValue = 3600}
        e.target.value = numValue;
        setSecondToRest(3, numberInTemplate, numValue);
    }

    //изменение поля веса или повторов в упражнении сплита
    const changeExersiseDataHandler = (e) => {
        const { id, name } = e.target;

        const updateExerciseList = [];

        var numValue = Number(e.target.value);
        if(numValue<0) {numValue = numValue*(-1)}
        if(numValue>500) {numValue = 500}

        exercises.forEach(element => {
            if(element.numberInSplit===Number(id)){
                switch (name) {
                    case "weight":
                        updateExerciseList.push({
                            id: element.id,
                            numberInSplit: element.numberInSplit,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            weight: numValue,
                            reps: element.reps
                        });
                        break;
        
                    case "reps":
                        updateExerciseList.push({
                            id: element.id,
                            numberInSplit: element.numberInSplit,
                            exerciseTypeId: element.exerciseTypeId,
                            exerciseType: element.exerciseType,
                            weight: element.weight,
                            reps: numValue
                        });
                        break;
                
                    default:
                        break;

                }

            } else {
                updateExerciseList.push(element);
            }
        });

        setExercises(3, numberInTemplate, updateExerciseList);
    }

    const getSortedExercises = () => {
        let sortedExersices = [];

        sortedExersices = exercises.map((exercise) => (
            <ExerciseBlock
            key={exercise.numberInSplit} 
            id={exercise.id}
            exerciseTypeName={exercise.exerciseType}
            weight={exercise.weight}
            reps={exercise.reps}
            isEndExerciseInBlock={exercise.numberInSplit===exercises.length-1}
            {...exercise}/>
        ));

        sortedExersices.sort(((a, b) => a.key > b.key ? 1 : -1));
        
        return sortedExersices;
    }

    const ExerciseBlock = ({id, numberInSplit, 
        exerciseTypeName,
        weight, reps,
        isEndExerciseInBlock}) => {

        // функция блока, вызывающая в нем селектор
        const callExersiseSelector = () => {
            setNumberElementWhereOpenExerciceTypeSelector(numberInSplit);
        }

        // передает id упражнения родителю и закрываеи селектор
        const selectExersiseForBlockItem = (selectedExesiseTypeId) => {
            selectExersiseTypeForItem(numberInSplit, selectedExesiseTypeId);
            setNumberElementWhereOpenExerciceTypeSelector(null);
        }

        return(
            <div className='container-exercise-template-block'>
                <div className='exercise-template-block'>   
                    <div className='container-move-exercise-buttons-split-template-block'>
                        <div className='plug-move-button-exercise-buttons-split-template-block' hidden={numberInSplit!=0}/>
                        <button className='move-button-exercise-buttons-split-template-block' onClick={() => moveExerciseInBlockHandler(numberInSplit, numberInSplit-1)} hidden={numberInSplit===0}>&#9650;</button>
                        <button className='move-button-exercise-buttons-split-template-block' onClick={() => moveExerciseInBlockHandler(numberInSplit, numberInSplit+1)} hidden={isEndExerciseInBlock}>&#9660;</button>
                    </div>
                    <div className='container-properties-split-template-block'>
                        <div className='container-exercise-split-template-block'>
                            <h2 className='title-exercise-split-template-block' onClick={() => callExersiseSelector()}>{exerciseTypeName==null?"Выберите тип упражнения":exerciseTypeName}</h2>
                            <button className='delete-exercise-split-template-block' onClick={() => deleteExercisefromBlockHandler(numberInSplit)}>X</button> 
                        </div>
                        {numberElementWhereOpenExerciceTypeSelector === numberInSplit
                        ?<ExercisesSelector setExericeType={selectExersiseForBlockItem} />
                        :null}
                        <div className='container-propertiey-split-template-block'>
                            <div className='container-property-title-exercise-split-template-block'> 
                                <span>вес</span>
                            </div>
                            <input className="input-exercise-split-template-block" type="number" min="0" step="5" value={weight} name="weight" id={numberInSplit} onChange={changeExersiseDataHandler}/>
                        </div>
                        <div className='container-propertiey-split-template-block'>
                            <div className='container-property-title-exercise-split-template-block'> 
                                <span>повторы</span>
                            </div>
                            <input className="input-exercise-split-template-block" type="number" min="0" value={reps} name="reps" id={numberInSplit}  onChange={changeExersiseDataHandler}/>
                        </div>
                    </div>                    
                </div>
            </div>
        );

    }

    return (
        <div className='container-split-template-block'>
            <div className='container-move-block-buttons-split-template-block'>
                <div className='plug-move-button-block-buttons-split-template-block' hidden={numberInTemplate!=0}/>
                <button className='move-button-block-buttons-split-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate-1)} hidden={numberInTemplate===0}>&#9650;</button>
                <button className='move-button-block-buttons-split-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate+1)} hidden={isEndTemplateBlock}>&#9660;</button>
            </div>
            <div className='content-container-split-template-block'>
                <button className='delete-button-block-split-template-block' onClick={() => deleteBlockHandler(numberInTemplate)}>X</button>
                <div className='exercise-list-container-split-template-block'>
                   {getSortedExercises()}
                    <button className='add-exersise-button-block-buttons-split-template-block' onClick={addExerciseInBlockHandler}>+</button>
                </div>
                <div className='container-resttime-split-template-block'>
                    <div className='container-property-split-template-block'>
                        <span>перерыв</span>
                    </div>
                    <input className='input-resttime-split-template-block' type="number" value={secondsToRest} min="0" max="3600" step="10" onChange={changeValueSecondToRestHandler}></input>
                    <span>сек</span>
                </div>
            </div>
        </div>
    );
}

export default SplitTemplateBlock;
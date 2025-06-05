import React from 'react';
import { useSelector } from 'react-redux';


import ExercisesSelector from 'components/selectors/exercises-selector/ExercisesSelector'

import './WarmUpTemplateBlock.css'

const WarmUpTemplateBlock = ({numberInTemplate,
    exercises, setExercises,
    moveBlockHandler, isEndTemplateBlock, deleteBlockHandler}) =>
{

    const exersiseList = useSelector(state => state.typesLookup.ExerciseTypeList);
    // идентификатор позцици упражнения в котором вызвали селектор
    const [numberElementWhereOpenExerciceTypeSelector, setNumberElementWhereOpenExerciceTypeSelector] = React.useState(null);
    
    // обработчик выбора упражнения из селектора. через позицию выбранного блока и ИД упражнения заменяет
    // упражнение на упражнение с новым типом (в конце передает в родителя оновый массив упражнений)
    const selectExersiseTypeForItem = (numberInWarmUp, exerciseTypeId) => {
        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInWarmUp === numberInWarmUp) {
                updateExersiseList.push({
                    id: element.id,
                    numberInWarmUp: element.numberInWarmUp,
                    exerciseTypeId: exerciseTypeId,
                    exerciseType: exersiseList.find(e => e.id ===exerciseTypeId).name
                });
            }
            else {
                updateExersiseList.push(element);
            }
        });

        setExercises(4, numberInTemplate, updateExersiseList);
    }

    // перемешение упражнения в блоке
    const moveExerciseInBlockHandler = (oldNumberInWarmUp, newNumberInWarmUp) => {

        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInWarmUp === oldNumberInWarmUp)
            {
                updateExersiseList.push({
                    id: element.id,
                    numberInWarmUp: newNumberInWarmUp,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType
                });
            }
            else if (element.numberInWarmUp === newNumberInWarmUp) {
                updateExersiseList.push({
                    id: element.id,
                    numberInWarmUp: oldNumberInWarmUp,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType
                });
            }
            else {
                updateExersiseList.push(element);
            }
        });

        setExercises(4, numberInTemplate, updateExersiseList);
    }

    // добавление упражнения в блок
    const addExerciseInBlockHandler = () => {
        
        const updateExersiseList = [];

        exercises.forEach(element => {
            updateExersiseList.push(element);
        });

        updateExersiseList.push({
            id: null,
            numberInWarmUp: exercises.length,
            exerciseTypeId: null,
            exerciseType: null
        });

        setExercises(4, numberInTemplate, updateExersiseList);
    }

    //удаление упражнения из блока
    const deleteExercisefromBlockHandler = numberInWarmUp => {

        const updateExersiseList = [];

        exercises.forEach(element => {
            if (element.numberInWarmUp < numberInWarmUp) {
                updateExersiseList.push(element);
            }
            else if (element.numberInWarmUp > numberInWarmUp) {
                updateExersiseList.push({
                    id: element.id,
                    numberInWarmUp: element.numberInWarmUp-1,
                    exerciseTypeId: element.exerciseTypeId,
                    exerciseType: element.exerciseType
                });
            }
        });

        setExercises(4, numberInTemplate, updateExersiseList);
    }

    const getSortedExercises = () => {
        let sortedExersices = [];

        sortedExersices = exercises.map((exersise) => (
            <ExerciseBlock
            key={exersise.numberInWarmUp} 
            id={exersise.id}
            numberInWarmUp={exersise.numberInWarmUp}
            exerciseTypeName={exersise.exerciseType}
            setExerciseTypeId={selectExersiseTypeForItem}
            isEndExersiseInBlock={exersise.numberInWarmUp===exercises.length-1}
            {...exersise}/>
        ));

        sortedExersices.sort(((a, b) => a.key > b.key ? 1 : -1));
        
        return sortedExersices;
    }

    // Элемент упражнения (работает в двух режимах, в режиме отображения упражнения и в режиме выбора типа упр. (покахывает селектор))
    const ExerciseBlock = ({id, numberInWarmUp, exerciseTypeName, setExerciseTypeId, isEndExersiseInBlock}) => {

        // функция блока, вызывающая в нем селектор
        const callExersiseSelector = () => {
            setNumberElementWhereOpenExerciceTypeSelector(numberInWarmUp);
        }

        // передает id упражнения родителю и закрываеи селектор
        const selectExersiseForBlockItem = (selectedExesiseTypeId) => {
            setExerciseTypeId(numberInWarmUp, selectedExesiseTypeId);
            setNumberElementWhereOpenExerciceTypeSelector(null);
        }

        return(
            <div className='container-exercise-template-block'>
                <div className='exercise-template-block'>
                    <div className='container-move-exercise-buttons-warmup-template-block'>
                        <div className='plug-move-button-exercise-buttons-warmup-template-block' hidden={numberInWarmUp!=0}/>
                        <button className='move-button-exercise-buttons-warmup-template-block' onClick={() => moveExerciseInBlockHandler(numberInWarmUp, numberInWarmUp-1)} hidden={numberInWarmUp===0}>&#9650;</button>
                        <button className='move-button-exercise-buttons-warmup-template-block' onClick={() => moveExerciseInBlockHandler(numberInWarmUp, numberInWarmUp+1)} hidden={isEndExersiseInBlock}>&#9660;</button>
                    </div>
                    <h2 className='title-exercise-warmup-template-block' onClick={() => callExersiseSelector()}>{exerciseTypeName==null?"Выберите тип упражнения":exerciseTypeName}</h2>
                    <button className='delete-exercise-warmup-template-block' onClick={() => deleteExercisefromBlockHandler(numberInWarmUp)}>X</button>   
                </div>
                {numberElementWhereOpenExerciceTypeSelector === numberInWarmUp
                ?<ExercisesSelector setExericeType={selectExersiseForBlockItem} />
                :null}
            </div>
        );
    }

    return (
        <div className='container-warmup-template-block'>
            <div className='container-move-block-buttons-warmup-template-block'>
                <div className='plug-move-button-block-buttons-warmup-template-block' hidden={numberInTemplate!=0}/>
                <button className='move-button-block-buttons-warmup-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate-1)} hidden={numberInTemplate===0}>&#9650;</button>
                <button className='move-button-block-buttons-warmup-template-block' onClick={() => moveBlockHandler(numberInTemplate, numberInTemplate+1)} hidden={isEndTemplateBlock}>&#9660;</button>
            </div>
            <div className='content-container-warmup-template-block'>
                <button className='delete-button-block-warmup-template-block' onClick={() => deleteBlockHandler(numberInTemplate)}>X</button>
                <div className='exercise-list-container-warmup-template-block'>
                   {getSortedExercises()}
                    <button className='add-exersise-button-block-buttons-warmup-template-block' onClick={addExerciseInBlockHandler}>+</button>
                </div>
            </div>
        </div>
    );
}

export default WarmUpTemplateBlock;
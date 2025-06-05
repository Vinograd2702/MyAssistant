import React from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { getTempleteList, getTemplateById } from 'store/sport/templates/templatesSlice';
import TemplateLookupBlock from 'components/blocks/templat-lookup-block/TemplateLookupBlock';

import { getExerciseGroupList, getExerciseTypeList } from 'store/sport/execises/execisesSlice'

import './TemplateCreatorPage.css';

const TemplateCreatorPage = () => {
    
    const navigate = useNavigate();
    const location = useLocation();

    const dispatch = useDispatch();


    React.useState( async () => {
        await dispatch(getTempleteList());
    }, []);

    const templateLookupdtoList = useSelector(state => state.templatesLookup.templateLookupDto);

    const moveToTemplateUpdateHandler = async (templateId) => {
        await dispatch(getExerciseGroupList());
        await dispatch(getExerciseTypeList());
        await dispatch(getTemplateById({templateId: templateId}));
        navigate('/template_edit/'+ templateId);
    }

    const moveToTemplateCreateHandler = () => {
        navigate('/template_edit');
    }

    return(
        <div className="page-tempcreate">
            <div className="head-tempcreate">
                <h1>Шаблоны тренировок</h1> 
            </div>
            <div className='page-content-tempcreate'>
                <div className='user-templates-list-tempcreate'>
                    {templateLookupdtoList.map((templateItem) => (
                        <TemplateLookupBlock 
                        key = {templateItem.id} 
                        templateId = {templateItem.id} 
                        name={templateItem.name} 
                        description={templateItem.description} 
                        {...templateItem} 
                        handle={moveToTemplateUpdateHandler}/> 
                    ))}

                </div>
                <button className='add-template-button-tempcreate' onClick={moveToTemplateCreateHandler}>Добавить шаблон тренировки</button>
            </div>
        </div>
    )
}

export default TemplateCreatorPage;

import './TemplateLookupBlock.css';

const TemplatLookupBlock = ({templateId, name, description, handle}) => {
    
    return(
        <div className="template-lookup-block" templateid={templateId}
        onClick={() => handle(templateId)}>
            <h1 className='title-template-lookup-block'>{name}</h1>
            <hr className='hr-template-lookup-block'></hr>
            <h2 className='description-template-lookup-block'>{description}</h2>
        </div>
    )
}

export default TemplatLookupBlock;
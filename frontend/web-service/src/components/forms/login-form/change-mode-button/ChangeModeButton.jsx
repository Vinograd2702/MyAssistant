import './ChangeModeButton.css';

const ChangeModeButton = ({id, text, handle, setedMode, activeMode}) => {
    
    const checkActive = (setedMode, activeMode) => {
        if(setedMode === activeMode)
        {
            return "is-active";
        }
        else
        {
            return "is-inactive";
        }
    }

    return(

        <button id={id} className={"button-block " + checkActive(setedMode, activeMode)} onClick={() => handle(setedMode)}>{text}</button>
    );
}

export default ChangeModeButton;
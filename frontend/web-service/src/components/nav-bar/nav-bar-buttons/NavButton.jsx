import './NavButton.css'

const NavButton = ({id, isActivePage, handle, redirectRoute, srcSvg}) => {
    return(
        <div>
            <button id={id} className={'home-nav-button '+ isActivePage(redirectRoute)}
             onClick={() => handle(redirectRoute)}>
                <img src={srcSvg} alt={redirectRoute} width="40px" height="40px" />
            </button>
        </div>
    )
}

export default NavButton;
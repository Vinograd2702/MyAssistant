import './LoginInput.css';

const LoginInput = ({id, type, placeholder, value, setValue}) => {
    
    const handleChange = (e) => {
        const { id, value } = e.target;
        console.log(value);
        console.log(id);
        setValue(value)
    }


    return(
        <input id={id}
        type={type}
        className="input-block" 
        placeholder={placeholder}
        value={value}
        onChange={handleChange} />
    );
}

export default LoginInput;
import { Routes, Route, Link } from 'react-router-dom';

//Views
import MainView from './views/MainView'; 
import LoginView from './views/LoginView'; 

function App() {
    return(
        <div>
            <h1>App</h1>
            <Routes>
                <Route path="/" element={<MainView />} />
                <Route path="/Login" element={<LoginView />} />
            </Routes>     
        </div>
    );
}

export default App;
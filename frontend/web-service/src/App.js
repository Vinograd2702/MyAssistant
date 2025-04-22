import { Routes, Route, Link } from 'react-router-dom';

//Views
import MainView from './views/MainView'; 
import LoginView from './views/LoginView'; 

function App() {
    return(
        <Routes>
            <Route path="/*" element={<MainView />} />
            <Route path="/login" element={<LoginView />} />
        </Routes>     
    );
}

export default App;
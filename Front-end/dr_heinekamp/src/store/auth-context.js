import React, { useCallback, useState } from "react";


const AuthContext = React.createContext(
    {
        email: '',
        token: '',
        userId:'',
        isLoggedIn: false,
        login: (token) => { },
        logout: () => { }
    });


const retrieveStoredToken = () => {
    const storedToken = localStorage.getItem('token');
    const storedemail = localStorage.getItem('email');
    const storeduserId = localStorage.getItem('userId');
    return {
        token: storedToken,
        email: storedemail,
        userId: storeduserId
    };
}

export const AuthContextProvider = (props) => {
    const tokenData = retrieveStoredToken();

    let intialToken;
    let Email;
    let UserId;
    if (tokenData) {
        intialToken = tokenData.token;
        Email = tokenData.email;
        UserId = tokenData.userId;
    }
    const [token, setToken] = useState(intialToken);
    const [email, setEmail] = useState(Email);
    const [userId, setUserId] = useState(UserId);
    const userIsLoggedIn = !!token;


    const logoutHandler = useCallback(() => {
        localStorage.removeItem('token');
        localStorage.removeItem('email');
        localStorage.removeItem('userId');
        setToken(null);
        setEmail('');
        setUserId('');
    }, [])

    const loginHandler = (token,  email, userId) => {
        
        localStorage.setItem('token', token);
        localStorage.setItem('email', email);
        localStorage.setItem('userId', userId);
        setToken(token);
        setEmail(email);
        setUserId(userId);
    }


    const contextToken = {
        email: email,
        token: token,
        isLoggedIn: userIsLoggedIn,
        login: loginHandler,
        logout: logoutHandler,
        userId: userId
    }

    return <AuthContext.Provider value={contextToken}>
        {props.children}
    </AuthContext.Provider>
}

export default AuthContext;
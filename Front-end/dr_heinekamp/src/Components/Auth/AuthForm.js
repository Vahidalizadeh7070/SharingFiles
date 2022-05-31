import React, { useContext, useRef, useState } from 'react';
import AuthContext from '../../store/auth-context';
import { useHistory } from 'react-router-dom';

const AuthForm = () => {
  const context = useContext(AuthContext)
  const history = useHistory();
  const [isLogin, setIsLogin] = useState(true);
  const [message, setMessage] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const emailInputRef = useRef();
  const passwordInputRef = useRef();

  const switchAuthModeHandler = () => {
    setIsLogin((prevState) => !prevState);
  };

  const formSubmitHandler = (event) => {
    event.preventDefault();
    setMessage('');
    setIsLoading(true);
    const enteredEmail = emailInputRef.current.value;
    const enteredPassword = passwordInputRef.current.value;


    let url;
    if (isLogin) {
      url = 'https://sharingfileapp2022.azurewebsites.net/api/auth/login';
    }
    else {
      url = 'https://sharingfileapp2022.azurewebsites.net/api/auth/register';
    }
    fetch(url,
      {
        method: 'POST',
        body: JSON.stringify(
          {
            email: enteredEmail, password: enteredPassword
          }
        ),
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: "include"
      }
    ).then(res => {
      if (res.ok) {
        if (isLogin) {
          // do something with login 
          // You can redirect or show a string message

          setMessage('Authentication succeeded.');
        }
        else {
          history.replace('/auth');
          setMessage('Your registration completed successfully.');
        }
        setIsLoading(false);
        return res.json();
      }
      else {
        return res.json().then((data) => {
          let errorMessage = 'Authentication failed';
          if (data && data.error && data.error.message) {
            errorMessage = data.error.message;
          }
          throw new Error(errorMessage);
        });
      }
    }).then((data) => {
      context.login(data.token, data.email, data.userId);
      history.replace('/dashboard');

    }).catch((error) => {
      setIsLoading(false);
      setMessage(error.message);
    });
  }

  return (
    <section className='rounded-3'>
      <div className='p-3'>
        <h3 className='text-center'>{isLogin ? 'Sign In' : 'Sign Up'}</h3>
        {message && <h5 className='text-danger text-center pt-3'>{message}</h5>}
        <h4 className='text-danger text-center'>
          {
            isLoading && <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          }
        </h4>
        <form onSubmit={formSubmitHandler}>
          <div className='pb-3'>
            <label htmlFor='email'>Your Email</label>
            <input className='form-control' type='email' id='email' required ref={emailInputRef} />
          </div>
          <div className='pb-3'>
            <label htmlFor='password'>Your Password</label>
            <input className='form-control' type='password' id='password' required ref={passwordInputRef} />
          </div>
          <div className='pb-3 text-center'>
            <p>
              <button className='btn btn-primary rounded-3'>
                {
                  isLoading && <div className="spinner-border" role="status">
                    <span className="visually-hidden">Loading...</span>
                  </div>
                }
                
                {!isLoading && isLogin && 'Login'}
                {!isLoading && !isLogin && 'Create Account'}
                </button>
            </p>
            <button
              type='button'
              className='btn text-primary'
              onClick={switchAuthModeHandler}
            >
              {isLogin ? 'Create new account' : 'Login with existing account'}
            </button>
          </div>
        </form>
      </div>
    </section>
  );
};

export default AuthForm;

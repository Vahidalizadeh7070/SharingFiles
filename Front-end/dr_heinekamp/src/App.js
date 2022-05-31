import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import './App.css';
import Layout from './Components/Layout/Layout';
import { Switch, Route } from 'react-router-dom';
import React, { useContext } from 'react';
import AuthContext from './store/auth-context';
import AuthPage from './Pages/AuthPage';
import { Redirect } from 'react-router-dom';
import Dashboard from './Components/Dashboard/Dashboard';
import InboxPage from './Pages/InboxPage';
import AddFilesPage from './Pages/AddFilesPage';

function App() {
  const context = useContext(AuthContext);

  return (
    <Layout>
      <Switch>
        <Route path='/' exact>
          <AuthPage />
        </Route>
        {!context.isLoggedIn && (
          <Route path='/auth'>
            <AuthPage />
          </Route>

        )}
        {context.isLoggedIn && (
          <Route path='/dashboard'>
            <div className='container'>
              <Dashboard />
            </div>
          </Route>
        )}
        {context.isLoggedIn && (
          <Route path='/inbox'>
            <div className='container'>
              <InboxPage />
            </div>
          </Route>
        )}
        {context.isLoggedIn && (
          <Route path='/AddFiles'>
            <div className='container'>
              <AddFilesPage />
            </div>
          </Route>
        )}
        <Route path='*'>
          <Redirect to='/' />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;

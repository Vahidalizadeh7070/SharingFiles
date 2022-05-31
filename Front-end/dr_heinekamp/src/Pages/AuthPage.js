import AuthForm from '../Components/Auth/AuthForm';
import React from 'react';
import { Files } from 'react-bootstrap-icons';


const AuthPage = () => {
  return (
    <div className='container'>
      <div className='row mt-4 mb-4'>
        <div className='col-md-6 offset-md-3'>
          <div className='shadow rounded-3'>
            <div className='p-3 text-center'>
              <h2 className='pt-5 pb-3 text-danger'>
                Sharing files
              </h2>
              <h5>Upload, Download and Share your files</h5>
              <p className='text-danger pt-3 pb-3'><Files size={50} /></p>
              <hr/>
              <AuthForm />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AuthPage;

import React, { Fragment } from 'react';

import MainNavigation from './MainNavigation';

const Layout = (props) => {
  return (
    <Fragment>
      <MainNavigation />
      <main>{props.children}</main>
      <hr />
      <div className='container pb-5 pt-5'>
        <small>Sharing files</small>
        <small className='text-secondary float-end'>Developed by: vahidalizadeh1990@gmail.com</small>
      </div>
    </Fragment>
  );
};

export default Layout;

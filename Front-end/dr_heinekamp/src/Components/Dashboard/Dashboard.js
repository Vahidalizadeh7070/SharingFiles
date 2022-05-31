import React, { Fragment, useContext, useState } from "react";
import { Plus, Power } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";
import AuthContext from '../../store/auth-context';
import Details from "./Files/Details";
import FilesList from "./Files/FilesList";

const Dashboard = () => {
    const history = useHistory();
    const context = useContext(AuthContext);
    const [details, setDetails] = useState({});

    const logoutHandler = () => {
        context.logout();
        history.replace('/auth');
    }

    const DetailsHandler = (value) => {
        setDetails(value);
    }
    

    return (
        <Fragment>
            <div className="container mt-3">
                <div className="row">
                    <div className="shadow rounded-3">
                        <div className="p-3">
                            <div className="row">
                                <div className="col-10">
                                    <h3 className="text-secondary">Dashboard</h3>
                                </div>
                                <div className="col-2">
                                    <button className="btn btn-danger mx-1 rounded-circle btn-sm float-end" onClick={logoutHandler}><Power size={16} className="mb-1" /></button>
                                    <Link to="/AddFiles" className="btn btn-success rounded-circle btn-sm float-end"><Plus size={16} className="mb-1" /></Link>
                                </div>
                            </div>
                            <hr />
                            <div className="row">
                                <div className="col-md-6 pb-3">
                                    <div className="border border-1 rounded-3">
                                        <div className="p-3">
                                            <FilesList FileDetails={DetailsHandler}/>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <div className="border border-1 rounded-3">
                                        <div className="p-3">
                                            <Details DetailsFile={details}/>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </Fragment>
    )
}
export default Dashboard;
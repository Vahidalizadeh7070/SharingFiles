import React, { Fragment, useContext, useState, useCallback } from "react"
import Moment from 'moment';
import { Download, Share, Upload } from "react-bootstrap-icons";
import AuthContext from "../../../store/auth-context";
import ConvasUsers from "../Users/ConvasUsers";
import Modal from "../Users/Modal";
import AddMultipleFiles from "./AddMultipleFiles";
import ListOfSubFiles from "./ListOfSubFiles";

const Details = props => {
    let details = props.DetailsFile;
    const context = useContext(AuthContext);
    const [openConvas, setOpenConvas] = useState(false);
    const [modal, setModal] = useState(false);
    const [error, setError] = useState();
    const [newSubFiles, setNewSubFiles] = useState([]);
    
    const OffConvasUsersOpen = () => {
        setOpenConvas(!openConvas);
    }
    


    const MyDownload = useCallback(async () => {
        try {
            const response = await fetch(
                'https://sharingfileapp2022.azurewebsites.net/api/download/downloadbymyself?userId=' + context.userId + '&file=' + props.DetailsFile.file);

            if (response.status === 400) {
                throw new Error('There is no file');
            }
            else if (!response.ok) {
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();

        }
        catch (error) {
            setError(error.message);
        }
    }, [context.userId, props.DetailsFile.file]);

    const MultipleModal = () => {
        setModal(!modal);
    }
    const SendNewFiles = (value) =>
    {
        setNewSubFiles(value);
    }

    return (
        Object.keys(details).length !== 0 ? (
            <Fragment>
                <h3>Details</h3>
                {error && <h5 className="text-danger text-center">{error}</h5>}
                <hr />
                <div className="text-center">
                    <h4 className="text-secondary">{props.DetailsFile.title}</h4>
                    <p className="small fw-light">{Moment(props.DetailsFile.uploadDate).format('dddd - DD MMMM yyyy')}</p>
                </div>
                <hr />
                <div className="text-left">
                    <h5>Main file</h5>
                    <p className="small">You can download main file by click on the Download.</p>
                    <p className="small">If you need to add more files to <b>{props.DetailsFile.title}</b> you can click on upload.</p>
                    <p className="small">Furthermore, you can share this file with all subfiles among the users by clicking on the share.</p>
                    <div className="pb-5">
                        <button className="btn btn-success btn-sm shadow rounded-3 mx-1 float-end" type="button" onClick={MyDownload}>
                            Download <Download className="mb-1" />
                        </button>
                        <button
                            className="btn btn-primary btn-sm rounded-circle float-end" type="button" onClick={OffConvasUsersOpen}>
                            <Share className="mb-1" />
                        </button>
                        <button
                            className="btn btn-info btn-sm rounded-circle float-end mx-1" type="button" onClick={MultipleModal}>
                            <Upload className="mb-1 text-light" />
                        </button>
                        {modal && <Modal title="Upload multiple" onCloseModal={MultipleModal}>
                            <AddMultipleFiles LastFileUploaded={SendNewFiles} fileId={props.DetailsFile.id} fileName={props.DetailsFile.title} />
                        </Modal>}
                        {openConvas && <div id="modal"><ConvasUsers fileId={props.DetailsFile.id} onClose={OffConvasUsersOpen} /></div>}
                    </div>
                    <hr />
                    <div className="row">
                        <ListOfSubFiles showDeleteButton={true} files={newSubFiles} fileId={props.DetailsFile.id} />
                    </div>
                </div>
            </Fragment >
        ) :
            (
                <Fragment>
                    <div className="mt-5 mb-5">
                        {Object.keys(details).length !== 0 ? <h3 className="text-danger text-center">Please select a file from the list.</h3>
                            : <h3 className="text-danger text-center">Please add a file to the list.</h3>}
                    </div>
                </Fragment>
            )

    )

}
export default Details;
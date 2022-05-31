import React, { Fragment, useCallback, useContext, useEffect, useState } from "react"
import Moment from 'moment';
import FileIcon from "./FileIcon";
import { Download, ThreeDots, XLg } from "react-bootstrap-icons";
import AuthContext from "../../../store/auth-context";

const FileItem = (props) => {
    const context = useContext(AuthContext);
    const [downloadCount,setDownloadCount] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const Details = (event) => {
        event.preventDefault();
        props.FileDetails(props);
    }

    const count =useCallback(async () => {
        const response = await fetch('https://sharingfileapp2022.azurewebsites.net/api/inbox/count?fileId=' + props.id);

        const data = await response.json();
        setDownloadCount(data);

    },[props.id])

    useEffect(()=>{
        count();
    },[count])
    
    const Delete = () => {
        setIsLoading(true);
        fetch("https://sharingfileapp2022.azurewebsites.net/api/userFiles/delete?id=" + props.id + "&userId=" + context.userId,
            {
                method: 'Delete'
            }
        ).then(res => {
            if (res.ok) {
                setIsLoading(false);
                props.Remove(props.id);
                return res.json();
            }
            else {
                return res.json().then((data) => {
                    let errorMessage = 'Delete failed';
                    if (data && data.error && data.error.message) {
                        errorMessage = data.error.message;
                    }
                    throw new Error(errorMessage);
                });
            }
        }).catch((error) => {
            setIsLoading(false);
        });
    }

    return (
        <Fragment>
            <div className="col-lg-6 pb-3">
                <div className="card rounded-3 h-100">
                    <div className="card-body">
                        <h5 className="card-title">{props.title}</h5>
                        <p className="card-text">{Moment(props.uploadDate).format('dddd - DD MMMM yyyy')}</p>
                        <p>
                            <FileIcon format={props.file} />
                        </p>
                        <button type="button" onClick={Details} className="border border-1 btn btn-sm rounded-circle">
                            <ThreeDots className="text-primary mb-1" size={15} />
                        </button>
                        <button type="button" onClick={Delete} className="border border-1 btn btn-sm rounded-circle mx-1">
                        {isLoading && <div className="d-flex justify-content-center">
                                <div className="spinner-border" role="status">
                                    <span className="visually-hidden">Loading...</span>
                                </div>
                            </div>}
                            {!isLoading && <XLg className="text-danger mb-1" size={15} />}
                        </button>
                        <label className="float-end text-info p-1 text-center"><Download className="mb-1"/> {downloadCount} times</label>
                    </div> 
                </div>
            </div>
        </Fragment>
    )
}
export default FileItem;
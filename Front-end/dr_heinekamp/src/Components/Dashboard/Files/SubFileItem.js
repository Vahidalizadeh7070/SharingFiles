import React, { Fragment, useState, useCallback } from "react"
import { Download, XLg } from "react-bootstrap-icons";
import FileIcon from "./FileIcon";



const SubFileItem = (props) => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState();
    
    const MySubFileDownload = useCallback(async () => {
        try {
            const response = await fetch(
                'https://sharingfileapp2022.azurewebsites.net/api/download/downloadsubfilesbymyself?userfileId=' + props.fileId + '&subfileid=' + props.id);

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
    }, [props.fileId, props.id]);

   

    const Delete = () => {
        setIsLoading(true);
        fetch("https://sharingfileapp2022.azurewebsites.net/api/userfiles/deletesubfiles?id=" + props.id + "&userFilesId=" + props.fileId,
            {
                method: 'Delete'
            }
        ).then(res => {
            if (res.ok) {
                setIsLoading(false);
                props.onRemove(props.id);
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
                            <h5 className="card-title">{props.fileName}</h5>
                            <p>
                                <FileIcon format={props.fileName} />
                            </p>
                            {props.showDeleteButton && <button type="button" onClick={Delete} className="border border-1 btn btn-sm rounded-circle mx-1">
                                {isLoading && <div className="d-flex justify-content-center">
                                    <div className="spinner-border" role="status">
                                        <span className="visually-hidden">Loading...</span>
                                    </div>
                                </div>}
                                {!isLoading && <XLg className="text-danger mb-1" size={15} />}
                            </button>
                            }

                            <button className="btn btn-primary btn-sm shadow rounded-circle mx-1" type="button" onClick={MySubFileDownload}>
                                <Download className="mb-1" />
                            </button>
                        </div>
                    </div>
                </div>
           
        </Fragment>
    )
}
export default SubFileItem;
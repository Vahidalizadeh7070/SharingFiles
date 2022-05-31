import React, { Fragment, useEffect, useState } from "react";

const AddMultipleFiles = (props) => {
    const [message, setMessage] = useState('');
    const [error, setError] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [file, setFile] = useState([]);
    const fileId = props.fileId;

    const saveFile = (e) => {
        const files = [];
        for (let index = 0; index < e.target.files.length; index++) {
            files.push(e.target.files[index])
        }
        setFile(files);
    }


    const postData = (e) => {
        setIsLoading(true);
        e.preventDefault();
        const formData = new FormData();
        formData.append("UserFilesId", fileId)
        for (let index = 0; index < file.length; index++) {
            formData.append("UploadFile", file[index])
        }


        fetch("https://sharingfileapp2022.azurewebsites.net/api/userFiles/uploadmultiple",
            {
                method: 'POST',
                body: formData
            }
        ).then(res => {
            if (res.ok) {
                setMessage('Your files uploaded.');
                setIsLoading(false);
                props.LastFileUploaded(file);
                return res.json();
            }
            else {
                return res.json().then((data) => {
                    let errorMessage = 'Upload file failed';
                    if (data && data.error && data.error.message) {
                        errorMessage = data.error.message;
                    }
                    throw new Error(errorMessage);
                });
            }
        }).catch((error) => {
            setIsLoading(false);
            setError(error.message);
        });
    }
    useEffect(() => {
        setTimeout(() => {
            setMessage('');
            setError('');
        }, 3000)
    }, [])

    return (
        <Fragment>
            <div className="row">
                <h5 className="text-secondary text-center">You can add extra files for {props.fileName}</h5>
                {message && <h6 className="text-danger text-center">{message}</h6>}
                {error && <h6 className="text-danger text-center">{error}</h6>}
                <div className="form-group">
                    <label className="form-label" htmlFor="file">File</label>
                    <input type="file" className="form-control" id="file" onChange={saveFile} multiple />
                </div>
            </div>
            <div className="col-md-12">
                <button className="btn btn-primary float-end rounded-3 shadow mt-2 mb-4" type="button" onClick={postData}>
                    {
                        isLoading && <div className="d-flex justify-content-center">
                            <div className="spinner-border" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                    }
                    {!isLoading && "Upload"}
                </button>
            </div>
        </Fragment>
    )
}
export default AddMultipleFiles;
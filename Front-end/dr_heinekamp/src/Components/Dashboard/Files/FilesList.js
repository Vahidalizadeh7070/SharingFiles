import React, { Fragment, useContext, useState, useCallback, useEffect } from "react"
import AuthContext from "../../../store/auth-context";
import FileItem from "./FileItem";

const FilesList = (props) => {
    const [isLoading, setIsLoading] = useState(false);
    const [files, setFiles] = useState([]);
    const [error, setError] = useState(null);
    const context = useContext(AuthContext);

    const RetrieveFiles = useCallback(async () => {
        setIsLoading(true);
        try {
            const response = await fetch('https://sharingfileapp2022.azurewebsites.net/api/UserFiles?userId=' + context.userId);

            if (response.status === 404) {
                throw new Error('There is no file in your list');
            }
            else if (!response.ok) {
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();

            const loadFiles = [];
            for (const key in data.value) {

                loadFiles.push(
                    {
                        id: data.value[key].id,
                        key: data.value[key].id,
                        title: data.value[key].title,
                        uploadDate: data.value[key].uploadDate,
                        file: data.value[key].file
                    }
                );
            }
            setFiles(loadFiles);
        }
        catch (error) {
            setError(error.message);

        }
        setIsLoading(false);
    }, [context.userId]);

    

    useEffect(() => {
        RetrieveFiles();
    }, [RetrieveFiles])



    let content = <p className='fs-4 text-center'>There is no File in the list...</p>;
    if (isLoading) {
        content = (
            <div className="d-flex justify-content-center mt-5 mb-5">
                <div className="spinner-border" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
            </div>
        );
    }
    if (error) {
        content = <p className='text-danger text-center fs-4'>{error}</p>;

    }
    const details = (value) => {
        props.FileDetails(value);
    }
    const Remove = (value) => {
        setFiles(files.filter(element => element.id !== value))
    }

    if (files.length > 0) {
        content = (
            files.map(file =>
                <FileItem
                    id={file.id}
                    key={file.id}
                    title={file.title}
                    uploadDate={file.uploadDate}
                    file={file.file}
                    FileDetails={details}
                    Remove={Remove}
                />
            )
        )
    }
    return (
        <Fragment>
            <h3 className="text-secondary">Files</h3>
            <p className="small">You can observe all of your files that you have uploaded.</p>
            <p className="small fw-light">If you want to delete a file, make sure about it that there is not any subfile.</p>
            <hr />
            <div className="row">
                {content}
            </div>
        </Fragment>
    )
}
export default FilesList;
import { Fragment, useState, useCallback, useEffect } from "react"
import SubFileItem from "./SubFileItem";

const ListOfSubFiles = (props) => {

    const [subFiles, setSubFiles] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState();
    const [isEmpty, setIsEmpty] = useState(false);
    const RetrieveSubFiles = useCallback(async () => {
        setIsLoading(true);
        try {
            const response = await fetch('https://sharingfileapp2022.azurewebsites.net/api/userfiles/ListOfSubFiles?fileId=' + props.fileId);

            if (response.status === 404) {
                setIsEmpty(true);
                throw new Error('There is no file in your list');
            }
            else if (!response.ok) {
                setIsEmpty(true);
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();

            const loadSubFiles = [];
            for (const key in data.value) {

                loadSubFiles.push(
                    {
                        id: data.value[key].id,
                        key: data.value[key].id,
                        fileName: data.value[key].fileName
                    }
                );
            }

            setIsEmpty(false);
            setSubFiles(loadSubFiles);
        }
        catch (error) {
            setIsEmpty(true);
            setError(error.message);

        }
        setIsLoading(false);
    }, [props]);

    useEffect(() => {
        RetrieveSubFiles();
    }, [RetrieveSubFiles])

    const Remove = (value) => {
        setSubFiles(subFiles.filter(element => element.id !== value))
    }


    let content = <p className='fs-4 text-center'>There is no extra files in the list...</p>;
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


    if (subFiles.length > 0 && !isEmpty) {
        content = (
            subFiles.map(subfile =>
                <SubFileItem
                    id={subfile.id}
                    key={subfile.id}
                    fileName={subfile.fileName}
                    onRemove={Remove}
                    fileId={props.fileId}
                    
                    showDeleteButton={props.showDeleteButton}
                />
            )
        )
    }
    return (
        <Fragment>
            <h3 className="text-secondary">Other files</h3>
            <p className="small fw-light">You can see other files that you have been uploaded.</p>
            <hr />
            {content}
        </Fragment>
    )
}
export default ListOfSubFiles;
import { Fragment, useContext, useState } from "react"
import { ArrowLeft } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";
import AuthContext from "../../../store/auth-context";

const AddFileForm = () => {
    const history = useHistory();
    const context = useContext(AuthContext);
    const [title, setTitle] = useState('');
    const [file, setFile] = useState();
    const [message, setMessage] = useState('');
    const [error, setError] = useState();
    const [isLoading, setIsLoading] = useState(false);

    const TitleEventHandler = (event) => {
        setTitle(event.target.value);
    }

    const saveFile = (e) => {
        setFile(e.target.files[0]);
    }

    const postData = (e) => {
        setIsLoading(true);
        e.preventDefault();
        const formData = new FormData();
        formData.append("Title", title)
        formData.append("UploadFile", file)
        formData.append("UserId", context.userId)

        fetch("https://sharingfileapp2022.azurewebsites.net/api/userFiles/upload",
            {
                method: 'POST',
                body: formData
            }
        ).then(res => {
            if (res.ok) {
                setMessage('Upload file succeeded.');
                setIsLoading(false);
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
        }).then((data) => {
            history.replace('/dashboard');

        }).catch((error) => {
            setIsLoading(false);
            setError(error.message);
        });

    }

    return (
        <Fragment>
            <div className="col-md-6 offset-md-3">
                <div className="shadow rounded-3 mt-3">
                    <div className="p-3">
                        <div className="row">
                            <div className="col-10">
                                <h3 className="text-secondary">Add file</h3>
                                <p className="small">You can add any type of file then you can add multiple files from details section in your dashboard.</p>
                            </div>
                            <div className="col-2">
                                <Link to="/dashboard" className="btn btn-sm rounded-circle border border-1 float-end shadow"><ArrowLeft className="pb-1" size={18} /></Link>
                            </div>
                        </div>
                        {message && <h4 className="text-danger text-center">{message}</h4>}
                        {error && <h4 className="text-danger text-center">{error}</h4>}
                        <hr />
                        <div className="row">
                            <div className="col-md-6">
                                <label className="form-label" htmlFor="Title">Title</label>
                                <input className="form-control" id="Title" onChange={TitleEventHandler} placeholder="Enter your title" />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label" htmlFor="file">File</label>
                                <input type="file" className="form-control" id="file" onChange={saveFile} />
                            </div>

                        </div>
                        <div className="row">
                            <div className="pt-3">
                                <button className="btn btn-primary shadow rounded-3 float-end" onClick={postData}>
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
                        </div>
                    </div>
                </div>
            </div>
        </Fragment>
    )
}
export default AddFileForm;
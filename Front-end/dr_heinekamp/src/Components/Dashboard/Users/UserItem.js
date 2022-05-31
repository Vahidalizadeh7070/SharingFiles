import React, { Fragment, useState } from "react"

const UserItem = (props) => {

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');
    const fileId = props.fileId;

    const StoreInfo = event => {
        let url = 'https://sharingfileapp2022.azurewebsites.net/api/share/';
        let method = '';
        let UserProfile = {};

        UserProfile = {
            userFilesId: fileId,
            userId: props.id,
        }
        method = 'POST'
        event.preventDefault();
        setIsLoading(true);
        setMessage('');

        fetch(url,
            {
                method: method,
                body: JSON.stringify(UserProfile),
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        ).then(res => {
            if (res.ok) {
                setMessage('You have shared successfully.');
                setIsLoading(false);
                props.ChangeStateOfForm();
                return res.json();
            }
            else {
                return res.json().then((data) => {
                    let errorMessage = 'There is an error';
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
    return (
        <Fragment>
            <div className="card mb-3 rounded-3">
                <div className="card-body">
                    <h5 className="card-title">{props.userName}</h5>
                    {message && <div className="small fw-light text-danger">{message}</div>}
                    <button className="btn btn-sm btn-warning shadow rounded-3 float-end" onClick={StoreInfo}>
                        {
                        isLoading && <div className="d-flex justify-content-center">
                            <div className="spinner-border" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                        }
                        {!isLoading && "Share"}
                    </button>
                </div>
            </div>
        </Fragment>
    )
}
export default UserItem;
import React, { Fragment, useCallback, useContext, useEffect, useState } from "react"
import Moment from 'moment';
import FileIcon from "../Dashboard/Files/FileIcon";
import '../Inbox/inbox.css'
import AuthContext from "../../store/auth-context";
import ListOfSubFiles from "../Dashboard/Files/ListOfSubFiles";

const InboxItem = (props) => {
    const context = useContext(AuthContext);
    const [error, setError] = useState();
    const [expire, setExpire] = useState(true);
    const checkExpiration = useCallback(() => {
        const current = new Date();
        if (Moment(props.expirationDate).format('DD') >= current.getDate()) {
            setExpire(false);
        }
    }, [props.expirationDate])
    useEffect(() => {
        checkExpiration();
    }, [checkExpiration])

    const Download = useCallback(async () => {
        try {
            const response = await fetch(
                'https://sharingfileapp2022.azurewebsites.net/api/download/download?userId=' + context.userId + '&fileId=' + props.userFilesId);

            if (response.status === 400) {
                throw new Error('There is no file');
            }
            else if (!response.ok) {
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();

            console.log(data);
        }
        catch (error) {
            setError(error.message);
        }
    }, [context.userId, props.userFilesId]);

    return (
        <Fragment>
            <div className="col-lg-6 pb-3">
                <div className="card rounded-3 h-100  bg-inbox">
                    <div className="card-body">
                        <h5 className="card-title">{props.senderUserName}</h5>
                        <p className="card-text">{Moment(props.expirationDate).format('dddd - DD MMMM yyyy')}</p>
                        <FileIcon format={props.fileName} />
                        <hr />

                        {expire && <p className="text-center text-danger">It expired. You can ask from {props.senderUserName} to send it again.</p>}
                        {!expire && <div>
                            <p>Main file: {props.title}</p>
                            <button className="btn btn-sm btn-success rounded-3 shadow" onClick={Download}>Download</button>
                            <div className="row pt-5">
                                <ListOfSubFiles showDeleteButton={false} fileId={props.userFilesId} />
                            </div>
                        </div>
                        }
                    </div>
                </div>
            </div>
        </Fragment>
    )
}
export default InboxItem;
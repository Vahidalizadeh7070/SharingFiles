import React, { Fragment, useCallback, useState, useEffect, useContext } from "react";
import AuthContext from "../../store/auth-context";
import InboxItem from "./InboxItem";

const Inbox = () => {
    const context = useContext(AuthContext);
    const userId = context.userId;
    const [isLoading, setIsLoading] = useState(false);
    const [inbox, setInbox] = useState([]);
    const [error, setError] = useState(null);

    const RetrieveInbox = useCallback(async () => {
        setIsLoading(true);
        try {
            const response = await fetch('https://sharingfileapp2022.azurewebsites.net/api/inbox?userId=' + userId);
            if(response.status===404)
            {
                setError("There is no file in your inbox");
            }
            else if (!response.ok) {
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();

            const loadInbox = [];
            for (const key in data.value) {

                loadInbox.push(
                    {
                        id: data.value[key].id,
                        userId: data.value[key].userId,
                        userFilesId: data.value[key].userFilesId,
                        expirationDate: data.value[key].expirationDate,
                        downloadURL: data.value[key].downloadURL,
                        download: data.value[key].download,
                        senderUserName: data.value[key].senderUserName,
                        fileName: data.value[key].fileName,
                        title: data.value[key].title,
                    }
                );
            }
            setInbox(loadInbox);

        }
        catch (error) {
            setError(error.message);
        }
        setIsLoading(false);
    }, [userId]);

    useEffect(() => {
        RetrieveInbox();
    }, [RetrieveInbox])

    let content = <p className='fs-4 text-center'>There is no file in your inbox ...</p>;
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
    if (inbox.length > 0) {
        content = (
            inbox.map(inboxes =>
                <InboxItem
                    id={inboxes.id}
                    key={inboxes.id}
                    userId={inboxes.userId}
                    userFilesId={inboxes.userFilesId}
                    expirationDate={inboxes.expirationDate}
                    downloadURL={inboxes.downloadURL}
                    download={inboxes.download}
                    senderUserName={inboxes.senderUserName}
                    fileName={inboxes.fileName}
                    title = {inboxes.title}
                />
            )
        )
    }
    return (
        <Fragment>
            <h3 className="text-secondary">Inbox</h3>
            <p className="small">You can download all files that have been shared for you</p>
            <hr />
            <div className="row">
                {content}
            </div>
        </Fragment>
    )
}
export default Inbox;
import React, { Fragment, useCallback, useState, useEffect } from "react"
import UserItem from "./UserItem";

const UserList = (props) => {
    const [isLoading, setIsLoading] = useState(false);
    const [users, setUsers] = useState([]);
    const [error, setError] = useState(null);

    const RetrieveUsers = useCallback(async () => {
        setIsLoading(true);
        try {
            const response = await fetch('https://sharingfileapp2022.azurewebsites.net/api/users');
            if (!response.ok) {
                throw new Error('Something went wrong!!!');
            }
            const data = await response.json();
            const loadUsers = [];
            for (const key in data.value) {

                loadUsers.push(
                    {
                        id: data.value[key].id,
                        userName: data.value[key].userName
                    }
                );
            }
            setUsers(loadUsers);
        }
        catch (error) {
            setError(error.message);
        }
        setIsLoading(false);
    }, []);

    useEffect(() => {
        RetrieveUsers();
    }, [RetrieveUsers])

    let content = <p className='fs-4 text-center'>There is no Users in the list...</p>;
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
    if (users.length > 0) {
        content = (
            users.map(user =>
                <UserItem
                    id={user.id}
                    key={user.id}
                    userName={user.userName}
                    fileId={props.fileId}
                />
            )
        )
    }
    return (
        <Fragment>
            {content}
        </Fragment>
    )
}
export default UserList;
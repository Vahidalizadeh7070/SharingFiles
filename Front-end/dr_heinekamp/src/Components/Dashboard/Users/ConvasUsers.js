import React from "react";
import Modal from "./Modal";
import UsersList from './UsersList';


const ConvasUsers = (props) => {
    return (
        <Modal title="Share" onCloseModal={props.onClose}>
            
            <UsersList fileId={props.fileId}/>
        </Modal>
    )
}
export default ConvasUsers;
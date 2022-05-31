import ReactDOM from "react-dom";
import { Fragment } from "react";
import CartModal from './Modal.module.css';

const Backdrop = (props) => {
    return <div className={`${CartModal.backdrop}`} onClick={props.onclose}></div>;

};

const ModalOverlay = (props) => {
    return (
        <div className={`${CartModal.modal} modal-dialog`}>
            <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title">{props.title}</h5>
                    <button onClick={props.onClose} type="button" className="btn-close" ></button>
                </div>
                <div className="modal-body">
                    {props.children}
                </div>
            </div>
        </div>
    )
}


const Modal = props => {
    return (
        <Fragment>
            {ReactDOM.createPortal(<Backdrop onclose={props.onCloseModal} />, document.getElementById('backdrop'))}
            {ReactDOM.createPortal(<ModalOverlay title={props.title} onClose={props.onCloseModal}>{props.children}</ModalOverlay>, document.getElementById('overlays'))}
        </Fragment>
    )
}
export default Modal;
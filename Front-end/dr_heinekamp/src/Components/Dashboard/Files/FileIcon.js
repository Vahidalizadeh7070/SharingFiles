import React, { Fragment } from "react"
import { FileEarmarkFill, FileEarmarkPdfFill, FileEarmarkText, FileEarmarkWordFill, FiletypeCsv, FiletypeJpg, FiletypePng, FiletypeXlsx } from "react-bootstrap-icons";

const FileIcon = (props) => {
    const lastIndex = props.format.lastIndexOf('.');
    const formatFile = props.format.slice(lastIndex + 1);
    let content;
    switch (formatFile) {
        case "pdf":
            content = <FileEarmarkPdfFill className="text-danger" />
            break;
        case "docx":
            content = <FileEarmarkWordFill className="text-primary" />
            break;
        case "txt":
            content = <FileEarmarkText className="text-secondary" />
            break;
        case "jpg":
            content = <FiletypeJpg className="text-warning" />
            break;
        case "png":
            content = <FiletypePng className="text-danger" />
            break;
        case "xlsx":
            content = <FiletypeXlsx className="text-success" />
            break;
        case "csv":
            content = <FiletypeCsv className="text-success" />
            break;
        default:
            content = <FileEarmarkFill />
    }
    return (
        <Fragment>
            {content}
        </Fragment>
    )
}
export default FileIcon;
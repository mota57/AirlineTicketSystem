import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "./endpoints";
import { Link } from "react-router-dom";
import {LoadingIcon} from "./LoadingIcon";
import ConfirmModal from "./ConfirmModal";


  /**
   *  <IndexComponent
   *     urlGetRecords:{}
   *     tootbarButton:[
   *      {
   *        label: '',
   *        action:() =>(),
   *        location:'toolbar',
   *        component: ?
   *      }
   *    ],
   *    />
   * 
   * 
   *  
   * 
   * <GenericCrudTable
      loadUrl={`${apiUrls.terminal().url}?airportId=${recordid}`}
      deleteUrl={(terminalid) => `${apiUrls.terminal().url}/${terminalid}`}
      createUrl={`${url}/form/0`}
      detailUrl={(terminalid) => `${url}/form/${terminalid}`}
      headerList={["Terminal", "Aerolinea", "Estatus"]}
      actionHeader ={"Acciones"}
      columns={(terminal) => (
        <>
          <td >{terminal.name}</td>
          <td>{terminal.airlineName}</td>
          <td style={{ fontWeight: "400" }}>
            {terminal.isActive ? "Activo" : "Inactivo"}
          </td>
        </>
      )}
   */

/**
 *
 * @param {loadUrl, createUrl,buttonActions} props
 * @returns
 */

export default function GenericCrudTable(props) {
  const [records, setRecords] = useState([]);
  const [state, setState] = useState({ isModalDelete: false, recordId: null, isXHR:true });

  useEffect(() => {
    loadRecords();
  }, []);

  function loadRecords() {
    if (props.loadUrl) {
      axios
        .get(props.loadUrl)
        .catch((e) => console.error("XHR error ", e))
        .then((data) => setRecords(data != null && data.data ? data.data : []))
        .finally(() => setState({isXHR:false}));
    }else {
        setState({isXHR:false})
    }
  }

  function handleDelete(recordid) {
    axios
      .delete(props.deleteUrl(recordid))
      .catch(() => {
        alert("un error a ocurrido lo sentimos.");
      })
      .then(() => {
        loadRecords();
      })
  }


  if(state.isXHR) {
      return (
        <LoadingIcon/>
      )
  }
  
  return (
    <div className="container m-top-1">
    

      <div className="card">
        <div className="card-header bg-dark">
        
      <Link to={props.createUrl} className="btn btn-primary m-b-1">
        Agregar
      </Link>
        </div>
        <div className="card-body">

       
      <table className="table table-striped">
        <thead>
          <tr>
            {props.headerList.map((header,index) => (
               typeof(header) === "string" ?  (<th key={index}>{header}</th>)
              : (<td {...header.props} >{header.label}</td>)
            ))}
            {typeof(props.actionHeader) == "string" ?
              (<th>{props.actionHeader}</th>): (<>{props.actionHeader}</>)
            }
          </tr>
        </thead>
        <tbody>
          {records.map((record) => (
            <tr key={record.id}>
              {props.columns(record)}
              <td>
                <div className="">
                  {props.detailUrl && (
                    <Link
                      to={props.detailUrl(record.id)}
                      className="btn btn-success m-r-1-sm"
                    >
                      Detalle
                    </Link>
                  )}
                  {props.deleteUrl && (
                    <button
                      className="btn btn-danger  m-r-1-sm"
                      onClick={() => ConfirmModal(() => handleDelete(record.id)) }
                    >
                      Eliminar
                    </button>
                  )}
                  {props.buttonActions && props.buttonActions(record.id)}
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      </div>
      </div>
    </div>
  );
}

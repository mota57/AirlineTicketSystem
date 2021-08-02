import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link, useParams, useRouteMatch } from "react-router-dom";
import { ModalDelete } from "./app.modals";

export default function GateIndexComponent(props) {
  const [records, setRecords] = useState([]);
  const [state, setSate] = useState({ showModalDelete: false, recordId: null });
  const { recordid } = useParams();
  const { url } = useRouteMatch();

  let ctrlApiUrl = apiUrls.gate().url;
  let formUrl = `${url}/form`;

  useEffect(() => {
    load();
  }, []);

  function load() {
    axios
      .get(`${ctrlApiUrl}?airportId=${recordid}`)
      .catch((e) => console.error("XHR error ", e))
      .then((data) => setRecords(data != null && data.data ? data.data : []));
  }

  function handleDelete(recordIdToDelete) {
    axios
      .delete(`${ctrlApiUrl}/${recordIdToDelete}`)
      .catch(() => {
        closeModal();
        alert("un error a ocurrido lo sentimos.");
      })
      .then(() => {
        load();
        closeModal();
      });
  }

  function closeModal() {
    changeProperty({ showModalDelete: false, recordId: null });
  }

  function changeProperty(objectUpdate) {
    setSate({ ...state, ...objectUpdate });
  }

  return (
    <div className="container m-top-1">
      <b>{url}</b>
      <br />
      <ModalDelete
        showModal={state.showModalDelete}
        handleOnClose={() => closeModal()}
        handleOk={() => handleDelete(state.recordId)}
      />

      <div>
        <Link to={`${formUrl}/0`} className="btn btn-primary">
          Agregar Puerta
        </Link>
      </div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Puerta</th>
            <th>Is Active</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {records.map((recordObj) => (
            <tr key={recordObj.id}>
              <td style={{ width: "800px" }}>{recordObj.name}</td>
              <td style={{ width: "200px", fontWeight: "400" }}>
                {recordObj.isActive ? "Activo" : "Inactivo"}
              </td>
              <td>
                <div className="" style={{ width: "400px" }}>
                  <Link
                    to={`${formUrl}/${recordObj.id}`}
                    className="btn btn-success m-r-1-sm"
                  >
                    Editar
                  </Link>

                  {/* <button
                    className="btn btn-danger  m-r-1-sm"
                    onClick={() =>
                      changeProperty({
                        recordId: recordObj.id,
                        showModalDelete: true,
                      })
                    }
                  >
                    Eliminar
                  </button> */}
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

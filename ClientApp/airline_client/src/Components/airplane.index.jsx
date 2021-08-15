import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link, useParams } from "react-router-dom";
import { ModalDelete } from "./app.modals";
import ConfirmModal from "../utils/ConfirmModal";

export default function AirplaneComponent(props) {
  const { airlineid } = useParams();
  const [state, setState] = useState({
    airplaneName: "",
    records: [],
  });

  console.log("airlineid::", airlineid);

  useEffect(() => {
    if (airlineid > 0) {
      loadAirplanes();
    }
  }, []);
 
  function loadAirplanes() {
    axios
      .get(`${apiUrls.airplane.byairlineid}/${airlineid}`)
      .catch((e) => console.error("XHR error ", e))
      .then((data) => {
        let dataRecords = data != null && data.data ? data.data : [];
        setState({
          ...state,
          records: dataRecords,
        });
      });
  }

  function handleDelete(airplaneId) {
    if (isNaN(airplaneId) || Number(airplaneId) <= 0) {
      throw new Error("bad request airplane id");
    }

    axios
      .delete(`${apiUrls.airplane.url}?airplaneId=${airplaneId}`)
      .catch(() => alert('an error ocurred'))
      .then(() => {
        loadAirplanes();
      });
  }



  function changeProperty(objectUpdate) {
   
    setState({...state, ...objectUpdate});
  }

  if (isNaN(airlineid) ||  Number(airlineid) <= 0) {
    return (
      <div className="alert alert-danger">
        Recurso no encontrado
        <Link to="/airline"> Ir a aerolineas</Link>
      </div>
    );
  }
  console.log('state::', state);
  return (
    <>
      {/* <ModalDelete
        showModal={state.showModalDelete}
        handleOnClose={() => closeModal()}
        handleOk={() => handleDelete(state.airplaneId)}
      /> */}

      <div className="container m-top-1">
        <h1>Aviones de la aerolinea {airlineid}</h1>
        <div>
          <Link
            to={`/airplane/form?airlineid=${airlineid}`}
            className="btn btn-primary m-b-1 m-r-1-sm"
          >
            Agregar
          </Link>
        </div>
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Marca</th>
              <th>Modelo</th>
              <th>Codigo</th>
              <th>Total de Asientos</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {state.records.map((airplane) => (
              <tr key={airplane.id}>
                <td>{airplane.brand}</td>
                <td>{airplane.model}</td>
                <td>{airplane.code}</td>
                <td>{airplane.totalSeats}</td>
                <td>
                  <div style={{ width: "200px" }}>
                    <Link
                      to={`/airplane/form?airlineid=${airlineid}&airplaneid=${airplane.id}`}
                      className="btn btn-success m-r-1-sm"
                    >
                      Editar
                    </Link>
                    <button className="btn btn-danger" onClick={() => ConfirmModal(() => handleDelete(airplane.id) ) }>Delete</button>
                    
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

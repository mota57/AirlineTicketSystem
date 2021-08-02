import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link} from "react-router-dom";
import { ModalDelete } from "./app.modals";

export default function AirlineComponent(props) {
  const [airlines, setAirlines] = useState([]);
  const [state, setState] = useState({showModalDelete:false})

  useEffect(() => {
    loadAirlines();
  }, []);
  
  function loadAirlines() {
    axios
      .get(apiUrls.airline.url)
      .catch((e) => console.error("XHR error ", e))
      .then((data) => setAirlines(data != null && data.data ? data.data : []));
  }

  function handleDelete(airlineid) {
    axios
      .delete(`${apiUrls.airline.url}/${airlineid}`)
      .catch(() => {
          closeModal();
          alert('un error a ocurrido lo sentimos.')
      })
      .then(() => {
        loadAirlines();
        closeModal();
      });
  }


  function closeModal() {
    changeProperty({showModalDelete: false, airlineId: null });
  }

  function changeProperty(objectUpdate) {
    setState({...state, ...objectUpdate});
  }

  return (
    <div className="container m-top-1">

    <ModalDelete
        showModal={state.showModalDelete}
        handleOnClose={() => closeModal()}
        handleOk={() => handleDelete(state.airlineId)}
      />

      <div >

      <Link to={`/airline/form/0`}  className="btn btn-primary">Agregar Aerolinea</Link>

        </div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Airline</th>
            <th>Is Active</th>
            <th>Informacion</th>
          </tr>
        </thead>
        <tbody>
          {airlines.map((airline) => (
            <tr key={airline.id}>
              <td style={{ width: "800px" }}>{airline.name}</td>
              <td style={{ width: "200px", fontWeight:'400' }}>{airline.isActive ? 'Activo' : 'Inactivo'}</td>
              <td>
                <div className="" style={{ width: "400px" }}>
                
                <Link 
                  to={`/airline/form/${airline.id}`} 
                  className="btn btn-success m-r-1-sm">Detalle</Link>

                <button className="btn btn-danger  m-r-1-sm" onClick={() => changeProperty({airlineId: airline.id, showModalDelete:true})}>
                  Eliminar
                </button>

                <Link to={`/airplane/${airline.id}`} className="btn btn-primary m-r-1-sm">
                  Aviones</Link>
                <Link to={`/bagprice/${airline.id}`} className="btn btn-primary">
                  Maletas</Link>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

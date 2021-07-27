import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../../utils/endpoints";

import { Link, useParams } from "react-router-dom";
import { ModalDelete } from "../app.modals";

export default function AirportAirlineIndex(props) {
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
   */

  let { airportid } = useParams();
  


 function AirportAirlineVM(data) {
      this.airlines = data?.data || [];
      this.showModalDelete = false;
      this.airlineId = null;
  }

  const [state, setState] = useState(new AirportAirlineVM());

  useEffect(() => {
    loadAirlines();
  }, []);

  function loadAirlines() {
    axios
      .get(`${apiUrls.airline_airport.airlinesByAirportid}/${airportid}`)
      .catch((e) => {
        alert("sorry an error occurred");
        console.error("loadAirlines ", e);
      })
      .then((data) => {
        setState({...state, airlines: data.data});
      });
  }


  function handleDelete(airlineid) {
    if (isNaN(airlineid) || Number(airlineid) < 0) {
      throw new Error("airline id not pass");
    }

    axios
      .delete(
        `${apiUrls.airline_airport.url}?airlineid=${airlineid}&airportid=${airportid}`
      )
      .catch((e) => {
        console.error(e);
        closeModal();
      })
      .then(() => {
        loadAirlines();
      });
  }

  function closeModal() {
    changeProperty({ showModalDelete: false, airlineId:null });
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

      <div>
        <Link
          to={`/airport_airlines/form?airportId=${airportid}&airlineId=0`}
          className="btn btn-primary"
        >
          Agregar
        </Link>
      </div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Airline</th>
            <th>Is Active</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {state.airlines.map((airline) => (
            <tr key={airline.id}>
              <td style={{ width: "800px" }}>{airline.name}</td>
              <td style={{ width: "200px", fontWeight: "400" }}>
                {airline.isActive ? "Activo" : "Inactivo"}
              </td>
              <td>
                <div style={{ width: "200px" }}>
                  <button
                    onClick={() =>
                      changeProperty({
                        airlineId:airline.id,
                        showModalDelete: true,
                      })
                    }
                    className="btn btn-success"
                  >
                    Eliminiar
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

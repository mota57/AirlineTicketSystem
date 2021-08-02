import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../../utils/endpoints";

import { Link, useParams,useRouteMatch } from "react-router-dom";
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

  let { recordid } = useParams();
  let {  url } = useRouteMatch();


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
      .get(`${apiUrls.airline_airport.airlinesByAirportid}/${recordid}`)
      .catch((e) => {
        alert("sorry an error occurred");
        console.error("loadAirlines ", e);
      })
      .then((data) => {
        setState({...state, airlines: data.data, showModalDelete: false, airlineId:null});
      });
  }


  function handleDelete(airlineAirportId) {
   

    axios
      .delete(
        `${apiUrls.airline_airport.delete}/${airlineAirportId}`
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
          to={`${url}/form/0`}
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
          {state.airlines.map((dto) => (
            <tr key={dto.id}>
              <td style={{ width: "800px" }}>{dto.airlineName }</td>
              <td style={{ width: "200px", fontWeight: "400" }}>
                {dto.isActive ? "Activo" : "Inactivo"}
              </td>
              <td>
                <div style={{ width: "200px" }}>


                <Link
                    to={`${url}/form/${dto.id}` }
                    className="btn btn-primary m-r-1-sm"
                  >
                    Editar
                  </Link>

                  <button
                    onClick={() =>
                      changeProperty({
                        airlineId:dto.id,
                        showModalDelete: true,
                      })
                    }
                    className="btn btn-danger"
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

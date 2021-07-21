import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link, Redirect, useHistory } from "react-router-dom";

export default function AirlineComponent(props) {
  const [airlines, setAirlines] = useState([]);
  const history = useHistory();
  
  useEffect(() => {
    axios
      .get(apiUrls.airline.url)
      .catch((e) => console.error("XHR error ", e))
      .then((data) => setAirlines(data != null && data.data ? data.data : []));
  }, []);
  
 
  
  return (
    <div className="container m-top-1">
      <div >

      <Link to={`/airline/form`}  className="btn btn-primary">Agregar</Link>

        </div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Airline</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {airlines.map((airline) => (
            <tr key={airline.id}>
              <td style={{ width: "900px" }}>{airline.name}</td>
              <td>
                <div className="" style={{ width: "2 00px" }}>
                <Link to={`/airline/form/${airline.id}`} className="btn btn-success">Detalle</Link>
                  {/* <button
                    onClick={() => setRedirectForm(airline.id)}
                    className="btn btn-success"
                  >
                    Details
                  </button> */}

                <Link to={`/airplane/${airline.id}`} className="btn btn-primary">
                  Aviones</Link>
               
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

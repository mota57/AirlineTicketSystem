import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Redirect } from "react-router-dom";

export default function AirportComponent(props) {
  const [airports, setAirports] = useState([]);
  const [recordid, setRecordId] = useState(-1); //0 create, > 0 update
  const [redirectCreate, setRedirectCreate] = useState(-1); //1 go to create
  

  useEffect(() => {
    axios
      .get(apiUrls.airport.url)
      .catch((e) => console.error("XHR error ", e))
      .then((data) => setAirports(data != null && data.data ? data.data : []));
  }, []);

  if(redirectCreate >= 0) {
    return <Redirect to={{pathname:`/airport/form`, state: { recordid:0 }}} />;
  }

  if (recordid >= 0) {
    return <Redirect to={{pathname:`/airport/dashboard/${recordid}`, state: { recordid }}} />;
  }



  return (
    <div className="container m-top-1">
      <button
        className="btn btn-primary m-b-1"
        onClick={() => setRedirectCreate(0)}
      >
        Agregar
      </button>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Airport</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {airports.map((airport) => (
            <tr key={airport.id}>
              <td style={{ width: "1000px" }}>{airport.name}</td>
              <td>
                <div className="form-group">
                  <button
                    onClick={() =>  setRecordId(airport.id)}
                    className="btn btn-success"
                  >
                    Details
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

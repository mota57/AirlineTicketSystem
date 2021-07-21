import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Redirect, useParams } from "react-router-dom";

export default function AirplaneComponent(props) {
  const {airlineid} = useParams();

  const [state, setState] = useState({ airplaneName:'', records:[]});
  const [redirectFormId, setRedirectFormId] = useState(-1);
 
  
  useEffect(() => {
    axios
      .get(apiUrls.airplane.url)
      .catch((e) => console.error("XHR error ", e))
      .then((data) =>{
          let dataRecords = data != null && data.data ? data.data : []
          setState({...state, ['records']: dataRecords});
      }) 
      
  }, []);
  
 
  if (redirectFormId >= 0) {
    return (
     <Redirect to={{pathname:`/airplane/form?airlineid=${airlineid}&airplaneid=${redirectFormId || 0}`}} />
    );
  }
  
  return (
    <div className="container m-top-1">
        <h1>Aviones de la aerolinea</h1>
      <div >
      <button
        className="btn btn-primary m-b-1 m-r-1-sm"
        onClick={() => setRedirectFormId(0)}
        >
        Agregar
      </button>
        </div>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Airplane</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {state.records.map((airplane) => (
            <tr key={airplane.id}>
              <td style={{ width: "900px" }}>{airplane.name}</td>
              <td>
                <div className="" style={{ width: "2 00px" }}>
                  <button
                    onClick={() => setRedirectFormId(airplane.id)}
                    className="btn btn-success"
                  >
                    Edit
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

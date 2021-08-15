import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import {  Link } from "react-router-dom";
import GenericCrudTable from "../utils/GenericCrudTable";

export default function AirportComponent(props) {
  return (
  <GenericCrudTable
      loadUrl={apiUrls.airport.url}
      createUrl={`/airport/create`}
      // deleteUrl={(airportid) => `${apiUrls.airline.url}/${airportid}`}
      // detailUrl={(airportid) => `/airline/form/${airportid}`}
      headerList={[
      "Aeropuerto",
      "Es activo" 
      ]}
      columns={(airport) => (
        <>
          <td style={{ width: "800px" }}>{airport.name}</td>
          <td style={{ width: "200px", fontWeight: "400" }}>
            {airport.isActive ? "Activo" : "Inactivo"}
          </td>
        </>
      )}
      actionHeader ={<th style={{width:'40%'}}>Acciones</th>}
      buttonActions={(airportid) => (
        <>
                <Link
                    to={`/airport/details/${airportid}`}
                    className="btn btn-success"
                  >
                    Details
                  </Link>
        </>
      )}
    />
    )
}


// export default function AirportComponent(props) {
//   const [airports, setAirports] = useState([]);
  

//   useEffect(() => {
//     axios
//       .get(apiUrls.airport.url)
//       .catch((e) => console.error("XHR error ", e))
//       .then((data) => setAirports(data != null && data.data ? data.data : []));
//   }, []);


//   return (
//     <div className="container m-top-1">
//       <Link
//         to='/airport/create'
//         className="btn btn-primary m-b-1"
//       >
//         Agregar
//       </Link>
//       <table className="table table-striped">
//         <thead>
//           <tr>
//             <th>Airport</th>
//             <th>Actions</th>
//           </tr>
//         </thead>
//         <tbody>
//           {airports.map((airport) => (
//             <tr key={airport.id}>
//               <td style={{ width: "1000px" }}>{airport.name}</td>
//               <td>
//                 <div className="form-group">
//                   <Link
//                     to={`/airport/details/${airport.id}`}
//                     className="btn btn-success"
//                   >
//                     Details
//                   </Link>
//                 </div>
//               </td>
//             </tr>
//           ))}
//         </tbody>
//       </table>
//     </div>
//   );
// }

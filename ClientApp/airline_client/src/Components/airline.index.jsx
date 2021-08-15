import { useEffect, useState } from "react";
import axios from "axios";
import { ModalDelete } from "./app.modals";
import apiUrls from "../utils/endpoints";

import { Link} from "react-router-dom";
import GenericCrudTable from "../utils/GenericCrudTable";

export default function AirlineComponent(props) {
  return (
    <GenericCrudTable
      loadUrl={apiUrls.airline.url}
      createUrl={`/airline/form/0`}
      // deleteUrl={(airlineid) => `${apiUrls.airline.url}/${airlineid}`}
      detailUrl={(airlineid) => `/airline/form/${airlineid}`}
      headerList={[
      "Airline",
      "Is Active" 
      ]}
      columns={(airline) => (
        <>
          <td style={{ width: "800px" }}>{airline.name}</td>
          <td style={{ width: "200px", fontWeight: "400" }}>
            {airline.isActive ? "Activo" : "Inactivo"}
          </td>
        </>
      )}
      actionHeader ={<th style={{width:'30%'}}>Acciones</th>}
      buttonActions={(airlineid) => (
        <>
          <Link
            to={`/airplane/${airlineid}`}
            className="btn btn-primary m-r-1-sm"
          >
            Aviones
          </Link>
          <Link to={`/bagprice/${airlineid}`} className="btn btn-primary">
            Maletas
          </Link>
        </>
      )}
    />
  );

}

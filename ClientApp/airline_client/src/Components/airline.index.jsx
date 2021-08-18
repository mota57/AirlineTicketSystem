import { useEffect, useState } from "react";
import axios from "axios";
import { ModalDelete } from "./app.modals";
import apiUrls from "../utils/endpoints";

import { Link} from "react-router-dom";
import GenericCrudTable from "../utils/GenericCrudTable";
import BadgeIsActive from "../utils/BadgeIsActive";

export default function AirlineComponent(props) {
  return (
    <GenericCrudTable
      loadUrl={apiUrls.airline.url}
      createUrl={`/airline/form/0`}
      // deleteUrl={(airlineid) => `${apiUrls.airline.url}/${airlineid}`}
      detailUrl={(airlineid) => `/airline/form/${airlineid}`}
      headerList={[
      "Airline",
      "Total Aviones",
      "Is Active" 
      ]}
      columns={(airline) => (
        <>
          <td >{airline.name}</td>
          <td>{airline.totalAirplanes}</td>
          <td style={{ fontWeight: "400" }}>
            <BadgeIsActive isActive={airline.isActive}/>
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

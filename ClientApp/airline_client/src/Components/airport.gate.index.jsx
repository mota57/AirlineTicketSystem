import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link, useParams, useRouteMatch } from "react-router-dom";
import { ModalDelete } from "./app.modals";
import GenericCrudTable from "../utils/GenericCrudTable";
import BadgeIsActive from "../utils/BadgeIsActive";

export default function GateIndexComponent(props) {
  const { recordid } = useParams(); //airportid
  const { url } = useRouteMatch();
  
  return (
    <GenericCrudTable
      loadUrl={`${apiUrls.gate().url}?airportId=${recordid}`}
      // deleteUrl={(gateid) => `${apiUrls.airline.url}/${airlineid}`}
      createUrl={`${url}/form/0`}
      detailUrl={(gateid) => `${url}/form/${gateid}`}
      headerList={["Puerta", "Aerolineas" , "Estatus" ]}
      columns={(gate) => (
        <>
          <td >{gate.name}</td>
          <td>{gate.airlineName}</td>
          <td style={{ fontWeight: "400" }}>
            <BadgeIsActive isActive={gate.isActive}/>
          </td>
        </>
      )}
      actionHeader ={"Acciones"}
     
    />
  )
}
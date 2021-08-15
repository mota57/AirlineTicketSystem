import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";
import { Link, useParams, useRouteMatch } from "react-router-dom";
import ConfirmModal from '../utils/ConfirmModal';
import GenericCrudTable from '../utils/GenericCrudTable';


export default function TerminalIndexComponent(props) {
  const { recordid } = useParams(); //airportid
  const { url } = useRouteMatch();
  
  return (
    <GenericCrudTable
      loadUrl={`${apiUrls.terminal().url}?airportId=${recordid}`}
      deleteUrl={(terminalid) => `${apiUrls.terminal().url}/${terminalid}`}
      createUrl={`${url}/form/0`}
      detailUrl={(terminalid) => `${url}/form/${terminalid}`}
      headerList={["Terminal", "Aerolinea", "Estatus"]}
      actionHeader ={"Acciones"}
      columns={(terminal) => (
        <>
          <td >{terminal.name}</td>
          <td>{terminal.airlineName}</td>
          <td style={{ fontWeight: "400" }}>
            {terminal.isActive ? "Activo" : "Inactivo"}
          </td>
        </>
      )}
     
    />
  )
}


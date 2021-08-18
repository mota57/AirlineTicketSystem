import { useEffect, useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";

import { Link, useParams } from "react-router-dom";
import { ModalDelete } from "./app.modals";
import ConfirmModal from "../utils/ConfirmModal";
import GenericCrudTable from "../utils/GenericCrudTable";

export default function AirplaneComponent(props) {
  const { airlineid } = useParams();

  console.log("airlineid::", airlineid);

  if (isNaN(airlineid) || Number(airlineid) <= 0) {
    return (
      <div className="alert alert-danger">
        Recurso no encontrado
        <Link to="/airline"> Ir a aerolineas</Link>
      </div>
    );
  }
  return (
    <>
      <div className="container">
        <h1>Aviones de la aerolinea {airlineid}</h1>
      </div>
      <GenericCrudTable
        loadUrl={`${apiUrls.airplane.byairlineid}/${airlineid}`}
        createUrl={`/airplane/form?airlineid=${airlineid}`}
        // deleteUrl={(terminalid) => `${apiUrls.airplane.url}?airplaneId=${airplaneId}`}
        // detailUrl={(terminalid) => `${url}/form/${terminalid}`}
        // actionHeader ={""}
        headerList={[
          "Marca",
          "Modelo",
          "Codigo",
          "Total de Asientos",
          "Acciones",
        ]}
        columns={(airplane) => (
          <>
            <td>{airplane.brand}</td>
            <td>{airplane.model}</td>
            <td>{airplane.code}</td>
            <td>{airplane.totalSeats}</td>
            <td>
              <div style={{ width: "200px" }}>
                <Link
                  to={`/airplane/form?airlineid=${airlineid}&airplaneid=${airplane.id}`}
                  className="btn btn-success m-r-1-sm"
                >
                  Editar
                </Link>
                {/* <button className="btn btn-danger" onClick={() => ConfirmModal(() => handleDelete(airplane.id) ) }>Delete</button> */}
              </div>
            </td>
          </>
        )}
      />
    </>
  );
}

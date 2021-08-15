import apiUrls from "../../utils/endpoints";
import {useParams,useRouteMatch } from "react-router-dom";
import GenericCrudTable from '../../utils/GenericCrudTable';

export default function AirportAirlineIndex(props) {
  const { recordid } = useParams(); //airportid
  const { url } = useRouteMatch();
  
  return (
    <GenericCrudTable
      loadUrl={`${apiUrls.airline_airport.airlinesByAirportid}/${recordid}`}
      deleteUrl={(airlineid) => `${apiUrls.airline_airport.delete}/${airlineid}`}
      createUrl={`${url}/form/0`}
      detailUrl={(airlineid) => `${url}/form/${airlineid}`}
      headerList={["Aerolinea", "Estatus"]}
      actionHeader ={"Acciones"}
      columns={(record) => (
        <>
          <td >{record.airlineName}</td>
          <td style={{ fontWeight: "400" }}>
            {record.isActive ? "Activo" : "Inactivo"}
          </td>
        </>
      )}
     
    />
  )
}
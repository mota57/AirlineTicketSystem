import { useState } from "react";
import { changeHandlerBuilder, UrlSearchExtension, FormRawErrorHelper } from "../../utils/methods";
import { Form } from "react-bootstrap";
import { Redirect, useLocation } from "react-router-dom";
import FormError from "../form.error";
import apiUrls from "../../utils/endpoints";
import Picklist from "../picklist";
import axios from "axios";

export default function AirportAirlineForm(props) {
  var urlSearch = new UrlSearchExtension(useLocation().search);

  const id = urlSearch.getNumberParam("id");
  const isCreate = id == null || id <= 0 ? true : false;
  const airlineId = urlSearch.getNumberParam("airlineId");
  
  const airportId = urlSearch.getNumberParam("airportId");
  
  const [redirectTo, setRedirect] = useState(null);
  console.log("id::", id);

  const [state, setState] = useState({
    airportName: "LOADING",
    airlineId,
    airportId,
    formErrorObj: null,
  });

  useState(() => {
    //load airport content
    axios
      .get(`${apiUrls.airport.getbyid}/${airportId}`)
      .catch(() => setRedirect())
      .then((data) => setState({ ...state, airportName: data.data.name })); 
  }, []);

  function onSubmit(e) {
    e.preventDefault();
    
    var formError = new FormRawErrorHelper();
  
    if (state.airlineId == null || state.airlineId <= 0) {
      formError.pushError("Aerolinea", "Este campo es requerido");
    }

    if(formError.hasErrors()) {
      setState({ ...state, formErrorObj: formError });
      return;
    } else {
      setState({ ...state, formErrorObj: null });
    }

    if (isCreate) {
      axios
        .post(`${apiUrls.airline_airport.url}`, {
          airlineId: state.airlineId,
          airportId: state.airportId,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    } else {
      axios
        .put(`${apiUrls.airline_airport.url}/${id}`, {
          airlineId: state.airlineId,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    }
  }

  function redirectToIndex() {
    setRedirect(`/airport_airlines/${state.airportId}`);
  }

  const changeHandler = changeHandlerBuilder(setState, state);

  if (redirectTo) {
    return <Redirect to={redirectTo} />;
  }

  return (
    <>
      <pre>{JSON.stringify(state, null, 2)}</pre>
      <div className="container">
        <h3>Asociar una aerolinea</h3>

        <FormError formerrorobj={state.formErrorObj} />

        <form onSubmit={onSubmit} className="col-xs-6 col-md-6">
          <Form.Group className="mb-3" controlId="airportId">
            <Form.Label>Aeropuerto</Form.Label>
            <Form.Control
              type="text"
              disabled={true}
              value={state.airportName}
            />
          </Form.Group>
      
          <div className="form-group m-top-1">
            <Picklist
              recordid={state.airlineId}
              fieldName="airlineId"
              label="Aerolinea"
              url={`${apiUrls.airline_airport.getAirlinesToSelect}/${state.airportId}`}
              handleOnChange={changeHandler}
              logConsole={true}
            />
          </div>

          <div className="form-group m-top-1">
            <input
              type="submit"
              value="Guardar"
              className="btn btn-primary m-r-1-sm"
            />
            <input
              type="button"
              value="Cancelar"
              className="btn btn-danger"
              onClick={() => redirectToIndex()}
            />
          </div>
        </form>
      </div>
    </>
  );
}

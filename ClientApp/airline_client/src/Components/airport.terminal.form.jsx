import { useState } from "react";
import { changeHandlerBuilder, FormRawErrorHelper } from "../utils/methods";
import { Redirect, useParams, useRouteMatch } from "react-router-dom";
import FormError from "./form.error";
import apiUrls from "../utils/endpoints";
import Picklist from "./picklist";
import axios from "axios";

export default function TerminalFormComponent(props) {
  const { recordid, terminalid } = useParams();
  const isCreate = terminalid === null || terminalid <= 0 ? true : false;
  const [redirectTo, setRedirect] = useState(null);
  // const { path, url } = useRouteMatch();

  let airportId = recordid;

  let formApiUrl = apiUrls.terminal().url;
  let formByIdUrl = `${apiUrls.terminal().getbyid}/${terminalid}`

  const [state, setState] = useState({
    name: "",
    isActive: false,
    airlineId:0,
    formErrorObj: null,
  });

  useState(() => {
    //load content
    if (!isCreate) {
      axios
        .get(formByIdUrl)
        .catch(() => setRedirect())
        .then((data) =>
          setState({
            ...state,
            name: data.data.name,
            airlineId: data.data.airlineId,
            isActive: data.data.isActive,
          })
        );
    }
  }, []);

  function onSubmit(e) {
    e.preventDefault();

    var formRawError = new FormRawErrorHelper();

    if (state.name === null || state.name.trim().length === 0) {
      formRawError.pushError("Nombre Terminal", "Este campo es requerido");
    }

    if (formRawError.hasErrors()) {
      setState({ ...state, formErrorObj: formRawError.rawObj });
      return;
    } else {
      setState({ ...state, formErrorObj: null });
    }

    if (isCreate) {
      axios
        .post(`${formApiUrl}`, {
          airportId,
          name: state.name,
          airlineId: state.airlineId,
          isActive: state.isActive,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    } else {
      axios
        .put(`${formApiUrl}/${terminalid}`, {
          name: state.name,
          airlineId: state.airlineId,
          isActive: state.isActive,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    }
  }

  const changeHandler = changeHandlerBuilder(setState, state);

  function redirectToIndex() {
    setRedirect(`/airport/details/${airportId}/terminal`);
  }

  if (redirectTo) {
    return <Redirect to={redirectTo} />;
  }

  return (
    <>
      <pre>{JSON.stringify(state, null, 2)}</pre>
      <div className="container">
        <FormError formerrorobj={state.formErrorObj} />
        <h4 className="mb-3">Terminal </h4>
        <form onSubmit={onSubmit} className="col-xs-6 col-md-6">
          <div className="form-group">
            <label className="form-label">Nombre </label>
            <input
              type="text"
              className="form-control"
              name="name"
              value={state.name}
              onChange={changeHandler}
            />
          </div>

          <div className="form-check m-top-1">
            <input
              checked={state.isActive}
              type="checkbox"
              className="form-check-input"
              value={state.isActive || false}
              name="isActive"
              onChange={(e) => changeHandler(e, state.isActive)}
              id="active"
            />
            <label className="form-check-label" htmlFor="#active">
              Activo
            </label>
          </div>


          <Picklist
              recordid={state.airlineId}
              formName="airlineId"
              fieldName="airlineName"
              fieldValue="airlineId"
              label="Aerolinea"
              url={`${apiUrls.airline_airport.airlinesByAirportid}/${airportId}`}
              handleOnChange={changeHandler}
              logConsole={false}
            />

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

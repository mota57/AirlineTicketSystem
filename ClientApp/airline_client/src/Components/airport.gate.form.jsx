import { useState } from "react";
import { changeHandlerBuilder, FormRawErrorHelper } from "../utils/methods";
import { Redirect, useParams } from "react-router-dom";
import FormError from "./form.error";
import apiUrls from "../utils/endpoints";
import axios from "axios";
import Picklist from "./picklist";

export default function GateFormComponent(props) {
  const { recordid, gateid } = useParams();
  const isCreate = gateid === null || gateid <= 0 ? true : false;
  const [redirectTo, setRedirect] = useState(null);
  // const { path, url } = useRouteMatch();

  let airportId = recordid;

  let formApiUrl = apiUrls.gate().url;

  const [state, setState] = useState({
    name: "",
    isActive: false,
    airlinesId: [],
    formErrorObj: null,
  });

  useState(() => {
    //load airport content
    console.log("use state callled");
    if (!isCreate) {
      axios
        .get(`${apiUrls.gate().getbyid}/${gateid}`)
        .catch(() => setRedirect())
        .then((data) =>
          setState({
            ...state,
            name: data.data.name,
            isActive: data.data.isActive,
            airlinesId: data.data.airlinesId,
          })
        );
    }
  }, []);

  function onSubmit(e) {
    e.preventDefault();

    var formRawError = new FormRawErrorHelper();

    if (state.name === null || state.name.trim().length === 0) {
      formRawError.pushError("Nombre", "Este campo es requerido");
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
          airlinesId: state.airlinesId,
          isActive: state.isActive,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    } else {
      axios
        .put(`${formApiUrl}/${gateid}`, {
          name: state.name,
          isActive: state.isActive,
          airlinesId: state.airlinesId,
          airportId,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    }
  }

  const changeHandler = changeHandlerBuilder(setState, state);

  function redirectToIndex() {
    setRedirect(`/airport/details/${airportId}/gate`);
  }

  if (redirectTo) {
    return <Redirect to={redirectTo} />;
  }

  return (
    <div className="">
      <div className="row  justify-content-md-center">
        <div className="col-md-6">
         
            
          <div className="card">
            
            <div className="card-body">
              <div className="">
                <FormError formerrorobj={state.formErrorObj} />

              <h2>Puerta de embarque o desembarque</h2>
                <form onSubmit={onSubmit}>
                  <div className="form-group">
                    <label className="form-label">Name </label>
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
                    recordid={state.airlinesId}
                    formName={`airlinesId`}
                    label="Aerolinea"
                    url={`${apiUrls.airline_airport.airlinesByAirportid}/${airportId}`}
                    handleOnChange={changeHandler}
                    logConsole={false}
                    fieldName="airlineName"
                    fieldValue="airlineId"
                    multiple={true}
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
            </div>
          </div>
        </div>
      </div>

      <div className="col-md-12">

<pre>{JSON.stringify(state, null, 2)}</pre>
</div>
    </div>
  );
}

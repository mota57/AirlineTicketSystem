import { useState } from "react";
import {
  changeHandlerBuilder,
  UrlSearchExtension,
  FormRawErrorHelper,
} from "../../utils/methods";
import { Form } from "react-bootstrap";
import { Redirect, useParams } from "react-router-dom";
import FormError from "../form.error";
import apiUrls from "../../utils/endpoints";
import Picklist from "../picklist";
import axios from "axios";

export default function AirportAirlineForm(props) {
  const { recordid, id } = useParams();

  const isCreate = id === null || id <= 0 ? true : false;

  const [redirectTo, setRedirect] = useState(null);

  let airportId = recordid;

  const [state, setState] = useState({
    id,
    airportName: "",
    airlineName: "",
    airlineId: -1,
    airportId,
    isActive: false,
    formErrorObj: null,
  });

  useState(() => {
    //load airport content
    if (!isCreate) {
      axios
        .get(`${apiUrls.airline_airport.url}/${id}`)
        .catch(() => setRedirect())
        .then((data) =>
          setState({
            ...state,
            airportName: data.data.airportName,
            airlineName: data.data.airlineName,
            airlineId: data.data.airlineId,
            isActive: data.data.isActive,
          })
        );
    }
  }, []);

  function onSubmit(e) {
    e.preventDefault();

    var formError = new FormRawErrorHelper();

    if (state.airlineId === null || state.airlineId <= 0) {
      formError.pushError("Aerolinea", "Este campo es requerido");
    }

    if (formError.hasErrors()) {
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
          isActive: state.isActive,
        })
        .then(() => redirectToIndex())
        .catch((data) => {
          setState({ ...state, formErrorObj: data });
        });
    } else {
      axios
        .put(`${apiUrls.airline_airport.url}/${id}`, {
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
    setRedirect(`/airport/details/${airportId}/airport_airlines`);
  }

  if (redirectTo) {
    return <Redirect to={redirectTo} />;
  }
  return (
    <div className="row justify-content-md-center">
      <div className="col-md-5">
        <div className="card">
          <div className="card-body">
          
            <div className="">
            { isCreate ?
              (<h3>Asociar una aerolinea</h3>)
              :(<h3>Aerolinea asociada</h3>)
            }
              <FormError formerrorobj={state.formErrorObj} />

              <form onSubmit={onSubmit}>
                {isCreate ? (
                  <div className="form-group m-top-1">
                    <Picklist
                      recordid={state.airlineId}
                      formName="airlineId"
                      label="Aerolinea"
                      url={`${apiUrls.airline_airport.getAirlinesToSelect}/${state.airportId}`}
                      handleOnChange={changeHandler}
                      logConsole={true}
                    />
                  </div>
                ) : (
                  <div className="form-group">
                    <label className="label">Aerolinea</label>
                    <input type="text" className="form-control" defaultValue={state.airlineName} disabled/>
                  </div>
                )}

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
  );
}

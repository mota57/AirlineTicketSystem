import axios from "axios";
import { useEffect, useState } from "react";
import apiUrls from "../utils/endpoints";
import { Redirect, useLocation, Link } from "react-router-dom";
import FormError from "./form.error";
import { changeHandlerBuilder, UrlSearchExtension } from "../utils/methods";

export default function AirplaneFormComponent(props) {
  const urlSearch = new UrlSearchExtension(useLocation().search);
  const airlineId = urlSearch.getNumberParam("airlineid");

  const airplaneId = urlSearch.getNumberParam("airplaneid");

  console.log(`recordid airlineid::${airlineId}, airplaneId::${airplaneId}`);

  const [record, setRecord] = useState({
    brand: "",
    model: "",
    code: "",
    totalSeats: 0,
    airlineId,
  });

  const recordid = airplaneId || 0;
  const [redictTo, setRedirect] = useState(null);
  const [formErrorObj, setFormErrorObj] = useState(null);

  const changeHandler = changeHandlerBuilder(setRecord, record);

  useEffect(() => {
    if (recordid > 0) {
      axios
        .get(`${apiUrls.airplane.getbyid}/${recordid}`)
        .then((data) =>
          setRecord({
            name: data.data.name,
            brand: data.data.brand,
            model: data.data.model,
            code: data.data.code,
            totalSeats: data.data.totalSeats,
            airlineId: data.data.airlineId,
          })
        )
        .catch((e) => {
          console.error(e);
        });
    }
  }, []);

  function onSubmit(e) {
    e.preventDefault();

    if (recordid <= 0) {
      axios
        .post(apiUrls.airplane.url, record)
        .then(() => setRedirect(`/airplane/${airlineId}`))
        .catch((data) => {
          setFormErrorObj(data);
        });
    } else {
      axios
        .put(`${apiUrls.airplane.url}/${recordid}`, record)
        .then(() => setRedirect(`/airplane/${airlineId}`))
        .catch((data) => {
          setFormErrorObj(data);
        });
    }
  }

  if (airlineId === 0) {
    return (
      <div className="alert alert-danger">
        Recurso no encontrado
        <Link to="/airline">Ir a aerolineas</Link>
      </div>
    );
  }

  if (redictTo != null) {
    return <Redirect to={redictTo} />;
  }

  return (
    <>
      {/* <pre>{JSON.stringify(formErrorObj, null, 2)}</pre> */}
      <div
        className="row justify-content-md-center"
        style={{ marginTop: "20px" }}
      >
        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              {/* body */}

              <h3>Avion</h3>

              <FormError formerrorobj={formErrorObj} />

              <form onSubmit={onSubmit} className="col-xs-6 col-md-6">
                <div className="form-group">
                  <label className="form-label">Marca </label>
                  <input
                    type="text"
                    className="form-control"
                    name="brand"
                    value={record.brand}
                    onChange={changeHandler}
                  />
                </div>
                <div className="form-group">
                  <label className="form-label">Model </label>
                  <input
                    type="text"
                    className="form-control"
                    name="model"
                    value={record.model}
                    onChange={changeHandler}
                  />
                </div>

                <div className="form-group">
                  <label className="form-label">Codigo </label>
                  <input
                    type="text"
                    className="form-control"
                    name="code"
                    value={record.code}
                    onChange={changeHandler}
                  />
                </div>

                <div className="form-group">
                  <label className="form-label">Total de asientos </label>
                  <input
                    type="number"
                    className="form-control"
                    name="totalSeats"
                    value={record.totalSeats}
                    onChange={changeHandler}
                  />
                </div>

                <div className="form-group m-top-1">
                  <input
                    type="submit"
                    value="Guardar"
                    className="btn btn-primary"
                  />
                  <input
                    type="button"
                    value="Cancelar"
                    className="btn btn-danger"
                    onClick={() => setRedirect(`/airplane/${airlineId}`)}
                  />
                </div>
              </form>

              {/* end body */}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

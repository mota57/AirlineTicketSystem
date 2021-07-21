import axios from "axios";
import { useEffect, useState } from "react";
import apiUrls from "../utils/endpoints";
import { Redirect, useParams } from "react-router-dom";
import FormError from "./form.error";
import { changeHandlerBuilder } from "../utils/methods";

export default function AirplaneFormComponent(props) {
  const { airplaneid, airlineid } = useParams();

  const [record, setRecord] = useState({
    name: "",
    brand: "",
    model: "",
    code: "",
    totalSeats: 0,
    airlineid,
  });

  const recordid = airplaneid || 0;
  const [shouldRedirect, setRedirect] = useState(false);
  const [formErrorObj, setFormErrorObj] = useState(null);

  console.log("recordid airplane", recordid);

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
          })
        )
        .catch((e) => console.error(e));
    }
  }, []);

  function onSubmit(e) {
    e.preventDefault();

    if (recordid <= 0) {
      axios
        .post(apiUrls.airplane.url, record)
        .then(() => setRedirect(true))
        .catch((data) => {
          setFormErrorObj(data);
        });
    } else {
      axios
        .put(`${apiUrls.airplane.url}/${recordid}`, record)
        .then(() => setRedirect(true))
        .catch((data) => {
          setFormErrorObj(data.response.data);
        });
    }
  }

  if (shouldRedirect) {
    let url = `/airplane`;
    return <Redirect to={url} />;
  }

  return (
    <>
      <pre>{JSON.stringify(record, null, 2)}</pre>
      <div className="container">
        <h3>Avion</h3>

        <FormError formerrorobj={formErrorObj} />

        <form onSubmit={onSubmit} className="col-xs-6 col-md-6">
          <div className="form-group">
            <label className="form-label">Name </label>
            <input
              type="text"
              className="form-control"
              name="name"
              value={record.name}
              onChange={changeHandler}
            />
          </div>

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

          {/* <div className="form-check m-top-1">
            <input
              checked={record.isActive}
              type="checkbox"
              className="form-check-input"
              value={record.isActive || false}
              name="isActive"
              onChange={(e) => changeHandler(e, record.isActive)}
              id="active"
            />
            <label className="form-check-label" htmlFor="#active">
              Activo 
            </label>
          </div> */}

          <div className="form-group m-top-1">
            <input type="submit" value="Guardar" className="btn btn-primary" />
            <input
              type="button"
              value="Cancelar"
              className="btn btn-danger"
              onClick={() => setRedirect(true)}
            />
          </div>
        </form>
      </div>
    </>
  );
}

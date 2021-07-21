import axios from "axios";
import { useEffect, useState } from "react";
import apiUrls from "../utils/endpoints";
import { Redirect, useParams } from "react-router-dom";
import FormError from "./form.error";
import { changeHandlerBuilder } from "../utils/methods";

export default function AirlineFormComponent(props) {
  const { airlineid } = useParams();
  const recordid = airlineid || 0;
  const [record, setRecord] = useState({
    name: "",
    isActive: true,
  });

  const [shouldRedirect, setRedirect] = useState(false);
  const [formErrorObj, setFormErrorObj] = useState(null);
  const changeHandler = changeHandlerBuilder(setRecord, record);

  
  console.log("recordid airline", recordid);

  useEffect(() => {
    if (recordid > 0) {
      axios
        .get(`${apiUrls.airline.getbyid}/${recordid}`)
        .then((data) =>
          setRecord({
            name: data.data.name,
            isActive: data.data.isActive,
          })
        )
        .catch((e) => console.error(e));
    }
  }, []);

  /*
  @param1 {url, record, onSuccess, setErrorState} props
  */
  function onSubmit(e) {
    e.preventDefault();
    if (recordid <= 0) {
      axios
        .post(apiUrls.airline.url, record)
        .then(() => setRedirect(true))
        .catch((data) => {
          setFormErrorObj(data);
        });
    } else {
      axios
        .put(`${apiUrls.airline.url}/${recordid}`, record)
        .then(() => setRedirect(true))
        .catch((data) => {
          setFormErrorObj(data.response.data);
        });
    }
  }

  if (shouldRedirect) {
    return <Redirect to={`/airline`} />;
  }

  return (
    <>
      <pre>{JSON.stringify(record, null, 2)}</pre>
      <div className="container">
        <h3>Aerolinea</h3>
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
          <div className="form-check m-top-1">
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
          </div>

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

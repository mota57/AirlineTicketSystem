import axios from "axios";
import { useEffect, useState } from "react";
import apiUrls from "../utils/endpoints";
import CountryPicklist from "./country.picklist";
import { Redirect, useParams } from "react-router-dom";
import FormError from './form.error';
import {changeHandlerBuilder} from '../utils/methods';


export function AirportCreateComponent(props) {
   return ( <AirportFormComponent recordid="0" redirect="/airport" />)
}

export function AirportEditComponent(props) {
  const { recordid } = useParams();
  return ( <AirportFormComponent recordid={recordid} redirect={`/airport/details/${recordid}`} />)
}


export function AirportFormComponent(props) {
 
  let recordid = props.recordid;

  const [record, setRecord] = useState( {
    name:'',
    isActive:true,
    code:'',
    countryId:-1
  })

  const [shouldRedirect, setRedirect] = useState(false);
  const [formErrorObj,  setFormErrorObj] = useState(null);
  

  const  changeHandler = changeHandlerBuilder(setRecord , record);
 

  useEffect(() => {
   
      if(recordid > 0) {
        axios
        .get(`${apiUrls.airport.getbyid}/${recordid}`)
        .then(data => setRecord({
          name:data.data.name,
          isActive:data.data.isActive,
          code:data.data.code,
          countryId:data.data.countryId
        }))
        .catch(e => console.error(e));
      }
   
  }, []);


  function onSubmit(e) {
    e.preventDefault();
    //why I put this?
    if(record.countryId === null || record.countryId <= 0) {
      setFormErrorObj({ rawerror: true, errors:{'Country': ['Este campo es requerido']}})
      return;
    }


    if(recordid <= 0) {
      axios
      .post(apiUrls.airport.url, record)
      .then(() => setRedirect(true))
      .catch((data) => {
       
        setFormErrorObj(data);
      });
    } else {
      axios
      .put(`${apiUrls.airport.url}/${recordid}`, record)
      .then(() => setRedirect(true))
      .catch((data) => {
        setFormErrorObj(data.response.data);
      });
    }
  }

  if (shouldRedirect) {
    // let url = recordid != null && recordid > 0 ? `/airport/details/${recordid}`: `/airport`;
    let url = props.redirect;
    return <Redirect to={url} />;
  }
  

  return (
    <>
    <pre>
      {JSON.stringify(record, null, 2) }
    </pre>
      <div className="container">
        <h3>Aeropuerto</h3>
     
          <FormError formerrorobj={formErrorObj}/>

       
        <form onSubmit={onSubmit} className="col-xs-6 col-md-6">
          <div className="form-group">
            <label className="form-label">Name </label>
            <input
              type="text"
              className="form-control"
              name="name"
              value={record.name }
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
            <label className="form-label">Codigo</label>
            <input
                type="text"
                className="form-control"
                value={record.code }
                name="code"
                onChange={changeHandler}
              />
          </div>
          <div className="form-group m-top-1">
            <CountryPicklist countryid={record.countryId} handleOnChange={changeHandler} />
          </div>

          <div className="form-group m-top-1">
            <input type="submit" value="Guardar" className="btn btn-primary" />
            <input type="button" value="Cancelar" className="btn btn-danger" onClick={() => setRedirect(true)}/>
          </div>
        </form>
      </div>
    </>
  );
}

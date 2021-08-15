import axios from "axios";
import { useEffect, useState } from "react";

import { Form } from "react-bootstrap";

/**
 * 
 * @param {value, label,fieldName,url, handleOnChange(e), logConsole=true|false } props 
 * The datasource expect a object like  {id:0, name:'...'}, if not use fieldKey, fieldValue.
 * @returns 
 *  MUST PASS FIELD NAME OTHERWISE IS NOT GOING TO WORK
 *  <Picklist 
              value={state.airlineId}
              fieldName={<propertyName>}
              fieldValue={<propertyValue>}
              name="airlineId" 
              label="Aerolinea" 
              url={`${apiUrls.airline.url}`} 
              handleOnChange={changeHandler}
              logConsole={true}
              />
 */




export default function FormGroupPickList(props) {
  
  const [state, setState] = useState({
    records: [],
    value: props.value ? props.value : -1,
  });

  useEffect(() => {
    axios
      .get(props.url)
      .catch((e) => console.error(e))
      .then((data) => {
        logInfo("picklist:: ", data.data);
        setState({ ...state, records: data.data, value: props.value });
      });
    }, []);
    
  logInfo("picklist::", state);
  
  function logInfo(label, data) {
    if (props.logConsole) {
      console.log(label, data);
    }
  }

  return (
    <>
      <label className="form-label">
        {props.label ? props.label : ""} 
      </label>
      <Form.Control
        as="select"
        name={props.name}
        value={state.value}
        onChange={(e) => {
          setState({ ...state, value: Number(e.target.value) });
          if(props.handleOnChange) {
            props.handleOnChange(e);
          }
        }}
      >
        <option value="-1" key="-1">
          ninguno
        </option>
        {state.records.map((r) => (
          <option 
            value={props.fieldValue ? r[props.fieldValue] : r.id} 
            key={r.id}
            >
            {props.fieldName ? r[props.fieldName] : r.name}
          </option>
        ))}
      </Form.Control>
    </>
  );
}









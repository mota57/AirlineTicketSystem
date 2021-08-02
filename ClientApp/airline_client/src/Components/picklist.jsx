import axios from "axios";
import { useEffect, useState } from "react";

import { Form } from "react-bootstrap";

/**
 * 
 * @param {recordid, label,fieldName,url, handleOnChange(e), logConsole=true|false } props 
 * @returns 
 *  MUST PASS FIELD NAME OTHERWISE IS NOT GOING TO WORK
 *  <Picklist 
              recordid={state.airlineId}
              fieldName={<propertyName>}
              fieldValue={<propertyValue>}
              formName="airlineId" 
              label="Aerolinea" 
              url={`${apiUrls.airline.url}`} 
              handleOnChange={changeHandler}
              logConsole={true}
              />
 */
export default function Picklist(props) {
  const [state, setState] = useState({
    records: [],
    recordId: props.recordid ? props.recordid : -1,
  });

  useEffect(() => {
    axios
      .get(props.url)
      .catch((e) => console.error(e))
      .then((data) => {
        logInfo("data from get url picklist::", data.data);
        setState({ ...state, records: data.data, recordId: props.recordid });
      });
    }, []);
    
  logInfo("state::", state);
  
  function logInfo(label, data) {
    if (props.logConsole) {
      console.log(label, data);
    }
  }

  return (
    <>
      <label className="form-label">
        {props.label} 
      </label>
      <Form.Control
        as="select"
        name={props.formName}
        value={state.recordId}
        onChange={(e) => {
          setState({ ...state, recordId: Number(e.target.value) });
          props.handleOnChange(e);
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

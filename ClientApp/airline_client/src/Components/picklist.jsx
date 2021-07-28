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
              fieldName="airlineId" 
              label="Aerolinea" 
              url={`${apiUrls.airline.url}`} 
              handleOnChange={changeHandler}
              logConsole={true}
              />
 */
export default function Picklist(props) {
  const [state, setState] = useState({ 
    records: [], 
    recordId: props.recordid ? props.recordid : -1
  });

  useEffect(() => {
    
      setState({ ...state, recordId: props.recordid });
    
  },[props.recordid]);


  useEffect(() => {
    axios
      .get(props.url)
      .catch((e) => console.error(e))
      .then((data) => {
          logInfo("data from get url picklist::", data.data);
          setState({ ...state, records: data.data, recordId: props.recordid});
      });
  }, []);



  function logInfo(label, data) {
    if (props.logConsole) {
      console.log(label, data);
    }
  }
  console.log('state::', state);

  return (
    <>
      <label className="form-label">
        {props.label} {state.recordId}
      </label>
      <Form.Control
        as="select"
        name={props.fieldName}
        value={state.recordId}
       
        onChange={(e) => {
          setState({ ...state,  recordId: Number(e.target.value) });
          props.handleOnChange(e);
        }}
      >
           <option value="-1" key="-1">ninguno</option>
        {state.records.map((r) => (
          <option 
            value={r.id} 
            key={r.id}
            >
            {r.name}
          </option>
        ))}
      </Form.Control>
    </>
  );
}

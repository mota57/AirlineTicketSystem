import axios from "axios";
import { useEffect, useState } from "react";



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
              multiple={true|false}
              />
 */
export default function Picklist(props) {
  console.log("recordid::", props.recordid);

  const [records, setRecords] = useState([]);
 

  useEffect(() => {
    if (props.url) {
      axios
        .get(props.url)
        .catch((e) => console.error(e))
        .then((data) => {
          logInfo("data from get url picklist::", data.data);
          setRecords(data.data);
        });
    }
  }, []);

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
      <select
        multiple={props.multiple ?? false}
        className="form-control"
        name={props.formName}
        value={props.recordid ?? -1}
        onChange={(e) => {
          props.handleOnChange(e);
        }}
      >
 
        <option value="" key="-1">
          ninguno
        </option>
        {records.map((r) => (
          <option
            value={props.fieldValue ? r[props.fieldValue] : r.id}
            key={r.id}
          >
            {props.fieldName ? r[props.fieldName] : r.name}
          </option>
        ))}
      </select>
    </>
  );
}

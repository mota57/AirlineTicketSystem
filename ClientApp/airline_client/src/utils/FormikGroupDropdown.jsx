import { Field, ErrorMessage, useField } from "formik";
import axios from "axios";
import { FormErrorMessage } from "./FormErrorMessages";
import { useState, useEffect } from "react";
import { useFormikContext } from "formik";
/*
<FormikGroupDropdown 
              records={records...}
              fieldName={<propertyName>}
              fieldValue={<propertyValue>}
              name="airlineId" 
              label="Aerolinea" 
              url={`${apiUrls.airline.url}`} 
              logConsole={true}
              /></FormikGroupDropdown>
              
    */



//props{records, value, url, label, name, fieldValue, fieldName}
export default function FormikGroupDropdown(props) {
  const { setFieldValue } = useFormikContext();
  const [state, setState] = useState({
    records: props.records != null ? props.records : [],
    // value: props.value ? props.value : -1,
  });
  const [field, meta, helpers] = useField(props);

  useEffect(() => {
    if (props.url) {
      axios
        .get(props.url)
        .catch((e) => console.error(e))
        .then((data) => {
          logInfo("picklist:: ", data.data);
          setState({ ...state, records: data.data });
        });
    } else {
      setState({...state, records:[]})
    }
  }, [props.url]);

  logInfo("picklist::", state);

  function logInfo(label, data) {
    if (props.logConsole) {
      console.log(label, data);
    }
  }

  let mapProps = (propObj) => {
     let output ={};
     let ignoreProperties = ['records', 'url', 'label', 'name', 'fieldValue', 'fieldName','logConsole'];
     Object.keys(propObj).forEach(key => {
        if(ignoreProperties.indexOf(key) === -1){
          output[key] = propObj[key];
        }
     });
     return output;
  };

  return (
    <>
      <label className="form-label">{props.label ? props.label : ""}</label>
      <select
       {...field}
       {...mapProps(props)}
        className="form-select"
        name={props.name}
        onChange={(event) => {
          //setState({ ...state, value: Number(event.target.value) });
          setFieldValue(props.name, Number(event.target.value));
        }}
      >
        <option value={0} key="0">
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
      </select>
      <ErrorMessage name={props.name}>
        {(message) => <FormErrorMessage message={message} />}
      </ErrorMessage>
    </>
  );
}


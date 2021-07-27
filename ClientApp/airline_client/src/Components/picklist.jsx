import axios from "axios"
import { useEffect, useState } from "react"

import {Form} from 'react-bootstrap';

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
    const [records, setRecords] = useState([]);
    
    const [recordId, setRecordId] = useState(props.recordid ? props.recordid : -1);


    useEffect( () =>{
        axios.get(props.url)
        .catch(e => console.error(e))
        .then(data => {
          logInfo('data from get url picklist::', data)
          setRecords(data.data)
        });
    },[])

    function logInfo(label, data) {
      if(props.logConsole) {
          console.log(label, data);
      }
    }

    return (
        <>
        <label className="form-label">{props.label}  </label>  
        {/* {recordId} */}
       
        <Form.Control
          as="select"
          name={props.fieldName}
          value={recordId}
          onChange={(e) => {
            setRecordId(e.target.value); 
            props.handleOnChange(e);
          }}
        >
             <option value="-1" key="-1">ninguno</option>
         {records.map(r => (<option value={r.id} key={r.id}>{r.name}</option>))}
        </Form.Control>
        </>
      )

}
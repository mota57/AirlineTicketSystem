import axios from "axios"
import { useEffect, useState } from "react"
import apiUrls from '../utils/endpoints';
import {Form} from 'react-bootstrap';

/**
 * 
 * @param {recordid, label,fieldname,url, handleOnChange(e), } props 
 * @returns 
 */
export default function Picklist(props) {
    const [records, setRecords] = useState([]);
    
    const [recordId, setRecordId] = useState(props.recordid ? props.recordid : -1);
    
    useEffect(() => {
      if(props.recordid != null) {
        setRecordId(props.recordid);
      }  
    },[props.recordid]);

    useEffect( () =>{
        axios.get(url)
        .catch(e => console.error(e))
        .then(data => {
          console.log(data);
          setRecords(data.data)
        });
    },[])

    return (
        <>
        <label className="form-label">{props.label}  </label>  
       
        <Form.Control
          as="select"
          name={props.fieldname}
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
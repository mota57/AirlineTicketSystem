import axios from "axios"
import { useEffect, useState } from "react"
import apiUrls from '../utils/endpoints';
import {Form} from 'react-bootstrap';

export default function CountryPicklist(props) {
    const [countries, setCountry] = useState([]);
    
    const [countryId, setCountryId] = useState(props.countryid ? props.countryid : -1);
    // if(props.countryid != null) {
    //   console.log('props country', props);
    //   setCountryId(props.countryid)
    // }
    useEffect(() => {
      if(props.countryid != null) {
        setCountryId(props.countryid);
      }  
    },[props.countryid]);

    useEffect( () =>{
        axios.get(apiUrls.countries.url)
        .catch(e => console.error(e))
        .then(data => {
          console.log(data);
          setCountry(data.data)
        });
    },[])

    return (
        <>
        <label className="form-label">Pais  </label>  
       
        <Form.Control
          as="select"
          name="countryId"
          value={countryId}
          onChange={(e) => {
            setCountryId(e.target.value); 
            props.handleOnChange(e);
          }}
        >
             <option value="-1" key="-1">ninguno</option>
         {countries.map(country => (<option value={country.id} key={country.id}>{country.name}</option>))}
        </Form.Control>
        </>
      )

}
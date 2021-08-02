import { useEffect,useState } from "react";
import { Link,  useParams} from "react-router-dom"
import axios from 'axios';
import apiUrls from '../utils/endpoints';
export default function AirlineDashboard(props) {   
    var { recordid } = useParams();

    const [record, setRecord] = useState( {
      name:'',
      isActive:true,
    })

    useEffect(() => {
      axios
      .get(`${apiUrls.airline.getbyid}/${recordid}`)
      .then(data => setRecord({
        name:data.data.name,
        isActive:data.data.isActive,
      }))
    }, [])
    return(
        <>
  <h1>{record.name}</h1>
<div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4 py-5">
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#bootstrap"></use></svg>
        <div>
          {/* <h4 className="fw-bold mb-0">Modificar</h4> */}
          <Link className="btn btn-primary" 
          to={{ pathname:`/airline/form/${recordid}`, state: { recordid:recordid }} }>Modificar aerolinea</Link>
        </div>
      </div>

      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#bootstrap"></use></svg>
        <div>
          {/* <h4 className="fw-bold mb-0">Modificar</h4> */}
          <Link className="btn btn-primary" 
          to={{ pathname:`/airplane/airlineid=:${recordid}`, state: { recordid:recordid }} }>Aviones</Link>
        </div>
      </div>
      

      
      
      
    </div>
        </>
    )
}
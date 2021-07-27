import { useEffect,useState } from "react";
import { Link, useLocation, useParams} from "react-router-dom"
import axios from 'axios';
import apiUrls from '../utils/endpoints';
export default function AirportDashboard(props) {   
    var { recordid } = useParams();

    const [record, setRecord] = useState( {
      name:'',
      isActive:true,
      code:'',
      countryId:-1
    })

    useEffect(() => {
      axios
      .get(`${apiUrls.airport.getbyid}/${recordid}`)
      .then(data => setRecord({
        name:data.data.name,
        isActive:data.data.isActive,
        code:data.data.code,
        countryId:data.data.countryId
      }))
    }, [])
    return(
        <>
  <h1>{record.name}</h1>
<div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4 py-5">
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#bootstrap"></use></svg>
        <div>
          {/* <h4 className="fw-bold mb-0">Datos</h4> */}
          <Link className="btn btn-primary" 
          to={{ pathname:`/airport/form/${recordid}`, state: { recordid:recordid }} }>Datos</Link>
        </div>
      </div>
       <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#cpu-fill"></use></svg>
        <div>
          {/* <h4 className="fw-bold mb-0">Aerolineas</h4> */}
          <Link className="btn btn-primary" 
          to={`/airport_airlines/${recordid}`}>Aerolineas</Link>
        </div>
      </div>
     {/* <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#calendar3"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div>
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#home"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div>
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#speedometer2"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div>
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#toggles2"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div>
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#geo-fill"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div>
      <div className="col d-flex align-items-start">
        <svg className="bi text-muted flex-shrink-0 me-3" width="1.75em" height="1.75em"><use xlinkHref="#tools"></use></svg>
        <div>
          <h4 className="fw-bold mb-0">Featured title</h4>
          <p>Paragraph of text beneath the heading to explain the heading.</p>
        </div>
      </div> */}
    </div>
        </>
    )
}
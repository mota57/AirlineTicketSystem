import { useEffect,useState } from "react";
import { Link,  useParams, Route, Switch, useRouteMatch} from "react-router-dom"
import axios from 'axios';
import apiUrls from '../utils/endpoints';
import RouteAirportConfig from "../route.airport.config";

export default function AirportDashboard(props) {   
    var { recordid } = useParams();
    let { path } = useRouteMatch();
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

    return (
      <div className="container" style={{backgroundColor:"#ebedef"}}>
        
        <div className="border-bottom  justify-content-between mb-3 pb-2 pt-3 text-center">
          <h1 className="h2"><Link to={`/airport/details/${recordid}`}>{record.name}</Link></h1>
        </div>

          <Switch>
            {RouteAirportConfig.map((r) => (
                <Route key={r.path} path={`${path}${r.path}`} exact={r.exact}>
                  <r.componente></r.componente>
                </Route>
              ))}
              <Route exact path={path}>
              <DashboardMenu airportid={recordid} />
            </Route>
          </Switch>
      </div>
    )
}


function DashboardMenu ({airportid}) {
  const pathUrl = `/airport/details/${airportid}`
  var options = [
    {
      icon: "fa fa-window-maximize",
      label: "Datos Basicos",
      url: `${pathUrl}/edit`,
      labelUrl: "Editar",
      stateUrl: { airportid },
    },

    {
      icon: "fa fa-plane",
      label: "Aerolineas",
      url: `${pathUrl}/airport_airlines/${airportid}`,
      stateUrl: { airportid },
      labelUrl: "Acceder",
    },

    {
      icon: "fa fa-flag",
      label: "Puertas Embarque/Desembarque",
      url: `${pathUrl}/gate`,
      stateUrl: { airportid },
      labelUrl: "Acceder",
    },
    {
      icon: "fa fa-taxi",
      label: "Terminales",
      url: `${pathUrl}/terminal`,
      stateUrl: { airportid },
      labelUrl: "Acceder",
    }  
  ];


  return (
    <div >
      <div className="row">
        {options.map((menu) => (
          <div className="col-sm-4 m-b-1" key={menu.url}>
            <div className="card">
              <div className="card-body">
                <label className="card-title">
                  <i
                    className={` ${ menu.icon ? menu.icon : "fa fa-window-maximize"} bi text-muted flex-shrink-0 me-3`}
                    style={{ fontSize: "30px" }}
                  ></i>
                  {menu.label}
                </label>
                <br/>
                <Link  className="btn btn-primary"
                   to={{
                    pathname: [menu.url],
                    state: { ...menu.stateUrl },
                  }}
                >
                  {menu.labelUrl}
                </Link>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
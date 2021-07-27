import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import RouteConfig from "./route.config";

import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import SideBarComponent from "./Components/sidebar";

import AirportComponent from "./Components/airport.index";
import AirportDashboard from "./Components/airport.dashboard";
import AirportFormComponent from "./Components/airport.form";
import AirlineComponent from "./Components/airline.index";
import AirlineFormComponent from "./Components/airline.form";
import AirplaneComponent from "./Components/airplane.index";
import AirplaneFormComponent from "./Components/airplane.form";
import ErrorBoundary from "./Components/ErrorBoundary";
// import AirportAirlineIndex from "./Components/airport.airlines/airport.airlines.index";

function App() {
  return (
    <Router>
      <div className=" row">
        <SideBarComponent />
          <div className="col-md-9 ">
            <Switch>
            

              {RouteConfig.map((r) => (
                <Route key={r.path} path={r.path} exact={r.exact}>
                  <r.componente></r.componente>
                </Route>
              ))}
            </Switch>
          </div>
      </div>
    </Router>
  );
}

export default App;

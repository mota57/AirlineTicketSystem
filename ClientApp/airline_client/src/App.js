import logo from "./logo.svg";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";


import AirportComponent from "./Components/airport.index";
import AirportFormComponent from "./Components/airport.form";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import SideBarComponent from "./Components/sidebar";
import AirportDashboard from "./Components/airport.dashboard";

import AirlineComponent from "./Components/airline.index";
import AirlineFormComponent from "./Components/airline.form";
import AirplaneFormComponent from "./Components/airplane.form";
import AirplaneComponent from "./Components/airplane.index";

function App() {
  return (
    <Router>
      <div className=" row" >
       
        <SideBarComponent/>
        

        <div className="col-md-9 ">
          <Switch>
          <Route path="/airplane/:airlineid">
             <AirplaneComponent/>
            </Route>

          
            <Route path={`/airplane/form`}>
              <AirplaneFormComponent />
            </Route>

            

          <Route path="/airline/form/:airlineid">
              <AirlineFormComponent />
            </Route>

           

          <Route path="/airline">
              <AirlineComponent />
            </Route>


            <Route path="/airport/form">
              <AirportFormComponent />
            </Route>
            <Route path="/airport/dashboard/:recordid">
              <AirportDashboard />
            </Route>
            <Route path="/">
              <AirportComponent />
            </Route>
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;

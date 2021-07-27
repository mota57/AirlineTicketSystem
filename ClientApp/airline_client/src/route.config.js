import AirportDashboard from "./Components/airport.dashboard";
import AirportFormComponent from "./Components/airport.form";
import AirlineComponent from "./Components/airline.index";
import AirlineFormComponent from "./Components/airline.form";
import AirplaneComponent from "./Components/airplane.index";
import AirplaneFormComponent from "./Components/airplane.form";
import AirportAirlineIndex from "./Components/airport.airlines/airport.airlines.index";
import AirportAirlineForm from "./Components/airport.airlines/airport.airlines.form";
import AirportComponent from "./Components/airport.index";
import ErrorPage from './Components/ErrorPage';

  {/* <Route path="/airplane/:airlineid">
              <AirplaneComponent />
              </Route>
              <Route path="/airplane/form">
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
            </Route> */}



const RouteConfig = [
   {path: '/error_page/:status', componente: ErrorPage},

   {path: '/airport_airlines/form', componente: AirportAirlineForm},
   {path: '/airport_airlines/:airportid', componente: AirportAirlineIndex},
    
   {path: '/airplane/form', componente: AirplaneFormComponent},
   {path: '/airplane/:airlineid', componente: AirplaneComponent, },

    {path: '/airline/form/:airlineid', componente: AirlineFormComponent},
    {path: '/airline', componente: AirlineComponent, },

    {path: '/airport/form', componente: AirportFormComponent},
    {path: '/airport/dashboard/:recordid', componente: AirportDashboard, },

    {path: '/airport', componente: AirportComponent },
    {path: '/', componente: AirportComponent, exact:true },
]


    


  

export default RouteConfig
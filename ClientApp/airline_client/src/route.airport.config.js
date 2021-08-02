import GateIndexComponent from "./Components/airport.gate.index";
import GateFormComponent from './Components/airport.gate.form';
import TerminalIndexComponent from './Components/airport.terminal.index';
import TerminalFormComponent from  './Components/airport.terminal.form';
import AirportAirlineForm from "./Components/airport.airlines/airport.airlines.form";
import AirportAirlineIndex from "./Components/airport.airlines/airport.airlines.index";
import {AirportEditComponent} from "./Components/airport.form";

const RouteAirportConfig = [
  {path: '/gate/form/:gateid', componente: GateFormComponent },
  {path: '/gate', componente: GateIndexComponent },
  {path: '/terminal/form/:terminalid', componente: TerminalFormComponent },
  {path: '/terminal', componente: TerminalIndexComponent },
  {path: '/airport_airlines/form/:id', componente: AirportAirlineForm},
  {path: '/airport_airlines', componente: AirportAirlineIndex},
  {path: '/edit', componente: AirportEditComponent},
]

export default RouteAirportConfig;
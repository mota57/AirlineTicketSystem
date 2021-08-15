import AirportDashboard from "./Components/airport.dashboard";
import { AirportCreateComponent } from "./Components/airport.form";
import AirlineComponent from "./Components/airline.index";
import AirlineFormComponent from "./Components/airline.form";
import AirplaneComponent from "./Components/airplane.index";
import AirplaneFormComponent from "./Components/airplane.form";
import AirportComponent from "./Components/airport.index";
import ErrorPage from "./Components/ErrorPage";
import { AirlineBagPriceFormView } from "./Components/airline.bagprice";
import TodoComponent from "./Components/TodoComponent";
import { TicketIndex, CreateTickets, EditTickets } from "./Components/airport.ticket";


const RouteConfig = [
  { path: "/ticketAdmin", componente: TicketIndex },
  { path: "/flight/create", componente: CreateTickets },
  { path: "/flight/edit/:flightid", componente: EditTickets },
  { path: "/todo", componente: TodoComponent },

  { path: "/error_page/:status", componente: ErrorPage },

  { path: "/airplane/form", componente: AirplaneFormComponent },
  { path: "/airplane/:airlineid", componente: AirplaneComponent },

  { path: "/airline/form/:airlineid", componente: AirlineFormComponent },
  { path: "/bagprice/:airlineid", componente: AirlineBagPriceFormView },
  { path: "/airline", componente: AirlineComponent },

  { path: "/airport/create", componente: AirportCreateComponent },
  { path: "/airport/details/:recordid", componente: AirportDashboard },

  { path: "/airport", componente: AirportComponent },
  { path: "/", componente: AirportComponent, exact: true },
];

export default RouteConfig;

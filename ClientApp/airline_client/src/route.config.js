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
import FlightsDashboard from "./Components/flights.dashboard";


const RouteConfig = [
  { path: "/flights", componente: FlightsDashboard, isAdmin:false, sidebar:true, label:'Vuelos', order:4},

  { path: "/ticketAdmin", componente: TicketIndex , isAdmin:true, sidebar:true, label:'Tickets',order:3 },
  { path: "/flight/create", componente: CreateTickets },
  { path: "/flight/edit/:flightid", componente: EditTickets },
  { path: "/todo", componente: TodoComponent },

  { path: "/error_page/:status", componente: ErrorPage },

  { path: "/airplane/form", componente: AirplaneFormComponent },
  { path: "/airplane/:airlineid", componente: AirplaneComponent },

  { path: "/airline/form/:airlineid", componente: AirlineFormComponent },
  { path: "/bagprice/:airlineid", componente: AirlineBagPriceFormView },
  { path: "/airline", componente: AirlineComponent ,isAdmin:true, sidebar:true, label:'Aerolinea', order:2},

  { path: "/airport/create", componente: AirportCreateComponent },
  { path: "/airport/details/:recordid", componente: AirportDashboard },

  { path: "/airport", componente: AirportComponent, isAdmin:true, sidebar:true, label:'Aeropuertos' ,order:1},
  { path: "/", componente: AirportComponent, exact: true },
];

// const sideBarObj  = [
//   {
//     label: "Airports",
//     isActive: false,
//     url:'/airport'
//   },
//   {
//     label: "Airline",
//     isActive: false,
//     url:'/airline'
//   },
//   {
//     label: "Tickets",
//     isActive: false,
//     url:'/ticketAdmin',
//     isAdmin:true
//   },
//   {
//     label: "Vuelos",
//     isActive: false,
//     url:'/flights'
//   },
// ]

export default RouteConfig;

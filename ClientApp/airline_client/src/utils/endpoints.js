const apiUrl = process.env.REACT_APP_APP_URL

const endpoints = {
    airport: {
        url: `${apiUrl}/airportapi`,
        getbyid: `${apiUrl}/airportapi/getbyid`,
    },
    countries: {
        url:`${apiUrl}/countryapi`
    },
    airline: {
        url:`${apiUrl}/airlineapi`,
        getbyid:`${apiUrl}/airlineapi/getbyid`,
    },
    airplane: {
        url:`${apiUrl}/airplaneapi`,
        getbyid:`${apiUrl}/airplaneapi/getbyid`,
        byairlineid:`${apiUrl}/airplaneapi/GetByAirlineId`
    },
    airline_airport: {
        url:`${apiUrl}/AirportAirlineApi`,
        airlinesByAirportid:`${apiUrl}/AirportAirlineApi/GetAirlinesByAirportId`,
        getAirlinesToSelect:`${apiUrl}/AirportAirlineApi/GetAirlinesToSelect`,
        getAirportByAirlineId:(airlineid) =>  airlineid ? `${apiUrl}/AirportAirlineApi/GetAirportByAirlineId/${airlineid}` : null,
        delete:`${apiUrl}/AirportAirlineApi/DeleteAirlineAirport`,
    },
    gate: () => {
        let url = `${apiUrl}/gateapi`;
        return {
            url,
            getbyid:`${url}/getbyid`,
            getGatesByAirportAirline: (airportid, airlineid) => {
                if(airportid > 0 && airlineid > 0 ) {
                    return `${url}/getGatesByAirportAirline?airportid=${airportid}&airlineid=${airlineid}`;
                } 
                return null;
            }
        }
    },
    terminal: () => {
        let url = `${apiUrl}/TerminalApi`;
        return {
            url,
            getbyid:`${url}/getbyid`,
            getAirlines:`${url}/GetAirlinesForTerminalsToSelect`,
            getTerminalByParams:(airlineid, airportid) =>`${url}/getTerminalByParams?airlineid=${airlineid || 0}&airportid=${airportid || 0}`
         }
    },
    bagPrice: () => {
        let url = `${apiUrl}/bagpriceapi`;
        return {
            url,
            getByAirlineId: (airlineid) => `${url}?airlineid=${airlineid}`,
            delete: (id) => `${url}/${id}`,
            update: (id) => `${url}/${id}`
        }
    },
    bagPriceDetail: () => {
        let url = `${apiUrl}/BagPriceDetailApi`;
        return {
            url,
            update: (id) => `${url}/${id}`,
            delete: (id) => `${url}/${id}`
        }
    },
    flight: () => {
        let url = `${apiUrl}/flightapi`;
        return {
            url,
            byId: (id) => `${url}/${id}`,
            update: (id) => `${url}/${id}`,
            delete: (id) => `${url}/${id}`
        }
    },
    flightScale: () => {
        let url = `${apiUrl}/flightScaleapi`;
        return {
            url,
            byId: (id) => `${url}/${id}`,
            update: (id) => `${url}/${id}`,
            delete: (id) => `${url}/${id}`
        }
    },
    flightEcommerce: () => {
        let url = `${apiUrl}/FlightEcommerceApi`;
        return  {
            search: `${url}/SearchAirportCountry`,
            getAvailableFlights:`${url}/getAvailableFlights`
        }
    }
}


export default  endpoints;
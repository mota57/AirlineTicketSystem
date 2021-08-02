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
        delete:`${apiUrl}/AirportAirlineApi/DeleteAirlineAirport`,
    },
    gate: () => {
        let url = `${apiUrl}/gateapi`;
        return {
            url,
            getbyid:`${url}/getbyid`,
        }
    },
    terminal: () => {
        let url = `${apiUrl}/TerminalApi`;
        return {
            url,
            getbyid:`${url}/getbyid`,
            getAirlines:`${url}/GetAirlinesForTerminalsToSelect`
        }
    },
    bagPrice: () => {
        let url = `${apiUrl}/bagpriceapi`;
        return {
            getByAirlineId: (airlineid) => `${url}/getByAirlineId/${airlineid}`,
        }
    }
}


export default  endpoints;
const apiUrl = process.env.REACT_APP_APP_URL



var apiUrls = {
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
        getAirlinesToSelect:`${apiUrl}/AirportAirlineApi/GetAirlinesToSelect`
    }
}



export default apiUrls;
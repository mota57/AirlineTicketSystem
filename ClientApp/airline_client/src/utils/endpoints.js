const apiUrl = process.env.REACT_APP_APP_URL



var applicationApi = {
    airport: {
        url: `${apiUrl}/airportapi`,
        getbyid: `${apiUrl}/airportapi/getbyid`,
    },
    countries: {
        url:`${apiUrl}/countryapi`
    },
    airline: {
        url:`${apiUrl}/airlineapi`,
        getbyid:`${apiUrl}/airlineapi/getbyid`
    },
    airplane: {
        url:`${apiUrl}/airplaneapi`,
        getbyid:`${apiUrl}/airplaneapi/getbyid`
    }
}



export default applicationApi;
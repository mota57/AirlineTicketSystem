import { Row, FloatingLabel, Form, Col, Card, Button } from "react-bootstrap";
import apiUrls from "../utils/endpoints";
import { useState } from "react";
import { Formik, ErrorMessage, useFormikContext } from "formik";
import * as Yup from "yup";
import axios from "axios";
import { AsyncTypeahead } from "react-bootstrap-typeahead";

export default function FlightsDashboard() {
  const [flights, setFlights] = useState([
    {
      dateTimeStart: new Date().toLocaleDateString(),
      dateTimeEnd: getDayAfter().toLocaleDateString(),
      countryOrigin: "Dominican Republic",
      countryDestiny: "Orlando",
      airlineName: "American Airline",
      totalTime: "16h 36",
      totalScales: 2,
      scalesInfo: [
        {
          totalTime: "2h 11min",
          airportDestiny: "MIAMI (MIA)",
        },
        {
          totalTime: "3h 11min",
          airportDestiny: "ATLANTA",
        },
      ],
      price: "$233",
    },
  ]);

  function getDayAfter() {
    var date = new Date();
    date.setDate(date.getDate() + 1);
    return date;
  }

  function getDayBefore() {
    var date = new Date();
    date.setDate(date.getDate() - 1);
    return date;
  }

  const shema = Yup.object().shape({
    airportDepartureId: Yup.mixed().notOneOf([null], "Este campo es requerido"),
    airportArrivalId: Yup.mixed().notOneOf(
      [null, "", Yup.ref("airportDepartureId")],
      "Este campo es requerido y no puede ser igual al origen"
    ),
    dateDeparture: Yup.date().min(
      getDayBefore(),
      "No puede elegir un dia anterior a la fecha de hoy"
    ),
  });

  function loadRecords(values) {
    return axios.get(apiUrls.flightEcommerce().getAvailableFlights, values)
    .then((data) => setFlights(data.response.data))

  }

  let marginSize = "2rem";

  return (
    <>
      <Formik
        validationSchema={shema}
        onSubmit={(values, actions) => {
          console.log('submmited') ;
          // same shape as initial values
          //loadRecords(values).finally(() => actions.setSubmitting(false));
        }}
        initialValues={{
          airportDepartureId: null,
          airportArrivalId: null,
          dateDeparture: "",
        }}
      >
        {({ handleChange, errors, values, ...formikProps }) => (
          <Card
            className="m-top-1"
            style={{ marginLeft: marginSize, marginRight: marginSize }}
          >
            {/* {console.log(formikProps)} */}
            <Card.Header>
              {JSON.stringify(values, null, 2)}
              <br />
              <hr />
              {JSON.stringify(errors, null, 2)}
            </Card.Header>
            <Card.Body>
              <Form noValidate>
                <Row className="mb-4">
                  <Form.Group as={Col} className="col-md-4">
                    <Form.Label>Origen</Form.Label>
                    <TypeAheadVuelo name="airportDepartureId" />

                    <Form.Control.Feedback type="invalid">
                      {errors.airportDepartureId}
                    </Form.Control.Feedback>
                  </Form.Group>

                  <Form.Group as={Col} className="col-md-4">
                    <Form.Label>Destino</Form.Label>
                    <TypeAheadVuelo name="airportArrivalId" />

                    <div className="text text-danger">
                      {errors.airportArrivalId}
                    </div>
                  </Form.Group>

                  <Form.Group as={Col}>
                    <Form.Label>Salida</Form.Label>
                    <Form.Control
                      type="date"
                      name="dateDeparture"
                      value={values.dateDeparture}
                      onChange={handleChange}
                      isInvalid={errors.dateDeparture}
                    />

                    <Form.Control.Feedback type="invalid">
                      {errors.dateDeparture}
                    </Form.Control.Feedback>
                  </Form.Group>

                  <Col>
                    <Button
                      style={{ marginTop: "30px" }}
                      onClick={() => formikProps.submitForm()}
                      className="btn btn-primary"
                    >
                      Buscar
                    </Button>
                  </Col>
                </Row>
              </Form>
            </Card.Body>
          </Card>
        )}
      </Formik>

      {flights != null &&
        flights.length > 0 &&
        flights.map((f) => (
          <div 
            style={{ marginLeft: marginSize, marginRight: marginSize }}
          >
            <Card>
              <Card.Body>
                <Row>


                <div className="col">
                  <b>{f.dateTimeStart} - {f.dateTimeEnd} </b>
                  <p>{f.countryOrigin} - {f.countryDestiny} </p>
                  <p>{f.airlineName}</p>
                </div>

                <div className="col text-end">
                  <h3><span class="badge bg-secondary">{f.price}</span></h3>

                  {f.totalTime} ({f.totalScales} escalas)
                  <ul style={{listStyle:'none'}}>
                   {f.scalesInfo != null && f.scalesInfo.length > 0 
                   && f.scalesInfo.map((scaleInfo) => (
                    <li>{scaleInfo.totalTime} en {scaleInfo.airportDestiny}</li> 
                   ))} 
                  </ul>
                </div>

                </Row>

              </Card.Body>
            </Card>
          </div>
        ))}
    </>
  );
}

/**
 *
 * @param {name} props
 * @returns
 */
function TypeAheadVuelo(props) {
  validateProps();
  const [isLoading, setIsLoading] = useState(false);
  const [options, setOptions] = useState([]);
  const { setFieldValue, errors } = useFormikContext();

  function handleSearch(query) {
    setIsLoading(true);

    axios
      .get(`${apiUrls.flightEcommerce().search}/${query}`)
      .then((respuesta) => {
        setOptions(respuesta.data);
        setIsLoading(false);
      });
  }

  function validateProps() {
    if (props.name == null || props.name.trim() == "")
      throw "props.name is required";
  }

  // Bypass client-side filtering by returning `true`. Results are already
  // filtered by the search endpoint, so no need to do it again.
  const filterBy = () => true;
  return (
    <>
      <AsyncTypeahead
        id={props.name}
        name={props.name}
        onChange={(records) => {
          if (records.length > 0) {
            setFieldValue(props.name, records[0].airportId);
          } else {
            setFieldValue(props.name, null);
          }
        }}
        options={options}
        labelKey={(record) => record.countryAirportName}
        filterBy={filterBy}
        isLoading={isLoading}
        onSearch={handleSearch}
        isInvalid={errors[props.name] != null}
        minLength={2}
        flip={true}
        renderMenuItemChildren={(record) => (
          <>
            <span>{record.countryAirportName}</span>
          </>
        )}
      />
    </>
  );
}

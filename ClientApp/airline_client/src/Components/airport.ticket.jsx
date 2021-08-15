import { useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";
import { LoadingIcon } from "../utils/LoadingIcon";
import { ModalDelete } from "./app.modals";
import {
  Accordion,
  Card,
  useAccordionButton,
  AccordionContext,
} from "react-bootstrap";
import { Formik, Form, FieldArray, Field } from "formik";
import * as Yup from "yup";
import { useParams } from "react-router";

import {
  FormGroupDate,
  FormGroupText,
  FormikGroupDropdown,
  FormGroupInput,
  FormGenericLoader,
} from "../utils/FormGroups";

import GenericCrudTable from "../utils/GenericCrudTable";

console.log(Yup);

export function TicketIndex() {
  return (
    <>
      <GenericCrudTable
        loadUrl={apiUrls.flight().url}
        // deleteUrl={(id) => `${apiUrls.flight().url}/${id}`}
        createUrl={`/flight/create`}
        detailUrl={(id) => `/flight/edit/${id}`}
        headerList={["Vuelo", "Origen", "Destino"]}
        actionHeader={<td style={{width:'200px'}}></td>}
        columns={(record) => (
          <>
            <td style={{ width: "800px" }}>Vuelo #{record.id}</td>
            <td> {record.from}</td>
            <td> {record.to}</td>
          </>
        )}
      />
    </>
  );
}

export function CreateTickets() {
  const [flightId, setFlightId] = useState(null);
  const [isXHR, setXHR] = useState(false);

  async function createRecord() {
    setXHR(true);
    try {
      var response = await axios.post(apiUrls.flight().url, { id: 0 });
      setFlightId(response.data);
    } catch (e) {
      console.error("ERROR AT CreateTickets", e);
      alert("Error al crear registro");
    } finally {
      setXHR(false);
    }
  }

  return (
    <>
      {flightId == null && (
        <button
          disabled={isXHR}
          className="btn btn-primary m-top-1"
          onClick={() => createRecord()}
        >
          Create un nuevo vuelo
        </button>
      )}
      {isXHR && <LoadingIcon />}

      {flightId && <TicketForm model={FlightFactory(flightId)} />}
    </>
  );
}

export function EditTickets() {
  const { flightid } = useParams();
  //load it
  return (
    <div className="container">
      <FormGenericLoader url={apiUrls.flight().byId(flightid)}>
        {(record) => (
          <>
            <TicketForm model={record} />
          </>
        )}
      </FormGenericLoader>
    </div>
  );
}

function FlightFactory(id = 0) {
  return {
    id,
    flightScales: [],
  };
}

function FlightScaleFactory(id = 0, flightId = 0) {
  return {
    id,
    code: "",
    airportDepartureId: 0,
    airportArrivalId: 0,
    departTime: new Date(),
    arrivalTime: new Date(),
    airplaneId: 0,
    airlineId: 0,
    minPrice: 0,
    terminalId: 0,
    gateId: 0,
    flightId,
    totalPaid: 0,
  };
}

export function TicketForm(props) {
  const [isXHRIndex, setXHRIndex] = useState(-1);
  //const [errors, setError] = useState([]);

  function pushFlightScale(flightId, push) {
    push(FlightScaleFactory(0, flightId));
  }

  async function addOrUpdateFlightScale(flight, index, formikProps) {
    if (!formikProps.isValid) return;

    try {
      if(flight.id > 0) {
        await axios.put(`${apiUrls.flightScale().url}/${flight.id}`, flight);
      } else {
        var response = await axios.post(apiUrls.flightScale().url, flight);
        formikProps.setFieldValue(`flightScales.${index}.id`, response.data)
      }
    } catch (e) {
      console.error("ERROR AT addOrUpdateFlightScale", e);
      alert("Error al crear registro");
    } finally {
    }
  }

  function ButtonRemoveRecord({ deleteUrl, index, removeMethod }) {
    function removeFieldArrayRecord() {
      console.log(`ButtonRemoveRecord::removeFieldArrayRecord:: ${deleteUrl}`);

      axios
        .delete(deleteUrl)
        .then((data) => {
          removeMethod(index);
        })
        .catch((e) => {
          alert("error al remover registro");
          console.error("error al savar:: ", e);
        });
    }

    return (
      <button
        onClick={() => removeFieldArrayRecord()}
        className="btn fa fa-trash pull-right"
      ></button>
    );
  }

  function AccordionTogggle({ children, eventKey, callback }) {
    const { activeEventKey } = useContext(AccordionContext);

    const decoratedOnClick = useAccordionButton(
      eventKey,
      () => callback && callback(eventKey)
    );

    const isCurrentEventKey = activeEventKey === eventKey;

    return (
      <span
        // style={{   backgroundColor: isCurrentEventKey ? '#dbdbff' : 'lavender' }}
        onClick={decoratedOnClick}
      >
        {children}
      </span>
    );
  }

  function getNextHour(hour) {
    var date = new Date();
    date.setHours(date.getHours() + hour);
    return date;
  }

  const validationSchema = Yup.object().shape({
    flightScales: Yup.array().of(
      Yup.object().shape({
        airlineId: Yup.mixed().notOneOf([0], "Este campo es requerido"), // these constraints take precedence
        airportDepartureId: Yup.mixed().notOneOf(
          [0],
          "Este campo es requerido"
        ), // these constraints take precedence
        airportArrivalId: Yup.mixed().notOneOf(
          [0, Yup.ref("airportDepartureId")],
          "Este campo es requerido y debe ser diferente al aeropuerto de origen"
        ),
        minPrice: Yup.number().min(1, "El precio minimo es de ${min} dolar."),
        departTime: Yup.date()
          .min(
            getNextHour(1),
            "La fecha del vuelo tiene que ser mayor o igual al dia de hoy. (con una 1 hora de antelacion)"
          )
          .max(
            Yup.ref("arrivalTime"),
            "La fecha del vuelo tiene que ser menor a la fecha del destino."
          ),
        arrivalTime: Yup.date().min(
          getNextHour(2),
          "La fecha del vuelo tiene que ser mayor que la fecha salida, por lo menos una HORA DE DIFERENCIA"
        ),
        airplaneId:Yup.mixed().notOneOf(
          [0, null, ""],
          "Este campo es requerido"
        ),
      })
    ),
  });

  return (
    <div className="col-md-9">
      <Formik
        validationSchema={validationSchema}
        enableReinitialize={true}
        initialValues={props.model}
        onSubmit={(values, actions) => {
          
          setTimeout(() => {
            console.log(JSON.stringify(values, null, 2));
            actions.setSubmitting(false);
          }, 1000);
        }}
      >
        {(formikProps) => (
          <Form>
            {JSON.stringify(formikProps.values, null, 2)}

            <FieldArray name="flightScales">
              {({ insert, remove, push }) => (
                <>
                  <Accordion>
                    {formikProps.values.flightScales?.length > 0 &&
                      formikProps.values.flightScales.map((flight, index) => (
                        <Card key={index}>
                          <Card.Header>
                            <AccordionTogggle eventKey={index}>
                              <label
                                className="fw-normal fs-2"
                                style={{ cursor: "pointer" }}
                              >
                                Escala # {index + 1}
                              </label>
                            </AccordionTogggle>
                            <ButtonRemoveRecord
                              deleteUrl={apiUrls
                                .flightScale()
                                .delete(flight.id)}
                              index={index}
                              removeMethod={remove}
                            />
                          </Card.Header>
                          <Accordion.Collapse eventKey={index}>
                            <Card.Body>
                              <div className="row">
                                <div className="col-md-12">
                                  <FormikGroupDropdown
                                    name={`flightScales.${index}.airlineId`}
                                    label="Aerolinea"
                                    url={apiUrls.airline.url}
                                    logConsole={false}
                                  />
                                </div>
                                <div className="col-md-6">
                                  <FormikGroupDropdown
                                    name={`flightScales.${index}.airportDepartureId`}
                                    label="Origen"
                                    url={apiUrls.airline_airport.getAirportByAirlineId(
                                      flight.airlineId
                                    )}
                                    logConsole={false}
                                  />
                                </div>

                                <div className="col-md-6">
                                  <FormGroupInput
                                    name={`flightScales.${index}.departTime`}
                                    type="datetime-local"
                                    label="Fecha de salida"
                                  />
                                </div>
                              </div>
                              <div className="row">
                                <div className="col-md-6">
                                  <FormikGroupDropdown
                                    name={`flightScales.${index}.airportArrivalId`}
                                    label="Destino"
                                    url={apiUrls.airline_airport.getAirportByAirlineId(
                                      flight.airlineId
                                    )}
                                    logConsole={false}
                                  />
                                </div>

                                <div className="col-md-6">
                                  <FormGroupInput
                                    name={`flightScales.${index}.arrivalTime`}
                                    type="datetime-local"
                                    label="Fecha de llegada"
                                  />
                                </div>
                              </div>
                              <FormikGroupDropdown
                                name={`flightScales.${index}.airplaneId`}
                                label="Avion"
                                url={`${apiUrls.airplane.byairlineid}/${flight.airlineId}`}
                                fieldName="brand"
                                logConsole={false}
                              />

                              <FormGroupInput
                                name={`flightScales.${index}.minPrice`}
                                type="number"
                                label="Precio Minimo"
                              />

                               <FormikGroupDropdown
                                name={`flightScales.${index}.gateId`}
                                label="Puerta"
                                url={apiUrls.gate().getGatesByAirportAirline(flight.airportDepartureId, flight.airlineId)}
                                logConsole={false}
                              />

                              <FormikGroupDropdown
                                name={`flightScales.${index}.terminalId`}
                                label="Terminal"
                                url={apiUrls.terminal().getTerminalByAirlineId(flight.airlineId)}
                                logConsole={false}
                              /> 

                              {isXHRIndex == index ? (
                                <>
                                  <button className="btn btn-outline-primary m-top-1 m-r-1-sm">
                                    <LoadingIcon />
                                  </button>
                                </>
                              ) : (
                                <button
                                  className="btn btn-outline-primary m-top-1 m-r-1-sm"
                                  disabled={isXHRIndex == index}
                                  onClick={() =>
                                    addOrUpdateFlightScale(
                                      flight,
                                      index,
                                      formikProps
                                    )
                                  }
                                >
                                  Guardar
                                </button>
                              )}
                            </Card.Body>
                          </Accordion.Collapse>
                        </Card>
                      ))}
                    <div className="d-grid gap-2 m-top-1">
                      <button
                        className="btn btn-primary"
                        type="button"
                        onClick={() =>
                          pushFlightScale(formikProps.values.id, push)
                        }
                      >
                        Agregar Escala
                      </button>
                    </div>
                  </Accordion>
                </>
              )}
            </FieldArray>
          </Form>
        )}
      </Formik>
    </div>
  );
}

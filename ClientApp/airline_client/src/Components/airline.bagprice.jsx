import { useEffect } from "react";
import { Formik, Form, FieldArray, Field } from "formik";
import { useParams, Link, Redirect } from "react-router-dom";
import FormGroupInput from "../utils/FormGroupInput";
import { useState } from "react";
import axios from "axios";
import apiUrls from "../utils/endpoints";
import { LoadingIcon } from "../utils/LoadingIcon";
import { ModalDelete } from "./app.modals";

export function AirlineBagPriceFormView() {
  const { airlineid } = useParams();
  const [bagPriceMaster, setBagPriceMaster] = useState(undefined);
  const [isXHR, setXHR] = useState(true);

  console.log("AirlineBagPriceCreate::airlineid::", airlineid);

  useEffect(() => {
    axios
      .get(apiUrls.bagPrice().getByAirlineId(airlineid))
      .then((data) => {
        console.log("data xhr ", data.data);
        setBagPriceMaster(data.data);
      })
      .catch(() => console.error("error"))
      .finally(() => setXHR(false));
  }, []);

  function createNewBagPrice() {
    setXHR(true);
    var model = createDefaultMasterModel(airlineid);
    axios
      .post(apiUrls.bagPrice().url, model)
      .then((data) => {
        model.id = data.data;
        setBagPriceMaster(model);
      })
      .catch(() => console.error("error"))
      .finally(() => setXHR(false));
  }

  if (isXHR)
    return (
      <>
        <LoadingIcon />
      </>
    );

  if (!bagPriceMaster)
    return (
      <div className="card">
        <div className="card-body">
          <button
            className="btn btn btn-outline-primary"
            onClick={() => createNewBagPrice()}
          >
            Crear configuracion de precio por libra
          </button>
        </div>
      </div>
    );

  return (
    <>
      <AirlineBagPriceForm
        onDelete={() => setBagPriceMaster(null)}
        model={bagPriceMaster}
        onSubmit={async (values) => {
          // await new Promise((r) => setTimeout(r, 500));
          // console.log("on submitting");
          // alert(JSON.stringify(values, null, 2));
        }}
      />
    </>
  );
}

function createDefaultMasterModel(airlineIdParam) {
  const BagPriceMasterModel = {
    id: 0,
    airlineid: airlineIdParam,
    //percentOfIncreaseAfterMaxPound: 0,
    //details: [createDefaultDetail()],
  };
  return BagPriceMasterModel;
}

function createDefaultDetail(masterId = 0) {
  return {
    id: 0,
    price: 1,
    poundStart: 1,
    poundEnd: 2,
    bagPriceMasterId: masterId,
  };
}

export function AirlineBagPriceForm(props) {
  console.log(props);
  const [isModalDelete, setModalDelete] = useState(false);
  const model = props.model;

  function saveMaster(formVal) {
    var updateProps = {
      percentOfIncreaseAfterMaxPound: formVal.percentOfIncreaseAfterMaxPound,
    };
    axios.put(apiUrls.bagPrice().update(model.id), updateProps).catch((e) => {
      alert("error al savar");
      console.error("error al savar:: ", e);
    });
  }

  //REMOVE REGISTRO COMPLETO
  function handleDelete() {
    axios
      .delete(apiUrls.bagPrice().delete(model.id))
      .then((data) => {
        props.onDelete();
      })
      .finally(() => setModalDelete(false));
  }

  function addNewRango(pushMethod) {
    ///llamar al serverside
    var rango = createDefaultDetail(model.id);
    axios
      .post(apiUrls.bagPriceDetail().url, rango)
      .then((data) => {
        rango.id = data.data;
        pushMethod(rango);
      })
      .catch(() => alert("error al agregar rango"));
  }

  function saveRango(detail) {
    axios.put(apiUrls.bagPriceDetail().update(detail.id), detail).catch((e) => {
      alert("error al savar");
      console.error("error al savar:: ", e);
    });
  }
  function removeRango(detail, index, removeMethod) {
    let deleteUrl = apiUrls.bagPriceDetail().delete(detail.id);
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
    <>
      <ModalDelete
        showModal={isModalDelete}
        handleOnClose={() => setModalDelete(false)}
        handleOk={() => handleDelete()}
      />

      <div className="container card ">
        <h2>Precio por libra</h2>
        <div>
          <button
            className="btn btn btn-outline-danger pull-right"
            onClick={() => setModalDelete(true)}
          >
            Eliminar configuracion
          </button>
        </div>

        <Formik
          initialValues={model}
          onSubmit={props.onSubmit}
          className="card-body"
        >
          {(formikProps) => (
            <Form className="card-body">
              <div>
                <div className="card col-md-4">
                  <div className="card-header">Parametros bases</div>
                  <div className="card-body">
                    <FormGroupInput
                      name="percentOfIncreaseAfterMaxPound"
                      label="Porciento de incremento de precio por cada libra, luego de sobrepasar libra maxima"
                      type="number"
                    />
                    <ButtonSave
                      onClick={() => saveMaster(formikProps.values)}
                    ></ButtonSave>
                  </div>
                </div>
                <FieldArray name="details">
                  {({ insert, remove, push }) => (
                    <>
                      {formikProps.values.details?.length > 0 &&
                        formikProps.values.details.map((detail, index) => (
                          <div key={index}>
                            <div className="card col-md-4">
                              <div className="card-header">
                                #{index + 1} Precio entre rango
                                <button
                                  onClick={() =>
                                    removeRango(detail, index, remove)
                                  }
                                  className="btn fa fa-trash pull-right"
                                ></button>
                              </div>

                              <div className="card-body">
                                <FormGroupInput
                                  name={`details.${index}.price`}
                                  label="Precio"
                                  type="money"
                                />

                                <FormGroupInput
                                  name={`details.${index}.poundStart`}
                                  label="Libra inical"
                                  type="number"
                                />
                                <FormGroupInput
                                  name={`details.${index}.poundEnd`}
                                  label="Libra final"
                                  type="number"
                                />
                              </div>
                              <div className="card-footer">
                                <ButtonSave
                                  onClick={() => saveRango(detail, index)}
                                  label="Guardar"
                                ></ButtonSave>
                              </div>
                            </div>
                            <hr />
                          </div>
                        ))}
                      <ButtonSave
                        onClick={() => addNewRango(push)}
                        label="Agregar nuevo rango"
                        disabled={formikProps.isSubmitting}
                      />
                    </>
                  )}
                </FieldArray>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </>
  );
}

function ButtonSave(props) {
  return (
    <button
      disabled={props.disabled}
      onClick={() => props.onClick()}
      className={
        "m-top-1 " +
        (props.className ? props.className : "btn btn-outline-dark")
      }
    >
      {props.label ? props.label : "Guardar"}
    </button>
  );
}

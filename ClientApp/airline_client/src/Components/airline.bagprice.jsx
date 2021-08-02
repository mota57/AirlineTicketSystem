// import { useEffect } from "react";
import { Formik, Form, FieldArray } from "formik";
import { useParams, Link } from "react-router-dom";
import FormGroupInput from "../utils/FormGroupInput";
import { useState } from "react";
// import axios from "axios";
// import apiUrls from "../utils/endpoints";

export function AirlineBagPrice() {
  const { airlineid } = useParams();
  const [bagPriceMaster, setBagPriceMaster] = useState(undefined);
  const [isXHR, setXHR] = useState(true);
  console.log("AirlineBagPriceCreate::airlineid::", airlineid);

  // useEffect(() => {

  //   axios.get(apiUrls.bagPrice.getByAirlineId(airlineid))
  //   .then((data) => {
  //     setBagPriceMaster(data.data);
  //   })
  //   .catch(() => console.error('error'))
  //   .finally(() => setXHR(false))
  // },[])

  // if(isXHR) {
  //     return (<div>
  //     <i class="fa fa-cog fa-spin fa-3x fa-fw"></i>
  //     <span class="sr-only">Loading...</span>
  //   </div>)
  // }

  return (
    <>
      <AirlineBagPriceForm
        model={bagPriceMaster ? bagPriceMaster : createDefaultMasterModel()}
        onSubmit={async (values) => {
          await new Promise((r) => setTimeout(r, 500));
          console.log('on submitting');
          // alert(JSON.stringify(values, null, 2));
        }}
      />
    </>
  );
}

function createDefaultMasterModel() {
  const BagPriceMasterModel = {
    percentOfIncreaseAfterMaxPound: 0,
    details: [createDefaultDetail()],
  };
  return BagPriceMasterModel;
}

function createDefaultDetail(masterId = 0) {
  return {
    Id: 0,
    price: 35,
    poundStart: 0,
    poundEnd: 50,
    bagPriceMasterId: masterId,
  };
}

export function AirlineBagPriceForm(props) {
  let cancelUrl = `/bagprice/${props.airlineid}`;
  const model = props.model;


  function addNewRango(pushMethod) {
    ///llamar al serverside
    pushMethod(createDefaultDetail())
  }

  function removeRango(detail, index, removeMethod) {
    removeMethod(index);
  }

  function saveRango(detail) {}

  return (
    <>
      <div className="container card ">
        <h2>Precio por libra</h2>
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
                      field="percentOfIncreaseAfterMaxPound"
                      label="Porciento de incremento de precio por cada libra, luego de sobrepasar libra maxima"
                      type="number"
                    />
                  </div>
                </div>
                <FieldArray name="details">
                  {({ insert, remove, push }) => (
                    <>
                      {formikProps.values.details.length > 0 &&
                        formikProps.values.details.map((detail, index) => (
                          <div key={index}>
                            <div className="card col-md-4">
                              <div className="card-header">
                                #{index + 1} Precio entre rango
                                <ButtonSubmit
                                  onClick={() => removeRango(detail, index, remove)}
                                  className="btn fa fa-trash pull-right"
                                ></ButtonSubmit>
                              </div>

                              <div className="card-body">
                                <FormGroupInput
                                  field={`details.${index}.poundStart`}
                                  label="Libra inical"
                                  type="number"
                                />
                                <FormGroupInput
                                  field={`details.${index}.poundEnd`}
                                  label="Libra final"
                                  type="number"
                                />
                                <FormGroupInput
                                  field={`details.${index}.price`}
                                  label="Precio"
                                  type="money"
                                />
                              </div>
                              <div className="card-footer">
                                <ButtonSubmit
                                  onClick={() => saveRango(detail, index)}
                                  label="Guardar"
                                ></ButtonSubmit>
                              </div>
                            </div>
                            <hr />
                          </div>
                        ))}
                      <ButtonSubmit  
                        onClick={() => addNewRango(push)}
                        label="Agregar nuevo rango"  
                        disabled={formikProps.isSubmitting}
                        />
                    </>
                  )}
                </FieldArray>
                <div className="form-group row" style={{ marginTop: "10rem" }}>
                  <div className="col-sm-10">
                    
                    <Link to={cancelUrl} className="btn btn-danger">
                      Cancelar
                    </Link>
                  </div>
                </div>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </>
  );
}

function ButtonSubmit(props) {
  return (
    <button
      type="submit"
      disabled={props.disabled}
      onClick={() => props.onClick()}
      className={props.className ? props.className : "btn btn-outline-dark"}
    >
      {props.label}
    </button>
  );
}

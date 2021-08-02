import { Field, ErrorMessage } from "formik";

export default function FormGroupInput({
  field,
  label,
  placeholder = "",
  type = "text",
}) {
  return (
    <div className="form-group">
      {label ? <label htmlFor={field}>{label}</label> : null}
      <Field
        name={field}
        className="form-control"
        placeholder={placeholder}
        type={type}
      />
      <ErrorMessage name={field}>
        {(mensaje) => <div className="text-danger">{mensaje}</div>}
      </ErrorMessage>
    </div>
  );
}

/**
 *  
 * <div className="form-group row">
                    <label
                      for="inputPassword3"
                      className="col-sm-2 col-form-label"
                    >
                      Precio
                    </label>
                    <div className="col-sm-4">
                      <input
                        type="number"
                        className="form-control"
                        id="inputPassword3"
                        placeholder=""
                      />
                    </div>
 */

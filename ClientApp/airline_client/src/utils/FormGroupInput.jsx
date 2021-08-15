import { Field, ErrorMessage } from "formik";
/**
 * 
 * @param {label, name} param0 
 * @returns 
 */
export default function FormGroupInput({
  name,
  label,
  placeholder = "",
  type = "text",
}) {
  return (
    <div className="form-group">
      {label ? <label htmlFor={name}>{label}</label> : null}
      <Field
        name={name}
        className="form-control"
        placeholder={placeholder}
        type={type}
      />
      <ErrorMessage name={name}>
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

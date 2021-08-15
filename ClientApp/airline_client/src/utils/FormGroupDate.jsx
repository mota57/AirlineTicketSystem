import { useFormikContext } from "formik";

/**
 * 
 * @param {field:string, label:string} props 
 * @returns 
 */
export default function FormGroupDate(props) {
    const { values, setFieldValue, validateForm, touched, errors } = useFormikContext();
    return (
        <div className="form-group">
            <label htmlFor={props.name}>{props.label}</label>
            <input type="datetime-local" className="form-control"
                id={props.name}
                name={props.name}
                defaultValue={values[props.name]?.toLocaleDateString('en-CA')}
                onChange={e => {
                    console.log(e.currentTarget.value);
                    const fecha = new Date(e.currentTarget.value);
                    setFieldValue(props.name, fecha);
                    validateForm();
                }}
            />
            {touched[props.name] && errors[props.name] ? 
            <p className="text-danger">{errors[props.name].toString()}</p> : null }
        </div>
    )
}


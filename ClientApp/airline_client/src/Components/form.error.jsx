import {uuidv4} from '../utils/methods';

function BuildErros(errors) {

  return (
    <>
      {Object.keys(errors).map((errorKey) => {
     
        return (
          <>
          <label >{errorKey}</label>
          <ul>
            {errors[errorKey].map((errorText) => {
              return (
                <li key={uuidv4()}>
                  <label className="text-danger">{errorText}</label>
                </li>
              );
            })}
          </ul>
          </>
        );
      })}
    </>
  );
}



export default function FormError(props) {
  console.log('formerror::',props);
  if(props.formerrorobj == null) {
    return <></>
  } else if (props.formerrorobj.rawerror) {
    return <> {BuildErros(props.formerrorobj.errors)}</>;
  } else if (props.formerrorobj && props.formerrorobj.status == 400) {
    let errors = props.formerrorobj.response.data;
    return <> {BuildErros(errors)}</>;
  } else {
    return (
      <>
        <div className="alert alert-danger">An error ocurred</div>
      </>
    );
  }
}

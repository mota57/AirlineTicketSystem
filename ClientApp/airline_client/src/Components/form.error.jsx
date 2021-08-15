import {uuidv4} from '../utils/methods';


export default function FormError(props) {
  console.log(props.formerrorobj);
  if(props.formerrorobj === null) {
    return <></>
  } else if (props.formerrorobj.rawerror) {
    return <> {BuildErros(props.formerrorobj.errors)}</>;
  // } else if (props.formerrorobj?.request?.status === 404) {
  //   return <> <Redirect to={"/error_page/404"}/> </>;
  } else if (props.formerrorobj?.request?.status === 400) {
    let errors = props.formerrorobj.response.data;
    return <> {BuildErros(errors)}</>;
  } else {
    return (
      <>
        <div className="alert alert-danger">Lo sentimos un error a ocurrido.</div>
      </>
    );
  }
}




function BuildErros(errors) {

  return (
    <>
      {Object.keys(errors).map((errorKey) => {
     
        return (
          <>
          <label >{errorKey}</label>
          <ul style={{listStyle:'none'}}>
            {errors[errorKey].map((errorText) => {
              let uid = uuidv4();
              return (
                <li key={uid} data-id={uid}>
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

import axios from "axios";

export function changeHandlerBuilder(setRecord, record) {
  const changeHandler = (e, booleanValue) => {
    var val = e.target.value;
    console.log('changeHandlerBuilder',e);
    if (e.target.nodeName == "SELECT") {
      val = Number(val);
    }
    setRecord({
      ...record,
      [e.target.name]: booleanValue != null ? !booleanValue : val,
    });
  };
  return changeHandler;
}

export function uuidv4() {
  const a = crypto.getRandomValues(new Uint16Array(8));
  let i = 0;
  return "00-0-4-1-000".replace(/[^-]/g, (s) =>
    ((a[i++] + s * 0x10000) >> s).toString(16).padStart(4, "0")
  );
}

export class UrlSearchExtension {
  constructor(search) {
    this.query = new URLSearchParams(search);
  }

  getNumberParam(key) {
    let val = Number(this.query.get(key));
    if (isNaN(val)) {
      return -1;
    }
    return val;
  }
}


export class FormRawErrorHelper {

    constructor() {

        this.formErrorObj = {
            rawerror: true,
            errors: {  
            }
        }
    }
    
    pushError(key, message) {
        if(this.formErrorObj.errors.hasOwnProperty(key)) {
            this.formErrorObj.errors[key].push(message);
        } else {
            this.formErrorObj.errors[key] = [message];
        }
    }

    hasErrors()  { return Object.keys(this.formErrorObj.errors).length > 0 }

}
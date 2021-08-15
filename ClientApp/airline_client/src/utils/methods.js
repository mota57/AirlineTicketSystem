// import axios from "axios";

export function changeHandlerBuilder(setRecord, record) {
  const changeHandler = (e, booleanValue) => {
    var val = e.target.value;
    console.log('changeHandlerBuilder',e);
    if (e.target.nodeName === "SELECT") {
      if(e.target.multiple) {
        val = val != "" ?  Array.from(e.target.selectedOptions).map(p => Number(p.value)) : []
      } else {
        val = Number(val);
      }
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

        this.rawObj = {
            rawerror: true,
            errors: {  
            }
        }
    }
    
    pushError(key, message) {
        if(this.rawObj.errors.hasOwnProperty(key)) {
            this.rawObj.errors[key].push(message);
        } else {
            this.rawObj.errors[key] = [message];
        }
    }

    hasErrors()  { return Object.keys(this.rawObj.errors).length > 0 }

}



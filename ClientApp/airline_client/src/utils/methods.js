
export function changeHandlerBuilder(setRecord, record) {
    
    const changeHandler = (e, booleanValue) => {
        var val =  e.target.value;
        if(e.target.name == "countryid") {
            val = Number(val)
        }
        setRecord({...record, [e.target.name]: booleanValue != null ? !booleanValue :val})
    }
    return changeHandler;
}



export function uuidv4() {
    const a = crypto.getRandomValues(new Uint16Array(8));
    let i = 0;
    return '00-0-4-1-000'.replace(/[^-]/g, 
            s => (a[i++] + s * 0x10000 >> s).toString(16).padStart(4, '0')
    );
  }
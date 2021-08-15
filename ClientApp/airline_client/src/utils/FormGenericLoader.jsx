import { useState, useEffect } from "react";
import axios from "axios";
import { LoadingIcon } from "./LoadingIcon";

export default function FormGenericLoader({url, children}) {
    const [records, setRecords] = useState(false);
    const [isXHR, setXHR] = useState(true);
    
    useEffect(() => {
      loadRecords();
    }, []);
  
    function loadRecords() {
      if (url) {
        axios
          .get(url)
          .catch((e) => console.error("XHR error ", e))
          .then((data) => setRecords(data != null && data.data ? data.data : []))
          .finally(() => setXHR(false));
      }else {
        setXHR(false);
      }
    }
  
    if (isXHR) {
      return (
        <>
          <LoadingIcon />
        </>
      );
    }
  
    return (
      <>
      {children(records ? records: [] )}
      </>
    )
  }
  
  
  
  
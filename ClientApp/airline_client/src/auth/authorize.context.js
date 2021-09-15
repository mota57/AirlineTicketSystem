import { createContext } from "react";

const AuthorizeContext = createContext({
    claims:[],//name,val
    update: () => {}
});
export default AuthorizeContext;
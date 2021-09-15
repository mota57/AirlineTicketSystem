import {useContext} from 'react';
import AuthorizeContext from './authorize.context';

function AuthHelper()   {
    const ctx = useContext(AuthorizeContext);
    function isAdmin()  {
        return (ctx.claims.findIndex(c => c.name == 'role' && c.value == 'admin') > -1)
    }

    return  {
        isAdmin,
        claims:ctx.claims
    };
}

export default AuthHelper;
import { useState } from 'react';
import { useEffect } from 'react';
import { useContext } from 'react';
import AuthorizeContext from './authorize.context';

export default function AuthorizeContent(props) {
    const [authorize, setAuthorize] = useState(true);
    let { claims } = useContext(AuthorizeContext);
    useEffect(() => {
        if(props.role) {
           let didFoundClaimRole = claims.findIndex(p => p.name == 'role' && p.value == props.role) > -1
           setAuthorize(didFoundClaimRole);
        } else {
            setAuthorize(claims != null && claims.length > 0)
        }

    },[claims, props.role])
    
    return(
        <>
        { authorize ? props.content : props.notAuthorizeContent}
        </>
    )
}
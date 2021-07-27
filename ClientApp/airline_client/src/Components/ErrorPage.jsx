import { useParams} from 'react-router-dom';

export default function ErrorPage() {
    var {status} = useParams();
    
    if(status == 404) {
        return (<> 
            <h1>Recurso no encontrado.</h1>
        </>)
    } else {
        return (<> 
            <h1>Lo sentimos un error a ocurrido.</h1>
        </>)
    }
}
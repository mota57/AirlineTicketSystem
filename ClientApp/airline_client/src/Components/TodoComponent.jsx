export default function TodoComponent() {
  return (
    <>
      <div>
        <h3>TASK LIST</h3>
        <ul className="group-list">
        <li className="list-group-item">
            <span className="badge bg-dark">WORKING</span> CREAR /ACTUALIZAR TICKET
                * ONLY SHOW AIRLINES THAT ARE ACTIVE TO USE.
                * ALLOW an airline to be part of the same gate in the same airport
                <br/>
                * * An airline can have a limit of flights per day, base on the total airplanes, so if a flight has 4 aiplanes, that means can only have for 4 flights for that date. <br/>
                * * FlightScale avoid set same datetime to depart for 2 or more scales<br/>
                * * FlightScale avoid set same duplicate departure or destination for 2 or more scales<br/>
                <br/>
                <hr/>
                <p>If an airport is inactive it should not in tickets.</p>


          </li>
        <li className="list-group-item">
            <span className="badge bg-success">PENDING</span> AL ELIMINAR AEROLINEA ENTONCES PREVENIR ELIMINAR, PORQUE YA EXISTEN REGISTROS, LO QUE SE PUEDE HACER ES DESACTIVARLA, PARA NO AGREGAR MAS VUELOS.
          </li>
          <li className="list-group-item">
           <span className="badge bg-danger m-r-1-sm">BUG </span> 
           <ul>

            <li>AIRPORT/TERMINAL LA COLUMNA DE ACTIVO TIENE UN LABEL INCORRECTO, DEBERIA DE DECIR "ESTATUS".</li>
            <li>AIRPORT/TERMINAL LA COLUMNA DE LOS BOTOS DE ACCIONES ESTA INCOMPLETA.</li>
            <li>AIRPORT/TERMINAL PONER VALIDACION DE QUE EL AEROPUERTO DEBE SER OPCIONAL.</li>
           </ul>
          </li>
        <li className="list-group-item">
           <span className="badge bg-danger m-r-1-sm">BUG </span> 
            AIRPORT/AIRLINE DEBERIA SOLO MOSTRAR EL CAMPO ACTIVO O INACTIVO y poder ser editable.<br/>
          </li>
        <li className="list-group-item">
            <span className="badge bg-success">PENDING</span> CREAR BOTON PARA ELIMINAR AEROPUERTO EN EL DASHBOARD.
          </li>
          
          <li className="list-group-item">
            <span className="badge bg-info">NEW</span> escalas
          </li>
          <li className="list-group-item">
            <span className="badge bg-info">NEW</span> pasajeros
          </li>
          <li className="list-group-item">
            <span className="badge bg-info">NEW</span> ticket comprar pasajero
          </li>
          <li className="list-group-item">
            <span className="badge bg-info">NEW</span> cancelar el vuelo.
          </li>

          <li className="list-group-item">
            <span className="badge bg-danger">BUG </span>
            <label className="text-decoration-line-through">
              Boton crear aeropuerto no esta dirigiendose al pantalla de
              creacion.
            </label>
          </li>

          
          <li className="list-group-item">
            <span className="badge bg-success">PENDING</span>
            <span className="badge bg-info">[LOW PRIORITY]</span><br/> 
             ELIMINAR TERMINAL  
          </li>

          <li className="list-group-item">
            <span className="badge bg-success">PENDING</span>
            <span className="badge bg-info">[LOW PRIORITY]</span><br/> 
            PUERTAS DE EMBARQUE 
          </li>

          <li className="list-group-item">
            <span className="badge bg-success">PENDING</span> AGREGAR PAGINACION Y BUSQUEDA
          </li>
        </ul>
      </div>
    </>
  );
}

import { useState, useContext } from "react";
import { Link } from "react-router-dom";
import { uuidv4 } from "../utils/methods";
import { Offcanvas } from "react-bootstrap";
import RouteConfig from "../route.config";
import AuthHelper from "../auth/authorize.helper";

export default function SideBarComponent(props) {

  const [show, setShow] = useState(false);
  const authHelper = AuthHelper();

  let sideBarObj = RouteConfig.filter(m => m.sidebar).map(route => ({
    ...route,
    isActive:false
   }));
  
  let indexStorage = localStorage.getItem('sidebarid', 1);
  try {

    sideBarObj[indexStorage != null && indexStorage >= 0 ? indexStorage : 0].isActive = true;
  } catch {
    sideBarObj[0].isActive = true;
  }

  const [menuSidebar, setMenuSidebar] = useState(sideBarObj);

  function handleSetActive(index) {
    let menuUpdated = menuSidebar.map((el, i) => {
      if (i === index) {
        localStorage.setItem('sidebarid', i);
        return Object.assign({}, el, { isActive: true });
      } else if (el.isActive === true) {
        return Object.assign({}, el, { isActive: false });
      } else {
        return el;
      }
    });
    setMenuSidebar(menuUpdated);
  }

  return (
    <>
    <i  role="button" tabIndex="0" className="fa fa-bars fa-3x m-1" onClick={() => setShow(true)}></i>

<Offcanvas show={show} onHide={() => setShow(false)} className="bg-dark">
        <Offcanvas.Header closeButton>
          <Offcanvas.Title style={{color:"white"}}>Airport menu</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
    <div
      className="col-md-3 d-flex flex-column flex-shrink-0 p-3 text-white bg-dark"
      style={{ width: "280px", height: "1000px" }}
    >
       
  
    
      <ul className="nav nav-pills flex-column mb-auto">
        {menuSidebar.filter(v => v.isAdmin == false 
          || (v.isAdmin && authHelper.isAdmin())).map((m, i) => (
          <li className="nav-item" onClick={() => handleSetActive(i)} key={uuidv4()}>
            <Link
              to={m.path}
              className={`nav-link  ${m.isActive === true ? "active" : ""}`}
              aria-current="page"
            >
              <svg className="bi me-2" width="16" height="16">
                <use xlinkHref="#home"></use>
              </svg>
              {m.label}
            </Link>
          </li>
        ))}
      </ul>
      <hr />
    </div>
    </Offcanvas.Body>
      </Offcanvas>
    </>
  );
}


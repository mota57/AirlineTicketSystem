import { useState } from "react";
import { Link } from "react-router-dom";
import { uuidv4 } from "../utils/methods";

export default function SideBarComponent(props) {
  const sideBarObj  = [
    {
      label: "Airports",
      isActive: false,
      url:'/airport'
    },
    {
      label: "Airline",
      isActive: false,
      url:'/airline'
    },
  ]
  let indexStorage = localStorage.getItem('sidebarid', 1);
  sideBarObj[indexStorage != null && indexStorage >= 0 ? indexStorage : 0].isActive = true;

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
    <div
      className="col-md-3 d-flex flex-column flex-shrink-0 p-3 text-white bg-dark"
      style={{ width: "280px", height: "1000px" }}
    >
      <a
        href="/"
        className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <svg className="bi me-2" width="40" height="32">
          <use xlinkHref="#bootstrap"></use>
        </svg>
        <span className="fs-4">Sidebar</span>
      </a>
      <hr />
      <ul className="nav nav-pills flex-column mb-auto">
        {menuSidebar.map((m, i) => (
          <li className="nav-item" onClick={() => handleSetActive(i)} key={uuidv4()}>
            <Link
              to={m.url}
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
  );
}


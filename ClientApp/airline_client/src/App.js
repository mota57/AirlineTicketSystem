import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "font-awesome/css/font-awesome.css";
import "react-bootstrap-typeahead/css/Typeahead.css";

import RouteConfig from "./route.config";

import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import SideBarComponent from "./Components/sidebar";
import AuthorizeContext from "./auth/authorize.context";
import NotAuthorizeRouteContent from "./auth/NotAuthorizeRoute";
import AuthHelper from "./auth/authorize.helper";
import { useState } from "react";

function App() {
  const authStateObj = {
    claims: [{ name: "role", value: "admin" }],
    update: () => {},
  };
  const [authState, setAuthState] = useState(authStateObj);

  return (
    <Router>
      <AuthorizeContext.Provider value={authState}>
        <SideBarComponent />
        <div className=" row" style={{ backgroundColor: "#ebedef" }}>
          <div className="col-md-12" style={{ minHeight: "1000px" }}>
            <SwitchReactRoutes routes={RouteConfig} />
          </div>
        </div>
      </AuthorizeContext.Provider>
    </Router>
  );
}
function SwitchReactRoutes({ routes }) {
  const helper = AuthHelper();
  return (
    <>
      <Switch>
        {routes.map((r) => (
          <Route key={r.path} path={r.path} exact={r.exact}>
            {r.isAdmin && helper.isAdmin() == false ? (
              <>
                <NotAuthorizeRouteContent />
              </>
            ) : (
              <r.componente></r.componente>
            )}
          </Route>
        ))}
      </Switch>
    </>
  );
}
export default App;

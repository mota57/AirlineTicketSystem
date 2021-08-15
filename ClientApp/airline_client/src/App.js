import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "font-awesome/css/font-awesome.css"

import RouteConfig from "./route.config";

import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import SideBarComponent from "./Components/sidebar";


function App() {
  return (
    <Router>
     
        <SideBarComponent />
      <div className=" row" style={{backgroundColor:"#ebedef"}}>
          <div className="col-md-12" style={{minHeight:"1000px"}}>
            <Switch>
            
            
              {RouteConfig.map((r) => (
                <Route key={r.path} path={r.path} exact={r.exact}>
                  <r.componente></r.componente>
                </Route>
              ))}
            </Switch>
          </div>
      </div>
    </Router>
  );
}

export default App;

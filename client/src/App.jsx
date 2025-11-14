import React from "react"
import 'bootstrap/dist/css/bootstrap.min.css';
import "./css/App.css";
import
{
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate
} from "react-router-dom";
import OrderForm from "./components/OrderForm"
import Summary from "./components/Summary";

function App()
{
  return (
    <Router>
      <Routes>
        <Route index element={<Navigate to={"/order"} />} />
        <Route exact path="/order" element={<OrderForm />} />
        <Route exact path="/summary/:id" element={<Summary />} />
      </Routes>
    </Router>
  )
}

export default App

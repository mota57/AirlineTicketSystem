export default function NotAuthorizeRouteContent() {
  return (
    <div className="container-fluid py-5">
    <i className="fa fa-exclamation-triangle fa-5x" aria-hidden="true"></i>
        <h1 className="display-5 fw-bold">You are not authorize</h1>
        <p className="col-md-8 fs-4">You do not have the authorization to see the content</p>
        <button className="btn btn-primary btn-lg" type="button">Go Home</button>
      </div>
  )
}
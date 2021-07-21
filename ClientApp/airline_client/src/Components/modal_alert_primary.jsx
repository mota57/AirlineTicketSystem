import { Modal, Button } from "react-bootstrap";

function ModalAlertPrimary(props) {
  const [show, setShow] = useState(true);

  const handleClose = () => setShow(false);
  const handleOnOk = () => {
    if (props.handleOk) {
      props.handleOk();
    } else {
      setShow(false);
    }
  };

  return (
    <>
      {/* <Button variant="primary" onClick={handleOk}>
          Launch demo modal
        </Button> */}

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{props.heading}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{props.text}</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            {props.cancelText}
          </Button>
          <Button variant="primary" onClick={handleOnOk}>
            {props.okText}
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

import { Modal, Button } from "react-bootstrap";

export default function ModalAlert(props) 
{
  //const [show, setShow] = useState(props.showModal || false);

  //const handleOnClose = () => setShow(false);

  // const handleOnOk = () => {
  //   if (props.handleOk) {
  //     props.handleOk()
  //   }
  // };

  return (
    <>
      {/* <Button variant="primary" onClick={handleOk}>
          Launch demo modal
        </Button> */}

      <Modal show={props.showModal} onHide={props.handleOnClose}>
        <Modal.Header closeButton>
          <Modal.Title>{props.heading}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{props.children}</Modal.Body>
        <Modal.Footer>
           
            <Button variant="secondary" onClick={props.handleOnClose}>
              {props.cancelText}
            </Button>
            
              <Button variant="primary" onClick={props.handleOk}>
                {props.okText}
              </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export function ModalDelete(props) {
  console.log('props::',props);
  return (

      <>
      <ModalAlert 
        showModal={props.showModal}
        handleOnClose={props.handleOnClose}
        handleOk={props.handleOk}
        heading={"Alerta"} 
        cancelText={"Cancelar"}
        okText={"Ok"}
      >
        {props.children || <p>Esta seguro de eliminar este registro?</p>}
      </ModalAlert>
      </>
   )
}
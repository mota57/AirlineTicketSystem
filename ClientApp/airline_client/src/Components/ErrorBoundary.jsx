import {Component} from 'react';

export default class  ErrorBoundary extends Component {
    constructor(props) {
      super(props);
      this.state = { hasError: false };
    }
  
    static getDerivedStateFromError(error) {
      // Update state so the next render will show the fallback UI.
      return { hasError: true , error};
    }
  
    componentDidCatch(error, errorInfo) {
      console.error('error boundary message', error);
      console.error('error boundary errorInfo',errorInfo);
    }
  
    render() {
      if (this.state.hasError) {
        // You can render any custom fallback UI
        return <h1>Lo sentimos un error a ocurrido.</h1>;
      }
  
      return this.props.children; 
    }
  }
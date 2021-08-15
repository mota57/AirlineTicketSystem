## programatic navigation
* https://dev.to/projectescape/programmatic-navigation-in-react-3p1l


## React.useState does not reload state from props
* https://stackoverflow.com/questions/54865764/react-usestate-does-not-reload-state-from-props


## set multile values setfieldVlaue formkit

https://github.com/formium/formik/issues/581

address = {
    billingAddress: {
        postalCode: '12345,
        state: 'Texas',
        city: 'Dallas',
        line1: '123 ABC Rd',
        line2: '',
        contact: {
            firstName: 'John',
            lastName: 'Doe',
            email: 'jdoe@gmail.com',
        },
    }
}

setFieldValue('billingAddress', address);   // this works, if I want to change multiple fields with an Object
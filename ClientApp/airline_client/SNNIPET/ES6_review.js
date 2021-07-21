/*
* let & const
* arrow function
* string interpolation
* Object Notation.
* desctructor
* classes
* Array Map
* Promises. async await fetch
* Modules
*/

function printSeperator(name) {
    name = name != null ? name : "";
    var content = "";
    for (let index = 0; index < 50; index++) {
        content += "="
    }
    console.log(content + name + content + "\n\n");
}

printSeperator('CLASSES');

class Shape {
    constructor() {

    }

    area() {
        console.log('area shape');
    }
}

class Rectangle extends  Shape{
    constructor()  {
        super();
    }

    area() {
        console.log('area Rectangle');
    }

    static Foo(){
        console.log('im a static function')
    }
}

var s = new Rectangle();
s.area();

Rectangle.Foo();
printSeperator('ARRAY MAP');

var personas = [
    {id:1, nombre:"faa"},
    {id:2, nombre: "fa0o"},
    {id:3, nombre: "fee"},
]
console.log(personas.map(p => { return  {foo:p.id } }))
console.log(personas.map(p => { return  `<label>nombre</label><input type='text' value='${p.nombre}'/> ` }))

printSeperator('ARRAY MAP');

if(typeof(fetch) !== 'undefined') {

    fetch('https://jsonplaceholder.typicode.com/todos/1')
    .then(response => response.json())
    .then(json => console.log(json))

    

    async function ejemploPromesa() {
        try {

            
            var respuesta = await fetch('https://jsonplaceholder.typicode.com/todos/1')
            const content = await respuesta.json();
            console.log('content ', content);
        } catch (ex) {
            console.error(ex);
        }
    }

    ejemploPromesa();
}

/**
 * #name.js
 * const nombre  = 'hector mota';
 * const edad = 23;
 * export (nombre as default, edad) <! -- nombre is the default export
 * -----------------------------
 * 
 * 
 * # index.js
 * import myDefaultObjectOrFunction, {edad} from './personas'
 * 
 * console.log(myDefaultProp, edad) <--- 'hector mota'+
 *  
 * 
 */
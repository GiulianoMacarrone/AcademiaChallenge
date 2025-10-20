# Challenge

## Giuliano Sebastian Macarrone

### Objetivos

  ¡Bienvenido al **Challenge**!
  
  El objetivo de este desafío es brindarte la oportunidad de demostrar tus conocimientos y habilidades técnicas.

  Recuerda que dispones de **48 horas** para completar el desafío.


### Aclaraciones

#### Solución

La solución está creada utilizando **Visual Studio**, aunque también podés usar **Visual Studio Code** si lo preferís. El proyecto se compone de dos partes:
- **AcademiaChallenge**: una *Class Library* que contiene la lógica de negocio.
- **AcademiaChallengeTests**: un proyecto de tests que permite verificar el correcto funcionamiento de la lógica de negocio.

#### Lenguaje de programación

El lenguaje de programación del desafío es **C#**. La evaluación del challenge se focalizará en la lógica, técnicas de programación, patrones de diseño, y metodologías aplicadas mas que en el uso del lenguaje.

#### Buenas prácticas en código

Se valorará positivamente el uso de **buenas prácticas** de programación, como la correcta **modularización**, nombres **descriptivos**, el manejo de **warnings**, y la aplicación de sugerencias (**hints**). 

#### Integración continua

Se provee un pipeline de **integración continua**. Cada vez que realices un *push* a la rama `main`, este pipeline se ejecutará automáticamente. Asegúrate de que el código compile correctamente y que los tests se ejecuten sin problemas antes de cada *push*.

#### Uso del tiempo

Priorizá tus tareas de la forma mas eficiente posible, evitá quedarte trabado, ante dudas o inconvenientes continuá con el siguiente ítem.
Valoraremos mejor que tengas implementaciones mínimas y con faltantes de cada tema antes que tener terminado y completo sólo una pequeña parte.

### Consideraciones

La clase `Negocio` es la que implementará toda la lógica, exponiendo varios métodos públicos que no deben ser modificados, los cuales servirán para interactuar con el mismo.

Se brinda una arquitectura que podría ser mejorable pero al mismo tiempo suficiente para poder implementar la mayor parte de las funcionalidades requeridas.
Se recomienda no hacer modificaciones o refactors innecesarios para no alejarse mucho de la arquitectura y patrones propuestos.

La representación interna de la clase Negocio que se da implementada podría no almacenar suficientes datos para poder implementar todas las funcionalidades faltantes, podés agregar o modificar lo que consideres necesario. 

Se da una implementación mínima que satisface un único caso de uso completo, incluyendo un unittest que verifica su correcto funcionamiento. Para seguir agregando funcionalidad va a ser necesario generar nuevo código y modificar el preexistente. 


### Terminar implementación

El código brindado resuelve únicamente el caso de facturar un único pedido con sólo un renglón a un único cliente existente que tiene sólo un recibo.
Es necesario mejorar la implementación para que resuelva mas casos.

- Poder tener mas de un cliente, y facturarle a uno u a otro de forma exitosa.
- Poder tener mas de un recibo, y facturar uno u otro de forma exitosa.
- Poder tener mas de un pedido, y facturar uno u otro de forma exitosa.
- Poder tener mas de un renglon en un pedido y facturarlo de forma exitosa.
- ¿ Falta algún caso ? Documentalo acá e implementalo.


### Validaciones simples de consistencia de datos

En todo sistema es clave que los datos estén siempre consistentes. Para esto se implementan validaciones que se ejecutan antes de hacer cualquier modificacion de datos.
Se da implementada ya la validación de cliente duplicado, con su unittest que verifica el correcto funcionamiento.

Se pide agregar el resto de validaciones de consistencia

- Al agregar clientes:
- - No se puede agregar el mismo cliente mas de 1 vez.
- - No se pueden tener dos clientes con el mismo código.
- - No se pueden tener dos clientes con la misma descripción.
- Al agregar pedidos:
- - La numeración de los pedidos comienza en 1 y es correlativa, sin saltearse números.
- - El cliente debe existir, y los datos del pedido deben ser consistentes con los datos preexistentes del cliente.
- - Los renglones no pueden tener artículos repetidos.
- - Las cantidades pedidas de cada renglón de pedido tienen que tener sentido.
- - Los artículos se identifican de forma unívoca según su codigo, validar que un mismo artículo siempre tenga mismo código, descripción y precio unitario.
- - Los totales de cada renglón de pedido deben estar correctos.
- Al agregar un recibo:
- - La numeración de los recibos comienza en 1 y es correlativa, sin saltearse números.
- - El cliente debe existir, y los datos del recibo deben ser consistentes con los datos preexistentes del cliente.
- ¿ Faltan validaciones de consistencia ?
- - Documentalas acá e implementalas.

### Validaciones de circuito

La clase `Negocio` expone el método `Facturar` que genera una factura tomando datos de un pedido cuando ya ha sido pagado habiendo registrado el pago mediante un recibo.
Antes de generar la factura es necesario validar situaciones indeseadas.
Se pide completar validaciones de circuito al facturar

- El importe del pedido se corresponda con el importe total a facturar, que se compone como suma del importe del pedido mas los impuestos del cliente.
- El cliente del pedido y del recibo deben ser el mismo.
- ¿ Se te ocurren mas validaciones aplicables ? Documentalas e implementalas.

### UnitTests 
  
  La clase de tests `NegocioTest` ya cuenta con algunos UnitTests implementados a modo de ejemplo.

  Estos tests aseguran que los métodos proporcionados hasta el momento funcionan de acuerdo con las especificaciones.

#### Tu desafío
  Nos gustaría que completes los UnitTests para la funcionalidad faltante. El objetivo es que asegures la correcta implementación de las funcionalidades que programaste.
  Un buen conjunto de tests te ayudará a validar que la lógica del sistema está correctamente implementada y a identificar errores antes de que ocurran en producción.

### Diseño y Estructura
  Se realizaron las siguientes mejoras de diseño y arquitectura:
  - Modularización del Dominio: El módulo principal Negocio se modularizó en clases:
    - NegocioCliente
    - NegocioArticulo
    - NegocioPedido
    - NegocioRecibo 
    - NegocioFactura

  - Diseño orientado a objetos:
    - Se añadió la clase Articulo
    - Los renglones (Pedido y Factura), junto con sus entidades, ahora usan Objetos de Dominio (Articulo y Cliente) en lugar de datos sueltos.
    - Se implementó la función EliminarCliente() para su uso en distintos testeos.
  
  - Mapeo de Datos: Para aislar el modelo de Factura de las entidades de dominio internas.
    - Se generó carpeta Mapper
      - ArticuloMapper
      - ClienteMapper
    - Se generó carpeta DTOs
      - DTOClienteFactura
      - DTOArticuloFactura

    Factura usa un DTOClienteFactura para manejar los datos del cliente asegurandose de que se pueda acceder al cliente (Internal) en todo momento, con aquellos datos necesarios. También al usar un DTO y usar el setter "init" garantizamos que una vez creada la instancia de factura la misma sea inmutable (mantener un registro histórico).

  - Correcciones de Bugs y Robustez: Se reemplazó el uso de los .Single en la lógica para el manejo de múltiples instancias de un mismo objeto.
  - Se añadieron excepciones personalizadas y la cobertura de Unit Tests se amplió para cubrir las validaciones de consistencia de datos y de circuito.
  
  - Numeración Correlativa
    - Recibo (Controlado por Dominio): La numeración correlativa (comenzando en 1) se garantiza mediante la generación interna del número en NegocioRecibo. El método AgregarRecibo no acepta el número como parámetro ya que es un valor de control interno del dominio.

    - Pedido (Validado por Input): El número se acepta como input y el sistema valida su correlatividad, demostrando un enfoque alternativo para datos proporcionados por el exterior.

    - La validación de consistencia de Precio Total se garantiza por construcción, ya que el valor final se calcula internamente usando el dato maestro (Articulo.PrecioUnitario) y la cantidad pedida, en lugar de recibirlo como input.

    Se eligió esta forma de correlatividad para mostrar dos enfoques distintos para una misma problemática.

### Implementaciones y Validaciones. Listado.

  - Puede haber más de un cliente, y se factura a uno u otro de forma exitosa
  - Se gestionan y facturan múltiples recibos de forma exitosa.
  - Se gestionan y facturan múltiples pedidos de forma exitosa.
  - Se facturan pedidos con varios renglones, calculando correctamente los totales.
  - No se puede agregar el mismo cliente más de 1 vez (usando el mismo código).
  - No se pueden tener dos clientes con el mismo código.
  - No se pueden tener dos clientes con la misma razón social (descripción).
  - No se puede agregar el mismo artículo más de 1 vez (usando el mismo código).
  - No se pueden tener dos artículos con la misma descripción.
  - Un mismo código de artículo siempre debe tener la misma descripción y precio unitario.
  - La numeración de los pedidos comienza en 1 y es correlativa, sin saltearse números (se valida que el número ingresado sea Max(Existente) + 1).
  - El cliente debe existir (validación de referencia), y los datos del pedido son consistentes con el cliente preexistente.
  - El artículo debe existir (validación de referencia).
  - Los renglones no pueden tener artículos repetidos (validado por código de artículo).
  - Las cantidades pedidas de cada renglón de pedido tienen que tener sentido (deben ser mayores a cero).
  - Los totales de cada renglón de pedido deben estar correctos: Se garantiza por diseño, ya que el PrecioTotal se calcula internamente (PrecioUnitario * Cantidad) y no se acepta como input para prevenir inconsistencias.
  - La numeración de los recibos comienza en 1 y es correlativa (se garantiza por diseño, ya que el número es generado internamente por la capa de negocio).
  - El cliente debe existir (validación de referencia), y los datos del recibo son consistentes con el cliente preexistente.
  - Se verifica que el Pedido exista antes de facturar.
  - Se verifica que el Recibo exista antes de facturar.
  - Se verifica que el Cliente (referenciado en el pedido y recibo) exista en la capa de datos maestra en el momento de la facturación.
  - El cliente del pedido y del recibo deben ser el mismo.
  - El importe del recibo debe corresponderse con el importe total a facturar (suma del total del pedido más los impuestos del cliente), utilizando una tolerancia decimal (0.01) para evitar errores de floating point.
  - Se verifica que si se solicita una factura, esta exista (FacturaNoEncontradaException).

### Autor
Implementación realizada por: Giuliano Sebastian Macarrone

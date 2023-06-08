# PRUEBA TÉCNICA - PLANTS VS ZOMBIES

En la siguiente prueba se presenta un juego tipo Plantas VS Zombies con las mecánicas principales. A continuación se da la siguiente información referente a este proyecto.

## NOTAS IMPORTANTES

A continuación se dan las siguientes notas importantes a considerar:

- El proyecto fue desarrollado en 3D, la razón por la que se tomó esta decisión es para minimizar la posibilidad de considerar haber hecho el proyecto a través de tutoriales en internet, ya que hay bastantes. Además, el elemento 3D le agregó retos que hicieron que el proceso de desarrollo fuera un poco diferente.
- Se minimizó u omitió utilizar elementos visuales como VFX, Materiales complejos, Efectos de sonido entre otros. Esto para dedicar todo el tiempo y energía y enfocarlo exclusivamente a la programación, estructura y optimización del proyecto.

## ACERCA DEL JUEGO

A continuación se describen las mecánicas desarrolladas en esta prueba:

- Flujo de juego: Se inicia el juego desde un menu principal, seleccionando un nivel, jugando el nivel y ganango o perdiendo, se regresa al menu principal para iniciar de nuevo.
- Gameplay General: Al entrar en un nivel, se pueden seleccionar las cartas disponibles y colocarlas en los cuadros del escenario, si la carta se puede comprar con la moneda de sol, la planta será colocada en el escenario y comenzará a funcionar. Durante el juego, aparecen zombies en un orden sucesivo para cada nivel (Siguiendo la dinámica del juego de PVZ original), los zombies aparecen en las filas del escenario de manera aleatoria. Si el jugador acaba con todos los zombies, gana, si los zombies sobrepasan el área de juego, pierde.
- Acciones de las cartas: Las cartas se pueden colocar en el escenario si hay suficiente sol para comprarlas, si no es posible, la carta esta deshabilitada mostrando un cuadro sombreado, cuando una carta es comprada esta se encuentra recargandose y no se puede comprar de nuevo hasta que esté lista.
- Plantas: Los tipos de cartas disponibles son el Girasol, DisparaChicharos, Nuez, DisparaChicharos congelados y DisparaChícharos doble, todas cumplen su función principal.
- Zombies: Los zombies disponibles son el Zombie simple, Zombie con Cono y Cubeta, Zombie con puerta y Zombie Michael Jackson (o discoteca). Todos los zombies cumplen su función principal.
- Niveles: Hay 4 niveles creados para esta prueba con dificultad incremental, al final se incluye un nivel titulado "Dark Souls" que fue hecho para hacer una pequeña prueba exhaustiva en el juego.
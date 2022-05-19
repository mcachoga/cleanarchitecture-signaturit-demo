Signaturit Clean Architecture App Demo

# Primeros Pasos

Crear la base de datos

Después, cambiar las cadenas de conexión en la API y web (tener en cuenta que son dos proyectos INDEPENDIENTES). La web no usa la api!!

Cadenas de conexion en API: 
Signaturit.Api / appsettings.json

Cadenas de conexion en Web:
Signaturit.Web / appsettings.json

Una vez creada la base de datos, y asignandola en las cadenas de conexión, ejecutar (USANDO EL PROYECTO DE INFRASTRUCTURE):

''''ts
update-database -context IdentityContext
update-database -context ApplicationDbContext
''''

Esto ya debería de generar las tablas correctamente. A veces da un error (me ha pasado muuuy pocas veces), por lo que ejecuto estos
comandos y vuelvo a actualizar la base datos con los comandos anteriores:

''''ts
add-migration initial2 -context ApplicationDbContext
add-migration initialIdentity2 -context IdentityContext
''''

Se le puede poner el nombre que se quiera, pero initial y initialIdentity ya existen.

# Referencias

Se han usado varios proyectos, no recuerdo para citarlos todos, pero se ha usado alguna implementacion de:

* Plantilla AdminLTE (Gratuita, con soporte para Identity, multidiomas y theme). No me he preocupado mucho que funcionen esos aspectos,
  mas alla que te puedas logear en WEB y que se te genere el token en API. He copiado ideas de varios proyectos, no recuerdo todos para citarlos.
  Se usa postman para las pruebas.

* Agregadas pantallas de Login y todo lo de identity
* Codigo basado principalmente de: https://github.com/jasontaylordev/CleanArchitecture (aunque está para NET6, se ha podido cambiar a NET5 sin muchos problemas)
* Se ha estudiado: https://github.com/jbogard

* Validaciones sin lanzar error, que es lo que me has costado (las validaciones solo en un solo lugar y que se puedan usar en ambos proyectos)
gracias a: https://dev.to/isaacojeda/parte-2-cqrs-y-mediatr-validando-con-fluentvalidation-14i0 y sobre todo a las dos series 
super interesantes de https://davidrogers.id.au/wp/?p=3196

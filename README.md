# Identificador de elementos duplicados
Aplicación para identificar grupos de viviendas repetidas en bases de datos censales.

Esta aplicación fue elaborada y utilizada para detectar las viviendas duplicadas por parte del Instituto Nacional de Estadística y Censos (INDEC) de la Argentina en el censo de población del año 2010. Los datos finales de ese proceso pueden ver en: https://mapa.poblaciones.org/map/87101.

# Modo de uso

Para utilizar la aplicación, es posible descargarla desde:

https://github.com/discontinuos/duplicates-finder/releases/tag/on

Una vez expandido el archivo zip, puede ejecutar finde.exe. La aplicación ser verá del siguiente modo:

![aplicación](https://raw.githubusercontent.com/discontinuos/duplicates-finder/main/captura.png)

En esa ventana debe elegirse:
- el archivo SPSS con los casos (ej. personas)
- el campo que funcionará como clave. Si la clave por ejemplo es el identificador de viviendas, se recorrerán las personas generando identificadores de las viviendas que consideren los atributos de todas las personas de esa vivienda para definirlo.
- los campos excluidos en la comparación. Es importante que se considere detenidamente qué excluir. Las variables geográficas (ej. código de municipio) típicamente querrá excluirse si se quiere detectar viviendas duplicadas de un municipio en otro. Lo mismo para códigos de provincias o regiones. Si se quiere identificar viviendas repetidas, también deben excluirse de la comparación los identificadores de personas, o de hogares (si son identificadores únicos), pues harían nula la posibilidad de encontrar grupos de viviendas replicados. 

Luego se debe aplicar cada paso para producir primero los identificadores (hashes), luego los pares de hashes iguales, y luego los grupos. Al producir los grupos se puede probar generando grupos de viviendas duplicadas con diferentes puntos de corte.

Finalmente, permite 'expandir' los grupos, generando filas para cada uno de sus miembros. Como la tabla 'Groups' que genera solamente dice 'el grupo de esta familia comprende desde el ID_n1 al ID_n2', puede ser útil producir una tabla donde se copie todo ese rango de elementos. 

# Advertencia

Estos fuentes no están completamente documentados para su uso. Se agraceden contribuciones que faciliten su reutilización.

# Requisitos

Framework .NET 4.8
La aplicación utiliza el Microsoft Framework .NET 4.8. Esto es un runtime de Microsoft para ejecutar aplicaciones, similar a la virtual machine de Java. Si no estuviera instalado en la PC, podrá descargarse de https://dotnet.microsoft.com/es-es/download/dotnet-framework/net48.

SPSS.NET InteropLibrary 
La aplicación utiliza la librería de SPSS.NET InteropLibrary (https://spss.codeplex.com/) para generar archivos nativos de SPSS como salida. Esta librería no requiere una instalación independiente, pero posiblemente deba copiar los archivos de la carpeta SPSSio a su carpeta c:\windows\System32 (o equivalente... hacer Inicio > Ejecutar > %WINDIR%\SYSTEM32 <enter> puede ser una buena forma de identificar la carpeta System32 de la PC).

# Contacto

Pablo De Grande / pablodg@gmail.com

Perfil de publicaciones: https://aacademica.org/pablo.de.grande



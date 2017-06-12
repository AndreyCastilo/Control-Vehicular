# Control-Vehicular
Se le ha contratado para desarrollar un sistema multi-tenant con el fin de llevar el control de la flotilla vehicular de varias empresas proveedoras de servicios de transportes de estudiantes. Esto significa que el sistema debe permitir tener en la misma Base de datos todas empresas provedoras del servicio, cada empresa puede tener varios carros con sus respectivos estudiantes, en determinadas franjas horarias. Además de ello el sistema debe contemplar la captación de información adicional sobre la flotilla. Esto se detalla a continuación: 
1.	Datos de la empresa: nombre comercial, es empresa física o personal, cédula (jurídica o personal), número de teléfono, dirección Web, correo electrónico. 
2.	Por cada empresa se tienen varias unidades.
3.	Datos de las unidades: Placa, año, marca, modelo, moto, capacidad, fotografía de la unidad, de la tarjeta de circulación y de la revisión técnica. Además, se debe indicar la última revisión técnica y fecha de vencimiento. 
4.	Por cada empresa se tienen varios conductores.
5.	Datos del conductor: nombre completo, fotografía de la cédula, fotografía de la licencia, tipo de licencia, fecha de vencimiento de la licencia. 
6.	Por cada empresa se tienen varios seguros
7.	Datos de seguro: aseguradora, tipo de seguro, vigencia, detalle.
8.	Por cada empresa se tienen varios clientes
9.	Datos del cliente: nombre del cliente, dirección, mostrar cómo. Además de lo anterior se tiene una lista de hijos
10.	Se debe desarrollar una ventana Web diseñada para dispositivos mobiles, donde se registre cada cinco minutos su geolocalización, la misma será guardada en coordenadas en la base de datos. Esta interface debe poder marcar la lista de clientes que se visitarán.
11.	Padres de familia: en el caso de los padres de familia, se debe mostrar la distancia de la unidad que va atender a su hijo. Debe mostrar su ubicación actual y la ubicación de la unidad que atiende a su hijo.

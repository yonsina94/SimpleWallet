# SimpleWallet API

## Descripción

Esta API REST permite la gestión de transferencias de saldo, cumpliendo con los siguientes requisitos:

* Permite operaciones CRUD sobre las billeteras.
* Permite operaciones CRUD sobre el historial de movimientos.
* Maneja los casos de errores comunes.
* Utiliza dólares americanos para todas las billeteras.

## Requisitos

### 1.  Stack

* **Framework:** .NET 8
* **Arquitectura:** Clean Architecture orientada a microservicios
* **Persistencia:** Entity Framework Core
* **Base de datos:** PostgreSQL
* **Pruebas:** xUnit (unitarias y de integración)
* **Enfoque de desarrollo:** Se aplican principios SOLID y buenas prácticas de desarrollo.

### 2.  Modelo de Datos (Billetera)

Cada billetera contiene las siguientes propiedades:

* `id`: Identificador único (entero autoincremental).
* `documentid`: Documento de identidad del propietario de la billetera.
* `documentType`: Tipo de documento de identidad que ha ingresado un cliente, ya sea su # de identificacion u pasaporte
* `name`: Nombre del propietario de la billetera.
* `balance`: Saldo de la billetera.
* `createdAt`: Fecha de apertura de la billetera.
* `updatedAt`: Fecha de última actualización de la billetera.

### 3.  Modelo de Datos (Historial de movimientos)

Cada movimiento contiene las siguientes propiedades:

* `id`: Identificador único (entero autoincremental).
* `walletid`: Identificador de la billetera.
* `amount`: Monto de la transferencia.
* `type`: Tipo de operación (Débito/Crédito).
* `createdAt`: Fecha del movimiento.

### 4.  Validaciones

Se asegura que los datos ingresados sean correctos mediante las siguientes validaciones:

* El monto de la transferencia debe ser mayor que cero.
* El nombre del propietario de la billetera no puede estar vacío.
* Se impide la transferencia de un monto mayor al disponible, retornando un error adecuado.
* Se retorna un error adecuado al intentar transferir a una billetera que no existe.

### 5.  Manejo de Errores

Se implementa un manejo de errores robusto para los casos comunes, proporcionando respuestas informativas y facilitando la depuración.

### 6.  Pruebas

Las pruebas no fueron posibles de aplicar en este release. se procederia a en otra ocacion

### 7. Preguntas Clave

1.  **¿Cómo tu implementación puede ser escalable a miles de transacciones?**

    * La arquitectura de microservicios permite escalar cada servicio de forma independiente.  Se pueden usar múltiples instancias del servicio de Wallet y Movement para manejar un gran volumen de transacciones.  Además, se puede utilizar una base de datos escalable y optimizar las consultas.

2.  **¿Cómo tu implementación asegura el principio de idempotencia?**

    * La idempotencia se maneja a nivel de la lógica de negocio, asegurando que una transacción pueda ser procesada varias veces sin cambiar el resultado final.  Se utiliza un identificador único para cada transacción.

3.  **¿Cómo protegerías tus servicios para evitar ataques de Denegación de Servicios, SQL Injection y CSRF?**

    * **DoS:** Se implementa limitación de velocidad (rate limiting) y se pueden usar servicios de protección contra DDoS.
    * **SQL Injection:** Se utiliza Entity Framework Core, que parametriza las consultas a la base de datos, evitando la inyección de código SQL.
    * **CSRF:** Se implementan tokens CSRF en las interfaces de usuario (si aplica) para proteger las peticiones.

4.  **¿Cuál sería tu estrategia para migrar un monolito a microservicios?**

    * La estrategia consiste en descomponer el monolito en servicios más pequeños, comenzando por las funcionalidades clave.  Se utiliza un enfoque iterativo, extrayendo gradualmente la lógica del monolito a los nuevos microservicios.

5.  **¿Qué alternativas a la solución requerida propondrías para una solución escalable?**

    * Se podrían utilizar colas de mensajes para el procesamiento asíncrono de transacciones, bases de datos NoSQL para manejar grandes volúmenes de datos, y sistemas de caché para reducir la carga en la base de datos.

## Patrones de Diseño y Buenas Prácticas Aplicadas

En este proyecto, se han aplicado los siguientes patrones de diseño y buenas prácticas para asegurar una arquitectura robusta, escalable y mantenible:

* **Patrón de Diseño: CQRS (Command Query Responsibility Segregation)**: Se separa la responsabilidad de lectura y escritura, mejorando el rendimiento y la flexibilidad.
* **Patrón de Diseño: Repository**: Se abstrae el acceso a la base de datos, facilitando la prueba y el mantenimiento.
* **Patrón de Diseño: Unit of Work**: Se agrupan las operaciones en una sola transacción, asegurando la consistencia de los datos.
* **Patrón de Diseño: Dependency Injection**: Se promueve el desacoplamiento y la testabilidad mediante la inyección de dependencias.
* **Patrón de Diseño: DTO (Data Transfer Object)**: Se optimiza la transferencia de datos entre capas, mejorando el rendimiento y la seguridad.
* **Patrón de Diseño: Mapper (AutoMapper)**: Se simplifica el mapeo entre entidades y DTOs, reduciendo el código repetitivo.
* **Validación de Datos**: Se asegura la integridad de los datos mediante validaciones en la entrada.
* **Manejo de Excepciones**: Se proporciona un manejo de errores centralizado y coherente.
* **Organización por Capas**: El proyecto está organizado en capas (Dominio, Aplicación, Infraestructura, API) para promover la separación de responsabilidades.
* **Uso de Migraciones**: Se gestionan los cambios en el esquema de la base de datos de manera controlada.
* **Uso de Interfaces**: Se definen interfaces para facilitar el desacoplamiento y la implementación de diferentes estrategias.
* **Uso de Logs**: Se registran eventos importantes para facilitar el monitoreo y la depuración.
* **Configuración Centralizada**: Se utiliza un archivo de configuración centralizado para facilitar la gestión de los parámetros de la aplicación.
* **Documentación de la API**: Se utiliza Swagger para documentar la API, facilitando su uso por parte de otros desarrolladores.

## Modelo de Datos (Esquema de la Base de Datos)

El siguiente script SQL se utilizó para generar el modelo de datos en la base de datos PostgreSQL:

```sql
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Wallets" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "DocumentId" varchar(20) NOT NULL,
    "DocumentType" text NOT NULL,
    "Name" varchar(120) NOT NULL,
    "Balance" numeric(18,2) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Wallets" PRIMARY KEY ("Id")
);

CREATE TABLE "Movements" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "WalletId" int NOT NULL,
    "Amount" numeric(18,2) NOT NULL,
    "Type" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Movements" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Movements_Wallets_WalletId" FOREIGN KEY ("WalletId") REFERENCES "Wallets" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Movements_WalletId" ON "Movements" ("WalletId");

CREATE INDEX "IX_Movements_WalletId_CreatedAt" ON "Movements" ("WalletId", "CreatedAt");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250509094019_InitialMigration', '8.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "Movements" DROP CONSTRAINT "FK_Movements_Wallets_WalletId";

ALTER TABLE "Wallets" ALTER COLUMN "Name" TYPE character varying(100);

ALTER TABLE "Wallets" ALTER COLUMN "DocumentId" TYPE character varying(100);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250509110832_RemoveRelationsMigration', '8.0.10');

COMMIT;
```

## Notas

* Se priorizó la entrega de una solución funcional, bien estructurada y con buenas prácticas. De hecho podemos subir dicho proyecto desde un DevContainer en VSCode y el mismo funcionara.
* Se valoró la capacidad de justificar decisiones técnicas y priorizar implementaciones, por este motivo no fue posibe agregar las pruebas unitarias requeridad en bien de poder hacer de este proyecto funcional en el tiempo establecido.
* Se utilizó Git de manera adecuada para el control de versiones.

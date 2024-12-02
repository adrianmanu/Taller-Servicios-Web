# Proyecto SOAP con WCF

## Descripción del Proyecto
Este proyecto implementa un servicio SOAP utilizando **Windows Communication Foundation (WCF)**. El servicio gestiona registros de productos y categorías almacenados en una base de datos SQL Server.

## Características
- CRUD para la tabla **Products**:
  - Campos: `ProductID`, `ProductName`, `CategoryID`, `UnitPrice`, `UnitsInStock`.
- CRUD para la tabla **Categories**:
  - Campos: `CategoryID`, `CategoryName`, `Description`.
- Pruebas mediante:
  - Herramientas como **Postman**.
  - Scripts Python utilizando el paquete **zeep** para consumir el WSDL.

---
## Requisitos

1. **Sistema Operativo**: Windows 10 o superior.
2. **Herramientas**:
   - Visual Studio (2019 o superior).
   - SQL Server (LocalDB o instalación completa).
3. **Base de datos**:
   - Nombre: `Sales_DB`.
4. **.NET Framework**: 4.7.2.
5. **PowerShell** (para habilitar características de WCF).
## Configuración

### 1. Base de Datos
Crea una base de datos llamada `Sales_DB` y ejecuta los siguientes scripts para generar las tablas necesarias:


CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(50) NOT NULL,
    CategoryID INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    UnitsInStock INT NOT NULL
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX)
);

#### 3.2 Configuración de la Cadena de Conexión


### 2. Configuración de la Cadena de Conexión
En el archivo `web.config`, actualiza la cadena de conexión para apuntar a tu instancia local de SQL Server:

<connectionStrings>
    <add name="Sales_DB_Connection"
         connectionString="Server=localhost;Database=Sales_DB;User Id=sa;Password=TuContraseña;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
</connectionStrings>


#### 3.3 Habilitar WCF en Windows


### 3. Habilitar WCF en Windows
Si no tienes activado el soporte para WCF, habilítalo ejecutando el siguiente comando en PowerShell como administrador:

Enable-WindowsOptionalFeature -Online -FeatureName NetFx3, WCF-HTTP-Activation45 -All


### 4. Ejecutar el Proyecto

#### 4.1 Abrir y Ejecutar el Servicio


## Ejecutar el Proyecto

### 1. Abrir y Ejecutar el Servicio
1. Abre el proyecto en **Visual Studio**.
2. Compila el proyecto para verificar que no haya errores (`Ctrl+Shift+B`).
3. Ejecuta el servicio (`F5`) y asegúrate de que el navegador muestre la URL del WSDL, como:

http://localhost:58085/ProductService.svc?wsdl

## Pruebas

### **Usar Postman**
1. Abre **Postman** y crea una nueva solicitud.
2. Configura los siguientes parámetros:
   - **URL**: `http://localhost:58085/ProductService.svc`
   - **Método**: `POST`.
   - **Cabeceras**:
     ```
     Content-Type: text/xml
     ```
   - **Cuerpo**: Ejemplo para crear un producto:

     <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
       <soap:Body>
         <Create xmlns="http://tempuri.org/">
           <products>
             <ProductName>Example Product</ProductName>
             <CategoryID>1</CategoryID>
             <UnitPrice>10.50</UnitPrice>
             <UnitsInStock>100</UnitsInStock>
           </products>
         </Create>
       </soap:Body>
     </soap:Envelope>
     ```

3. Envía la solicitud y verifica la respuesta.

### **Usar Python**
Ejecuta el siguiente script en **Python** para consumir el servicio SOAP usando **zeep**:


from zeep import Client

# URL del WSDL
wsdl = 'http://localhost:58085/ProductService.svc?wsdl'

# Crear cliente SOAP
client = Client(wsdl)

# Llamar al método 'Create' para insertar un producto
response = client.service.Create({
    'ProductName': 'Example Product',
    'CategoryID': 1,
    'UnitPrice': 10.50,
    'UnitsInStock': 100
})

print(response)  # Respuesta del servidor

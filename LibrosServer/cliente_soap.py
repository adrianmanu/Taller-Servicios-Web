from zeep import Client, Settings
from zeep.exceptions import Fault
import logging

# Configuración de logging para depurar solicitudes y respuestas
logging.basicConfig(level=logging.INFO)
logging.getLogger("zeep.transports").setLevel(logging.DEBUG)

# URL del WSDL
WSDL_URL = 'http://localhost:8080/ProductSOAPService.svc?wsdl'

# Configuración del cliente
settings = Settings(strict=False, xml_huge_tree=True)
client = Client(wsdl=WSDL_URL, settings=settings)

# Funciones CRUD
def create_product(product_name, category_id, unit_price, units_in_stock):
    """Crear un nuevo producto."""
    product_data = {
        "ProductName": product_name,
        "CategoryID": category_id,
        "UnitPrice": unit_price,
        "UnitsInStock": units_in_stock
    }
    try:
        response = client.service.Create(product_data)
        print(f"Producto creado con éxito: {response}")
    except Fault as fault:
        print(f"Error al crear el producto: {fault}")

def retrieve_product(product_id):
    """Obtener un producto por su ID."""
    try:
        response = client.service.RetrieveById(product_id)
        print(f"Producto encontrado: {response}")
    except Fault as fault:
        print(f"Error al obtener el producto: {fault}")

def update_product(product_id, product_name, category_id, unit_price, units_in_stock):
    """Actualizar un producto existente."""
    updated_product_data = {
        "ProductID": product_id,
        "ProductName": product_name,
        "CategoryID": category_id,
        "UnitPrice": unit_price,
        "UnitsInStock": units_in_stock
    }
    try:
        response = client.service.Update(updated_product_data)
        if response:
            print(f"Producto con ID {product_id} actualizado con éxito.")
        else:
            print(f"No se pudo actualizar el producto con ID {product_id}.")
    except Fault as fault:
        print(f"Error al actualizar el producto: {fault}")

def delete_product(product_id):
    """Eliminar un producto por su ID."""
    try:
        response = client.service.Delete(product_id)
        if response:
            print(f"Producto con ID {product_id} eliminado con éxito.")
        else:
            print(f"No se pudo eliminar el producto con ID {product_id}.")
    except Fault as fault:
        print(f"Error al eliminar el producto: {fault}")

# Menú de pruebas
def main():
    while True:  # El ciclo continúa hasta que el usuario elija salir
        print("\nPruebas del CRUD SOAP con Python y Zeep:")
        print("1. Crear Producto")
        print("2. Obtener Producto por ID")
        print("3. Actualizar Producto")
        print("4. Eliminar Producto")
        print("5. Salir")

        try:
            choice = int(input("\nElige una opción (1-5): "))
            if choice == 1:
                product_name = input("Nombre del producto: ")
                category_id = int(input("ID de categoría: "))
                unit_price = float(input("Precio unitario: "))
                units_in_stock = int(input("Unidades en stock: "))
                create_product(product_name, category_id, unit_price, units_in_stock)

            elif choice == 2:
                product_id = int(input("ID del producto a buscar: "))
                retrieve_product(product_id)

            elif choice == 3:
                product_id = int(input("ID del producto a actualizar: "))
                product_name = input("Nuevo nombre del producto: ")
                category_id = int(input("Nueva ID de categoría: "))
                unit_price = float(input("Nuevo precio unitario: "))
                units_in_stock = int(input("Nuevas unidades en stock: "))
                update_product(product_id, product_name, category_id, unit_price, units_in_stock)

            elif choice == 4:
                product_id = int(input("ID del producto a eliminar: "))
                delete_product(product_id)

            elif choice == 5:
                print("Saliendo del programa.")
                break  # Sale del ciclo mientras (termina el programa)

            else:
                print("Opción no válida, intenta de nuevo.")

        except ValueError:
            print("Entrada inválida, por favor ingresa un número.")
        except Exception as e:
            print(f"Error inesperado: {e}")

if __name__ == "__main__":
    main()

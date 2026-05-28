using Microsoft.AspNetCore.Mvc;
using proyect.Models;

namespace proyect.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{

    // Simulamos una base de datos en memoria
    private static List<Producto> productos = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Producto A", Precio = 10.99m, Stock = 100 },
        new Producto { Id = 2, Nombre = "Producto B", Precio = 20.50m, Stock = 50 },
        new Producto { Id = 3, Nombre = "Producto C", Precio = 5.75m, Stock = 200 }
    };

    // GET: api/productos
    [HttpGet]
    public ActionResult<IEnumerable<Producto>> Get()
    {
        return Ok(productos);
    }

    // GET: api/productos/1 utilizamos fromroute 

    [HttpGet("{id}")]
    public ActionResult<Producto> GetById([FromRoute] int id)
    {
        var producto = productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return NotFound("no se encontro el producto");
        return Ok(producto);
    }

    // POST: api/productos con frombody

    [HttpPost]
    public ActionResult<Producto> create(Producto producto)
    {
        producto.Id = productos.Max(p => p.Id) + 1;
        productos.Add(producto);
        return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
    }

    // PUT: api/productos/{id}

    [HttpPut("{id}")]
    public ActionResult Put([FromRoute] int id, Producto producto)
    {
        var existingProducto = productos.FirstOrDefault(p => p.Id == id);
        if (existingProducto == null)
            return NotFound("no se encontro el producto");

        existingProducto.Nombre = producto.Nombre;
        existingProducto.Precio = producto.Precio;
        existingProducto.Stock = producto.Stock;

        return NoContent();
    }

    // DELETE: api/productos/{id}

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var producto = productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return NotFound("no se encontro el producto");

        productos.Remove(producto);
        return NoContent();
    }
}
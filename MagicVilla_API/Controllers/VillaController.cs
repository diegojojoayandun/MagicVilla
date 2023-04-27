using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {

            _logger = logger;
            _db = db;

        }
        //Get All villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas() // async Task, used to nanage asyncronism
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(await _db.Villas.ToListAsync());
        }

        // Get villa by Id
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id) 
        {
            if (id==0)
            {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa==null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        // Add new villa to database
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Villas.FirstOrDefaultAsync(v=>v.Nombre.ToLower() == villaDto.Nombre.ToLower()) !=null) 
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            

            Villa modelo = new()
            {
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados=villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad

            };

            await _db.AddAsync(modelo);
            await _db.SaveChangesAsync();
            
            // To add a villa to list(non persistent)
            /*villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id +1;
            VillaStore.villaList.Add(villaDto);*/


            
            return CreatedAtRoute("GetVilla", new { id=modelo.Id}, modelo );

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {

            if (id == 0) 
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto) 
        {
            if(villaDto==null || id != villaDto.Id)
            {
                return BadRequest();
            }
            /*var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            villa.Nombre= villaDto.Nombre;
            villa.Ocupantes= villaDto.Ocupantes;
            villa.MetrosCuadrados=villaDto.MetrosCuadrados;*/

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad

            };

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();

        }


        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id ==0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            VillaUpdateDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados= villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad

            };

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();

        }

    }
}

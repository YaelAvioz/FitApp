using fitappserver.Model;
using fitappserver.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace fitappserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDTO> : ControllerBase where T : GenericEntity where TDTO : GenericEntity
    {
        protected readonly GenericService<T, TDTO> _genericService;

        public GenericController(IMapper mapper)
        {
            _genericService = new GenericService<T, TDTO>(mapper);
        }

        [HttpGet]
        public async Task<ActionResult<List<TDTO>>> GetAll()
        {
            List<TDTO> all = await _genericService.GetAll();
            return Ok(all);
        }

        [HttpPost]
        public async Task<ActionResult<TDTO>> Create([FromBody] T entity)
        {
            TDTO newEntity = await _genericService.Create(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, newEntity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDTO>> Get(int id)
        {
            TDTO entity = await _genericService.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TDTO>> Update([FromRoute] int id, [FromBody] T newEntity)
        {
            if (await Get(id) == null)
            {
                return NotFound();
            }
            TDTO updatedEntity = await _genericService.Update(id, newEntity);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await Get(id) == null)
            {
                return NotFound();
            }
            _genericService.Delete(id);
            return NoContent();
        }
    }
}
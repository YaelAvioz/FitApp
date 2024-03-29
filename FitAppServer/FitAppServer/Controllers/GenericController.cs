﻿using FitAppServer.Model;
using FitAppServer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServer.Controllers
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

        [HttpGet("next/{skip}")]
        public async Task<ActionResult<List<TDTO>>> GetNext(int skip)
        {
            List<TDTO> all = await _genericService.GetNext(skip);
            return Ok(all);
        }

        [HttpPost]
        public async Task<ActionResult<TDTO>> Create([FromBody] T entity)
        {
            TDTO newEntity = await _genericService.Create(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, newEntity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDTO>> Get(string id)
        {
            TDTO entity = await _genericService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<TDTO>> Update(string id, [FromBody] T newEntity)
        {
            TDTO entity = await _genericService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }
            TDTO updatedEntity = await _genericService.Update(id, newEntity);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            TDTO entity = await _genericService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }
            _ = _genericService.Delete(id);
            return NoContent();
        }
    }
}
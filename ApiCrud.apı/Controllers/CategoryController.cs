using ApiCrud.apı.DTOs.Categories;
using ApiCrud.Core.Entities;
using ApiCrud.DAL.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.apı.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppdbContext _db;
        IMapper _mapper;

        public CategoryController(AppdbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            return category == null ? NotFound() : Ok(category);

        }
        [HttpGet]
        public IActionResult Get()
        {


            return Ok(_db.Categories.ToList());

        }

        [HttpPost]
        public ActionResult Create(CreateCategoryDTo category)
        {
            var newcategory = _mapper.Map<Category>(category);
            _db.Categories.Add(newcategory);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newcategory);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);

        }

        [HttpPut]
        public IActionResult Update(UpdateCategoryDTo dto)
        {var category = _db.Categories.AsNoTracking().FirstOrDefault(c => c.Id == dto.Id);
            if (category is null)
            {
                return NotFound();
            }
            category = _mapper.Map<Category>(dto);
            _db.Categories.Update(category);
            _db.SaveChanges();
            return Ok();


        }
    }
}


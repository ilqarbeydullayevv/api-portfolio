using ApiCrud.apı.DTOs.Categories;
using ApiCrud.apı.DTOs.Products;
using ApiCrud.Core.Entities;
using ApiCrud.DAL.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppdbContext _context;
    private readonly IMapper _mapper;

    public ProductController(AppdbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
   
        return Ok(_context.products.ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var product = await _context.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

     
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDTo DTo)
    {
        var product = _mapper.Map<Product>(DTo);
        _context.products.Add(product);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created,product);
     
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDTo DTo)
    {
        if (id != DTo.Id)
        {
            return NotFound();
        }

        var product = await _context.products.FindAsync(id);
        if (product == null) return NotFound();

        _mapper.Map(DTo, product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null) return NotFound();

        _context.products.Remove(product);
        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
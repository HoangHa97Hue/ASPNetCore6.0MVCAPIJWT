using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Contexts;
using TodoAPI.Entities;

namespace TodoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MealCategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MealCategoriesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/MealCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealCategory>>> GetMealCategorys()
        {
          if (_context.MealCategorys == null)
          {
              return NotFound();
          }
            IEnumerable < MealCategory > tempList= await _context.MealCategorys.ToListAsync();
            return await _context.MealCategorys.ToListAsync();
        }

        // GET: api/MealCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealCategory>> GetMealCategory(string id)
        {
          if (_context.MealCategorys == null)
          {
              return NotFound();
          }
            var mealCategory = await _context.MealCategorys.FindAsync(id);

            if (mealCategory == null)
            {
                return NotFound();
            }

            return mealCategory;
        }

        // PUT: api/MealCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMealCategory(string id, MealCategory mealCategory)
        {
            if (id != mealCategory.MealCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(mealCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MealCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MealCategory>> PostMealCategory(MealCategory mealCategory)
        {
          if (_context.MealCategorys == null)
          {
              return Problem("Entity set 'MyDbContext.MealCategorys'  is null.");
          }
            _context.MealCategorys.Add(mealCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MealCategoryExists(mealCategory.MealCategoryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMealCategory", new { id = mealCategory.MealCategoryID }, mealCategory);
        }

        // DELETE: api/MealCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealCategory(string id)
        {
            if (_context.MealCategorys == null)
            {
                return NotFound();
            }
            var mealCategory = await _context.MealCategorys.FindAsync(id);
            if (mealCategory == null)
            {
                return NotFound();
            }

            _context.MealCategorys.Remove(mealCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealCategoryExists(string id)
        {
            return (_context.MealCategorys?.Any(e => e.MealCategoryID == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_Costs.Models;

namespace Lab2_Costs.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class CostItemsController : ControllerBase
    {
        private readonly CostContext _context;

        public CostItemsController(CostContext context)
        {
            _context = context;
        }

        // GET: api/CostItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostItem>>> GetCostItems(DateTimeOffset? from = null,
            DateTimeOffset? to = null, CostType? type= null)
        {
            // filter by date from
            //filter by type 

            
            IQueryable<CostItem> result = _context.CostItems;

            if (from != null && to != null && type != null)
            {
                result = result.Where(c => from <= c.Date && c.Date <= to && type == c.Type );
            }
            else if (from == null && to != null && type != null)
            {
                result = result.Where(c => c.Date <= to && type == c.Type);

            } else if (from == null && to == null && type != null)
            {
                result = result.Where(c => type == c.Type);

            } else if(from != null && to != null && type == null) 
            {
                result = result.Where(c => from <= c.Date && c.Date <= to);
            }
            else if (from != null && to == null && type == null)
            {
                result = result.Where(c => from <=  c.Date);
            }
            else if (from != null && to == null && type != null)
            {
                result = result.Where(c => from <= c.Date && type == c.Type);
            }
            else if (from == null && to != null && type == null)
            {
                result = result.Where(c => c.Date <= to);
            }
             


            var resultList = await result.ToListAsync();
            return resultList;
           // return await _context.CostItems.ToListAsync();
        }

        // GET: api/CostItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CostItem>> GetCostItem(string id)
        {
            var costItem = await _context.CostItems.FindAsync(id);

            if (costItem == null)
            {
                return NotFound();
            }

            return costItem;
        }

        // PUT: api/CostItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostItem(string id, CostItem costItem)
        {
            if (id != costItem.Id)
            {
                return BadRequest();
            }

      

            _context.Entry(costItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostItemExists(id))
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

        // POST: api/CostItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CostItem>> PostCostItem(CostItem costItem)
        {
           

            _context.CostItems.Add(costItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CostItemExists(costItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCostItem", new { id = costItem.Id }, costItem);
        }

        // DELETE: api/CostItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CostItem>> DeleteCostItem(string id)
        {
            var costItem = await _context.CostItems.FindAsync(id);
            if (costItem == null)
            {
                return NotFound();
            }

            _context.CostItems.Remove(costItem);
            await _context.SaveChangesAsync();

            return costItem;
        }

        private bool CostItemExists(string id)
        {
            return _context.CostItems.Any(e => e.Id == id);
        }
    }
}

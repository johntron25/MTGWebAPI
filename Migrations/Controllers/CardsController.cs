using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data;
using MTGCardApi.Models;
using MTGCardApi.Exceptions;

namespace MTGCardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly MTGDbContext _context;

        public CardsController(MTGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cards.Any(c => c.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                var card = await _context.Cards.FindAsync(id);
                if (card == null)
                {
                    throw new CardNotFoundException("Card not found.");
                }

                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (CardNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong while deleting the card.");
            }
        }
    }
}

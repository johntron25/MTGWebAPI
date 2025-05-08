using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data;
using MTGCardApi.Models;
using MTGCardApi.Exceptions;

namespace MTGCardApi.Controllers
{
    [ApiController] // tells .NET this is an API controller
    [Route("api/[controller]")] // route will be api/cards
    public class CardsController : ControllerBase
    {
        private readonly MTGDbContext _context;

        // constructor injects the database context
        public CardsController(MTGDbContext context)
        {
            _context = context;
        }

        // GET: api/cards
        // gets a list of all cards from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/cards/5
        // gets a single card by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            // return 404 if not found
            return card == null ? NotFound() : card;
        }

        // POST: api/cards
        // adds a new card to the database
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            // returns 201 Created with location of new card
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // PUT: api/cards/5
        // updates a card with matching id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            // id mismatch check
            if (id != card.Id)
                return BadRequest();

            // tell EF that this entity is being modified
            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if card is gone, return 404
                if (!_context.Cards.Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/cards/5
        // deletes a card from the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                var card = await _context.Cards.FindAsync(id);

                // throw a custom exception if not found
                if (card == null)
                    throw new CardNotFoundException("Card not found.");

                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

                return NoContent(); // success
            }
            catch (CardNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}

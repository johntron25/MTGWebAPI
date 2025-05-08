using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data;
using MTGCardApi.Models;

namespace MTGCardApi.Repositories
{
    public class CardRepository
    {
        private readonly MTGDbContext _context;

        public CardRepository(MTGDbContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetAllAsync() => await _context.Cards.ToListAsync();
        public async Task<Card?> GetByIdAsync(int id) => await _context.Cards.FindAsync(id);
        public async Task AddAsync(Card card) { _context.Cards.Add(card); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Card card) { _context.Entry(card).State = EntityState.Modified; await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null) { _context.Cards.Remove(card); await _context.SaveChangesAsync(); }
        }
    }
}
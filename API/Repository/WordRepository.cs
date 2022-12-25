using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
    public class WordRepository : IWordRepository
    {
        private readonly ApplicationDbContext _db;

        public WordRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddWord(Word vocable)
        {
            _db.Word.Add(vocable);
            return Save();
        }

        public bool DeleteWord(Word vocable)
        {
            _db.Word.Remove(vocable);
            return Save();
        }

        public bool ExistWord(string vocable)
        {
            bool value = _db.Word.Any(c => c.Vocable.ToLower().Trim() == vocable.ToLower().Trim());
            return value;
        }

        public bool ExistWord(int id)
        {
            return _db.Word.Any(c => c.Id == id);
        }

        public Word GetWord(int wordId)
        {
            return _db.Word.FirstOrDefault(c => c.Id == wordId);
        }

        public ICollection<Word> GetWords()
        {
            return _db.Word.OrderBy(c => c.Vocable).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateWord(Word vocable)
        {
            _db.Word.Update(vocable);
            return Save();
        }
    }
}

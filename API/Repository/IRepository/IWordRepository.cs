using API.Models;

namespace API.Repository.IRepository
{
    public interface IWordRepository
    {
        ICollection<Word> GetWords();
        Word GetWord(int wordId);

        bool ExistWord(string word);
        bool ExistWord(int id);
        bool AddWord(Word word);
        bool UpdateWord(Word word);
        bool DeleteWord(Word word);
        bool Save();
    }
}

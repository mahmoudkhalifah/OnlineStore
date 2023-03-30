using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category GetDetails(int id);
        public int Insert(Category category);
        public int UpdateCategory(int id, Category category);
        public int DeleteCategory(int id);
    }
}

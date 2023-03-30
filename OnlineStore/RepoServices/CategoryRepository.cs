using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class CategoryRepository : ICategoryRepository
    {
        public MainDBContext Context { get; }

        //request service of type mainDbContext to be injected in Ctor
        public CategoryRepository(MainDBContext context)
        {
            Context = context;
        }
        public int DeleteCategory(int id)
        {
            
            try
            {
                Context.Remove(Context.Categories.Find(id));
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetDetails(int id)
        {
            return Context.Categories.Find(id);
        }

        public int Insert(Category category)
        {
            
            try
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateCategory(int id, Category category)
        {
            
            try
            {
                Category category1 = Context.Categories.Find(id);
                category1.CategoryName = category.CategoryName;
                category1.IsDeleted = category.IsDeleted;

                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

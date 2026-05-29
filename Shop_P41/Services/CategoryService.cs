namespace Shop_P41.Services
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();
        public Category CreateCategory(Category category);
        public Category GetCategoryById(int id);
        public Category Update(int id, Category category);
        public Category Delete(int id);

    }
    public class CategoryService : ICategoryService
    {
        public Category CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public string GetCategoryName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Category Update(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}

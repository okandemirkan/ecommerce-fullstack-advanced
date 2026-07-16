using Domain.Exceptions;

namespace Domain.Entities
{
    public class Category : WorkspaceEntity<int>
    {
        public string CategoryName { get; private set; } = string.Empty;

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
        private Category(){ }
        public static Category AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new DomainException("Category name cannot be empty");

            return new Category
            {
                CategoryName = categoryName
            };
        }

        public void UpdateCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new DomainException("Category name cannot be empty");
            CategoryName = categoryName;
        }


    }
}

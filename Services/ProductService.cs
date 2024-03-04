
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductService{

    private DataClient _connection;

    public ProductService(DataClient connection){
        _connection = connection;
    }

    public List<ProductModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>("SELECT * FROM [Production].[Product]", Map);
            return result;
        }catch(Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductByName(string name)
    {
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>("SELECT * FROM [Production].[Product] WHERE  "+
            $"Name = '{name}'",Map);
            return result;
        }catch(Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductsByCategoryType(string categoryType)
    {
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>(
                "SELECT * "+
                $"FROM [Production].[Product] P "+
                $"INNER JOIN [Production].[ProductSubcategory] S "+
                $"ON P.ProductSubcategoryID = S.ProductSubcategoryID "+
                $"INNER JOIN  [Production].[ProductCategory] C "+
                $"ON S.ProductCategoryID = C.ProductCategoryID "+
                $"WHERE C.Name = '{categoryType}'",Map);
                return result;
        }catch(Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public ProductModel Map(IDataRecord record){
        ProductModel product = new ProductModel();
            product.ProductId = (int)record["ProductID"];
            product.Name = record["Name"] as string;
            product.ProductNumber = record["ProductNumber"] as string;
            product.DiscontinuedDate = record["DiscontinuedDate"] == System.DBNull.Value?(DateTime?)null:(DateTime)record["DiscontinuedDate"];
            product.SellStartDate = record["SellStartDate"] == System.DBNull.Value?(DateTime?)null:(DateTime)record["SellStartDate"];
            product.SellEndDate = record["SellEndDate"] == System.DBNull.Value?(DateTime?)null:(DateTime)record["SellEndDate"];
            product.SubCategory = record["ProductSubcategoryID"] == System.DBNull.Value?(int?)null:(int)record["ProductSubcategoryID"];
            return product;
    }
}
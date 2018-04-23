using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

public class Product {
    public int ProductCode { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public string Photo { get; set; }
    public int ItemsCount { get; set; }
}

public static class ShoppingCartStorage {
    const string SessionKeyAllProducts = "ShoppingCartStorage.AllProducts";
    public static HttpSessionState Session { get { return HttpContext.Current.Session; } }

    public static List<Product> AllProducts {
        get {
            if (Session[SessionKeyAllProducts] == null)
                Session[SessionKeyAllProducts] = LoadProducts();
            return (List<Product>)Session[SessionKeyAllProducts];
        }
    }

    public static List<Product> ShoppingCartProducts {
        get {
            return AllProducts.FindAll(p => p.ItemsCount > 0);
        }
    }

    public static void AddToCart(int productCode) {
        Product product = AllProducts.Find(i => i.ProductCode == productCode);

        if (product != null)
            product.ItemsCount++;
    }

    public static void RemoveFromCart(int productCode) {
        Product product = AllProducts.Find(i => i.ProductCode == productCode);

        if (product != null)
            product.ItemsCount--;
    }

    private static List<Product> LoadProducts() {
        OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM Cameras");
        OleDbDataAdapter da = new OleDbDataAdapter(selectCommand);
        DataTable dt = new DataTable();

        selectCommand.Connection = connection;
        da.Fill(dt);

        List<Product> result = new List<Product>();

        for (int i = 0; i < dt.Rows.Count; i++) {
            result.Add(new Product() {
                ProductCode = (int)dt.Rows[i]["ID"],
                Model = (string)dt.Rows[i]["Model"],
                Photo = (string)dt.Rows[i]["ImageFileName"],
                Price = (decimal)dt.Rows[i]["Price"]
            });
        }

        return result;
    }
}
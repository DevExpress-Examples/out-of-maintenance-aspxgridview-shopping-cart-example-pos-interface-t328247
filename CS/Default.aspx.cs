using System;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Init(object sender, EventArgs e) {
        if (!IsPostBack)
            ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_DataBinding(object sender, EventArgs e) {
        ASPxGridView1.DataSource = ShoppingCartStorage.ShoppingCartProducts;
    }

    protected void ASPxDataView1_Init(object sender, EventArgs e) {
        ASPxDataView1.DataSource = ShoppingCartStorage.AllProducts;
        ASPxDataView1.DataBind();
    }

    protected void btnAddToCart_Init(object sender, EventArgs e) {
        ASPxButton btn = (ASPxButton)sender;
        DataViewItemTemplateContainer container = (DataViewItemTemplateContainer)btn.NamingContainer;

        btn.JSProperties["cpProductCode"] = DataBinder.Eval(container.DataItem, "ProductCode");
    }

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        int productCode = Convert.ToInt32(e.Parameters);

        ShoppingCartStorage.AddToCart(productCode);
        ASPxGridView1.DataBind();

        int addedRowIndex = ASPxGridView1.FindVisibleIndexByKeyValue(productCode);

        ASPxGridView1.ScrollToVisibleIndexOnClient = addedRowIndex;
        ASPxGridView1.FocusedRowIndex = addedRowIndex;
    }

    protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e) {
        ShoppingCartStorage.RemoveFromCart(Convert.ToInt32(e.Keys[0]));
        ASPxGridView1.CancelEdit();
        ASPxGridView1.DataBind(); 
        e.Cancel = true;
    }
}
Imports System
Imports System.Web.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Collections.Generic

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then
			ASPxGridView1.DataBind()
		End If
	End Sub

	Protected Sub ASPxGridView1_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
		ASPxGridView1.DataSource = ShoppingCartStorage.ShoppingCartProducts
	End Sub

	Protected Sub ASPxDataView1_Init(ByVal sender As Object, ByVal e As EventArgs)
		ASPxDataView1.DataSource = ShoppingCartStorage.AllProducts
		ASPxDataView1.DataBind()
	End Sub

	Protected Sub btnAddToCart_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim btn As ASPxButton = DirectCast(sender, ASPxButton)
		Dim container As DataViewItemTemplateContainer = CType(btn.NamingContainer, DataViewItemTemplateContainer)

		btn.JSProperties("cpProductCode") = DataBinder.Eval(container.DataItem, "ProductCode")
	End Sub

	Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim productCode As Integer = Convert.ToInt32(e.Parameters)

		ShoppingCartStorage.AddToCart(productCode)
		ASPxGridView1.DataBind()

		Dim addedRowIndex As Integer = ASPxGridView1.FindVisibleIndexByKeyValue(productCode)

		ASPxGridView1.ScrollToVisibleIndexOnClient = addedRowIndex
		ASPxGridView1.FocusedRowIndex = addedRowIndex
	End Sub

	Protected Sub ASPxGridView1_RowDeleting(ByVal sender As Object, ByVal e As ASPxDataDeletingEventArgs)
		ShoppingCartStorage.RemoveFromCart(Convert.ToInt32(e.Keys(0)))
		ASPxGridView1.CancelEdit()
		ASPxGridView1.DataBind()
		e.Cancel = True
	End Sub
End Class
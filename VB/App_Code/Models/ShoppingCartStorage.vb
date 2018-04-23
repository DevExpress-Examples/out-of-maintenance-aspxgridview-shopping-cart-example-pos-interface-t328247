Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Data
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.SessionState

Public Class Product
	Public Property ProductCode() As Integer
	Public Property Model() As String
	Public Property Price() As Decimal
	Public Property Photo() As String
	Public Property ItemsCount() As Integer
End Class

Public NotInheritable Class ShoppingCartStorage

	Private Sub New()
	End Sub

    Private Const SessionKeyAllProducts As String = "ShoppingCartStorage.AllProducts"

	Public Shared ReadOnly Property Session() As HttpSessionState
		Get
			Return HttpContext.Current.Session
		End Get
	End Property

	Public Shared ReadOnly Property AllProducts() As List(Of Product)
		Get
			If Session(SessionKeyAllProducts) Is Nothing Then
				Session(SessionKeyAllProducts) = LoadProducts()
			End If
			Return DirectCast(Session(SessionKeyAllProducts), List(Of Product))
		End Get
	End Property

	Public Shared ReadOnly Property ShoppingCartProducts() As List(Of Product)
		Get
			Return AllProducts.FindAll(Function(p) p.ItemsCount > 0)
		End Get
	End Property

	Public Shared Sub AddToCart(ByVal productCode As Integer)
		Dim product As Product = AllProducts.Find(Function(i) i.ProductCode = productCode)

		If product IsNot Nothing Then
			product.ItemsCount += 1
		End If
	End Sub

	Public Shared Sub RemoveFromCart(ByVal productCode As Integer)
		Dim product As Product = AllProducts.Find(Function(i) i.ProductCode = productCode)

		If product IsNot Nothing Then
			product.ItemsCount -= 1
		End If
	End Sub

	Private Shared Function LoadProducts() As List(Of Product)
		Dim connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
		Dim selectCommand As New OleDbCommand("SELECT * FROM Cameras")
		Dim da As New OleDbDataAdapter(selectCommand)
		Dim dt As New DataTable()

		selectCommand.Connection = connection
		da.Fill(dt)

		Dim result As New List(Of Product)()

		For i As Integer = 0 To dt.Rows.Count - 1
			result.Add(New Product() With {.ProductCode = CInt(Math.Truncate(dt.Rows(i)("ID"))), .Model = CStr(dt.Rows(i)("Model")), .Photo = CStr(dt.Rows(i)("ImageFileName")), .Price = CDec(dt.Rows(i)("Price"))})
		Next i

		Return result
	End Function
End Class
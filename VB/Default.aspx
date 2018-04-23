<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>GridShoppingCart</title>
</head>
<body>
	<form id="form1" runat="server">
		<table><tr><td style="vertical-align:top">

			<dx:ASPxDataView ID="ASPxDataView1" runat="server" AllowPaging="true" 
				AllButtonPageCount="0" Layout="Table" OnInit="ASPxDataView1_Init">
				<SettingsTableLayout ColumnCount="2" RowsPerPage="6" />
				<ItemStyle Width="300px" Height="50px">
					<Paddings PaddingTop="5px" PaddingBottom="5px" />
				</ItemStyle>
				<ItemTemplate>
					<table style="width: 100%">
						<tr>
							<td>
								<dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl='<%#"~\Images\" & Eval("Photo")%>' />
							</td>
							<td>
								<div style="width: 5px" />
							</td>
							<td>
								<dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("Model")%>' Font-Bold="True" />
								<br />
								<dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%#Eval("Price", "{0:c}")%>' />
							</td>
							<td style="text-align: right">
								<dx:ASPxButton ID="btnAddToCart" runat="server" Text="Add To Cart" AutoPostBack="false" OnInit="btnAddToCart_Init">
									<ClientSideEvents Click="function(s, e) { grid.PerformCallback(s.cpProductCode); }" />
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</ItemTemplate>
				<EmptyDataTemplate>
					<span>The datasource is empty.</span>
				</EmptyDataTemplate>
			</dx:ASPxDataView>

		</td><td><div style="width: 15px"></div></td>
		<td style="vertical-align:top">

			<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" KeyFieldName="ProductCode" AutoGenerateColumns="False" Width="600px"
				OnDataBinding="ASPxGridView1_DataBinding" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting">
				<Columns>
					<dx:GridViewCommandColumn ShowDeleteButton="true" ButtonType="Button" Width="100px" />
					<dx:GridViewDataImageColumn FieldName="Photo">
						<PropertiesImage ImageUrlFormatString="~/Images/{0}" ImageHeight="50px" />
					</dx:GridViewDataImageColumn>
					<dx:GridViewDataTextColumn FieldName="Model" />
					<dx:GridViewDataTextColumn FieldName="Price" PropertiesTextEdit-DisplayFormatString="c" />
					<dx:GridViewDataTextColumn FieldName="ItemsCount" Caption="Count">
						<CellStyle Font-Size="Large" />
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="Total" UnboundType="Decimal" UnboundExpression="[Price] * [ItemsCount]" Width="100px">
						<PropertiesTextEdit DisplayFormatString="c" />
					</dx:GridViewDataTextColumn>
				</Columns>
				<TotalSummary>
					<dx:ASPxSummaryItem FieldName="ItemsCount" SummaryType="Sum" DisplayFormat="Count={0}" />
					<dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
				</TotalSummary>
				<Settings ShowTitlePanel="true" ShowFooter="true" VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
				<SettingsText Title="Shopping Cart" EmptyDataRow="Add the required items to the cart" />
				<SettingsBehavior AllowFocusedRow="true" />
				<SettingsPager Mode="ShowAllRecords" />
			</dx:ASPxGridView>

		</td></tr></table>
	</form>
</body>
</html>
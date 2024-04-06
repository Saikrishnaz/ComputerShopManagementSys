Imports System.Data.SqlClient
Imports System.Web.UI.WebControls

Public Class BillingForm
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)





    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        If IsAllSellFieldsFilled() Then
            Txt_CusName.Enabled = False
            Txt_CusiD.Enabled = False
            AssignTotalPriceToSellTextbox()
            AssignTotalQuantityToSellTextbox()

            ' Check if Selltable is initialized
            If Selltable Is Nothing Then
                Selltable = CreateAddcolumnToSellDatagridview() ' Initialize Selltable
            End If

            Try
                ' Create a new row and populate it with TextBox values
                Dim row As DataRow = Selltable.NewRow()
                row("InvoiceNo") = Txt_InvoiceNo.Text

                Dim sellQuantity As Integer
                Dim itemPrice As Decimal
                If Txt_ItemName.SelectedItem IsNot Nothing Then
                    Dim SelectedItemName As String = Txt_ItemName.Text ' Use .Text property to get the selected item's text
                    row("ItemName") = SelectedItemName
                Else
                    MessageBox.Show("Please select a category.")
                End If

                row("ItemType") = Txt_ItemType.Text ' Assuming Txt_ItemType is a TextBox
                ' Validating and converting SellQuantity
                If Integer.TryParse(Txt_SellQuantity.Text, sellQuantity) Then
                    row("SellQuantity") = sellQuantity
                Else
                    MessageBox.Show("Please enter a valid Sell Quantity.")
                    Exit Sub
                End If

                ' Validating and converting ItemPrice
                If Decimal.TryParse(Txt_ItemPrice.Text, itemPrice) Then
                    row("ItemPrice") = itemPrice
                Else
                    MessageBox.Show("Please enter a valid Item Price.")
                    Exit Sub
                End If

                ' Validating and converting SubTotalQuantity
                'If Integer.TryParse(Txt_Sell.Text, subTotalQuantity) Then
                '("SubTotalQuantity") = subTotalQuantity
                'Else
                ' MessageBox.Show("Please enter a valid Sub Total Quantity.")
                ' Exit Sub
                ' End If

                row("SubTotalPrice") = Txt_SellSubTotalPrice.Text
                row("BillDate") = DTP_SellBillDate.Value.Date
                ' ... (rest of the previous code)

                Selltable.Rows.Add(row)
                MessageBox.Show("Data added successfully.")
                DataGridView1.DataSource = Nothing
                DataGridView1.DataSource = Selltable
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

            Dim controlsToClear() As Control = {Txt_ItemType, Txt_SellSubTotalPrice, Txt_SellQuantity, Txt_ItemPrice}
            For Each control As Control In controlsToClear
                control.Text = String.Empty
            Next
            Txt_ItemName.SelectedIndex = -1
        Else
            MessageBox.Show("Please fill in all TextBoxes.")
        End If
    End Sub

    ' ... (rest of your code remains unchanged)



    Private Function CreateAddcolumnToSellDatagridview() As DataTable
        Dim table As New DataTable("table")
        table.Columns.Add("InvoiceNo", Type.GetType("System.String"))
        table.Columns.Add("ItemName", Type.GetType("System.String"))
        table.Columns.Add("ItemType", Type.GetType("System.String"))
        table.Columns.Add("SellQuantity", Type.GetType("System.Int32"))
        table.Columns.Add("ItemPrice", Type.GetType("System.Decimal"))
        ' table.Columns.Add("SubTotalQuantity", Type.GetType("System.Int32"))
        table.Columns.Add("SubTotalPrice", Type.GetType("System.Decimal"))
        table.Columns.Add("BillDate", Type.GetType("System.DateTime"))

        Return table
    End Function

    ' Use the returned DataTable to bind data to the DataGridView

    Private Function IsAllSellFieldsFilled() As Boolean
        ' Check if all required fields are filled
        Return Not (
            String.IsNullOrWhiteSpace(Txt_InvoiceNo.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_ItemType.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_CusName.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_SellQuantity.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_CusiD.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_ItemPrice.Text) OrElse
            Txt_ItemName.SelectedIndex = -1)
        ' Optionally, you can remove additional conditions (CheckBoxInternetBanking, CheckBoxMobilrBanking, CheckBoxChequebook, CheckBoxemailAlerts, CheckBoxestatement)
    End Function

    Private Sub ClearAllSellControls()
        ' Clear all input controls
        Dim controlsToClear() As Control = {Txt_InvoiceNo, Txt_SellTotalQuantity, Txt_ItemType, Txt_SellSubTotalPrice, Txt_CusName, Txt_SellQuantity, Txt_CusiD, Txt_ItemPrice, Txt_InvoiceNo, Txt_SellTotalQuantity, Txt_SellTotalPrice}
        For Each control As Control In controlsToClear
            control.Text = String.Empty
        Next
        Txt_ItemName.SelectedIndex = -1

        DTP_SellBillDate.Value = Date.Today
        Txt_InvoiceNo.Text = GenerateUniqueInvoiceNo(con, "MainBillData")

    End Sub
    ' Use the returned DataTable to bind data to the DataGridView
    Private Sub BindDataToSellDataGridView()
        ' Create the DataTable using the method
        Dim data As DataTable = CreateAddcolumnToSellDatagridview()

        ' Assuming DataGridView4 is the name of your DataGridView control
        DataGridView1.DataSource = data
    End Sub
    Private Selltable As DataTable ' Declare globally

    Sub AssignTotalPriceToSellTextbox()
        If Txt_SellTotalPrice.Text = "" Then
            Txt_SellTotalPrice.Text = Txt_SellSubTotalPrice.Text
        Else
            ' Check if both TextBoxes have non-empty and numeric values
            If Not String.IsNullOrEmpty(Txt_SellSubTotalPrice.Text) AndAlso Not String.IsNullOrEmpty(Txt_SellTotalPrice.Text) Then
                Dim value1 As Decimal
                Dim value2 As Decimal

                ' Try to parse the text values to Decimal
                If Decimal.TryParse(Txt_SellSubTotalPrice.Text, value1) AndAlso Decimal.TryParse(Txt_SellTotalPrice.Text, value2) Then
                    ' Perform the multiplication and assign the result to TextBox42
                    Txt_SellTotalPrice.Text = (value1 + value2).ToString()
                Else
                    ' Handle cases where the entered values are not valid numbers
                    MessageBox.Show("Please enter valid numeric values.")
                End If
            Else
                ' Handle cases where one or both TextBoxes are empty
                'MessageBox.Show("Both TextBoxes must have non-empty values.")
            End If
        End If
    End Sub
    Sub AssignTotalQuantityToSellTextbox()

        If Not String.IsNullOrEmpty(Txt_SellQuantity.Text) Then
            If String.IsNullOrEmpty(Txt_SellTotalQuantity.Text) Then
                Txt_SellTotalQuantity.Text = Txt_SellQuantity.Text
            Else
                ' Use Integer.TryParse to safely convert string to integer
                Dim currentQuantity As Integer = 0
                If Integer.TryParse(Txt_SellQuantity.Text, currentQuantity) Then
                    Dim totalQuantity As Integer = 0
                    If Integer.TryParse(Txt_SellTotalQuantity.Text, totalQuantity) Then
                        ' Accumulate the total quantity
                        Txt_SellTotalQuantity.Text = (totalQuantity + currentQuantity).ToString()
                    Else
                        ' Handle conversion failure for total quantity
                        MessageBox.Show("Invalid value for total quantity.")
                    End If
                Else
                    ' Handle conversion failure for current quantity
                    MessageBox.Show("Invalid value for current quantity.")
                End If
            End If
        End If

    End Sub

    Private Sub BillingForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Txt_SellTotalQuantity.Clear()
        Selltable = CreateAddcolumnToSellDatagridview()
        DataGridView1.DataSource = Selltable
        Txt_InvoiceNo.Text = GenerateUniqueInvoiceNo(con, "MainBillData")
        FillcomboBox(con, Txt_ItemName, "ItemData", "ItemName")
        AutoCompleteSearchBoxForTextBoxesTypeString(con, Txt_CusName, "Name", "CustomerData", 0)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Txt_CusName.TextChanged
        Try
            con.Open()
            Dim Query As String = "SELECT * FROM CustomerData WHERE Name = @SelectedValue"
            Dim cmd As New SqlCommand(Query, con)
            cmd.Parameters.AddWithValue("@SelectedValue", Txt_CusName.Text)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Txt_CusiD.Text = reader(0).ToString()
            Else
                ' Handle case where no data is found
            End If

            reader.Close()
        Catch ex As Exception
            ' Handle exceptions
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Txt_SellCategory_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Txt_ItemName.SelectionChangeCommitted
        Try
            con.Open()
            Dim Query As String = "SELECT * FROM ItemData WHERE ItemName = @SelectedValue"
            Dim cmd As New SqlCommand(Query, con)
            cmd.Parameters.AddWithValue("@SelectedValue", Txt_ItemName.SelectedValue.ToString())

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Txt_ItemType.Text = reader(2).ToString()
                Txt_ItemPrice.Text = reader(3).ToString()
            Else
                ' Handle case where no data is found
            End If

            reader.Close()
        Catch ex As Exception
            ' Handle exceptions
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

    Private Sub Txt_ItemPrice_TextChanged(sender As Object, e As EventArgs) Handles Txt_ItemPrice.TextChanged
        ' Check if both TextBoxes have non-empty and numeric values
        If Not String.IsNullOrEmpty(Txt_ItemPrice.Text) AndAlso Not String.IsNullOrEmpty(Txt_SellQuantity.Text) Then
            Dim value1 As Decimal
            Dim value2 As Decimal

            ' Try to parse the text values to Decimal
            If Decimal.TryParse(Txt_ItemPrice.Text, value1) AndAlso Decimal.TryParse(Txt_SellQuantity.Text, value2) Then
                ' Perform the multiplication and assign the result to TextBox42
                Txt_SellSubTotalPrice.Text = (value1 * value2).ToString()
            Else
                ' Handle cases where the entered values are not valid numbers
                MessageBox.Show("Please enter valid numeric values.")
            End If
        Else
            ' Handle cases where one or both TextBoxes are empty
            'clearox.Show("Both TextBoxes must have non-empty values.")
        End If
    End Sub

    Private Sub Txt_SellQuantity_TextChanged(sender As Object, e As EventArgs) Handles Txt_SellQuantity.TextChanged
        ' Check if both TextBoxes have non-empty and numeric values
        If Not String.IsNullOrEmpty(Txt_SellQuantity.Text) AndAlso Not String.IsNullOrEmpty(Txt_SellQuantity.Text) Then
            Dim value1 As Decimal
            Dim value2 As Decimal

            ' Try to parse the text values to Decimal
            If Decimal.TryParse(Txt_ItemPrice.Text, value1) AndAlso Decimal.TryParse(Txt_SellQuantity.Text, value2) Then
                ' Perform the multiplication and assign the result to TextBox42
                Txt_SellSubTotalPrice.Text = (value1 * value2).ToString()
            Else
                ' Handle cases where the entered values are not valid numbers
                MessageBox.Show("Please enter valid numeric values.")
            End If
        Else
            ' Handle cases where one or both TextBoxes are empty
            'clearox.Show("Both TextBoxes must have non-empty values.")
        End If
    End Sub

    Private Sub Guna2GradientButton4_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton4.Click
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            If DataGridView1 IsNot Nothing AndAlso DataGridView1.Rows.Count > 0 Then
                For Each row As DataGridViewRow In DataGridView1.Rows
                    Using cmd As New SqlCommand("INSERT INTO SubBillData (InvoiceNo, ItemName, ItemType, SellQuantity, ItemPrice, SubTotalPrice, BillDate) " &
                                        "VALUES (@InvoiceNo, @ItemName, @ItemType, @SellQuantity, @ItemPrice, @SubTotalPrice, @BillDate)", con)
                        cmd.Parameters.AddWithValue("@InvoiceNo", If(row.Cells("InvoiceNo").Value IsNot Nothing, row.Cells("InvoiceNo").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@ItemName", If(row.Cells("ItemName").Value IsNot Nothing, row.Cells("ItemName").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@ItemType", If(row.Cells("ItemType").Value IsNot Nothing, row.Cells("ItemType").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@SellQuantity", If(row.Cells("SellQuantity").Value IsNot Nothing, row.Cells("SellQuantity").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@ItemPrice", If(row.Cells("ItemPrice").Value IsNot Nothing, row.Cells("ItemPrice").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@SubTotalPrice", If(row.Cells("SubTotalPrice").Value IsNot Nothing, row.Cells("SubTotalPrice").Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@BillDate", If(row.Cells("BillDate").Value IsNot Nothing, row.Cells("BillDate").Value, DBNull.Value))

                        cmd.ExecuteNonQuery()
                    End Using
                Next


                ' Insert into MainBillData
                Dim query As String = "INSERT INTO MainBillData (InvoiceNo, CustomerName, CustomerID, TotalQuantity, TotalPrice, BillDate) " &
                        "VALUES (@InvoiceNo, @CustomerName, @CustomerID, @TotalQuantity,@TotalPrice, @BillDate)"
                Using Billcmd As New SqlCommand(query, con)
                    Billcmd.Parameters.AddWithValue("@InvoiceNo", If(Not String.IsNullOrEmpty(Txt_InvoiceNo.Text), Txt_InvoiceNo.Text, DBNull.Value))
                    Billcmd.Parameters.AddWithValue("@CustomerName", If(Not String.IsNullOrEmpty(Txt_CusName.Text), Txt_CusName.Text, DBNull.Value))
                    Billcmd.Parameters.AddWithValue("@CustomerID", If(Not String.IsNullOrEmpty(Txt_CusiD.Text), Txt_CusiD.Text, DBNull.Value))
                    Billcmd.Parameters.AddWithValue("@TotalQuantity", If(Not String.IsNullOrEmpty(Txt_SellTotalQuantity.Text), Txt_SellTotalQuantity.Text, DBNull.Value))
                    Billcmd.Parameters.AddWithValue("@TotalPrice", If(Not String.IsNullOrEmpty(Txt_SellTotalPrice.Text), Txt_SellTotalPrice.Text, DBNull.Value))
                    Billcmd.Parameters.AddWithValue("@BillDate", DTP_SellBillDate.Value.Date)

                    Billcmd.ExecuteNonQuery()
                End Using

                MsgBox("Sale data for Invoice No: " & Txt_InvoiceNo.Text & " has been updated successfully.")
                ClearAllSellControls()
                Txt_SellTotalQuantity.Clear()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        DataGridView1.Columns.Clear()
        Txt_CusName.Enabled = True
        Txt_CusiD.Enabled = True
        Txt_InvoiceNo.Text = GenerateUniqueInvoiceNo(con, "MainBillData")
    End Sub


    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        ClearAllSellControls()
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        BillingReport.Show()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()
    End Sub
End Class
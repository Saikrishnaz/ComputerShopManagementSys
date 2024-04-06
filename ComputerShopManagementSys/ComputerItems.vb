Imports System.Data.SqlClient
Public Class ComputerItems
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Function IsAllPassengerFieldsFilled() As Boolean
        ' Check if all required fields are filled
        Return Not (
            String.IsNullOrWhiteSpace(Txt_itemDesc.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_ItemPrice.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_ItemName.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_itemDesc.Text) OrElse
            Txt_ItemType.SelectedIndex = -1)
        ' Optionally, you can remove additional conditions (CheckBoxInternetBanking, CheckBoxMobilrBanking, CheckBoxChequebook, CheckBoxemailAlerts, CheckBoxestatement)
    End Function
    Private Sub ClearAllPassengerBusControls()
        ' Clear all input controls
        Dim controlsToClear() As Control = {Txt_ItemPrice, Txt_ItemName, Txt_itemDesc}
        For Each control As Control In controlsToClear
            control.Text = String.Empty
        Next
        Txt_ItemType.SelectedIndex = -1
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        If Not IsAllPassengerFieldsFilled() Then
            MsgBox("Please fill in all the Item details.")
            Exit Sub
        End If
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        If CHECKeMAIL(con, Txt_ItemName.Text, "ItemData", "ItemName") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("This Item Already Exist..")
            Exit Sub
        End If
        Try
            con.Open()
            Dim query As String = "INSERT INTO ItemData (ItemName, ItemType, ItemPrice,ItemDescription) " &
                        "VALUES (@ItemName, @ItemType, @ItemPrice,@ItemDescription)"
            Using cmd As New SqlCommand(query, con)
                ' Set parameter values...'
                cmd.Parameters.AddWithValue("@ItemName", If(Not String.IsNullOrEmpty(Txt_ItemName.Text), Txt_ItemName.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@ItemType", If(Txt_ItemType.SelectedIndex <> -1, Txt_ItemType.SelectedItem.ToString(), DBNull.Value))
                cmd.Parameters.AddWithValue("@ItemPrice", If(Not String.IsNullOrEmpty(Txt_ItemPrice.Text), Txt_ItemPrice.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("ItemDescription", If(Not String.IsNullOrEmpty(Txt_itemDesc.Text), Txt_itemDesc.Text, DBNull.Value))

                ' Execute the query...'
                cmd.ExecuteNonQuery()
                MsgBox("New Item Data has been scusses fully Registered.")
                ClearAllPassengerBusControls()
            End Using
        Catch ex As Exception
            ' Display specific error message...
            MsgBox("Error: " & ex.Message)
        End Try
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        ' Finally, repopulate the DataGridView and generate a unique invoice number
        Populatedvg(con, "ItemData", DataGridView1)
        FillcomboBox(con, BillingForm.Txt_ItemName, "ItemData", "ItemName")

    End Sub

    Private Sub ComputerItems_Load(sender As Object, e As EventArgs) Handles Me.Load
        Populatedvg(con, "ItemData", DataGridView1)
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        ClearAllPassengerBusControls()
        DataGridView1.ForeColor = Color.Black
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ItemReport.Show()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub
End Class
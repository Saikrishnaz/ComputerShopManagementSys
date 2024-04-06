Imports System.Data.SqlClient
Public Class BillingReport
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Sub BusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Populatedvg(con, "MainBillData", DataGridView1)
        selectedSearchBoxMaterial()
    End Sub


    Sub selectedSearchBoxMaterial()
        If RadioButton4.Checked Then
            AutoCompleteSearchBoxForTextBoxesTypeString(con, Txt_InvoiceNo, "InvoiceNo", "SubBillData", 0)
        ElseIf RadioButton7.Checked Then
            AutoCompleteSearchBoxForTextBoxesTypeString(con, Txt_InvoiceNo, "InvoiceNo", "MainBillData", 0)
        End If
    End Sub
    Private Sub Txt_BusName_TextChanged(sender As Object, e As EventArgs) Handles Txt_InvoiceNo.TextChanged
        If Txt_InvoiceNo.Text = "" Then
            If RadioButton4.Checked Then
                Populatedvg(con, "SubBillData", DataGridView1)
            ElseIf RadioButton7.Checked Then
                Populatedvg(con, "MainBillData", DataGridView1)
            End If
        Else
            DataGridView1.Columns.Clear()

            Try
                con.Open()

                ' Use parameterized query to avoid SQL injection
                Dim sql As String = ""

                If RadioButton4.Checked Then
                    sql = "SELECT * FROM SubBillData WHERE InvoiceNo = @Name"
                ElseIf RadioButton7.Checked Then
                    sql = "SELECT * FROM MainBillData WHERE InvoiceNo = @Name"
                End If
                Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, con)
                adapter.SelectCommand.Parameters.AddWithValue("@Name", Txt_InvoiceNo.Text)
                Dim ds As DataSet = New DataSet()
                If RadioButton4.Checked Then
                    adapter.Fill(ds, "SubBillData")
                    ' Assign the DataTable to the DataGridView's DataSource
                    DataGridView1.DataSource = ds.Tables("SubBillData")
                ElseIf RadioButton7.Checked Then
                    adapter.Fill(ds, "MainBillData")
                    ' Assign the DataTable to the DataGridView's DataSource
                    DataGridView1.DataSource = ds.Tables("MainBillData")
                End If
                con.Close()

            Catch ex As Exception
                ' Handle exceptions appropriately
                MessageBox.Show("Error: " & ex.Message)
                con.Close()
            End Try
            con.Close()
        End If
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If Not Txt_InvoiceNo.Text = "" Then
            Try
                con.Open()
                Dim query As String = "DELETE FROM CustomerData WHERE Name = @Name"
                Using cmd As New SqlCommand(query, con)
                    ' Set parameter values...'
                    cmd.Parameters.AddWithValue("@Name", If(Not String.IsNullOrEmpty(Txt_InvoiceNo.Text), Txt_InvoiceNo.Text, DBNull.Value))

                    ' Execute the query...'
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MsgBox("Customer Data has been successfully deleted.")
                        Txt_InvoiceNo.Clear()
                    Else
                        MsgBox("No matching record found for the provided Customer Name.")
                    End If
                End Using
                con.Close()

            Catch ex As Exception
                ' Display specific error message...
                MsgBox("Error: " & ex.Message)
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        End If
        Populatedvg(con, "CustomerData", DataGridView1)
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        Populatedvg(con, "MainBillData", DataGridView1)
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Populatedvg(con, "SubBillData", DataGridView1)
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        DataGridView1.Columns.Clear()
        con.Open()

        Dim sql As String = ""
        If RadioButton4.Checked = True Then
            sql = "SELECT * FROM SubBillData WHERE InvoiceNo = @InvoiceNo AND BillDate BETWEEN @StartDate AND @EndDate "
        ElseIf RadioButton7.Checked = True Then
            sql = "SELECT *  FROM MainBillData WHERE InvoiceNo = @InvoiceNo AND BillDate BETWEEN @StartDate AND @EndDate "
        End If
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        adapter.SelectCommand.Parameters.AddWithValue("@InvoiceNo", Txt_InvoiceNo.Text)
        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", DTPTechStudStrtDate.Value.Date)
        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", DTPTeachStudEndDate.Value.Date)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        con.Close()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub
End Class
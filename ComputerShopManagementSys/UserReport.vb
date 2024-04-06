Imports System.Data.SqlClient
Public Class UserReport
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Sub UserReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Populatedvg(con, "UserData", DataGridView1)
        AutoCompleteSearchBoxForTextBoxesTypeString(con, Txt_BusName, "UserName", "UserData", 0)
    End Sub

    Private Sub Txt_BusName_TextChanged(sender As Object, e As EventArgs) Handles Txt_BusName.TextChanged
        If Txt_BusName.Text = "" Then
            Populatedvg(con, "UserData", DataGridView1)
        Else
            DataGridView1.Columns.Clear()

            Try
                con.Open()

                ' Use parameterized query to avoid SQL injection
                Dim sql As String = "SELECT * FROM UserData WHERE UserName = @Name"
                Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, con)
                adapter.SelectCommand.Parameters.AddWithValue("@Name", Txt_BusName.Text)

                Dim ds As DataSet = New DataSet()
                adapter.Fill(ds, "UserData")

                ' Assign the DataTable to the DataGridView's DataSource
                DataGridView1.DataSource = ds.Tables("UserData")

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
        If Not Txt_BusName.Text = "" Then
            Try
                con.Open()
                Dim query As String = "DELETE FROM UserData WHERE UserName = @Name"
                Using cmd As New SqlCommand(query, con)
                    ' Set parameter values...'
                    cmd.Parameters.AddWithValue("@Name", If(Not String.IsNullOrEmpty(Txt_BusName.Text), Txt_BusName.Text, DBNull.Value))

                    ' Execute the query...'
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MsgBox("User Data has been successfully deleted.")
                        Txt_BusName.Clear()
                    Else
                        MsgBox("No matching record found for the provided User Name.")
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
        Populatedvg(con, "UserData", DataGridView1)
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub
End Class
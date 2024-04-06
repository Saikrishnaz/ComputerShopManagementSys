Imports System.Data.SqlClient

Public Class LoginForm
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Guna2TabControl1.TabMenuVisible = False
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        If Txt_UserName.Text = "" And Txt_Password.Text = "" Then
            MsgBox("Please enter your UserName And Password to login.")
            Exit Sub
        End If

        If CHECKeMAIL(con, Txt_UserName.Text, "UserData", "UserName") = False Then
            MsgBox("UserName Does not Exist. Please check your Mail.")
            Exit Sub
        End If

        Dim enteredPassword As String = Txt_UserName.Text.Trim()

        If Not String.IsNullOrEmpty(Txt_UserName.Text) Then
            If Not String.IsNullOrEmpty(Txt_UserName.Text) Then
                Dim storedPassword As String = ""
                ' Assuming con is a valid SqlConnection object
                Try
                    con.Open()
                    ' Check if the staff ID exists in the database
                    Dim query As String = "SELECT UserName FROM UserData WHERE UserName = @UserName AND Password = @Password"
                    Using cmd As New SqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@UserName", Txt_UserName.Text)
                        cmd.Parameters.AddWithValue("@Password", Txt_Password.Text)
                        ' Execute the query to retrieve the stored password
                        Dim result As Object = cmd.ExecuteScalar()
                        If result IsNot Nothing Then
                            storedPassword = result.ToString()
                        End If
                    End Using
                Catch ex As Exception
                    ' Handle the exception (e.g., display an error message)
                    MessageBox.Show("An error occurred: " & ex.Message)
                Finally
                    con.Close()
                End Try

                ' Check if the entered password matches the stored password
                If String.Equals(enteredPassword, storedPassword, StringComparison.OrdinalIgnoreCase) Then
                    HomePage.Show()
                Else
                    MsgBox("You've Entered Wrong Credential....")
                End If
            End If
        End If
        Txt_UserName.Clear()
        Txt_Password.Clear()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Txt_Password.PasswordChar = ControlChars.NullChar ' Display actual characters
        Else
            Txt_Password.PasswordChar = "*" ' Display asterisks
        End If
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        Guna2TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            TextBox1.ReadOnly = False
            TextBox4.ReadOnly = False
            TextBox3.Clear()
            TextBox4.Clear()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            TextBox3.ReadOnly = False
            TextBox4.ReadOnly = False
            TextBox1.Clear()
            TextBox4.Clear()
        End If
    End Sub


    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If RadioButton1.Checked Then
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            If CHECKeMAIL(con, TextBox1.Text, "UserData", "UserName") = True Then
                MsgBox("User with the same UserName ID already exists.")
                Exit Sub
            End If

            Try
                con.Open()
                Dim query As String = "UPDATE UserData SET UserName = @UserName " &
                            "WHERE AddharNo = @AddharNo"
                Using cmd As New SqlCommand(query, con)
                    ' Set parameter values...'
                    cmd.Parameters.AddWithValue("@AddharNo", If(Not String.IsNullOrEmpty(TextBox4.Text), TextBox4.Text, DBNull.Value))
                    cmd.Parameters.AddWithValue("@UserName", If(Not String.IsNullOrEmpty(TextBox1.Text), TextBox1.Text, DBNull.Value))

                    ' Execute the query...'
                    cmd.ExecuteNonQuery()
                    MsgBox("UserName has been successfully changed.")

                    TextBox1.Clear()
                    TextBox4.Clear()
                End Using
            Catch ex As Exception
                ' Display specific error message...
                MsgBox("Error: " & ex.Message)
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        ElseIf RadioButton2.Checked Then
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            If CHECKeMAIL(con, TextBox3.Text, "UserData", "Password") = True Then
                MsgBox("User with the same Password ID already exists.")
                Exit Sub
            End If

            Try
                    con.Open()
                Dim query As String = "UPDATE UserData SET Password = @Password " &
                                "WHERE AddharNo = @AddharNo"
                Using cmd As New SqlCommand(query, con)
                        ' Set parameter values...'
                        cmd.Parameters.AddWithValue("@AddharNo", If(Not String.IsNullOrEmpty(TextBox4.Text), TextBox4.Text, DBNull.Value))
                    cmd.Parameters.AddWithValue("@Password", If(Not String.IsNullOrEmpty(TextBox3.Text), TextBox3.Text, DBNull.Value))

                    ' Execute the query...'
                    cmd.ExecuteNonQuery()
                    MsgBox("Password has been successfully changed.")

                    TextBox3.Clear()
                    TextBox4.Clear()
                    End Using
                Catch ex As Exception
                    ' Display specific error message...
                    MsgBox("Error: " & ex.Message)
                Finally
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                End Try
            Else
            MsgBox("Please select the Crediential Type to Reset ...!! ")
        End If
    End Sub


    Private Sub Guna2GradientButton5_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton5.Click
        Guna2TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub

    Private Sub Guna2ControlBox2_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox2.Click
        Me.Close()

    End Sub
End Class
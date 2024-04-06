Imports System.Data.SqlClient
Public Class NewUser
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Function IsAllPassengerFieldsFilled() As Boolean
        ' Check if all required fields are filled
        Return Not (
            String.IsNullOrWhiteSpace(Txt_AddharNo.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_Pass.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_Email.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_Mobile.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_UserName.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_Age.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_AddharNo.Text))
        ' Optionally, you can remove additional conditions (CheckBoxInternetBanking, CheckBoxMobilrBanking, CheckBoxChequebook, CheckBoxemailAlerts, CheckBoxestatement)
    End Function
    Private Sub ClearAllPassengerBusControls()
        ' Clear all input controls
        Dim controlsToClear() As Control = {Txt_Pass, Txt_Email, Txt_Mobile, Txt_UserName, Txt_AddharNo, Txt_Age}
        For Each control As Control In controlsToClear
            control.Text = String.Empty
        Next
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        If Not IsAllPassengerFieldsFilled() Then
            MsgBox("Please fill in all the User details.")
            Exit Sub
        End If
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        If CHECKeMAIL(con, Txt_Mobile.Text, "UserData", "MobileNo") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("User with same Mobile Number Already Exist..")
            Exit Sub
        End If
        If CHECKeMAIL(con, Txt_AddharNo.Text, "UserData", "AddharNo") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("User with same Addhar Number Already Exist..")
            Exit Sub
        End If
        If CHECKeMAIL(con, Txt_UserName.Text, "UserData", "UserName") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("User with same UserName Already Exist..")
            Exit Sub
        End If
        If CHECKeMAIL(con, Txt_Email.Text, "UserData", "EmailID") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("User with same Email ID Already Exist..")
            Exit Sub
        End If
        Try
            con.Open()
            Dim query As String = "INSERT INTO UserData (UserName, Password, Age,EmailID,MobileNo,AddharNo) " &
                        "VALUES (@UserName, @Password, @Age,@EmailID,@MobileNo,@AddharNo)"
            Using cmd As New SqlCommand(query, con)
                ' Set parameter values...'
                cmd.Parameters.AddWithValue("@UserName", If(Not String.IsNullOrEmpty(Txt_UserName.Text), Txt_UserName.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@Password", If(Not String.IsNullOrEmpty(Txt_Pass.Text), Txt_Pass.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@Age", If(Not String.IsNullOrEmpty(Txt_Age.Text), Txt_Age.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@EmailID", If(Not String.IsNullOrEmpty(Txt_Email.Text), Txt_Email.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@MobileNo", If(Not String.IsNullOrEmpty(Txt_Mobile.Text), Txt_Mobile.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@AddharNo", If(Not String.IsNullOrEmpty(Txt_AddharNo.Text), Txt_AddharNo.Text, DBNull.Value))

                ' Execute the query...'
                cmd.ExecuteNonQuery()
                MsgBox("New User Data has been scusses fully Registered.")
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
        Populatedvg(con, "UserData", DataGridView1)
        Populatedvg(con, "UserData", UserReport.DataGridView1)
    End Sub

    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles Me.Load
        Populatedvg(con, "UserData", DataGridView1)
        DataGridView1.ForeColor = Color.Black
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        ClearAllPassengerBusControls()
        DataGridView1.ForeColor = Color.Black
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        UserReport.Show()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub
End Class
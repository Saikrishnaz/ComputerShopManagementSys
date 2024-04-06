Imports System.Data.SqlClient

Public Class CustomerRegisteration
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ComputerShopManagementSys\ComputerShopManagementSys\CompDataBase.mdf;Integrated Security=True"
    Dim con As New SqlConnection(connectionString)
    Private Function IsAllPassengerFieldsFilled() As Boolean
        ' Check if all required fields are filled
        Return Not (
            String.IsNullOrWhiteSpace(Txt_PsgnPinCode.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_PsgnAge.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_PsgnEmail.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_PsgnMobile.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_PsgnName.Text) OrElse
            String.IsNullOrWhiteSpace(Txt_PsgnPinCode.Text) OrElse
            Txt_PsgnGender.SelectedIndex = -1)
        ' Optionally, you can remove additional conditions (CheckBoxInternetBanking, CheckBoxMobilrBanking, CheckBoxChequebook, CheckBoxemailAlerts, CheckBoxestatement)
    End Function
    Private Sub ClearAllPassengerBusControls()
        ' Clear all input controls
        Dim controlsToClear() As Control = {Txt_PsgnAge, Txt_PsgnEmail, Txt_PsgnMobile, Txt_PsgnName, Txt_PsgnPinCode}
        For Each control As Control In controlsToClear
            control.Text = String.Empty
        Next
        Txt_PsgnGender.SelectedIndex = -1
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        If Not IsAllPassengerFieldsFilled() Then
            MsgBox("Please fill in all the Customer details.")
            Exit Sub
        End If
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        If CHECKeMAIL(con, Txt_PsgnMobile.Text, "CustomerData", "MobileNo") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("Customer with same Mobile Number Already Exist..")
            Exit Sub
        End If
        If CHECKeMAIL(con, Txt_PsgnEmail.Text, "CustomerData", "EmailID") = False Then
            'MsgBox("Email does not exist. You can proceed.")
        Else
            MsgBox("Customer with same Email ID Already Exist..")
            Exit Sub
        End If
        Try
            con.Open()
            Dim query As String = "INSERT INTO CustomerData (Name, Gender, Age,EmailID,MobileNo,Address) " &
                        "VALUES (@Name, @Gender, @Age,@EmailID,@MobileNo,@Address)"
            Using cmd As New SqlCommand(query, con)
                ' Set parameter values...'
                cmd.Parameters.AddWithValue("@Name", If(Not String.IsNullOrEmpty(Txt_PsgnName.Text), Txt_PsgnName.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@Gender", If(Txt_PsgnGender.SelectedIndex <> -1, Txt_PsgnGender.SelectedItem.ToString(), DBNull.Value))
                cmd.Parameters.AddWithValue("@Age", If(Not String.IsNullOrEmpty(Txt_PsgnAge.Text), Txt_PsgnAge.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@EmailID", If(Not String.IsNullOrEmpty(Txt_PsgnEmail.Text), Txt_PsgnEmail.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@MobileNo", If(Not String.IsNullOrEmpty(Txt_PsgnMobile.Text), Txt_PsgnMobile.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@Address", If(Not String.IsNullOrEmpty(Txt_PsgnPinCode.Text), Txt_PsgnPinCode.Text, DBNull.Value))

                ' Execute the query...'
                cmd.ExecuteNonQuery()
                MsgBox("New Customer Data has been scusses fully Registered.")
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
        Populatedvg(con, "CustomerData", DataGridView1)
        AutoCompleteSearchBoxForTextBoxesTypeString(con, BillingForm.Txt_CusName, "Name", "CustomerData", 0)
    End Sub

    Private Sub CustomerRegisteration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Populatedvg(con, "CustomerData", DataGridView1)
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        ClearAllPassengerBusControls()
        DataGridView1.ForeColor = Color.Black
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        CustomerReport.Show()
    End Sub

    Private Sub Guna2GradientButton4_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton4.Click
        NewUser.Show()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.Close()

    End Sub
End Class
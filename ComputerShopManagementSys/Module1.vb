Imports System.Data.SqlClient

Module Module1
    Public Function CHECKeMAIL(ByVal con As SqlConnection, email As String, table As String, Clm As String) As Boolean
        con.Open()

        ' Check if the generated StaffCode already exists in the table
        Dim query As String = "SELECT COUNT(*) FROM " & table & " WHERE " & Clm & " = @Email"
        Dim cmd As New SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@Email", email)
        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        con.Close()

        If count = 0 Then
            Return False ' 0 or False for email not found
        Else
            Return True ' 1 or True for email found
        End If
    End Function

    Public Sub Populatedvg(con As SqlConnection, tablename As String, datagrid As DataGridView)
        ' Function to generate a Temporary Table from the Database into a DataGridView
        datagrid.Columns.Clear()
        con.Open()
        Dim sql = "SELECT * FROM " & tablename ' Remove the single quotes
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter) ' This line should work correctly
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        datagrid.DataSource = ds.Tables(0)
        con.Close()
    End Sub
    Sub AutoCompleteSearchBoxForTextBoxesTypeString(con As SqlConnection, textbox1 As TextBox, ColumnName As String, TableName As String, columnNameIndex As Integer)
        con.Open()
        Dim Query As String = "SELECT " & ColumnName & " FROM " & TableName
        Dim Cmd As New SqlCommand(Query, con)
        Dim reader As SqlDataReader
        reader = Cmd.ExecuteReader
        Dim ElementsToSuggest As AutoCompleteStringCollection = New AutoCompleteStringCollection()

        While reader.Read
            ElementsToSuggest.Add(reader.GetString(columnNameIndex)) ' Use GetString to retrieve the column's value as a string

        End While
        textbox1.AutoCompleteCustomSource = ElementsToSuggest
        con.Close()
    End Sub

    Public Function GenerateUniqueInvoiceNo(ByVal con As SqlConnection, ByVal tblName As String) As String
        con.Open()

        Dim InvoiceNo As String = ""
        Dim isUnique As Boolean = False

        ' Loop until a unique account number is generated
        While Not isUnique
            ' Generate a random 10-digit account number
            Dim rnd As New Random()
            Dim tempInvoiceNo As String = GenerateRandomInvoiceNo()

            ' Check if the generated account number already exists in the table
            Dim query As String = "SELECT COUNT(*) FROM " & tblName & " WHERE InvoiceNo = @InvoiceNo"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If count = 0 Then
                ' If the InvoiceNo is unique, assign it and break the loop
                InvoiceNo = tempInvoiceNo
                isUnique = True
            End If
        End While

        con.Close()
        Return InvoiceNo
    End Function

    Public Function GenerateRandomInvoiceNo() As String
        ' Generate a random 10-digit account number
        Dim rnd As New Random()
        Dim InvoiceNo As String = rnd.Next(1000000, 999999999).ToString()
        Return InvoiceNo
    End Function


    Sub FillcomboBox(con As SqlConnection, cmbx As ComboBox, tblName As String, ColumnName As String)
        ' Open the connection
        con.Open()

        ' Create a SQL command to select all data from the specified table
        Dim cmd As New SqlCommand("SELECT * FROM " & tblName, con)

        ' Create a data adapter and a DataTable
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable

        ' Fill the DataTable with data from the database
        adapter.Fill(Tbl)

        ' Set the ComboBox's data source and member bindings
        cmbx.DataSource = Tbl
        cmbx.DisplayMember = ColumnName
        cmbx.ValueMember = ColumnName

        ' Close the connection
        con.Close()
    End Sub

End Module

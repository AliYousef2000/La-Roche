Imports System.Data.SqlClient
Public Class cexist

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox6.Text = ""
        TextBox8.Text = ""
        Dim con As New SqlConnection(Module1.str)
        Dim com As New SqlCommand(" select c_fname[First Name],c_lastname[Last Name],fathername[Father Name],mothername[Mother Name],address[Address],c_phone[Phone],nationality[Nationality],passport_no[Passport],FORMAT(Dob,'dd/MM/yyyy') as [Date of birth]  from client", con)
        Dim da As New SqlDataAdapter(com)
        Dim ds As New DataSet()
        con.Open()
        da.Fill(ds, "tabiv21")
        DataGridView1.DataSource = ds.Tables("tabiv21")
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If (TextBox1.Text.Trim = "" And TextBox2.Text.Trim = "" And TextBox6.Text.Trim = "" And TextBox8.Text.Trim = "") Then
                MsgBox("Please fill any field to search")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand(" select c_fname[First Name],c_lastname[Last Name],fathername[Father Name],mothername[Mother Name],address[Address],c_phone[Phone],nationality[Nationality],passport_no[Passport],FORMAT(Dob,'dd/MM/yyyy') as [Date of birth]   from client where c_fname=@a and c_lastname=@b", con)
                com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox1.Text.Trim
                com.Parameters.Add("@b", SqlDbType.VarChar).Value = TextBox2.Text.Trim
                If (TextBox1.Text.Trim = "" And TextBox2.Text.Trim = "" And TextBox8.Text.Trim = "") Then
                    Dim comphone As New SqlCommand(" select c_fname[First Name],c_lastname[Last Name],fathername[Father Name],mothername[Mother Name],address[Address],c_phone[Phone],nationality[Nationality],passport_no[Passport],FORMAT(Dob,'dd/MM/yyyy') as [Date of birth]   from client where c_phone=@a", con)
                    com = comphone
                    com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox6.Text.Trim
                Else
                    If (TextBox1.Text.Trim = "" And TextBox2.Text.Trim = "" And TextBox6.Text.Trim = "") Then
                        Dim compass As New SqlCommand(" select c_fname[First Name],c_lastname[Last Name],fathername[Father Name],mothername[Mother Name],address[Address],c_phone[Phone],nationality[Nationality],passport_no[Passport],FORMAT(Dob,'dd/MM/yyyy') as [Date of birth]   from client where passport_no=@a", con)
                        com = compass
                        com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox8.Text.Trim
                    End If
                End If
                    Dim da As New SqlDataAdapter(com)
                Dim ds As New DataSet()
                con.Open()
                da.Fill(ds, "tabiv2")
                DataGridView1.DataSource = ds.Tables("tabiv2")
                con.Close()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox6.Text = ""
                TextBox8.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
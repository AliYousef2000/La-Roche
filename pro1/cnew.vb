Imports System.Data.SqlClient
Public Class cnew
    Public Sub resetclient()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        DateTimePicker1.Value = Today.Date
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        resetclient()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If (TextBox1.Text.Trim = "" Or TextBox2.Text.Trim = "" Or TextBox3.Text.Trim = "" Or TextBox4.Text.Trim = "" Or TextBox5.Text.Trim = "" Or TextBox6.Text.Trim = "" Or TextBox7.Text.Trim = "" Or DateTimePicker1.Value.Date = Today.Date) Then
                MsgBox("Please Enter empty fields")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("insert into client(c_fname,c_lastname,fathername,mothername,address,c_phone,nationality,passport_no,Dob) values(@q,@w,@e,@r,@t,@y,@u,@i,@o)", con)
                com.Parameters.Add("@q", SqlDbType.VarChar).Value = TextBox1.Text.Trim
                com.Parameters.Add("@w", SqlDbType.VarChar).Value = TextBox2.Text.Trim
                com.Parameters.Add("@e", SqlDbType.VarChar).Value = TextBox3.Text.Trim
                com.Parameters.Add("@r", SqlDbType.VarChar).Value = TextBox4.Text.Trim
                com.Parameters.Add("@t", SqlDbType.VarChar).Value = TextBox5.Text.Trim
                com.Parameters.Add("@y", SqlDbType.VarChar).Value = TextBox6.Text.Trim
                com.Parameters.Add("@u", SqlDbType.VarChar).Value = TextBox7.Text.Trim
                com.Parameters.Add("@i", SqlDbType.VarChar).Value = TextBox8.Text.Trim
                com.Parameters.Add("@o", SqlDbType.Date).Value = DateTimePicker1.Value.Date
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Client Added")
                resetclient()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
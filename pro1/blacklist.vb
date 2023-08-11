Imports System.Data.SqlClient
Public Class blacklist
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Try
            If ((TextBox14.Text.Trim = "") Or (TextBox15.Text.Trim = "") Or (TextBox16.Text.Trim = "")) Then
                MsgBox("Enter Empty Fields")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("insert into blacklist(bfname,blastname,bphone,note) values(@a,@b,@c,@d)", con)

                com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox14.Text.Trim
                com.Parameters.Add("@b", SqlDbType.VarChar).Value = TextBox15.Text.Trim
                com.Parameters.Add("@c", SqlDbType.VarChar).Value = TextBox16.Text.Trim
                com.Parameters.Add("@d", SqlDbType.VarChar).Value = RichTextBox1.Text.Trim
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Add to blacklist")
                populateblack()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select * from blacklist where bphone=@a", con)
            com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox13.Text.Trim
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "resp1")
            DataGridView3.DataSource = ds.Tables("resp1")
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Try
            If DataGridView3.SelectedRows.Count > 0 Then
                Dim cell As DataGridViewRow = DataGridView3.CurrentRow
                Dim list_id As Integer = cell.Cells(0).Value
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("delete from blacklist where bid=@a", con)
                com.Parameters.Add("@a", SqlDbType.Int).Value = CInt(list_id)
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                populateblack()
                MsgBox("Deleted Succesfully")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub populateblack()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select * from blacklist", con)
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "blist")
            DataGridView3.AutoGenerateColumns = True
            DataGridView3.DataSource = ds.Tables("blist")
            DataGridView3.AutoGenerateColumns = False
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub blacklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populateblack()
    End Sub
End Class
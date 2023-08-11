Imports System.Data.SqlClient
Public Class database
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If (ComboBox9.SelectedIndex = -1) Then
                MsgBox("enter position")
            Else
                If ((IsNumeric(TextBox8.Text.Trim) = False) Or (IsNumeric(TextBox9.Text.Trim) = False)) Then
                    MsgBox("floor and rooms must be number !!")
                Else
                    Dim con As New SqlConnection(Module1.str)
                    Dim com As New SqlCommand("insert into apartment(app_id,block_id,rooms,floor_no,position) values(@a,@c,@b,@d,@e)", con)
                    com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox6.Text.Trim
                    com.Parameters.Add("@b", SqlDbType.Int).Value = CInt(TextBox8.Text.Trim)
                    com.Parameters.Add("@c", SqlDbType.VarChar).Value = ComboBox8.SelectedItem
                    com.Parameters.Add("@d", SqlDbType.Int).Value = CInt(TextBox9.Text.Trim)
                    com.Parameters.Add("@e", SqlDbType.VarChar).Value = ComboBox9.SelectedItem
                    con.Open()
                    com.ExecuteNonQuery()
                    con.Close()
                    MsgBox("Apartment Add")
                End If
            End If
            Populatedgv()
            'Populateapcb()
            populateavaapp()
            ComboBox8.SelectedItem = 0
            ComboBox9.SelectedIndex = 0
            TextBox6.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Button18_Click_1(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            If ((TextBox10.Text.Trim = "") Or (TextBox11.Text.Trim = "") Or (TextBox12.Text.Trim = "")) Then
                MsgBox("Enter empty fields")
            Else
                If ((IsNumeric(TextBox12.Text.Trim) = False) Or (IsNumeric(TextBox11.Text.Trim) = False)) Then
                    MsgBox("fees and apartment number must be number !!")
                Else
                    Dim con As New SqlConnection(Module1.str)
                    Dim com As New SqlCommand("insert into block(block_id,app_no,app_fees) values(@a,@c,@b)", con)
                    com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox10.Text.Trim
                    com.Parameters.Add("@b", SqlDbType.Decimal).Value = CDec(TextBox12.Text.Trim)
                    com.Parameters.Add("@c", SqlDbType.Int).Value = CInt(TextBox11.Text.Trim)
                    con.Open()
                    com.ExecuteNonQuery()
                    con.Close()
                    MsgBox("Block Add")
                End If
            End If
            Populatedgv()
            Populateblcb()
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Try
            If (TextBox10.Text.Trim = "") Then
                MsgBox("Fail to delete block !! Please Enter Block Id first")
            Else

                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("delete from block where block_id=@a", con)
                com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox10.Text.Trim
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Block deleted")
                Populateblcb()
                Populatedgv()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If (TextBox6.Text.Trim = "") Then
                MsgBox("Fail to delete apartment !! Please Enter Apartment Id first")
            Else

                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("delete from apartment where app_id=@a", con)
                com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox6.Text.Trim
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Apartment deleted")
                'Populateapcb()
                populateavaapp()
                Populatedgv()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
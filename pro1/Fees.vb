Imports System.Data.SqlClient
Public Class Fees

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Try
            If ((TextBox2.Text.Trim = "") Or (ComboBox7.SelectedIndex = -1)) Then
                MsgBox("Fill Empty Fields")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("update block set app_fees=@b where block_id=@c", con)
                com.Parameters.Add("@b", SqlDbType.Decimal).Value = CDec(TextBox2.Text.Trim)
                com.Parameters.Add("@c", SqlDbType.VarChar).Value = ComboBox7.SelectedItem
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("PRICE UPDATED")
                calcprice()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
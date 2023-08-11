Imports System.Data.SqlClient
Public Class order
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            If (ComboBox1.SelectedIndex = -1) Or (TextBox2.Text = "") Then
                MsgBox("Fill Empty Fields")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim rri As Integer

                Dim com12 As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        select reservation_id from reservation
                                        where (@a between checkin_date and checkout_date)
                                        and app_id = @c
                                       ", con)
                com12.Parameters.Add("@b", SqlDbType.Float).Value = CDec(TextBox2.Text.Trim)
                com12.Parameters.Add("@c", SqlDbType.VarChar).Value = ComboBox1.SelectedItem
                con.Open()
                Dim df As SqlDataReader = com12.ExecuteReader()
                While df.Read
                    rri = df.Item(0)
                End While
                con.Close()

                Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        update invoice
                                        set adds_on = adds_on + @b
                                        where rid = @c
                                       ", con)
                com.Parameters.Add("@b", SqlDbType.Float).Value = CDec(TextBox2.Text.Trim)
                com.Parameters.Add("@c", SqlDbType.Int).Value = CInt(rri)
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Done")
            End If
            TextBox2.Text = ""
            getammount()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populateordercb()
        If ComboBox1.Items.Count > 1 Then
            ComboBox1.SelectedIndex = 0
            getammount()
        End If
    End Sub
    Public Sub populateordercb()
        Dim con As New SqlConnection(Module1.str)

        Dim com2 As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                            select apartment.app_id [Apartment] from apartment inner join 
                                            (select * from reservation
                                            WHERE (@a >= checkin_date) and (@a <= checkout_date)) reservation 
                                            on  apartment.app_id = reservation.app_id", con)
        con.Open()
        Dim dr As SqlDataReader = com2.ExecuteReader()
        While dr.Read()

            ComboBox1.Items.Add(dr.Item(0))
        End While
        con.Close()
    End Sub
    Public Sub getammount()
        Dim con As New SqlConnection(Module1.str)

        Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        select adds_on from reservation inner join invoice on reservation.reservation_id = invoice.rid
                                        where (@a between checkin_date and checkout_date)
                                        and app_id = @c
                                       ", con)

        com.Parameters.Add("@c", SqlDbType.VarChar).Value = ComboBox1.SelectedItem.ToString
        con.Open()
        Dim df As SqlDataReader = com.ExecuteReader()
        While df.Read
            TextBox1.Text = df.Item(0)
        End While
        con.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        getammount()
    End Sub
End Class
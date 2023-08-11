Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class cash


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        switch(invoice)
        populateinvoice()
        Form2.ViewPrintPreview()
        TextBox1.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        switch(deposit)
        populatedeposit()
        Form2.ViewPrintPreview()
        TextBox1.Text = ""
    End Sub

    Public Sub populatedeposit()
        Try
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MsgBox("Enter ID")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("select reservation_id , invoice_id , reservation.app_id,fname, lastname,phone,FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],
                                        FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice,deposite,afterprice + adds_on[Total],block.app_fees
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid inner join apartment on apartment.app_id = reservation.app_id inner join [block] on 
                                        apartment.block_id = [block].block_id 
                                        where reservation.reservation_id = @b
                                         ", con)
                com.Parameters.Add("@b", SqlDbType.Int).Value = TextBox1.Text.Trim

                If TextBox1.Text = "" Then
                    Dim com2 As New SqlCommand("select reservation_id , invoice_id , reservation.app_id,fname, lastname,phone,FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],
                                        FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice,deposite,adds_on,afterprice + adds_on[Total],block.app_fees
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid inner join apartment on apartment.app_id = reservation.app_id inner join [block] on 
                                        apartment.block_id = [block].block_id 
                                        where invoice.invoice_id = @b
                                         ", con)
                    com = com2
                    com.Parameters.Add("@b", SqlDbType.Int).Value = TextBox1.Text.Trim
                Else

                End If


                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                While dr.Read

                    deposit.Label16.Text = Date.Now.Date
                    deposit.Label17.Text = Date.Now.ToString("HH:mm")
                    deposit.Label18.Text = dr.Item(0)
                    deposit.Label27.Text = dr.Item(2)
                    deposit.Label19.Text = dr.Item(3) & "  " & dr.Item(4)
                    deposit.Label20.Text = dr.Item(5)
                    deposit.Label21.Text = dr.Item(6)
                    deposit.Label22.Text = dr.Item(7)
                    deposit.Label24.Text = dr.Item(9) & " $"
                    deposit.Label25.Text = dr.Item(8) & " $"
                End While

                con.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub populateinvoice()
        Try
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MsgBox("Enter ID")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("select reservation_id , invoice_id , reservation.app_id,fname, lastname,phone,FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],
                                        FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice,adds_on,afterprice + adds_on[Total],block.app_fees,deposite,afterprice - deposite,afterprice + adds_on - deposite
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid inner join apartment on apartment.app_id = reservation.app_id inner join [block] on 
                                        apartment.block_id = [block].block_id 
                                        where reservation.reservation_id = @b
                                         ", con)
                com.Parameters.Add("@b", SqlDbType.Int).Value = TextBox1.Text.Trim

                If TextBox1.Text = "" Then
                    Dim com2 As New SqlCommand("select reservation_id , invoice_id , reservation.app_id,fname, lastname,phone,FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],
                                        FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice,adds_on,afterprice + adds_on[Total],block.app_fees,deposite,afterprice - deposite,afterprice + adds_on - deposite
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid inner join apartment on apartment.app_id = reservation.app_id inner join [block] on 
                                        apartment.block_id = [block].block_id 
                                        where invoice.invoice_id = @b
                                         ", con)
                    com = com2
                    com.Parameters.Add("@b", SqlDbType.Int).Value = TextBox1.Text.Trim
                Else

                End If


                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                While dr.Read
                    invoice.Label15.Text = dr.Item(1)
                    invoice.Label16.Text = Date.Now.Date
                    invoice.Label17.Text = Date.Now.ToString("HH:mm")
                    invoice.Label18.Text = dr.Item(0)
                    invoice.Label27.Text = dr.Item(2)
                    invoice.Label19.Text = dr.Item(3) & "  " & dr.Item(4)
                    invoice.Label20.Text = dr.Item(5)
                    invoice.Label21.Text = dr.Item(6)
                    invoice.Label22.Text = dr.Item(7)
                    invoice.Label23.Text = dr.Item(8) & " $"
                    invoice.Label28.Text = "(" & dr.Item(11) & " / night --without discount )"
                    invoice.Label29.Text = dr.Item(13) & " $"
                    invoice.Label30.Text = dr.Item(12) & " $"
                    invoice.Label24.Text = dr.Item(9) & " $"
                    invoice.Label25.Text = dr.Item(14) & " $"
                End While

                con.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MsgBox("Enter ID")
            Else
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("select reservation_id[Reservation] , invoice_id [Invoice] , app_id[Room],fname [First Name], lastname[Last Name],phone [Phone],FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice[Price],adds_on[Orders],afterprice + adds_on[Total]
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid
                                        where reservation.reservation_id = @b
                                         ", con)
                If TextBox1.Text.Trim = "" Then
                    Dim com2 As New SqlCommand("select reservation_id[Reservation] , invoice_id [Invoice] , app_id[Room],fname [First Name], lastname[Last Name],phone [Phone],FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice[Price],adds_on[Orders],afterprice + adds_on[Total]
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid
                                        where invoice.invoice_id = @b
                                         ", con)

                    com = com2
                    com.Parameters.Add("@b", SqlDbType.Int).Value = CInt(TextBox2.Text.Trim)

                Else
                    Dim com3 As New SqlCommand("select reservation_id[Reservation] , invoice_id [Invoice] , app_id[Room],fname [First Name], lastname[Last Name],phone [Phone],FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice[Price],adds_on[Orders],afterprice + adds_on[Total]
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid
                                        where reservation.reservation_id = @b
                                         ", con)

                    com = com3
                    com3.Parameters.Add("@b", SqlDbType.Int).Value = CInt(TextBox1.Text.Trim)
                End If

            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "tabiv")
            DataGridView4.DataSource = ds.Tables("tabiv")
            con.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populatecashc()
    End Sub
    Public Sub populatecashc()
        Dim con As New SqlConnection(Module1.str)
        ComboBox1.Items.Clear()


        Dim com2 As New SqlCommand("        DECLARE @a AS date = CONVERT(date, GETDATE())
                                            select app_id from reservation
                                             WHERE checkout_date = DATEADD(DAY, -1, @a)  
                                            ", con)
        con.Open()
        Dim dr As SqlDataReader = com2.ExecuteReader()
        While dr.Read()

            ComboBox1.Items.Add(dr.Item(0))
        End While
        con.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim con As New SqlConnection(Module1.str)
        Dim com2 As New SqlCommand("select reservation_id[Reservation] , invoice_id [Invoice] , app_id[Room],fname [First Name], lastname[Last Name],phone [Phone],FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],afterprice[Price],adds_on[Orders],afterprice + adds_on[Total]
                                        from reservation inner join invoice on reservation.reservation_id = invoice.rid
                                        where reservation.app_id = @b
                                         ", con)
        com2.Parameters.Add("@b", SqlDbType.VarChar).Value = ComboBox1.SelectedItem
        con.Open()
        MsgBox(ComboBox1.SelectedItem)
        Dim da As New SqlDataAdapter(com2)
        Dim ds As New DataSet()
        da.Fill(ds, "tabt")
        DataGridView4.DataSource = ds.Tables("tabt")
        con.Close()
    End Sub
End Class



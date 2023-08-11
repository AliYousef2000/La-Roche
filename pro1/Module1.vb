Imports System.Data.SqlClient
Module Module1
    Public str As String = "Data Source=UNKNOWN\SQLEXPRESS;Initial Catalog=rent;Integrated Security=True"
    'Public str As String = "Data Source=DESKTOP-6UA7K4S\SQLEXPRESS;Initial Catalog=rent;Integrated Security=True"

    Public Sub clientswitch(ByVal panel As Form)
        client.Panel1.Controls.Clear()
        panel.FormBorderStyle = FormBorderStyle.None
        panel.TopLevel = False
        client.Panel1.Controls.Add(panel)
        'Form2.Panel1.Dock = DockStyle.Fill
        panel.Show()
    End Sub

    Public Sub popall()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        select app_id[Apartment id],block_id[Block],rooms[Rooms],floor_no[Floor],position[Position],floor_no*0[f] from apartment 
                                        WHERE app_id NOT IN (SELECT app_id FROM reservation WHERE (@a >= checkin_date) and (@a <= checkout_date))
                                        union 
                                        select app_id[Apartment id],block_id[Block],rooms[Rooms],floor_no[Floor],position[Position],floor_no/floor_no[f] from apartment 
                                        WHERE app_id IN (SELECT app_id FROM reservation WHERE (@a >= checkin_date) and (@a <= checkout_date))", con)
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "tab1")
            apartment.DataGridView1.DataSource = ds.Tables("tab1")
            con.Close()
            For Each row As DataGridViewRow In apartment.DataGridView1.Rows
                If (row.Cells(5).Value = 0) Then
                    row.DefaultCellStyle.BackColor = Color.Lime
                Else
                    row.DefaultCellStyle.BackColor = Color.IndianRed
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub switch(ByVal panel As Form)

        Form2.Panel1.Controls.Clear()
        panel.FormBorderStyle = FormBorderStyle.None
        panel.TopLevel = False
        Form2.Panel1.Controls.Add(panel)
        'Form2.Panel1.Dock = DockStyle.Fill
        panel.Show()
    End Sub
    Public Sub returnres()
        switch(reservation)
    End Sub
    Public Sub reset_re()
        reservation.TextBox3.Text = ""
        reservation.TextBox4.Text = ""
        reservation.TextBox5.Text = ""
        reservation.TextBox7.Text = ""
        reservation.TextBox18.Text = ""
        reservation.TextBox20.Text = ""
        reservation.TextBox2.Text = ""
        reservation.DateTimePicker1.Value = Date.Today
        reservation.DateTimePicker2.Value = Date.Today
        reservation.ComboBox3.ResetText()
        Form2.count = 0
        Form2.res_id = 0
        calcprice()
    End Sub

    Public Sub Populateapcb()
        Try
            'apartment.ComboBox5.Items.Clear()
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_id from apartment", con)
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                'apartment.ComboBox5.Items.Add(dr.Item(0))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub populateavaapp()
        reservation.ComboBox3.Items.Clear()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_id from apartment where block_id=@a ", con)
            com.Parameters.Add("@a", SqlDbType.VarChar).Value = reservation.ComboBox4.SelectedItem
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                reservation.ComboBox3.Items.Add(dr.Item(0))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub Populateblcb()
        Try
            apartment.ComboBox6.Items.Clear()
            database.ComboBox8.Items.Clear()
            reservation.ComboBox4.Items.Clear()
            Fees.ComboBox7.Items.Clear()
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select block_id from block", con)
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                apartment.ComboBox6.Items.Add(dr.Item(0))
                reservation.ComboBox4.Items.Add(dr.Item(0))
                Fees.ComboBox7.Items.Add(dr.Item(0))
                database.ComboBox8.Items.Add(dr.Item(0))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub checkpaint()
        If (apartment.DataGridView1.Columns.Count = 7) Then
            For Each row As DataGridViewRow In apartment.DataGridView1.Rows
                row.DefaultCellStyle.BackColor = Color.Yellow
            Next
        End If
    End Sub

    Public Sub dgv1paint()
        If (apartment.DataGridView1.Columns.Count = 7) Then
            For Each row As DataGridViewRow In apartment.DataGridView1.Rows
                If IsDBNull(row.Cells(2).Value) Then
                    row.DefaultCellStyle.BackColor = Color.Lime
                Else
                    row.DefaultCellStyle.BackColor = Color.IndianRed
                End If
            Next
        End If
    End Sub


    Public Sub populatecrdg()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE()) select  reservation_id[ID],app_id[Apartment Id],DATEDIFF(day,checkin_date,checkout_date)[Nights],fname[First Name],lastname[Last Name],CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],phone[Phone],invoice.price[Price] ,invoice.afterprice[After Discount],invoice.deposite[Deposite],invoice.afterprice - invoice.deposite [Remain] ,invoice.afterprice+invoice.adds_on[Total] from reservation inner join invoice on invoice.rid = reservation.reservation_id where (checkin_date >= @a) or (checkin_date < @a and checkout_date >= @a )", con)
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "cur")
            reservation.DataGridView2.AutoGenerateColumns = True
            reservation.DataGridView2.DataSource = ds.Tables("cur")
            reservation.DataGridView2.AutoGenerateColumns = False
            Dim rowcount As Integer = reservation.DataGridView2.Rows.Count - 2

            For Each row As DataGridViewRow In reservation.DataGridView2.Rows
                If row.Index <= rowcount Then
                    row.Cells(2).Value = row.Cells(2).Value + 1
                End If
            Next
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub calcprice()
        Dim price As Decimal
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_fees from block where block_id=@a", con)
            com.Parameters.Add("@a", SqlDbType.VarChar).Value = reservation.ComboBox4.SelectedItem
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                price = dr.Item(0)
            End While
            con.Close()
            reservation.ComboBox3.ResetText()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Dim D1 As Date = reservation.DateTimePicker1.Value
        Dim D2 As Date = reservation.DateTimePicker2.Value
        Dim difference As TimeSpan = D2.Subtract(D1)
        reservation.TextBox7.Text = (CInt(difference.TotalDays) + 1) * price
    End Sub
    Public Sub Populateregv()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select reservation_id[ID],app_id[Apartment Id],DATEDIFF(day,checkin_date,checkout_date)[Nights],fname[First Name],lastname[Last Name],phone[Phone],FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],invoice.price[Price] ,invoice.afterprice[After Discount],invoice.deposite[Deposite],invoice.afterprice - invoice.deposite [Remain],invoice.afterprice+invoice.adds_on[Total] from reservation inner join invoice on invoice.rid = reservation.reservation_id", con)
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "res")
            reservation.DataGridView2.AutoGenerateColumns = True
            reservation.DataGridView2.DataSource = ds.Tables("res")
            reservation.DataGridView2.AutoGenerateColumns = False
            Dim rowcount As Integer = reservation.DataGridView2.Rows.Count - 2
            For Each row As DataGridViewRow In reservation.DataGridView2.Rows
                If row.Index <= rowcount Then
                    row.Cells(2).Value = row.Cells(2).Value + 1
                End If
            Next
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub populaterecb()
        Try

            reservation.ComboBox3.Items.Clear()
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("SELECT app_id FROM apartment WHERE app_id NOT IN (SELECT app_id FROM reservation WHERE ((checkin_date BETWEEN @a AND @b ) OR (checkout_date BETWEEN @a AND @b) OR (checkin_date <= @a AND checkout_date >= @b))) and block_id=@c", con)
            com.Parameters.Add("@a", SqlDbType.Date).Value = reservation.DateTimePicker1.Value
            com.Parameters.Add("@b", SqlDbType.Date).Value = reservation.DateTimePicker2.Value
            com.Parameters.Add("@c", SqlDbType.VarChar).Value = reservation.ComboBox4.SelectedItem
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                reservation.ComboBox3.Items.Add(dr.Item(0))
            End While
            con.Close()
            ' MsgBox("Invalid date")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub Populatedgv()
        'DataGridView1.Rows.Clear()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                        FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date] from apartment full join 
                                        (select * from reservation
                                        WHERE (@a >= checkin_date) and (@a <= checkout_date)) reservation 
                                        on  apartment.app_id = reservation.app_id", con)

            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "tab1000")
            apartment.DataGridView1.AutoGenerateColumns = True
            apartment.DataGridView1.DataSource = ds.Tables("tab1000")
            apartment.DataGridView1.AutoGenerateColumns = False
            con.Close()
            dgv1paint()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module







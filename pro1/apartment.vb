Imports System.Data.SqlClient
Public Class apartment
    Private Sub apartment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.RowTemplate.Height = 35
        'Populateapcb()
        Populateblcb()
        'ComboBox5.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        Populatedgv()
        dgv1paint()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Populatedgv()
        dgv1paint()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com2 As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                        select app_id[Apartment id],block_id[Block],rooms[Rooms],floor_no[Floor],position[Position],floor_no*0[f] from apartment 
                                        WHERE app_id NOT IN (SELECT app_id FROM reservation WHERE (@a >= checkin_date) and (@a <= checkout_date))
                                        union 
                                        select app_id[Apartment id],block_id[Block],rooms[Rooms],floor_no[Floor],position[Position],floor_no/floor_no[f] from apartment 
                                        WHERE app_id IN (SELECT app_id FROM reservation WHERE (@a >= checkin_date) and (@a <= checkout_date))", con)


            If (RadioButton1.Checked) Then
                Dim com As New SqlCommand("
                                            DECLARE @new TABLE (
                                                [Apartment] varchar(5),[Block] varchar(5),[Reservation] integer,[First Name] varchar(30),[Last Name] varchar(30),[Ch-In Date] date,[Ch-Out Date] date
                                            );

                                            DECLARE @a AS date = CONVERT(date, GETDATE());

                                            INSERT INTO @new ([Apartment], [Block], [Reservation], [First Name], [Last Name], [Ch-In Date], [Ch-Out Date])
                                            SELECT apartment.app_id AS [Apartment],apartment.block_id AS [Block],reservation_id AS [Reservation],fname AS [First Name],lastname AS [Last Name],checkin_date AS [Ch-In Date],checkout_date AS [Ch-Out Date]
                                            FROM apartment FULL JOIN ( SELECT * FROM reservation WHERE ((@a >= checkin_date) AND (@a <= checkout_date)) ) reservation ON apartment.app_id = reservation.app_id;

                                            SELECT [Apartment], [Block],[Reservation],[First Name],[Last Name],CONVERT(varchar, [Ch-In Date], 3) AS [Ch-In Date],CONVERT(varchar, [Ch-Out Date], 3) AS [Ch-Out Date]
                                            FROM @new
                                            WHERE [Block] = @b;
                                        ", con)
                com.Parameters.Add("@b", SqlDbType.VarChar).Value = ComboBox6.SelectedItem
                com2 = com
            End If
            If (RadioButton2.Checked) Then
                Dim com As New SqlCommand("
                                            DECLARE @new TABLE (
                                            [Apartment] varchar(5),[Block] varchar(5),[Reservation] integer,[First Name] varchar(30),[Last Name] varchar(30),[Ch-In Date] date,[Ch-Out Date] date
                                            );
                                             DECLARE @a AS date = CONVERT(date, GETDATE())
                                             INSERT INTO @new ([Apartment] ,[Block],[Reservation] ,[First Name] ,[Last Name] ,[Ch-In Date] ,[Ch-Out Date])

                                            select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                            CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment full join 
                                            (select * from reservation
                                            WHERE ((@a >= checkin_date) and (@a <= checkout_date))) reservation 
                                            on  apartment.app_id = reservation.app_id

                                            except

                                            select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                            CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment inner join 
                                            (select * from reservation
                                            WHERE (@a >= checkin_date) and (@a <= checkout_date)) reservation 
                                             on  apartment.app_id = reservation.app_id

                                             select * from @new where [Block] = @b", con)
                com.Parameters.Add("@b", SqlDbType.VarChar).Value = ComboBox6.SelectedItem
                com2 = com
            End If
            If (RadioButton3.Checked) Then
                Dim com As New SqlCommand("DECLARE @a AS date = CONVERT(date, GETDATE())
                                            select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                            CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment inner join 
                                            (select * from reservation
                                            WHERE (@a >= checkin_date) and (@a <= checkout_date) and reservation.block_id = @b) reservation 
                                            on  apartment.app_id = reservation.app_id", con)
                com.Parameters.Add("@b", SqlDbType.VarChar).Value = ComboBox6.SelectedItem
                com2 = com
            End If

            Dim da As New SqlDataAdapter(com2)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "tab")
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = ds.Tables("tab")
            DataGridView1.AutoGenerateColumns = False
            con.Close()
            dgv1paint()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("
                                        DECLARE @dateString varchar(20) = @a;
                                        DECLARE @convertedDate date;

                                        SET @convertedDate = CONVERT(date, @dateString, 23);

                                        select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                        CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment inner join 
                                        (select * from reservation
                                        WHERE (checkin_date = @convertedDate )) reservation 
                                        on  apartment.app_id = reservation.app_id

", con)
            com.Parameters.Add("@a", SqlDbType.Date).Value = DateTimePicker4.Value
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "resin")
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = ds.Tables("resin")
            DataGridView1.AutoGenerateColumns = False
            con.Close()
            checkpaint()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DateTimePicker5_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker5.ValueChanged
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("
                                        DECLARE @dateString varchar(20) = @a;
                                        DECLARE @convertedDate date;

                                        SET @convertedDate = CONVERT(date, @dateString, 23);

                                        select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
                                        CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment inner join 
                                        (select * from reservation
                                        WHERE (checkout_date = @convertedDate )) reservation 
                                        on  apartment.app_id = reservation.app_id
", con)
            com.Parameters.Add("@a", SqlDbType.Date).Value = DateTimePicker5.Value
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "resout")
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = ds.Tables("resout")
            DataGridView1.AutoGenerateColumns = False
            con.Close()
            checkpaint()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Try
            If ((DateTimePicker5.Value > DateTimePicker4.Value) Or (DateTimePicker5.Value = DateTimePicker4.Value)) Then
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand("SELECT app_id[Apartment id],block_id[Block],rooms[Rooms],floor_no[Floor],position[Position] FROM apartment WHERE app_id NOT IN (SELECT app_id FROM reservation WHERE ((checkin_date BETWEEN @a AND @b ) OR (checkout_date BETWEEN @a AND @b) OR (checkin_date <= @a AND checkout_date >= @b)))", con)
                com.Parameters.Add("@a", SqlDbType.Date).Value = DateTimePicker4.Value
                com.Parameters.Add("@b", SqlDbType.Date).Value = DateTimePicker5.Value
                Dim da As New SqlDataAdapter(com)
                Dim ds As New DataSet()
                con.Open()
                da.Fill(ds, "check")
                DataGridView1.AutoGenerateColumns = True
                DataGridView1.DataSource = ds.Tables("check")
                DataGridView1.AutoGenerateColumns = False
                con.Close()
            Else
                MsgBox("Invalid date")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Try

            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand(" select sum(afterprice + adds_on) from reservation inner join invoice on reservation.reservation_id = invoice.rid where (checkin_date >= @b) and (checkin_date <= @c)", con)
            com.Parameters.Add("@b", SqlDbType.Date).Value = DateTimePicker4.Value
            com.Parameters.Add("@c", SqlDbType.Date).Value = DateTimePicker5.Value
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            While dr.Read()
                If IsDBNull(dr.Item(0)) Then
                    TextBox19.Text = 0
                Else
                    TextBox19.Text = dr.Item(0)
                End If
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    'Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
    '    Try
    '        Dim con As New SqlConnection(Module1.str)
    '        Dim com As New SqlCommand("
    '                                        DECLARE @new TABLE (
    '                                            [Apartment] varchar(5),[Block] varchar(5),[Reservation] integer,[First Name] varchar(30),[Last Name] varchar(30),[Ch-In Date] date,[Ch-Out Date] date
    '                                            );
    '                                             DECLARE @z AS date = CONVERT(date, GETDATE())
    '                                             INSERT INTO @new ([Apartment] ,[Block],[Reservation] ,[First Name] ,[Last Name] ,[Ch-In Date] ,[Ch-Out Date])

    '                                            select apartment.app_id [Apartment],apartment.block_id [Block],reservation_id [Reservation],fname [First Name],lastname[Last Name],
    '                                            CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],CONVERT(VARCHAR, checkout_date, 103) as [Ch-Out Date] from apartment full join 
    '                                            (select * from reservation
    '                                            WHERE (@z >= checkin_date) and (@z <= checkout_date)) reservation 
    '                                            on  apartment.app_id = reservation.app_id

    '                                             select * from @new where [Apartment] = @b
    '", con)
    '        com.Parameters.Add("@b", SqlDbType.VarChar, 5).Value = ComboBox5.SelectedItem
    '        Dim da As New SqlDataAdapter(com)
    '        Dim ds As New DataSet()
    '        con.Open()
    '        da.Fill(ds, "tab")
    '        DataGridView1.AutoGenerateColumns = True
    '        DataGridView1.DataSource = ds.Tables("tab")
    '        DataGridView1.AutoGenerateColumns = False
    '        con.Close()
    '        dgv1paint()
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

End Class
Imports System.Data.SqlClient
Public Class reservation
    Dim perapp As String
    Private Sub reservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView2.RowTemplate.Height = 35
        Populateblcb()
        ComboBox4.SelectedIndex = 0
        DateTimePicker1.Value = Date.Today
        DateTimePicker2.Value = Date.Today.AddDays(1)
        TextBox9.Text = 0
        populatecrdg()
        calcprice()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            If DataGridView2.SelectedRows.Count > 0 Then
                Form2.count = 1
                Dim cell As DataGridViewRow = DataGridView2.CurrentRow
                Form2.res_id = cell.Cells(0).Value
                Dim con As New SqlConnection(Module1.str)
                Dim com As New SqlCommand(" select reservation_id,app_id,fname,lastname,checkin_date,checkout_date,phone,invoice.price,invoice.afterprice,invoice.deposite,invoice.adds_on,block_id,elec_in,elec_out from reservation inner join invoice on reservation.reservation_id = invoice.rid where reservation_id=@a ", con)
                com.Parameters.Add("@a", SqlDbType.Int).Value = CInt(Form2.res_id)
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader()
                While dr.Read()
                    TextBox3.Text = dr.Item(2)
                    TextBox4.Text = dr.Item(3)
                    ComboBox4.SelectedValue = dr.Item(11)
                    TextBox5.Text = dr.Item(6)
                    DateTimePicker1.Value = dr.Item(4)
                    DateTimePicker2.Value = dr.Item(5)
                    ComboBox3.Items.Clear()
                    ComboBox3.Items.Add(dr.Item(1))
                    ComboBox3.SelectedIndex = 0
                    TextBox2.Text = dr.Item(10)
                    TextBox7.Text = dr.Item(7)
                    TextBox8.Text = dr.Item(12)
                    TextBox9.Text = dr.Item(13)
                    TextBox18.Text = dr.Item(8)
                    TextBox20.Text = dr.Item(9)
                    Form2.appname = CStr(dr.Item(1))
                End While
            Else
                MsgBox("Please select row")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            If DataGridView2.SelectedRows.Count > 0 Then
                Dim cell As DataGridViewRow = DataGridView2.CurrentRow
                Dim res_id As Integer = cell.Cells(0).Value
                Dim con As New SqlConnection(Module1.str)
                Dim com2 As New SqlCommand("delete from invoice where rid=@a", con)
                com2.Parameters.Add("@a", SqlDbType.Int).Value = CInt(res_id)
                con.Open()
                com2.ExecuteNonQuery()
                con.Close()
                Dim com As New SqlCommand("delete from reservation where reservation_id=@a", con)
                com.Parameters.Add("@a", SqlDbType.Int).Value = CInt(res_id)
                con.Open()
                com.ExecuteNonQuery()
                con.Close()
                MsgBox("Reservation Deleted")
                populatecrdg()
                ComboBox4.SelectedIndex = 0
                populaterecb()
                Populatedgv()
            Else
                MsgBox("Please select row")
            End If
            newmap.popview()
            cash.populatecashc()
            order.populateordercb()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        apartment.DateTimePicker4.Value = Date.Today.AddDays(1)
        apartment.DateTimePicker5.Value = Date.Today.AddDays(1)
        TextBox1.Text = ""
        Populateregv()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        populatecrdg()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select  reservation_id[ID],app_id[Apartment Id],fname[First Name],lastname[Last Name],CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],phone[Phone],invoice.price[Price] ,invoice.afterprice[After Discount],invoice.deposite[Deposite],invoice.adds_on[Orders],invoice.afterprice+invoice.adds_on[Total],DATEDIFF(day,checkin_date,checkout_date)[Nights] from reservation inner join invoice on invoice.rid = reservation.reservation_id where phone=@a", con)
            com.Parameters.Add("@a", SqlDbType.VarChar).Value = TextBox1.Text.Trim
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "resp")
            DataGridView2.AutoGenerateColumns = True
            DataGridView2.DataSource = ds.Tables("resp")
            DataGridView2.AutoGenerateColumns = True
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        reset_re()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            If (Form2.count = 0) Then
                If ((ComboBox3.SelectedIndex = -1) Or (TextBox3.Text.Trim = "") Or (TextBox4.Text.Trim = "") Or (TextBox5.Text.Trim = "") Or (TextBox20.Text.Trim = "") Or (TextBox8.Text.Trim = "") Or (TextBox9.Text.Trim = "")) Then
                    MsgBox("Enter Empty Fields")
                Else
                    Dim con As New SqlConnection(Module1.str)
                    Dim ri As Integer

                    TextBox9.Text = 0

                    Dim com As New SqlCommand("insert into reservation(app_id,block_id,fname,lastname,phone,checkin_date,checkout_date) values(@a,@k,@b,@c,@d,@e,@f)", con)
                    com.Parameters.Add("@a", SqlDbType.VarChar).Value = ComboBox3.SelectedItem
                    com.Parameters.Add("@k", SqlDbType.VarChar).Value = ComboBox4.SelectedItem
                    com.Parameters.Add("@b", SqlDbType.VarChar).Value = TextBox3.Text.Trim
                    com.Parameters.Add("@c", SqlDbType.VarChar).Value = TextBox4.Text.Trim
                    com.Parameters.Add("@d", SqlDbType.VarChar).Value = TextBox5.Text.Trim
                    com.Parameters.Add("@e", SqlDbType.VarChar).Value = DateTimePicker1.Value
                    com.Parameters.Add("@f", SqlDbType.VarChar).Value = DateTimePicker2.Value
                    con.Open()
                    com.ExecuteNonQuery()
                    con.Close()

                    Dim com9 As New SqlCommand("select reservation_id from reservation where(app_id=@a and phone=@d and checkin_date=@e) ", con)
                    com9.Parameters.Add("@a", SqlDbType.VarChar).Value = ComboBox3.SelectedItem
                    com9.Parameters.Add("@d", SqlDbType.VarChar).Value = TextBox5.Text.Trim
                    com9.Parameters.Add("@e", SqlDbType.VarChar).Value = DateTimePicker1.Value
                    con.Open()
                    Dim dr2 As SqlDataReader = com9.ExecuteReader()
                    While dr2.Read()
                        ri = dr2.Item(0)
                    End While
                    con.Close()


                    If (TextBox18.Text.Trim = "") Then
                        Dim com8 As New SqlCommand("insert into invoice(rid,price,afterprice,deposite,adds_on,elec_in,elec_out) values(@m,@p,@x,@y,0,@l,0)", con)
                        com8.Parameters.Add("@m", SqlDbType.Int).Value = CInt(ri)
                        com8.Parameters.Add("@p", SqlDbType.Decimal).Value = CDec(TextBox7.Text.Trim)
                        com8.Parameters.Add("@x", SqlDbType.Decimal).Value = CDec(TextBox7.Text.Trim)
                        com8.Parameters.Add("@y", SqlDbType.Decimal).Value = CDec(TextBox20.Text.Trim)
                        com8.Parameters.Add("@l", SqlDbType.Int).Value = CInt(TextBox8.Text.Trim)
                        con.Open()
                        com8.ExecuteNonQuery()
                        con.Close()
                    Else
                        Dim com2 As New SqlCommand("insert into invoice(rid,price,afterprice,deposite,adds_on,elec_in,elec_out) values(@m,@p,@x,@y,0,@l,0)", con)
                        com2.Parameters.Add("@m", SqlDbType.Int).Value = CInt(ri)
                        com2.Parameters.Add("@p", SqlDbType.Decimal).Value = CDec(TextBox7.Text.Trim)
                        com2.Parameters.Add("@x", SqlDbType.Decimal).Value = CDec(TextBox18.Text.Trim)
                        com2.Parameters.Add("@y", SqlDbType.Decimal).Value = CDec(TextBox20.Text.Trim)
                        com2.Parameters.Add("@l", SqlDbType.Int).Value = CInt(TextBox8.Text.Trim)
                        con.Open()
                        com2.ExecuteNonQuery()
                        con.Close()
                    End If

                    MsgBox("Reservation Done")
                    reset_re()
                End If
            Else
                If ((ComboBox3.SelectedIndex = -1) Or (TextBox3.Text.Trim = "") Or (TextBox4.Text.Trim = "") Or (TextBox5.Text.Trim = "") Or (TextBox7.Text.Trim = "") Or (TextBox18.Text.Trim = "") Or (TextBox20.Text.Trim = "") Or (TextBox8.Text.Trim = "") Or (TextBox9.Text.Trim = "")) Then
                    MsgBox("Enter Empty Fields ")
                Else
                    Dim con As New SqlConnection(Module1.str)
                    Dim com2 As New SqlCommand("update reservation set app_id =@a ,block_id = @k ,fname = @b ,lastname =@c ,phone =@d,checkin_date                                      =@e ,checkout_date =@f where reservation_id=@w;
                                                update invoice set price =@p ,afterprice =@x ,deposite =@y, adds_on=@q , elec_in=@g ,elec_out=@l  where rid=@w ", con)
                    com2.Parameters.Add("@a", SqlDbType.VarChar).Value = ComboBox3.SelectedItem
                    com2.Parameters.Add("@k", SqlDbType.VarChar).Value = ComboBox4.SelectedItem
                    com2.Parameters.Add("@b", SqlDbType.VarChar).Value = TextBox3.Text.Trim
                    com2.Parameters.Add("@c", SqlDbType.VarChar).Value = TextBox4.Text.Trim
                    com2.Parameters.Add("@d", SqlDbType.VarChar).Value = TextBox5.Text.Trim
                    com2.Parameters.Add("@e", SqlDbType.VarChar).Value = DateTimePicker1.Value
                    com2.Parameters.Add("@f", SqlDbType.VarChar).Value = DateTimePicker2.Value
                    com2.Parameters.Add("@p", SqlDbType.Decimal).Value = CDec(TextBox7.Text.Trim)
                    com2.Parameters.Add("@x", SqlDbType.Decimal).Value = CDec(TextBox18.Text.Trim)
                    com2.Parameters.Add("@y", SqlDbType.Decimal).Value = CDec(TextBox20.Text.Trim)
                    com2.Parameters.Add("@q", SqlDbType.Decimal).Value = CDec(TextBox2.Text.Trim)
                    com2.Parameters.Add("@g", SqlDbType.Int).Value = CInt(TextBox8.Text.Trim)
                    com2.Parameters.Add("@l", SqlDbType.Int).Value = CInt(TextBox9.Text.Trim)
                    com2.Parameters.Add("@w", SqlDbType.Int).Value = CInt(Form2.res_id)
                    con.Open()
                    com2.ExecuteNonQuery()
                    con.Close()
                    MsgBox("Updated Succesfully")
                    reset_re()
                End If
            End If
            Populatedgv()
            populateavaapp()
            populaterecb()
            Populateregv()
            populatecrdg()
            newmap.popview()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim currentDate As DateTime = DateTime.Now
        map.Populatecolumnsrows(DateTime.DaysInMonth(currentDate.Year, currentDate.Month), currentDate.Month)
        cash.populatecashc()
        order.populateordercb()

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If Form2.count = 1 Then
            perapp = Form2.appname
            ComboBox3.Items.Clear()
            populaterecb()
            ComboBox3.Items.Add(perapp)
            calcprice()
        Else
            ComboBox3.Items.Clear()
            populaterecb()
            calcprice()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MinDate = DateTimePicker1.Value
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If Form2.count = 1 Then
            calcprice()
            populaterecb()
            ComboBox3.Items.Add(Form2.appname)
        Else
            calcprice()
            populaterecb()
        End If
    End Sub
    Private Sub ComboBox3_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox3.DrawItem
        If Form2.count = 1 Then


            If e.Index < 0 Then
                Return
            End If
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            Dim Cb As Windows.Forms.ComboBox = TryCast(sender, Windows.Forms.ComboBox)
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(New SolidBrush(Color.Lime), e.Bounds) ' selected item background color
            Else
                e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds) ' background color
            End If
            e.Graphics.DrawString(Cb.Items(e.Index).ToString(), e.Font, New SolidBrush(Color.Black), New Point(e.Bounds.X, e.Bounds.Y))
            ' New SolidBrush(Color.Red) font forecolor
            If e.Index = ComboBox3.Items.Count - 1 Then
                e.Graphics.FillRectangle(New SolidBrush(Color.Blue), e.Bounds) ' background color
                e.Graphics.DrawString(Cb.Items(e.Index).ToString(), e.Font, New SolidBrush(Color.White), New Point(e.Bounds.X, e.Bounds.Y))
            End If

        Else
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            Dim Cb As Windows.Forms.ComboBox = TryCast(sender, Windows.Forms.ComboBox)
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(New SolidBrush(Color.Lime), e.Bounds) ' selected item background color
            Else
                e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds) ' background color
            End If
            e.Graphics.DrawString(Cb.Items(e.Index).ToString(), e.Font, New SolidBrush(Color.Black), New Point(e.Bounds.X, e.Bounds.Y))
            ' New SolidBrush(Color.Red) font forecolor
        End If

    End Sub

    Private Sub DataGridView2_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView2.RowPrePaint
        ' Check if the current row index is within the valid range
        If e.RowIndex >= 0 AndAlso e.RowIndex < DataGridView2.Rows.Count - 1 Then
            ' Set the background color based on the row index
            If e.RowIndex Mod 2 = 0 Then
                ' Even row index, set background color to gray for all cells
                For Each cell As DataGridViewCell In DataGridView2.Rows(e.RowIndex).Cells
                    cell.Style.BackColor = Color.FromArgb(235, 235, 235)
                Next
            Else
                ' Odd row index, set background color to white for all cells
                For Each cell As DataGridViewCell In DataGridView2.Rows(e.RowIndex).Cells
                    cell.Style.BackColor = Color.White
                Next
            End If
        End If
    End Sub


    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        switch(newmap)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select  reservation_id[ID],app_id[Apartment Id],fname[First Name],lastname[Last Name],CONVERT(VARCHAR, checkin_date, 103) as [Ch-In Date],
                                       FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],phone[Phone],invoice.price[Price] ,invoice.afterprice[After Discount],invoice.deposite[Deposite],
                                        invoice.adds_on[Orders],invoice.afterprice+invoice.adds_on[Total],DATEDIFF(day,checkin_date,checkout_date)[Nights] from reservation inner join invoice 
                                        on invoice.rid = reservation.reservation_id  where reservation_id=@a", con)
            com.Parameters.Add("@a", SqlDbType.Int).Value = CInt(TextBox6.Text.Trim)
            Dim da As New SqlDataAdapter(com)
            Dim ds As New DataSet()
            con.Open()
            da.Fill(ds, "resp")
            DataGridView2.AutoGenerateColumns = True
            DataGridView2.DataSource = ds.Tables("resp")
            DataGridView2.AutoGenerateColumns = True
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class
Imports System.Data.SqlClient
Imports System.Globalization

Public Class newmap

    Dim frdate As Date
    Dim lsdate As Date
    Private Sub newmap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        popview()
        'popviewbydate()
    End Sub
    Public Sub popview()
        Try

            DataGridView1.RowTemplate.Height = 25

            DataGridView1.Columns.Clear()
            ' Set the initial date as today
            Dim currentDate As Date = Date.Today

            ' Create a new column for the fixed column on the left
            Dim fixedColumn As New DataGridViewTextBoxColumn()
            fixedColumn.HeaderText = "ROOM"
            fixedColumn.Frozen = True ' Make the column fixed
            DataGridView1.Columns.Add(fixedColumn)

            ' Loop through 31 times to create dynamic date columns
            For i As Integer = 0 To 30
                ' Create a new column with the current date as the header
                Dim newColumn As New DataGridViewTextBoxColumn()
                newColumn.HeaderText = currentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

                ' Add the column to the DataGridView
                DataGridView1.Columns.Add(newColumn)

                ' Increment the current date by one month
                currentDate = currentDate.AddDays(1)
            Next

            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_id from apartment", con)
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            Dim rowIndex As Integer = 0
            Dim columnIndex As Integer = 1
            'every read it loop the table
            While dr.Read()
                DataGridView1.Rows.Add(1)
                DataGridView1.Rows(rowIndex).Cells(0).Value = dr.Item(0).ToString
                rowIndex += 1
            End While
            con.Close()
            popmap()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub popviewbydate()
        Try
            DataGridView1.RowTemplate.Height = 25

            DataGridView1.Columns.Clear()
            ' Set the initial date as today
            Dim currentDate As Date = DateTimePicker1.Value
            Dim firstDateOfMonth As DateTime = New DateTime(currentDate.Year, currentDate.Month, 1)
            currentDate = firstDateOfMonth
            Dim monthdays As Integer = DateTime.DaysInMonth(currentDate.Year, currentDate.Month)

            ' Create a new column for the fixed column on the left
            Dim fixedColumn As New DataGridViewTextBoxColumn()
            fixedColumn.HeaderText = "ROOM"
            fixedColumn.Frozen = True ' Make the column fixed
            DataGridView1.Columns.Add(fixedColumn)

            ' Loop through 31 times to create dynamic date columns
            For i As Integer = 0 To monthdays - 1
                ' Create a new column with the current date as the header
                Dim newColumn As New DataGridViewTextBoxColumn()
                newColumn.HeaderText = currentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

                ' Add the column to the DataGridView
                DataGridView1.Columns.Add(newColumn)

                ' Increment the current date by one month
                currentDate = currentDate.AddDays(1)
            Next

            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_id from apartment", con)
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            Dim rowIndex As Integer = 0
            Dim columnIndex As Integer = 1
            'every read it loop the table
            While dr.Read()
                DataGridView1.Rows.Add(1)
                DataGridView1.Rows(rowIndex).Cells(0).Value = dr.Item(0).ToString
                rowIndex += 1
            End While
            con.Close()

            ' Add 16 rows to the DataGridView
            'DataGridView1.Rows.Add(15)

            '' Populate the DataGridView with sample data
            '' You can replace this code with your own logic to populate the table
            'For row As Integer = 0 To 7
            '    DataGridView1.Rows(row).Cells(0).Value = "A" & (row + 1)
            'Next

            'For row As Integer = 8 To 15
            '    DataGridView1.Rows(row).Cells(0).Value = "B" & (row - 7)
            'Next
            popmap()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub popmap()
        Try
            Dim con As New SqlConnection(Module1.str)
            Dim com As New SqlCommand("select app_id,fname,FORMAT(checkin_date,'dd/MM/yyyy') as [Ch-in Date],FORMAT(checkout_date,'dd/MM/yyyy') as [Ch-Out Date],lastname from reservation", con)
            con.Open()
            Dim dr As SqlDataReader = com.ExecuteReader()
            Dim rowIndex As Integer = 0
            Dim columnIndex As Integer = 1
            'every read it loop the table
            While dr.Read()
                'enter row

                While rowIndex < DataGridView1.Rows.Count - 1
                    Dim currentRow As DataGridViewRow = DataGridView1.Rows(rowIndex)
                    ' Access the cells or perform actions with the row data
                    'read cell room name
                    Dim cellValue As String = currentRow.Cells(0).Value.ToString()

                    'compare to app_id
                    If dr.Item(0) = cellValue Then
                        While columnIndex < DataGridView1.Columns.Count

                            Dim columnHeader As String = DataGridView1.Columns(columnIndex).HeaderText.ToString()


                            Dim format() = {"dd/MM/yyyy"}
                            Dim indate As Date = Date.ParseExact(dr.Item(2), format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                            Dim outdate As Date = Date.ParseExact(dr.Item(3), format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)

                            Dim convertedDate As Date = Date.ParseExact(columnHeader, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                            If ((indate < convertedDate Or indate = convertedDate) And (outdate > convertedDate Or outdate = convertedDate)) Then
                                DataGridView1.Rows(rowIndex).Cells(columnIndex).Value = dr.Item(1) & "   " & dr.Item(4)
                            End If

                            ' compare in and out date with the columnHeader
                            columnIndex += 1
                        End While
                    End If
                    rowIndex += 1
                End While
                rowIndex = 0
                columnIndex = 1
            End While
            con.Close()


            paintmap()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub paintmap()
        Try
            Dim rowIndex As Integer = 0
            Dim columnIndex As Integer = 1
            Dim flag As Integer
            Dim primary As Color
            Dim secondary As Color
            While rowIndex < DataGridView1.Rows.Count - 1
                Dim pre As String = ""
                Dim currentRow As DataGridViewRow = DataGridView1.Rows(rowIndex)


                If rowIndex Mod 2 = 0 Then
                    primary = Color.Lime
                    secondary = Color.PaleVioletRed
                Else
                    primary = Color.FromArgb(0, 162, 232)
                    secondary = Color.Orange
                End If



                While columnIndex < DataGridView1.Columns.Count

                    Dim cell As DataGridViewCell = DataGridView1.Rows(rowIndex).Cells(columnIndex)


                    If columnIndex = 1 Then
                        If cell.Value = "" Then
                            cell.Style.BackColor = Color.White
                            flag = 0
                        Else
                            If cell.Value <> "" Then
                                cell.Style.BackColor = primary
                                flag = 0
                            End If
                        End If



                    Else
                        If cell.Value = "" Then
                            cell.Style.BackColor = Color.White
                        Else
                            If cell.Value <> "" And cell.Value = pre And flag = 0 Then
                                cell.Style.BackColor = primary
                                flag = 0
                            Else
                                If cell.Value <> "" And cell.Value = pre And flag = 1 Then
                                    cell.Style.BackColor = secondary
                                    flag = 1
                                Else
                                    If cell.Value <> "" And cell.Value <> pre And flag = 0 Then
                                        cell.Style.BackColor = secondary
                                        flag = 1
                                    Else
                                        If cell.Value <> "" And cell.Value <> pre And flag = 1 Then
                                            cell.Style.BackColor = primary
                                            flag = 0

                                        End If

                                    End If

                                End If
                            End If
                        End If

                    End If
                    If DataGridView1.Rows(rowIndex).Cells(columnIndex).Value = "" Then

                    End If
                    pre = DataGridView1.Rows(rowIndex).Cells(columnIndex).Value
                    columnIndex += 1
                End While
                columnIndex = 1
                rowIndex += 1
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        popviewbydate()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Assuming you have a DataGridView control named "dataGridView1"

            ' Create a list to store the first and last selected cell data
            Dim selectedCellData As New List(Of Tuple(Of Point, String))

            ' Check if there are any selected cells
            If DataGridView1.SelectedCells.Count > 0 Then
                Dim firstCell As DataGridViewCell = DataGridView1.SelectedCells(0)
                Dim lastCell As DataGridViewCell = DataGridView1.SelectedCells(DataGridView1.SelectedCells.Count - 1)

                ' Get the column indices and row indices of the first and last cells
                Dim firstColumnIndex As Integer = firstCell.ColumnIndex
                Dim firstRowIndex As Integer = firstCell.RowIndex
                Dim lastColumnIndex As Integer = lastCell.ColumnIndex
                Dim lastRowIndex As Integer = lastCell.RowIndex

                ' Get the column header text of the first and last cells
                Dim cell As DataGridViewRow = DataGridView1.CurrentRow
                Dim lastColumnHeader As String = DataGridView1.Columns(firstColumnIndex).HeaderText
                Dim firstColumnHeader As String = DataGridView1.Columns(lastColumnIndex).HeaderText
                Dim roomname As String = cell.Cells(0).Value


                Dim format() = {"dd/MM/yyyy"}
                frdate = Date.ParseExact(firstColumnHeader, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                lsdate = Date.ParseExact(lastColumnHeader, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)

                If frdate > lsdate Then
                    MsgBox("Please select from left to right")
                Else
                    switch(reservation)
                    reservation.DateTimePicker1.Value = frdate
                    reservation.DateTimePicker2.Value = lsdate

                    If (roomname = "A1" Or roomname = "A2" Or roomname = "A3" Or roomname = "A4" Or roomname = "A5" Or roomname = "A6") Then
                        reservation.ComboBox4.SelectedItem = "A"
                    Else
                        reservation.ComboBox4.SelectedItem = "B"
                    End If
                    reservation.ComboBox3.SelectedItem = roomname
                End If
            End If
            populatecrdg()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Assuming you have a DataGridView control named "dataGridView1"

            ' Create a list to store the first and last selected cell data
            Dim selectedCellData As New List(Of Tuple(Of Point, String))

            ' Check if there are any selected cells
            If DataGridView1.SelectedCells.Count > 0 Then
                Dim firstCell As DataGridViewCell = DataGridView1.SelectedCells(0)
                Dim lastCell As DataGridViewCell = DataGridView1.SelectedCells(DataGridView1.SelectedCells.Count - 1)

                ' Get the column indices and row indices of the first and last cells
                Dim firstColumnIndex As Integer = firstCell.ColumnIndex
                Dim firstRowIndex As Integer = firstCell.RowIndex
                Dim lastColumnIndex As Integer = lastCell.ColumnIndex
                Dim lastRowIndex As Integer = lastCell.RowIndex

                ' Get the column header text of the first and last cells
                Dim cell As DataGridViewRow = DataGridView1.CurrentRow
                Dim lastColumnHeader As String = DataGridView1.Columns(firstColumnIndex).HeaderText
                Dim firstColumnHeader As String = DataGridView1.Columns(lastColumnIndex).HeaderText
                Dim roomname As String = cell.Cells(0).Value



                Dim format() = {"dd/MM/yyyy"}
                frdate = Date.ParseExact(firstColumnHeader, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                lsdate = Date.ParseExact(lastColumnHeader, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)



                If frdate > lsdate Then
                    MsgBox("Please select from left to right")
                Else


                    Dim con As New SqlConnection(Module1.str)
                    Dim res_id As Integer

                    Dim com3 As New SqlCommand("select reservation_id from reservation where checkin_date = @a  and app_id=@c", con)
                    com3.Parameters.Add("@a", SqlDbType.Date).Value = frdate
                    com3.Parameters.Add("@c", SqlDbType.VarChar).Value = roomname
                    con.Open()
                    Dim dr As SqlDataReader = com3.ExecuteReader()
                    While dr.Read()
                        res_id = dr.Item(0)
                    End While

                    con.Close()


                    Dim com2 As New SqlCommand("delete from invoice where rid=@a", con)
                    com2.Parameters.Add("@a", SqlDbType.Int).Value = CInt(res_id)
                    con.Open()
                    com2.ExecuteNonQuery()
                    con.Close()

                    Dim com As New SqlCommand("delete from reservation where reservation_id=@a", con)
                    com.Parameters.Add("@a", SqlDbType.Int).Value = res_id
                    con.Open()
                    com.ExecuteNonQuery()

                    con.Close()
                End If

            End If
            popview()
            populatecrdg()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Module1.returnres()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        popview()
    End Sub
End Class


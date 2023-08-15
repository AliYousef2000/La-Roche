
Imports System.Data.SqlClient
Public Class Form2

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Public count As Integer
    Public res_id As Integer
    Public appname As String = ""

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Visible = True
        apartment.DataGridView1.RowTemplate.Height = 35
        reservation.DataGridView2.RowTemplate.Height = 35
        blacklist.DataGridView3.RowTemplate.Height = 35
        'Populateapcb()
        Populateblcb()
        populatecrdg()
        reservation.ComboBox4.SelectedIndex = 0
        'apartment.ComboBox5.SelectedIndex = 0
        apartment.ComboBox6.SelectedIndex = 0
        Fees.ComboBox7.ResetText()
        reservation.DateTimePicker1.Value = Date.Today
        reservation.DateTimePicker2.Value = Date.Today.AddDays(1)
        Populatedgv()
        calcprice()
        blacklist.populateblack()
        switch(apartment)
        dgv1paint()
    End Sub


    Public Sub ViewPrintPreview()
        Dim printDocument As New Printing.PrintDocument()

        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage

        ' Create a PrintPreviewDialog and assign the print document to it
        Dim printPreviewDialog As New PrintPreviewDialog()
        printPreviewDialog.Document = printDocument

        ' Show the print preview dialog
        printPreviewDialog.ShowDialog()
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        ' Hide the form's background by setting DoubleBuffered property to True
        Me.DoubleBuffered = True

        ' Create a Bitmap to draw the panel contents
        Dim bitmap As New Bitmap(Panel1.Width, Panel1.Height)
        Panel1.DrawToBitmap(bitmap, New Rectangle(0, 0, Panel1.Width, Panel1.Height))

        ' Draw the bitmap onto the print page
        e.Graphics.DrawImage(bitmap, e.MarginBounds.Location)

        ' Specify if there are more pages to print
        e.HasMorePages = False

        ' Restore the form's background by setting DoubleBuffered property back to False
        Me.DoubleBuffered = False
    End Sub






    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        switch(Fees)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        switch(apartment)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        switch(reservation)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        switch(database)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        switch(blacklist)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ' Perform any cleanup or saving operations if needed
        ' Then restart the application
        Application.Restart()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        switch(cash)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        switch(order)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        switch(client)
    End Sub
End Class
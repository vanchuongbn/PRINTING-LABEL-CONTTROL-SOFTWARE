Imports System.Data.SqlClient
Module ModVar
    Public Strconn_Mark As String = "SERVER = 172.16.1.70;database=REPRINTLABEL;uid=qa;pwd=Uhan@123"
    'Public Strconn As String = "SERVER=172.16.1.70;database=dbFMAN;uid=sa;pwd=p@ssw0rd" ' 
    Public str_path As String = Application.ExecutablePath
    Public Username As String = "None"
    Public Username_Public As String = ""
    Public Flash As Boolean = False
    Public ModelID As String = ""
    Public PlanID_Public As String = ""
    Public ShiftID_Public As String = ""
    Public ModelID_Public As String = ""
    Public conn As SqlConnection
    Public NX As String = ""
    Public Po As String = ""
    Public Insp_Id As String = ""
    Public f_cust As Integer
End Module

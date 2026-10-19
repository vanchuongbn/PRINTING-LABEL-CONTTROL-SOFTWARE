
Imports System.Data.SqlClient
Module Module1
    Public LoginUser As String
    Public Section As String
    Public ControlNo As String
    Public Strconn As String = "SERVER = 172.16.1.70;database=REPRINTLABEL;uid=qa;pwd=Uhan@123"
    Public Strconn1 As String = "Server=HRSVR\SQL2005;database=dbPackingDescriptions;uid=sa;pwd=p@ssw0rd"
    Public Strconn2 As String = "SERVER = 172.16.1.70;database=QUALITYDATASYSTEM;uid=sa;pwd=p@ssw0rd"
    Public Strconn4 As String = "SERVER = 172.16.1.70;database=UnigenDataCollection1;uid=production;pwd=Ughn2012"
    Public Strconn5 As String = "SERVER = 172.16.1.66;database=UnigenDataCollection;uid=qa;pwd=Ughn@123"
    Public Strconn6 As String = "SERVER = 172.168.1.70;database=UnigenTrax;uid=Hanio_user;pwd=Hanio@123"
    Public Strconn7 As String = "SERVER=172.16.1.66;database=UnigenTrax;uid=sa;pwd=p@ssw0rd"


    'Public Strconn_local As String = "Server=.\SQLEXPRESS;database=REPRINTLABEL;uid=sa;pwd=ughn@123"
    'Public Strconn1 As String = "Server=UHAN-093\SQLEXPRESS;database=dbPackingDescriptions;uid=sa;pwd=12345678"
    'Public Strconn2 As String = "Server=UHAN-093\SQLEXPRESS;database=QUALITYDATASYSTEM;uid=sa;pwd=12345678"

End Module

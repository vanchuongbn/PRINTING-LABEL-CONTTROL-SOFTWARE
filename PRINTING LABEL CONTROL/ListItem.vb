Option Strict On
Option Explicit On
Public Class ListItem
    Private mvalue As String
    Private mID As String
    Public Sub New(ByVal strvalue As String, ByVal intID As String)
        mvalue = strvalue
        mID = intID
    End Sub

    Public Sub New()
        mvalue = ""
        mID = ""
    End Sub
    Property ID() As String
        Get
            Return mID
        End Get
        Set(ByVal Value As String)
            mID = Value
        End Set
    End Property
    Property ID1() As String
        Get
            Return mID
        End Get
        Set(ByVal Value As String)
            mID = Value
        End Set
    End Property
    Property value() As String
        Get
            Return mvalue
        End Get
        Set(ByVal Value As String)
            mvalue = Value
        End Set
    End Property

    Public Overrides Function tostring() As String
        Return mvalue
    End Function
End Class

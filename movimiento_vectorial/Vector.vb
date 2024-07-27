Public Class Vector2D
    Public x As Double = 0
    Public y As Double = 0
    Private posicion As SizeF

    Sub New(ByRef x As Double, ByRef y As Double)
        Me.x = x
        Me.y = y
    End Sub

    Function get_posicion() As SizeF
        posicion = New SizeF(Me.x, Me.y)
        Return posicion
    End Function

    Function normalizar() As Vector2D
        Dim v As Vector2D = New Vector2D(0, 0)
        Dim esc As Double = Math.Sqrt((Me.x * Me.x) + (Me.y * Me.y))

        If esc <> 0 Then
            v.x = Me.x / esc
            v.y = Me.y / esc
        End If

        Return v
    End Function

    Shared Operator *(ByVal v1 As Vector2D, ByVal esc As Integer) As Vector2D
        Dim v2 As Vector2D = New Vector2D(0, 0)
        v2.x = v1.x * esc
        v2.y = v1.y * esc
        Return v2
    End Operator

    Shared Operator +(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Vector2D
        Dim v3 As Vector2D = New Vector2D(0, 0)
        v3.x = v1.x + v2.x
        v3.y = v1.y + v2.y
        Return v3
    End Operator

    Shared Operator -(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Vector2D
        Dim v3 As Vector2D = New Vector2D(0, 0)
        v3.x = v1.x - v2.x
        v3.y = v1.y - v2.y
        Return v3
    End Operator

    Shared Operator <>(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Boolean
        Return Not (v1.x = v2.x And v1.y = v2.y)
    End Operator

    Shared Operator =(ByVal v1 As Vector2D, ByVal v2 As Vector2D) As Boolean
        Return (v1.x = v2.x And v1.y = v2.y)
    End Operator
End Class

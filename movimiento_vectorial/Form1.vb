Public Class Form1
    Private v1 As New Vector2D(0, 0)
    Private v2 As New Vector2D(0, 0)
    Private v3 As New Vector2D(0, 0)
    Private movs(4) As Int16
    Private velocidad As Double = 0
    Private delantera As Int16 = 0
    Private mov As Int16 = 0
    Private var As Double = 0.1
    Private pj As New RectangleF(30, 30, 20, 20)
    Private graficos As Graphics
    Private entidades As New List(Of RectangleF)
    Private avanzando As Boolean = False
    Private reversa As Boolean = False

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        e.Handled = True
        funcionTecla(e.KeyCode, 1)
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        e.Handled = True
        funcionTecla(e.KeyCode, 0)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        v3.x = 0
        v3.y = 30
        graficos = Me.CreateGraphics

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        disVel()

        entidades.Add(pj)

        logica_bucle()
        dibujado(entidades)

        entidades.Clear()
        'depurador()
    End Sub

    Sub avanzar()
        aumVel()

        Dim vpj As Vector2D = New Vector2D(pj.Location.X, pj.Location.Y)
        Dim v4 As Vector2D = v1

        If v3 <> vpj Then
            v4 = vpj - v3
        End If
        v2.x = v4.normalizar.y
        v2.y = -v4.normalizar.x
        v1 = v4.normalizar
        v2 += (v1 + v1 * velocidad).normalizar * 4 * mov

        If delantera = -1 And Not reversa Then
            v1 *= -0.9
            reversa = True
        ElseIf delantera = 1 And reversa Then
            reversa = False
            v1 *= -1
        End If

        If velocidad > 1 Then
            v3.x = pj.Location.X
            v3.y = pj.Location.Y
        End If
        pj.Location += ((v1 + (v2.normalizar * mov)).normalizar * velocidad).get_posicion
    End Sub

    Sub funcionTecla(ByVal tecla As Keys, ByVal estado As Byte)
        Select Case tecla
            Case Keys.W
                movs(0) = estado
                avanzando = True And estado
            Case Keys.S
                movs(1) = estado
                avanzando = True And estado
            Case Keys.A
                movs(2) = estado
            Case Keys.D
                movs(3) = estado
        End Select

        delantera = movs(0) - movs(1)
        mov = movs(2) - movs(3)
    End Sub

    Sub aumVel()
        If velocidad < 15 And avanzando Then
            velocidad += Math.Pow(1.01, 1 + var)
        End If
    End Sub

    Sub disVel()
        If velocidad > (var * 8) Then
            velocidad -= var * 8
        End If
    End Sub

    Sub depurador()
        Debug.Print(velocidad)
        graficos.DrawLine(Pens.Green, New Point(v2.get_posicion.Width + pj.Location.X, v2.get_posicion.Height + pj.Location.Y), New Point(v2.get_posicion.Width * 30 + pj.Location.X, v2.get_posicion.Height * 30 + pj.Location.Y))
        graficos.DrawLine(Pens.Red, New Point(v1.get_posicion.Width + pj.Location.X, v1.get_posicion.Height + pj.Location.Y), New Point(v1.get_posicion.Width * 30 + pj.Location.X, v1.get_posicion.Height * 30 + pj.Location.Y))
    End Sub

    Sub logica_bucle()
        If (pj.Location.X > 0) And (pj.Location.Y > 0) And ((pj.Location.X + pj.Size.Width) < Me.Size.Width) And ((pj.Location.Y + pj.Size.Height) < Me.Size.Height) Then
            avanzar()
        Else
            If delantera <> -1 Then
                velocidad = 1
                v1 = v1 * -1
                pj.Location += (v1 * 20).get_posicion
            Else
                velocidad = 1
                pj.Location -= (v1 * 10).get_posicion
                reversa = Not reversa
            End If
        End If
    End Sub

    Sub dibujado(ByRef entidades As List(Of RectangleF))
        graficos.Clear(Color.WhiteSmoke)
        graficos.DrawRectangles(Pens.Black, entidades.ToArray)
    End Sub
End Class
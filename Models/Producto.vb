Public Class Producto
    Private _idProducto As Integer
    Private _TipoProducto As String
    Private _Marca As String
    Private _Modelo As String
    Private _Precio As Decimal
    Private _Cantidad As Integer

    Public Sub New()
    End Sub

    Public Sub New(idProducto As Integer, tipoProducto As String, marca As String, modelo As String, precio As Decimal, cantidad As Integer)
        Me.IdProducto = idProducto
        Me.TipoProducto = tipoProducto
        Me.Marca = marca
        Me.Modelo = modelo
        Me.Precio = precio
        Me.Cantidad = cantidad
    End Sub

    Public Property IdProducto As Integer
        Get
            Return _idProducto
        End Get
        Set(value As Integer)
            _idProducto = value
        End Set
    End Property

    Public Property TipoProducto As String
        Get
            Return _TipoProducto
        End Get
        Set(value As String)
            _TipoProducto = value
        End Set
    End Property

    Public Property Marca As String
        Get
            Return _Marca
        End Get
        Set(value As String)
            _Marca = value
        End Set
    End Property

    Public Property Modelo As String
        Get
            Return _Modelo
        End Get
        Set(value As String)
            _Modelo = value
        End Set
    End Property

    Public Property Precio As Decimal
        Get
            Return _Precio
        End Get
        Set(value As Decimal)
            _Precio = value
        End Set
    End Property

    Public Property Cantidad As Integer
        Get
            Return _Cantidad
        End Get
        Set(value As Integer)
            _Cantidad = value
        End Set
    End Property
End Class

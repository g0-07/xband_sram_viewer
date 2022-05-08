Option Infer On

<AttributeUsage(AttributeTargets.Field)>
Public Class EndianAttribute
    Inherits Attribute

    Private privateEndianness As Endianness
    Public Property Endianness() As Endianness
        Get
            Return privateEndianness
        End Get
        Private Set(ByVal value As Endianness)
            privateEndianness = value
        End Set
    End Property

    Public Sub New(ByVal endianness As Endianness)
        Me.Endianness = endianness
    End Sub

End Class

Public Enum Endianness
    BigEndian
    LittleEndian
End Enum
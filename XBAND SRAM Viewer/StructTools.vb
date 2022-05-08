Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization

Public Module StructTools

    Public Sub RespectEndianness(type As Type, data() As Byte)
        Dim fields = type.GetFields().Where(Function(f) f.IsDefined(GetType(EndianAttribute), False)).Select(Function(f) New With {
            Key .Field = f,
            Key .Attribute = CType(f.GetCustomAttributes(GetType(EndianAttribute), False)(0), EndianAttribute),
            Key .Offset = Marshal.OffsetOf(type, f.Name).ToInt32()
        }).ToList()

        Try
            For Each field In fields
                If (field.Attribute.Endianness = Endianness.BigEndian AndAlso BitConverter.IsLittleEndian) OrElse (field.Attribute.Endianness = Endianness.LittleEndian AndAlso Not BitConverter.IsLittleEndian) Then
                    Array.Reverse(data, field.Offset, Marshal.SizeOf(field.Field.FieldType))
                End If
            Next field
        Catch ex As Exception

        End Try
    End Sub

    Public Function BytesToStruct(Of T As Structure)(rawData() As Byte) As T
        Dim result As T = CType(Nothing, T)
        RespectEndianness(GetType(T), rawData)
        Dim handle As GCHandle = GCHandle.Alloc(rawData, GCHandleType.Pinned)
        Try
            Dim rawDataPtr As IntPtr = handle.AddrOfPinnedObject()
            result = CType(Marshal.PtrToStructure(rawDataPtr, GetType(T)), T)
        Finally
            handle.Free()
        End Try
        Return result
    End Function

    Public Function StructToBytes(Of T As Structure)(data As T) As Byte()
        Dim rawData(Marshal.SizeOf(data) - 1) As Byte
        Dim handle As GCHandle = GCHandle.Alloc(rawData, GCHandleType.Pinned)
        Try
            Dim rawDataPtr As IntPtr = handle.AddrOfPinnedObject()
            Marshal.StructureToPtr(data, rawDataPtr, False)
        Finally
            handle.Free()
        End Try
        Return rawData
    End Function

    Public Function GetEnumMemberValue(Of T)(value As T) As String
        Return GetType(T).GetTypeInfo().DeclaredMembers.SingleOrDefault(Function(x) x.Name = value.ToString())?.GetCustomAttribute(Of EnumMemberAttribute)(False)?.Value
    End Function

    Public Function GetValue(parent As Object, fieldName As String) As Byte()
        Try
            Dim field As Reflection.FieldInfo = parent.GetType().GetField(fieldName, Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
            If IsArray(field.GetValue(parent)) Then
                Return field.GetValue(parent)
            Else
                Return BitConverter.GetBytes(field.GetValue(parent))
            End If
        Catch ex As Exception
            Return Array.Empty(Of Byte)()
        End Try
    End Function

    Public Function Search(src() As Byte, pattern() As Byte) As Integer
        Dim maxFirstCharSlot As Integer = src.Length - pattern.Length + 1
        For i As Integer = 0 To maxFirstCharSlot - 1
            If src(i) <> pattern(0) Then
                Continue For
            End If
            For j As Integer = pattern.Length - 1 To 1 Step -1
                If src(i + j) <> pattern(j) Then
                    Exit For
                End If
                If j = 1 Then
                    Return i
                End If
            Next j
        Next i
        Return -1
    End Function

End Module
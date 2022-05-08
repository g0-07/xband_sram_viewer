Imports System.Runtime.InteropServices

Public Class SramViewerForm

    Private Const applicationName As String = "XBAND SRAM Viewer"
    Private gamePatchBinary As New Dictionary(Of Integer, Byte())

    Private Sub LoadSram(filename As String)
        Try
            Dim rawData() As Byte = IO.File.ReadAllBytes(filename)

            CreateSubnode(Of SegaLowMem)(SramTreeView.Nodes, "SegaLowMem", rawData.Skip(0).Take(508).ToArray)
            CreateSubnode(Of SegaMidMem)(SramTreeView.Nodes, "SegaMidMem", rawData.Skip(Search(rawData.ToArray, {&HDE, &HAD, &HFE, &HED})).Take(16).ToArray)
            Dim segaHighMem As SegaHighMem = CreateSubnode(Of SegaHighMem)(SramTreeView.Nodes, "SegaHighMem", rawData.Skip(Search(rawData.ToArray, {&HBE, &HEF, &HFA, &HCE})).Take(40).ToArray)

            CreateSubnode(Of BoxIDReserved)(SramTreeView.Nodes, "BoxID", rawData.Skip(rawData.Length - 360).Take(360).ToArray)

            Dim blocksNode As TreeNode = SramTreeView.Nodes.Add("Blocks")
            Dim blocksPatchDBBoundary As Integer = segaHighMem.patchDBBoundary
            While blocksPatchDBBoundary <> 0 AndAlso blocksPatchDBBoundary <= 65535
                Dim blockData As Block = BytesToStruct(Of Block)(rawData.Skip(blocksPatchDBBoundary).Take(10).ToArray)
                blocksNode.Nodes.Add(blocksPatchDBBoundary, $"0x{Hex(blocksPatchDBBoundary)} - size: {blockData.logicalBlockSize} Bytes")
                If blockData.nextPtr <> 0 AndAlso blockData.nextPtr <= 65535 Then
                    blocksPatchDBBoundary = blockData.nextPtr
                Else
                    blocksPatchDBBoundary = 0
                End If
            End While

            Dim databaseNode As TreeNode = SramTreeView.Nodes.Add("Database")
            Dim databaseTypeListPtr As Integer = segaHighMem.typeListPtr
            For i = 1 To 109 ' 99 entries + 10 spare
                Dim databaseTN As DBTypeNode = BytesToStruct(Of DBTypeNode)(rawData.Skip(databaseTypeListPtr).Take(8).ToArray)
                databaseTypeListPtr += Marshal.SizeOf(databaseTN)
                Dim databaseDBType As DBTypes = CType(GetValue(databaseTN, "type")(0), DBTypes)
                Dim databaseDBTypeNode As TreeNode = databaseNode.Nodes.Add($"{databaseDBType}")
                Dim dbTypeNodeType As Type = GetType(DBTypeNode)
                For Each dbTypeNodeFieldInfo As System.Reflection.FieldInfo In dbTypeNodeType.GetFields()
                    Dim databaseContentNode As TreeNode = databaseDBTypeNode.Nodes.Add(dbTypeNodeFieldInfo.Name, $"{dbTypeNodeFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(databaseTN, dbTypeNodeFieldInfo.Name).Reverse.ToArray)}")
                    If dbTypeNodeFieldInfo.Name = "listPtr" Then
                        Dim databaseContentList As Integer = CInt(BitConverter.ToInt32(GetValue(databaseTN, dbTypeNodeFieldInfo.Name)))
                        While databaseContentList <> 0 AndAlso databaseContentList <= 65535
                            databaseDBTypeNode.BackColor = Color.LightGreen
                            Dim addressNode As TreeNode = databaseContentNode.Nodes.Add($"0x{Convert.ToHexString(BitConverter.GetBytes(databaseContentList).Reverse.ToArray)}")
                            Dim dbListData As DBListNode = BytesToStruct(Of DBListNode)(rawData.Skip(databaseContentList).Take(6).ToArray)
                            Dim blockData As Block = BytesToStruct(Of Block)(rawData.Skip(databaseContentList - 10).Take(10).ToArray)
                            If blockData.logicalBlockSize > 0 Then
                                dbListData.data = rawData.Skip(databaseContentList + 6).Take(blockData.logicalBlockSize - 6).Reverse.ToArray
                            End If
                            Dim dbListNodeType As Type = GetType(DBListNode)
                            Dim dataDetailNode As New TreeNode
                            For Each dbListNodeFieldInfo As System.Reflection.FieldInfo In dbListNodeType.GetFields()
                                If GetValue(dbListData, dbListNodeFieldInfo.Name).Length > 30 Then
                                    dataDetailNode = addressNode.Nodes.Add(dbListNodeFieldInfo.Name, $"{dbListNodeFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(dbListData, dbListNodeFieldInfo.Name).Reverse.Take(30).ToArray)} ...")
                                Else
                                    dataDetailNode = addressNode.Nodes.Add(dbListNodeFieldInfo.Name, $"{dbListNodeFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(dbListData, dbListNodeFieldInfo.Name).Reverse.ToArray)}")
                                End If
                            Next

                            If databaseDBType.ToString = "kStringType" Then
                                Dim xyString As XyString = BytesToStruct(Of XyString)(dbListData.data.Reverse.Take(4).ToArray)
                                xyString.cString = dbListData.data.Take(dbListData.data.Length - 4).ToArray
                                Dim xyStringType As Type = GetType(XyString)
                                For Each xyStringFieldInfo As System.Reflection.FieldInfo In xyStringType.GetFields()
                                    dataDetailNode.Nodes.Add(xyStringFieldInfo.Name, $"{xyStringFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(xyString, xyStringFieldInfo.Name).Reverse.ToArray)}")
                                Next
                                dataDetailNode.Nodes.Add($"text = {System.Text.Encoding.ASCII.GetString(xyString.cString.Reverse.ToArray)}")
                            ElseIf databaseDBType.ToString = "kGamePatchType" Then
                                Dim patchBlockHeader As PatchBlockHeader = BytesToStruct(Of PatchBlockHeader)(dbListData.data.Reverse.Take(20).ToArray)
                                Dim patchBlockHeaderType As Type = GetType(PatchBlockHeader)
                                For Each patchBlockHeaderFieldInfo As System.Reflection.FieldInfo In patchBlockHeaderType.GetFields()
                                    Dim patchNode As TreeNode = dataDetailNode.Nodes.Add(patchBlockHeaderFieldInfo.Name,
                                        $"{patchBlockHeaderFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(patchBlockHeader, patchBlockHeaderFieldInfo.Name).Reverse.ToArray)}" &
                                        $"{If(patchBlockHeaderFieldInfo.Name = "gameID" AndAlso GetEnumMemberValue(CType(patchBlockHeader.gameID, KnownGameIds)) IsNot Nothing, " (" & GetEnumMemberValue(CType(patchBlockHeader.gameID, KnownGameIds)) & ")", "")}")
                                    If patchBlockHeaderFieldInfo.Name = "gameID" Then
                                        patchNode.ToolTipText = patchBlockHeader.gameID.ToString
                                        gamePatchBinary.Add(patchBlockHeader.gameID, dbListData.data.Reverse.ToArray)
                                    End If
                                Next
                            ElseIf databaseDBType.ToString = "kDialogType" Then
                                Dim cpDialog As CPDialog = BytesToStruct(Of CPDialog)(dbListData.data.Reverse.Take(9).ToArray)
                                cpDialog.message = dbListData.data.Take(dbListData.data.Length - 9).ToArray
                                Dim cpDialogType As Type = GetType(CPDialog)
                                For Each cpDialogFieldInfo As System.Reflection.FieldInfo In cpDialogType.GetFields()
                                    dataDetailNode.Nodes.Add(cpDialogFieldInfo.Name, $"{cpDialogFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(cpDialog, cpDialogFieldInfo.Name).Reverse.ToArray)}")
                                Next
                                dataDetailNode.Nodes.Add($"text = {System.Text.Encoding.ASCII.GetString(cpDialog.message.Reverse.ToArray)}")
                            Else
                                addressNode.Nodes.Add($"text = {System.Text.Encoding.ASCII.GetString(dbListData.data.Reverse.ToArray)}")
                                addressNode.Nodes.Add($"size = {dbListData.data.Length}")
                            End If

                            If dbListData.nextPtr <> 0 AndAlso dbListData.nextPtr <= 65535 Then
                                databaseContentList = dbListData.nextPtr
                            Else
                                databaseContentList = 0
                            End If
                        End While
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, applicationName)
        End Try
    End Sub

    Private Function CreateSubnode(Of T As Structure)(parentNode As TreeNodeCollection, subnodeText As String, subnodeBytes As Byte()) As T
        If subnodeText IsNot Nothing Then parentNode = parentNode.Add(subnodeText).Nodes
        Dim subnodeData As T = BytesToStruct(Of T)(subnodeBytes)
        Dim subnodeType As Type = GetType(T)
        For Each subnodeFieldInfo As System.Reflection.FieldInfo In subnodeType.GetFields()
            Select Case subnodeFieldInfo.FieldType
                Case GetType(PhoneNumber)
                    CreateSubnode(Of PhoneNumber)(parentNode, subnodeFieldInfo.Name, StructToBytes(Of PhoneNumber)(subnodeFieldInfo.GetValue(subnodeData)))
                Case GetType(BoxIdentification)
                    CreateSubnode(Of BoxIdentification)(parentNode, subnodeFieldInfo.Name, StructToBytes(Of BoxIdentification)(subnodeFieldInfo.GetValue(subnodeData)))
                Case GetType(BoxSerialNumber)
                    CreateSubnode(Of BoxSerialNumber)(parentNode, subnodeFieldInfo.Name, StructToBytes(Of BoxSerialNumber)(subnodeFieldInfo.GetValue(subnodeData)))
                Case GetType(XBANDCard)
                    CreateSubnode(Of XBANDCard)(parentNode, subnodeFieldInfo.Name, StructToBytes(Of XBANDCard)(subnodeFieldInfo.GetValue(subnodeData)))
                Case GetType(String)
                    parentNode.Add(subnodeFieldInfo.Name, $"{subnodeFieldInfo.Name} = {subnodeFieldInfo.GetValue(subnodeData)}")
                Case GetType(Byte)
                    parentNode.Add(subnodeFieldInfo.Name, $"{subnodeFieldInfo.Name} = 0x{CInt(subnodeFieldInfo.GetValue(subnodeData)).ToString("X").PadLeft(2, "0"c)}")
                Case Else
                    parentNode.Add(subnodeFieldInfo.Name, $"{subnodeFieldInfo.Name} = 0x{Convert.ToHexString(GetValue(subnodeData, subnodeFieldInfo.Name).Reverse.ToArray)}")
            End Select
        Next
        Return subnodeData
    End Function

    Private Sub CopyToClipboard_Click(sender As Object, e As EventArgs) Handles CopyToClipboard.Click
        Clipboard.SetText(SramTreeView.SelectedNode.Text)
    End Sub

    Private Sub ExportGamePatch_Click(sender As Object, e As EventArgs) Handles ExportGamePatch.Click
        SaveDialog.FileName = SramTreeView.SelectedNode.Text.Replace("gameID = ", "").Replace(" ", "_")
        SaveDialog.Title = "Export GamePatch ..."
        If SaveDialog.ShowDialog = DialogResult.OK Then
            Try
                IO.File.WriteAllBytes(SaveDialog.FileName, (New Byte() {&H3}).Concat(gamePatchBinary(SramTreeView.SelectedNode.ToolTipText)).ToArray)
                MsgBox($"GamePatch successfully saved.", MsgBoxStyle.Information, applicationName)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, applicationName)
            End Try
        End If
    End Sub

    Private Sub ContextMenu_Paint(sender As Object, e As PaintEventArgs) Handles ContextMenu.Paint
        If SramTreeView.SelectedNode.Text.StartsWith("gameID =") Then
            ExportGamePatch.Visible = True
        Else
            ExportGamePatch.Visible = False
        End If
    End Sub

    Private Sub SramTreeView_MouseUp(sender As Object, e As MouseEventArgs) Handles SramTreeView.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim ClickPoint As Point = New Point(e.X, e.Y)
            Dim ClickNode As TreeNode = SramTreeView.GetNodeAt(ClickPoint)
            If ClickNode Is Nothing Then
                Return
            End If
            Dim ScreenPoint As Point = SramTreeView.PointToScreen(ClickPoint)
            Dim FormPoint As Point = Me.PointToClient(ScreenPoint)
            SramTreeView.SelectedNode = ClickNode
            ContextMenu.Show(Me, FormPoint)
        End If
    End Sub

    Private Sub HandleDragDrop(sender As Object, e As DragEventArgs) Handles DropLabel.DragDrop, SramTreeView.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        If IO.File.Exists(files(0)) Then
            gamePatchBinary.Clear()
            SramTreeView.Nodes.Clear()
            DropLabel.Visible = False
            LoadSram(files(0))
            Me.Text = $"{applicationName} - {files(0)}"
        End If
    End Sub

    Private Sub HandleDragEnter(sender As Object, e As DragEventArgs) Handles DropLabel.DragEnter, SramTreeView.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

End Class
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization

Public Module XbandStructures
    Enum KnownGameIds
        <EnumMember(Value:="Mortal Kombat")>
        mk = &HAB6348E9
        <EnumMember(Value:="Mortal Kombat II")>
        mk2 = &HC4CDDF0C
        <EnumMember(Value:="Mortal Kombat 3")>
        mk3 = &H6D14EB41
        <EnumMember(Value:="NBA Jam (old)")>
        jam1 = &HE30C296E
        <EnumMember(Value:="NBA Jam (rev2)")>
        jam2 = &H39677BDB
        <EnumMember(Value:="NHL Hockey '95")>
        nhl95 = &H8F6B9F70
        <EnumMember(Value:="NHL '96")>
        nhl96 = &HAFC0CE39
        <EnumMember(Value:="Madden '95")>
        mad95 = &H31ED8123
        <EnumMember(Value:="Madden '96")>
        mad96 = &H4D402D90
        <EnumMember(Value:="NBA Live '95")>
        nlv95 = &H192660
        <EnumMember(Value:="Super Street Fighter II)")>
        ssf2 = &H4D1C4E1D
        <EnumMember(Value:="Ballz")>
        ballz = &H67A218F
        <EnumMember(Value:="Primal Rage")>
        primrage = &HC6906E52
        <EnumMember(Value:="Weapon Lord")>
        wlord = &HBF33EFC7
        <EnumMember(Value:="FIFA Soccer '95")>
        fif95 = &H433E2840
    End Enum

    Enum DBTypes
        kReservedLow0 = 0
        kReservedLow1 = 1
        kReservedLow2 = 2
        kReservedLow3 = 3
        kReservedLow4 = 4
        kReservedLow5 = 5
        kReservedLow6 = 6
        kReservedLow7 = 7
        kReservedLow8 = 8
        kRAMIconType = 9
        kGamePatchType = 10
        kOtherNews = 11
        kDeferredDialogType = 12
        kAboutString = 13
        kReservedMid0 = 14
        kReservedMid1 = 15
        kReservedMid2 = 16
        kReservedMid3 = 17
        kReservedMid4 = 18
        kReservedMid5 = 19
        kReservedMid6 = 20
        kReservedMid7 = 21
        kSecretPasswords = 22
        kDailyNews = 23
        kNGPType = 24
        kScreenLEDAnimation = 25
        kSessionType = 26
        kStreamType = 27
        kSendQType = 28
        kCreditsDBType = 29
        kMailInBoxType0 = 30
        kMailInBoxType1 = 31
        kMailInBoxType2 = 32
        kMailInBoxType3 = 33
        kIconDescType = 34
        kIconBitMapType = 35
        kPersonificationType = 36
        kUserRankings = 37
        kScreenStateTableType = 38
        kScreenLayout = 39
        kKeyboardEntryLayout = 40
        kScreenGetDispatchType = 41
        kDialogTemplateType = 42
        kDialogType = 43
        kControlLayout = 44
        kObjectDesc = 45
        kDITLProcType = 46
        kSoundFXType = 47
        kSoundBGMType = 48
        kStringType = 49
        kClutType = 50
        kCoordinateType = 51
        kTestUserID = 52
        kNewsForm = 53
        kRestrictionsDBType = 54
        kAddressBookType0 = 55
        kAddressBookType1 = 56
        kAddressBookType2 = 57
        kAddressBookType3 = 58
        kAnimation = 59
        kFontType = 60
        kBitMapType = 61
        kCursorType = 62
        kPathType = 63
        kAnimatorType = 64
        kVectorType = 65
        kDecompressorType = 66
        kDialogDismissalType = 67
        kSecretListType = 68
        kSecretSequenceType = 69
        kWriteableString = 70
        kProgressType = 71
        kBackgroundMusicRef = 72
        kSoundEffectRef = 73
        kMailOutBoxType0 = 74
        kMailOutBoxType1 = 75
        kMailOutBoxType2 = 76
        kMailOutBoxType3 = 77
        kTauntGameOverLayout = 78
        kRadioButtonLayout = 79
        kCLUTDataType = 80
        kPatternDictionaryType = 81
        kTextBoxJizzlerType = 82
        kButtonGraphicType = 83
        kButtonSequenceType = 84
        kButtonSequenceFXType = 85
        kMazeDataType = 86
        kButtonAnimationType = 87
        kScreenLayoutType = 88
        kMathDataType = 89
        kSoundEffectRefString = 90
        kCodeletsType = 91
        kXBandNewsDataType = 92
        kSecretProcType = 93
        kChortleProcType = 94
        kROMDBConstantsType = 95
        kConstantsDBType = 96
        kConnectSequenceType = 97
        kPhoneNumberType = 98
        kLastDBType = 99
        kSpacer100 = 100
        kSpacer101 = 101
        kSpacer102 = 102
        kSpacer103 = 103
        kSpacer104 = 104
        kSpacer105 = 105
        kSpacer106 = 106
        kSpacer107 = 107
        kSpacer108 = 108
    End Enum

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Structure XyString
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public xPos As Short
        <FieldOffset(2), Endian(Endianness.BigEndian)>
        Public yPos As Short
        <FieldOffset(4)>
        Public cString As Byte()
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Structure CPDialog
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public minimumTime As Short
        <FieldOffset(2), Endian(Endianness.BigEndian)>
        Public maximumTime As Short
        <FieldOffset(4)>
        Public template As Byte
        <FieldOffset(5)>
        Public variation As Byte
        <FieldOffset(6)>
        Public defaultItem As Byte
        <FieldOffset(7)>
        Public unique As Byte
        <FieldOffset(8)>
        Public secretID As Byte
        <FieldOffset(9)>
        Public message As Byte()
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=10, Pack:=1)>
    Structure Block
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public nextPtr As Integer 'Block
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public prevPtr As Integer 'Block
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public logicalBlockSize As UShort
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Structure DBListNode
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public nextPtr As Integer 'DBListNode
        <FieldOffset(4)>
        Public id As Byte
        <FieldOffset(5)>
        Public flags As Byte
        <FieldOffset(6)>
        Public data As Byte()
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=8, Pack:=1)>
    Structure DBTypeNode
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public listPtr As Integer 'DBListNode
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public crc As UShort
        <FieldOffset(6)>
        Public type As Byte
        <FieldOffset(7)>
        Public flags As Byte
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=20, Pack:=1)>
    Structure PatchBlockHeader
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public gameID As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public patchVersion As Integer
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public patchFlags As Integer
        <FieldOffset(12), Endian(Endianness.BigEndian)>
        Public compressedLength As Integer
        <FieldOffset(16), Endian(Endianness.BigEndian)>
        Public checkSum As Short
        <FieldOffset(18), Endian(Endianness.BigEndian)>
        Public expLength As Short
    End Structure

    <StructLayout(LayoutKind.Sequential, Size:=26, Pack:=1)>
    Structure PhoneNumber
        <FieldOffset(0)>
        Public scriptID As Byte
        <FieldOffset(1)>
        Public pad As Byte
        <FieldOffset(2), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public phoneNumber As String
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=8, Pack:=1)>
    Structure BoxSerialNumber
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public region As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public box As Integer
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=24, Pack:=1)>
    Structure XBANDCard
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public token As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public type As Short
        <FieldOffset(6), Endian(Endianness.BigEndian)>
        Public rechargesLeft As Short
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public creditsLeft As Short
        <FieldOffset(10)>
        Public problem As Byte
        <FieldOffset(11)>
        Public pad As Byte
        <FieldOffset(12), Endian(Endianness.BigEndian)>
        Public serialNumber As Integer
        <FieldOffset(16), Endian(Endianness.BigEndian)>
        Public cardPrefix As Integer
        <FieldOffset(20), Endian(Endianness.BigEndian)>
        Public reserved As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, Size:=352, Pack:=1)>
    Structure BoxIdentification
        <FieldOffset(0)>
        Public boxID As BoxSerialNumber
        <FieldOffset(8)>
        Public localAccessNumber1 As PhoneNumber
        <FieldOffset(34)>
        Public localAccessNumber2 As PhoneNumber
        <FieldOffset(60)>
        Public boxPhoneNumber As PhoneNumber
        <FieldOffset(86)>
        Public userValidationFlags1 As Byte
        <FieldOffset(87)>
        Public userValidationFlags2 As Byte
        <FieldOffset(88)>
        Public userValidationFlags3 As Byte
        <FieldOffset(89)>
        Public userValidationFlags4 As Byte
        <FieldOffset(90), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)>
        Public userName1 As String
        <FieldOffset(124), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)>
        Public userName2 As String
        <FieldOffset(158), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)>
        Public userName3 As String
        <FieldOffset(192), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)>
        Public userName4 As String
        <FieldOffset(226)>
        Public userROMIconID1 As Byte
        <FieldOffset(227)>
        Public userROMIconID2 As Byte
        <FieldOffset(228)>
        Public userROMIconID3 As Byte
        <FieldOffset(229)>
        Public userROMIconID4 As Byte
        <FieldOffset(230)>
        Public userCustomROMClutID1 As Byte
        <FieldOffset(231)>
        Public userCustomROMClutID2 As Byte
        <FieldOffset(232)>
        Public userCustomROMClutID3 As Byte
        <FieldOffset(233)>
        Public userCustomROMClutID4 As Byte
        <FieldOffset(234), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public userPassword1 As String
        <FieldOffset(242), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public userPassword2 As String
        <FieldOffset(250), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public userPassword3 As String
        <FieldOffset(258), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public userPassword4 As String
        <FieldOffset(266), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)>
        Public boxHometown As String
        <FieldOffset(300), Endian(Endianness.BigEndian)>
        Public problemToken As Integer
        <FieldOffset(304), Endian(Endianness.BigEndian)>
        Public validationToken As Integer
        <FieldOffset(308)>
        Public lastCard As XBANDCard
        <FieldOffset(332), Endian(Endianness.BigEndian)>
        Public checksum As UShort
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=360, Pack:=1)>
    Structure BoxIDReserved
        <FieldOffset(0)>
        Public real As BoxIdentification
        <FieldOffset(352)>
        Public hiddenSerial1 As BoxSerialNumber
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=508, Pack:=1)>
    Structure SegaLowMem
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public BADhiddenSerial2a As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public BADhiddenSerial2b As Integer
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public accessFault As Integer
        <FieldOffset(12), Endian(Endianness.BigEndian)>
        Public addressError As Integer
        <FieldOffset(16), Endian(Endianness.BigEndian)>
        Public illegal As Integer
        <FieldOffset(20), Endian(Endianness.BigEndian)>
        Public zeroDivide As Integer
        <FieldOffset(24), Endian(Endianness.BigEndian)>
        Public chk As Integer
        <FieldOffset(28), Endian(Endianness.BigEndian)>
        Public fTrap As Integer
        <FieldOffset(32), Endian(Endianness.BigEndian)>
        Public privilege As Integer
        <FieldOffset(36), Endian(Endianness.BigEndian)>
        Public trace As Integer
        <FieldOffset(40), Endian(Endianness.BigEndian)>
        Public lineA As Integer
        <FieldOffset(44), Endian(Endianness.BigEndian)>
        Public lineF As Integer
        <FieldOffset(48), Endian(Endianness.BigEndian)>
        Public reserved30 As Integer
        <FieldOffset(52), Endian(Endianness.BigEndian)>
        Public coprocessor As Integer
        <FieldOffset(56), Endian(Endianness.BigEndian)>
        Public formatErr As Integer
        <FieldOffset(60), Endian(Endianness.BigEndian)>
        Public uninitInterrupt As Integer
        <FieldOffset(64), Endian(Endianness.BigEndian)>
        Public dispatcher0 As Integer
        <FieldOffset(68), Endian(Endianness.BigEndian)>
        Public dispatcher1 As Integer
        <FieldOffset(72), Endian(Endianness.BigEndian)>
        Public globalsStack As Integer
        <FieldOffset(76), Endian(Endianness.BigEndian)>
        Public dispatchTable As Integer
        <FieldOffset(80), Endian(Endianness.BigEndian)>
        Public ramTest1 As Integer
        <FieldOffset(84), Endian(Endianness.BigEndian)>
        Public modemDebug As Integer
        <FieldOffset(88)>
        Public VDPBusy As Byte
        <FieldOffset(89)>
        Public soundEnabled_osEnabled_tap2Found_padBits As Byte
        <FieldOffset(90), Endian(Endianness.BigEndian)>
        Public reserved As Integer
        <FieldOffset(94), Endian(Endianness.BigEndian)>
        Public reservedPPC As Short
        <FieldOffset(96), Endian(Endianness.BigEndian)>
        Public spurious As Integer
        <FieldOffset(100), Endian(Endianness.BigEndian)>
        Public level1 As Integer
        <FieldOffset(104), Endian(Endianness.BigEndian)>
        Public external As Integer
        <FieldOffset(108), Endian(Endianness.BigEndian)>
        Public level3 As Integer
        <FieldOffset(112), Endian(Endianness.BigEndian)>
        Public horizontal As Integer
        <FieldOffset(116), Endian(Endianness.BigEndian)>
        Public level5 As Integer
        <FieldOffset(120), Endian(Endianness.BigEndian)>
        Public vertical As Integer
        <FieldOffset(124), Endian(Endianness.BigEndian)>
        Public level7 As Integer
        '<FieldOffset(128), Endian(Endianness.BigEndian)>
        'Public trap() As Integer
        <FieldOffset(192), Endian(Endianness.BigEndian)>
        Public registerBase As Integer
        <FieldOffset(196), Endian(Endianness.BigEndian)>
        Public controlRegisters As Integer
        <FieldOffset(200), Endian(Endianness.BigEndian)>
        Public ticks As Integer
        <FieldOffset(204), Endian(Endianness.BigEndian)>
        Public gameDispatcher0 As Integer
        <FieldOffset(208), Endian(Endianness.BigEndian)>
        Public gameDispatcher1 As Integer
        <FieldOffset(212), Endian(Endianness.BigEndian)>
        Public gameGlobalsStack As Integer
        <FieldOffset(216), Endian(Endianness.BigEndian)>
        Public gameDispatchTable As Integer
        <FieldOffset(220), Endian(Endianness.BigEndian)>
        Public gameParams As Integer
        <FieldOffset(224), Endian(Endianness.BigEndian)>
        Public gtReadController As Integer
        <FieldOffset(228), Endian(Endianness.BigEndian)>
        Public gtSendController As Integer
        <FieldOffset(232), Endian(Endianness.BigEndian)>
        Public gtReadModem As Integer
        <FieldOffset(236), Endian(Endianness.BigEndian)>
        Public ramOffset As Integer
        <FieldOffset(240), Endian(Endianness.BigEndian)>
        Public romOffset As Integer
        <FieldOffset(244), Endian(Endianness.BigEndian)>
        Public syncoParams As Integer
        <FieldOffset(248), Endian(Endianness.BigEndian)>
        Public reservedTrapMacDebug As Integer
        <FieldOffset(252), Endian(Endianness.BigEndian)>
        Public reservedTrapF As Integer
        <FieldOffset(256), Endian(Endianness.BigEndian)>
        Public reserved100 As Integer
        <FieldOffset(260), Endian(Endianness.BigEndian)>
        Public hiddenSerial2a As Integer
        <FieldOffset(264), Endian(Endianness.BigEndian)>
        Public hiddenSerial2b As Integer
        <FieldOffset(268), Endian(Endianness.BigEndian)>
        Public pad1 As Integer
        <FieldOffset(272), Endian(Endianness.BigEndian)>
        Public pad2 As UShort
        <FieldOffset(274), Endian(Endianness.BigEndian)>
        Public pad3 As UShort
        '<FieldOffset(276), Endian(Endianness.BigEndian)>
        'Public reservedSegaPatch() As Integer
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=16, Pack:=1)>
    Structure SegaMidMem
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public ramTest As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public vblGTSession As Integer
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public currentNetStatsPtr As Integer 'NetErrorRecord
        <FieldOffset(12), Endian(Endianness.BigEndian)>
        Public osGlobalsBase As Integer
    End Structure

    <StructLayout(LayoutKind.Explicit, Size:=40, Pack:=1)>
    Structure SegaHighMem
        <FieldOffset(0), Endian(Endianness.BigEndian)>
        Public ramTest2 As Integer
        <FieldOffset(4), Endian(Endianness.BigEndian)>
        Public osCrashCount As UShort
        <FieldOffset(6), Endian(Endianness.BigEndian)>
        Public dbCrashCount As UShort
        <FieldOffset(8), Endian(Endianness.BigEndian)>
        Public patchDBBoundary As Integer
        <FieldOffset(12), Endian(Endianness.BigEndian)>
        Public osUnstable As Short
        <FieldOffset(14), Endian(Endianness.BigEndian)>
        Public dbUnstable As Short
        <FieldOffset(16), Endian(Endianness.BigEndian)>
        Public bootAttempts As UShort
        <FieldOffset(18), Endian(Endianness.BigEndian)>
        Public patchVersion As Short
        <FieldOffset(20), Endian(Endianness.BigEndian)>
        Public patchChecksum As UShort
        <FieldOffset(22), Endian(Endianness.BigEndian)>
        Public osBootVector As Integer
        <FieldOffset(26), Endian(Endianness.BigEndian)>
        Public typeListPtr As Integer 'DBTypeNode
        <FieldOffset(30), Endian(Endianness.BigEndian)>
        Public dispatcherChecksum As Short
        <FieldOffset(32), Endian(Endianness.BigEndian)>
        Public dispatchTableSize As Short
        <FieldOffset(34), Endian(Endianness.BigEndian)>
        Public dispCheckEnabled As UShort
        <FieldOffset(36), Endian(Endianness.BigEndian)>
        Public padBits As UShort
        <FieldOffset(38), Endian(Endianness.BigEndian)>
        Public maxDBCrashCount As UShort
    End Structure

End Module
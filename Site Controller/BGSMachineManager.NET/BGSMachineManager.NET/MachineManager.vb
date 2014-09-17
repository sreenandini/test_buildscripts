Option Strict Off
Option Explicit On
Imports System.Threading
Imports BMC.Common.LogManagement
Imports System.Runtime.InteropServices
Imports BMC.Common.Utilities


<System.Runtime.InteropServices.ProgId("MachineManager_NET.MachineManager")> Public Class MachineManager
    Implements IDisposable


    '============================================================================================
    ' ManagerManager
    '--------------------------------------------------------------------------------------------
    ' This object is a wrapper for sector 201 calls for controlling SAS machines
    '
    ' Enable/Disable machine
    ' Enable/Disable coin mech ..
    ' to name but a few ..
    '
    '--------------------------------------------------------------------------------------------
    ' Revision History
    '
    ' 30/12/07  Siva      Created based on VB version of BGSMachineManager dll
    '============================================================================================

    Private m_lLastError As Integer
    Private m_lDatapak As Integer

    Private Const ERR_FAILEDCALL As Short = -1
    Private Const ERR_DATAPAKNOTEXISTS As Short = -2
    Private Const ERR_DATAPAKISZERO As Short = -3

    'Direct comexchange declarations
    Public WithEvents client As ComExchangeLib.ExchangeClient
    Public WithEvents admin As ComExchangeLib.IExchangeAdmin
    Private m_bACK As Boolean
    Private m_clientACK As Boolean
    Private m_ClientServerACK As Boolean
    Public m_SectorUpdate As String
    Public m_UDPList As Collection

    Public ReadOnly Property LastError() As Short
        Get
            LastError = m_lLastError
        End Get
    End Property

    Public Function ClearHandpayLock(ByVal lDatapak As Integer) As Boolean
        '
        '   4D 01 00
        '
        Dim bytArray(0) As Byte
        bytArray(0) = 0

        m_lDatapak = lDatapak
        '''Commented by Ram
        ' RefreshUDP()
        ClearHandpayLock = SendSector201_Comexchange("Clear Handpay Lock", &H94S, bytArray, 1)
    End Function

    Public Function DisableMachine(ByVal lDatapak As Integer) As Boolean
        '
        '   01 01 00
        '
        Dim bytArray(0) As Byte
        bytArray(0) = 0

        m_lDatapak = lDatapak
        'Init()
        'RefreshUDP()

        DisableMachine = SendSector201_Comexchange("Disable Machine", &H1S, bytArray, 0)
    End Function

    Public Function DisableNoteAcceptor(ByVal lDatapak As Integer) As Boolean
        '
        '   07 01 00
        '
        Dim bytArray(0) As Byte
        bytArray(0) = 0

        m_lDatapak = lDatapak
        ' RefreshUDP()
        DisableNoteAcceptor = SendSector201_Comexchange("Disable Note Acceptor", &H7S, bytArray, 0)
    End Function

    Public Function EnableMachine(ByVal lDatapak As Integer) As Boolean
        '
        '   02 01 00
        '
        Dim bytArray(0) As Byte
        bytArray(0) = 0

        m_lDatapak = lDatapak
        'Init()
        'RefreshUDP()
        EnableMachine = SendSector201_Comexchange("Enable Machine", &H2S, bytArray, 0)
    End Function

    Public Function EnableNoteAcceptor(ByVal lDatapak As Integer) As Boolean
        '
        '   06 01 00
        '
        Dim bytArray(0) As Byte
        bytArray(0) = 0

        m_lDatapak = lDatapak

        EnableNoteAcceptor = SendSector201_Comexchange("Enable Note Acceptor", &H6S, bytArray, 0)
    End Function

    Public Function AddUDPToList(ByVal lDatapak As Integer, ByVal PortNo As Integer, ByVal PollType As Integer, ByVal PollingID As Integer, Optional ByVal Server As String = "") As Boolean
        Dim bTimedOut As Boolean = False
        m_bACK = False
        m_clientACK = False
        LogManager.WriteLog("MachineManager:AddUDPToList:  Started for datapak: " & lDatapak.ToString(), LogManager.enumLogLevel.Info)

        Try
            If Server.Trim().Length = 0 Then
                Server = BMCRegistryHelper.GetRegKeyValue("Cashmaster\Exchange", "Default_Server_Ip")

                If Server.Trim().Length = 0 Then
                    Throw New Exception("No Server Found")
                End If
            End If

            admin.AddUDPToList(Server, PortNo, lDatapak, PollingID, PollType)
            Dim MsgID As Integer
            MsgID = admin.LastMessageID
            'Default_Server_IP
            Dim dStartTime As Date = Now
            If MsgID > 0 Then
                Do
                    Application.DoEvents()
                    If Now > dStartTime.AddSeconds(60) Then
                        bTimedOut = True
                        LogManager.WriteLog("AddUDPToList:  Timed out : " & lDatapak.ToString(), LogManager.enumLogLevel.Info)
                    End If
                Loop Until m_clientACK Or bTimedOut
            Else
                LogManager.WriteLog("AddUDPToList: Last MsgId is o or less. So unable to add to polling list  : " & lDatapak.ToString(), LogManager.enumLogLevel.Info)
                bTimedOut = True
            End If

            If m_bACK = True Then
                LogManager.WriteLog("AddUDPToList:  Success", LogManager.enumLogLevel.Info)
                AddUDPToList = True
                '  RefreshUDP()
            Else
                LogManager.WriteLog("AddUDPToList:  Failure", LogManager.enumLogLevel.Info)
                AddUDPToList = False
            End If
        Catch ex As Exception
            LogManager.WriteLog("AddUDPToList:  Failed due to " + ex.Message.ToString(), LogManager.enumLogLevel.Info)
        End Try

    End Function

    Public Function RemoveUDPFromList(ByVal lDatapak As Integer) As Boolean
        Dim bTimedOut As Boolean = False
        m_bACK = False
        m_clientACK = False
        LogManager.WriteLog("MachineManager:RemoveUDPFromList:  Started for datapak: " & lDatapak.ToString(), LogManager.enumLogLevel.Info)
        Try
            RefreshUDP()
            client.RemoveUDPFromList(lDatapak, 0)
            Dim MsgID As Integer
            MsgID = admin.LastMessageID

            Dim dStartTime As Date = Now
            Do
                Application.DoEvents()
                If Now > dStartTime.AddSeconds(60) Then
                    bTimedOut = True
                    LogManager.WriteLog("RemoveUDPFromList:  Timed out : " & lDatapak.ToString(), LogManager.enumLogLevel.Info)
                End If
            Loop Until m_clientACK Or bTimedOut

            If m_bACK = True Then
                LogManager.WriteLog("RemoveUDPFromList:  Success", LogManager.enumLogLevel.Info)
                RemoveUDPFromList = True
            Else
                LogManager.WriteLog("RemoveUDPFromList:  Failure", LogManager.enumLogLevel.Info)
                RemoveUDPFromList = False
            End If
        Catch ex As Exception
            LogManager.WriteLog("RemoveUDPFromList:  Failed due to " + ex.Message.ToString(), LogManager.enumLogLevel.Info)
        End Try

    End Function

    Public Function SendSector201_Comexchange(ByRef sCommandText As String, ByRef lCommand As Integer, ByRef bytArray() As Byte, ByRef lLength As Integer) As Boolean
        '
        ' send required command to connexus
        '
        m_bACK = False
        Dim oSector201 As New ComExchangeLib.Sector201Data
        Dim curdate As Date = Now
        Dim bTimedOut As Boolean = False
        Dim bool_ServerUpdate_Timeout As Boolean = False

        LogManager.WriteLog("Timeout for Server Update is :" + bool_ServerUpdate_Timeout.ToString(), LogManager.enumLogLevel.Info)

        Try
            LogManager.WriteLog("[" & sCommandText & "] " & "Started", LogManager.enumLogLevel.Info)
            m_lLastError = 0

            If m_lDatapak = 0 Then
                m_lLastError = ERR_DATAPAKISZERO
                Exit Function
            End If

            With oSector201
                .Command = CByte(lCommand)
                .PutCommandDataVB(bytArray)
            End With
            m_ClientServerACK = False
            LogManager.WriteLog("Sending Command 201", LogManager.enumLogLevel.Info)
            client.RequestExWriteSector(m_lDatapak, 201, oSector201)
            LogManager.WriteLog("Command Sent Command 201", LogManager.enumLogLevel.Info)

            LogManager.WriteLog("Starting Command :: Time = " + Now.ToString(), LogManager.enumLogLevel.Info)
            Dim dStartTime As Date = Now
            Do

                Application.DoEvents()

                If Now > dStartTime.AddSeconds(30) Then
                    bTimedOut = True
                    LogManager.WriteLog("Timed out : " & m_lDatapak.ToString(), LogManager.enumLogLevel.Info)
                End If
            Loop Until m_clientACK Or bTimedOut

            LogManager.WriteLog("End of Time 201 :: Time = " + Now.ToString() + "  Client ACK =" + m_clientACK.ToString() + "  bTimedOut =" + bTimedOut.ToString(), LogManager.enumLogLevel.Info)

            If m_bACK = True Then
                LogManager.WriteLog("201 command success", LogManager.enumLogLevel.Info)
                SendSector201_Comexchange = True
            Else
                LogManager.WriteLog("201 command failure", LogManager.enumLogLevel.Info)
                SendSector201_Comexchange = False
            End If


        Catch ex As Exception

            m_lLastError = Err.Number
            LogManager.WriteLog("[" & sCommandText & "] " & Err.Description, LogManager.enumLogLevel.Info)
            SendSector201_Comexchange = False
        Finally
            If Marshal.IsComObject(oSector201) Then
                Marshal.ReleaseComObject(oSector201)
            End If
            Try
                If (IsNothing(oSector201)) Then
                    LogManager.WriteLog("oSector201 object is Released", LogManager.enumLogLevel.Info)
                Else
                    LogManager.WriteLog("oSector201 object is Not Properly Released", LogManager.enumLogLevel.Info)
                End If
            Catch ex As Exception

            End Try
            
            oSector201 = Nothing
            m_clientACK = False
            m_ClientServerACK = False
            m_bACK = False
            bTimedOut = False

        End Try
    End Function

    Public Function SendSector205_Comexchange(ByVal lUDP As Integer, ByVal Message As String) As Boolean
        Dim cmd As Byte
        Dim strArray() As String
        Dim StrCommand As String
        Dim i As Integer
        Dim Data(50) As Byte
        Dim sector205 As New ComExchangeLib.Sector205Data
        Dim Lent As Integer
        Dim bTimedOut As Boolean
        Dim bool_ServerUpdate_Timeout As Boolean

        Try
            'Dim Key As Microsoft.Win32.RegistryKey
            'Key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\Honeyframe\Cashmaster\Exchange", True)
            'client.RefreshActiveUPDs(Key.GetValue("Default_Server_Ip"))
            RefreshUDP()
            m_ClientServerACK = False
            m_clientACK = False
            Dim curdate As Date = Now
            Do
                Application.DoEvents()
                If Now > curdate.AddSeconds(30) Then
                    bool_ServerUpdate_Timeout = True
                End If
            Loop Until bool_ServerUpdate_Timeout Or m_ClientServerACK
            If m_ClientServerACK = False Then
                LogManager.WriteLog("Timeout for Server Update is :" + bool_ServerUpdate_Timeout.ToString(), LogManager.enumLogLevel.Info)
            End If

            Message = Message + ",0"
            strArray = Message.Split(",")
            Lent = UBound(strArray)
            For i = 0 To Lent - 1
                StrCommand = strArray(i)
                If IsNumeric(StrCommand) Then
                    If CLng(StrCommand) < 256 Then
                        cmd = CByte(StrCommand)
                        Data(i) = cmd
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
            Next i

            sector205.CommandLength = i
            sector205.Command = CByte(Data(0))
            sector205.PutCommandDataVB(Data)

            'REQUEST NOW
            client.RequestExWriteSector(lUDP, 205, sector205)

            'WAIT as its asunchronous call
            Dim dStartTime As Date = Now
            Do
                Application.DoEvents()
                If Now > dStartTime.AddSeconds(30) Then
                    bTimedOut = True
                    LogManager.WriteLog("Timed out : " & m_lDatapak.ToString(), LogManager.enumLogLevel.Info)
                End If
            Loop Until m_clientACK Or bTimedOut
            If m_bACK = True Then
                LogManager.WriteLog("205 command success", LogManager.enumLogLevel.Info)
                SendSector205_Comexchange = True
            Else
                LogManager.WriteLog("205 command failure", LogManager.enumLogLevel.Info)
                SendSector205_Comexchange = False
            End If
        Catch ex As Exception
            LogManager.WriteLog("205 command failure" + ex.Message.ToString(), LogManager.enumLogLevel.Error)

        Finally
            If Marshal.IsComObject(sector205) Then
                Marshal.ReleaseComObject(sector205)
            End If
            Try
                If (IsNothing(sector205)) Then
                    LogManager.WriteLog("oSector201 object is Released", LogManager.enumLogLevel.Info)
                Else
                    LogManager.WriteLog("oSector201 object is Not Properly Released", LogManager.enumLogLevel.Info)
                End If
            Catch ex As Exception
            End Try

            sector205 = Nothing
            m_clientACK = False
            m_ClientServerACK = False
            m_bACK = False
            bTimedOut = False
        End Try


    End Function

    Public Function SendSector203_Comexchange(ByVal Datapak As Integer) As Boolean

        Dim oSector203 As New ComExchangeLib.Sector203Data
        Dim bTimedOut As Boolean

        Try

            LogManager.WriteLog("Inside Sector203", LogManager.enumLogLevel.Info)
            LogManager.WriteLog("Sector203" + oSector203.Command.ToString, LogManager.enumLogLevel.Info)

            With oSector203
                .Command = 112
                '  LogManager.WriteLog("inside with Sector203" + oSector203.Command.ToString, LogManager.enumLogLevel.Info)
                ' .PutCommandDataVB("")
                LogManager.WriteLog("inside with Sector203 put command" + oSector203.Command.ToString, LogManager.enumLogLevel.Info)
            End With

            '     LogManager.WriteLog("Before request write sector", LogManager.enumLogLevel.Info)
            client.RequestExWriteSector(Datapak, 203, oSector203)

            'WAIT as its asunchronous call
            Dim dStartTime As Date = Now
            Do
                ' LogManager.WriteLog("Processing", LogManager.enumLogLevel.Info)
                Application.DoEvents()
                If Now > dStartTime.AddSeconds(30) Then
                    bTimedOut = True
                    LogManager.WriteLog("Timed out : " & m_lDatapak.ToString(), LogManager.enumLogLevel.Info)
                End If
            Loop Until m_clientACK Or bTimedOut
            If m_bACK = True Then
                LogManager.WriteLog("203 command success", LogManager.enumLogLevel.Info)
                SendSector203_Comexchange = True
            Else
                LogManager.WriteLog("203 command failure", LogManager.enumLogLevel.Info)
                SendSector203_Comexchange = False
            End If

        Catch ex As Exception
            LogManager.WriteLog("203 command failure" + ex.Message.ToString(), LogManager.enumLogLevel.Error)

        Finally
            If Marshal.IsComObject(oSector203) Then

                Marshal.ReleaseComObject(oSector203)
            End If
            Try
                LogManager.WriteLog(oSector203, LogManager.enumLogLevel.Info)
                If (IsNothing(oSector203)) Then
                    LogManager.WriteLog("oSector203 object is Released", LogManager.enumLogLevel.Info)
                Else
                    LogManager.WriteLog("oSector203 object is Not Properly Released", LogManager.enumLogLevel.Info)
                End If
            Catch ex As Exception
            End Try

            oSector203 = Nothing
            m_clientACK = False
            m_ClientServerACK = False
            m_bACK = False
            bTimedOut = False
        End Try

    End Function

    Private Sub client_ExchangeSectorUpdate() Handles client.ExchangeSectorUpdate
        Dim sector205 As ComExchangeLib.Sector205Data
        Dim UDPInfo As ComExchangeLib.IUDPinfo

        Dim punk As Object
        punk = New Object
        client.ExchangeReadSector(punk)
        UDPInfo = punk
        Dim iUDP As Long
        Dim i As Long
        iUDP = UDPInfo.UDPNo

        If TypeOf punk Is ComExchangeLib.Sector205Data Then
            sector205 = punk
            Dim counters205 As Object
            Dim ReturnObjectLength As Integer
            counters205 = sector205.Get205Data
            ReturnObjectLength = sector205.CommandLength
            m_SectorUpdate = "Sector 205 Data "
            For i = LBound(counters205) To ReturnObjectLength - 1
                m_SectorUpdate = m_SectorUpdate & vbCrLf & "counter " & Str(i) & "<" & counters205(i) & ">"
            Next i
        End If
        m_clientACK = True


    End Sub

    Public Sub New()
        MyBase.New()
        Try
            LogManager.WriteLog("init started", LogManager.enumLogLevel.Info)
            client = New ComExchangeLib.ExchangeClient
            LogManager.WriteLog("comexchangeclient obj created. Initializing exchange ...", LogManager.enumLogLevel.Info)
            client.InitialiseExchange(0)
            LogManager.WriteLog("Initialized exchange. refreshing active servers ...", LogManager.enumLogLevel.Info)
            client.RefreshActiveServers()
            LogManager.WriteLog("refreshing active servers completed...", LogManager.enumLogLevel.Info)

            LogManager.WriteLog("init started for client-admin", LogManager.enumLogLevel.Info)
            ''admin = New ComExchangeLib.ExchangeClient

            admin = client

            LogManager.WriteLog("comexchangeclient - admin obj created. Initializing exchange ...", LogManager.enumLogLevel.Info)
        Catch exp As Exception
            LogManager.WriteLog("init not done" & exp.ToString(), LogManager.enumLogLevel.Info)
        End Try

        LogManager.WriteLog("init done", LogManager.enumLogLevel.Info)

    End Sub

    Public Sub Init()

        Try
            LogManager.WriteLog("init started", LogManager.enumLogLevel.Info)
            client = New ComExchangeLib.ExchangeClient
            LogManager.WriteLog("comexchangeclient obj created. Initializing exchange ...", LogManager.enumLogLevel.Info)
            client.InitialiseExchange(0)
            LogManager.WriteLog("Initialized exchange. refreshing active servers ...", LogManager.enumLogLevel.Info)
            client.RefreshActiveServers()
            LogManager.WriteLog("refreshing active servers completed...", LogManager.enumLogLevel.Info)
        Catch exp As Exception
            LogManager.WriteLog("init not done" & exp.ToString(), LogManager.enumLogLevel.Info)
        End Try

        LogManager.WriteLog("init done", LogManager.enumLogLevel.Info)


    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose


        Try
            Marshal.ReleaseComObject(client)
            Marshal.FinalReleaseComObject(client)
        Catch ex As Exception
            LogManager.WriteLog("unable to release comexchange object" & ex.ToString(), LogManager.enumLogLevel.Error)
        End Try

    End Sub
    Private Sub client_ACK(ByVal MessageAck As ComExchangeLib.MessageAck) Handles client.ACK
        '
        Try
            m_bACK = CBool(MessageAck.ACK)
            m_clientACK = True
        Catch ex As Exception
            LogManager.WriteLog("exception" & MessageAck.ACK.ToString, LogManager.enumLogLevel.Info)
        End Try


        LogManager.WriteLog("MessageAck.ACK" & MessageAck.ACK.ToString(), LogManager.enumLogLevel.Info)


    End Sub


    Public Sub RefreshUDP()
        Try

            'Dim Key As Microsoft.Win32.RegistryKey
            'Dim Server As String
            'Key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\Honeyframe\Cashmaster\Exchange", True)
            'Server = Key.GetValue("Default_Server_IP").ToString()
            'client.RefreshActiveUPDs(Server)
            client.RefreshActiveUPDs(BMCRegistryHelper.GetRegKeyValue("Cashmaster\Exchange", "Default_Server_Ip"))

        Catch ex As Exception
            LogManager.WriteLog("RefreshUDP:  Failed due to " + ex.Message.ToString(), LogManager.enumLogLevel.Info)
        End Try
    End Sub
    Private Sub Client_UDPUpdate() Handles client.UDPUpdate
        Try

            m_ClientServerACK = True
            Dim i As Long
            Dim activeUDPs As Object
            m_UDPList = New Collection()
            m_UDPList.Clear()
            activeUDPs = client.ActiveUDPs
            For i = LBound(activeUDPs) To UBound(activeUDPs)
                Dim val As Object
                val = activeUDPs(i)
                m_UDPList.Add(CStr(val))
            Next i

        Catch ex As Exception

        End Try
    End Sub

    Public Function RefreshUDPList() As Boolean
        RefreshUDP()
        'client.RefreshActiveUPDs(BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\Honeyframe\Cashmaster\Exchange", True).GetValue("Default_Server_Ip"))
    End Function
    Private Sub client_ServerUpdate() Handles client.ServerUpdate
        ''
        '' received a server update, request a udp update
        ''


        client.RefreshActiveUPDs("") '_ComExchange.RefreshActiveUPDs("")
    End Sub

End Class
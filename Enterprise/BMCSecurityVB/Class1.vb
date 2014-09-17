Imports System.Threading
Imports System.Reflection
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32
Imports System.Security.Cryptography.X509Certificates
Imports BMC.Common.ConfigurationManagement
Imports BMC.Security
Imports BMC.Security.Interfaces
Imports BMC.Common.LogManagement
Imports BMC.Common.ExceptionManagement
Imports Microsoft.VisualBasic
Imports System
Imports BMC.Common.Utilities

<ComClass(BMCSecurityCallMethod.ClassId, BMCSecurityCallMethod.InterfaceId, BMCSecurityCallMethod.EventsId)> _
Public Class BMCSecurityCallMethod
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "281e5239-4f96-4d8a-b9d2-08ee7b6b0b84"
    Public Const InterfaceId As String = "0ccf53e3-7d16-4ea6-8212-1cff0c3218c8"
    Public Const EventsId As String = "569c3efa-461f-4663-819b-9c7905348184"
#End Region

    'Shared x509Certificate As New X509Certificate2
    'Shared x509Certificate2PublicKey As X509Certificate2
    Dim sPassword As String

    Public Sub New()
        MyBase.New()
    End Sub


    Public Function CreatHash(ByVal value As String) As String
        Dim stream As New MemoryStream(Encoding.Default.GetBytes(value))
        Dim alg As HashAlgorithm = HashAlgorithm.Create("MD5")
        Return Convert.ToBase64String(alg.ComputeHash(stream))
    End Function


    Public Function Bytearraytostring(ByVal bytes As Byte()) As String
        Return Convert.ToBase64String(bytes)
    End Function


    Public Function StringToByteArray(ByVal inputstring As String) As Byte()
        Return Convert.FromBase64String(inputstring)
    End Function

    Public Function Encrypt(ByVal original As String) As String
        'Return Encrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi")
        Return BMC.Common.Security.CryptographyHelper.Encrypt(original)
    End Function

    Public Function Decrypt(ByVal EncryptedString As String) As String
        'Return Decrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi", Encoding.Default)
        Return BMC.Common.Security.CryptographyHelper.Decrypt(EncryptedString)
    End Function

    Public Function Decrypt(ByVal original As String, ByVal key As String) As String
        Return Decrypt(original, key, Encoding.Default)
    End Function

    Public Function Decrypt(ByVal original As String, ByVal encoding As Encoding) As String
        Return Decrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi", encoding)
    End Function

    Public Function Encrypt(ByVal original As String, ByVal key As String) As String
        Dim buff As Byte()
        Dim kb As Byte()
        buff = Encoding.Default.GetBytes(original)
        kb = Encoding.Default.GetBytes(key)
        Return Convert.ToBase64String(Encrypt(buff, kb))
    End Function

    Public Function Decrypt(ByVal encrypted As String, ByVal key As String, ByVal encoding As Encoding) As String
        Dim buff As Byte()
        Dim kb As Byte()
        buff = Convert.FromBase64String(encrypted)
        kb = encoding.Default.GetBytes(key)
        Return encoding.GetString(Decrypt(buff, kb))
    End Function

    Public Function MakeMd(ByVal original As Byte()) As Byte()
        Dim keyhash
        Dim hashmd As New MD5CryptoServiceProvider()
        keyhash = hashmd.ComputeHash(original)
        Return keyhash
    End Function

    Public Function Encrypt(ByVal original As Byte(), ByVal key As Byte()) As Byte()

        Dim isRsa As String = "FALSE"
        Try

        Catch ex As Exception

        End Try

        Dim des As TripleDESCryptoServiceProvider
        des = New TripleDESCryptoServiceProvider
        des.Key = MakeMd(key)
        des.Mode = CipherMode.ECB
        Return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length)
    End Function

    Public Function Decrypt(ByVal encrypted As Byte(), ByVal key As Byte()) As Byte()

        Dim des As TripleDESCryptoServiceProvider
        des = New TripleDESCryptoServiceProvider
        des.Key = MakeMd(key)
        des.Mode = CipherMode.ECB
        Return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length)
    End Function

    Public Function Encrypt(ByVal original As Byte()) As Byte()
        Dim key = Encoding.Default.GetBytes("AnCaGaKaMaMaNaPoRaReSuVi")
        Return Encrypt(original, key)
    End Function


    Public Function Decrypt(ByVal encrypted As Byte()) As Byte()
        Dim key = Encoding.Default.GetBytes("AnCaGaKaMaMaNaPoRaReSuVi")
        Return Decrypt(encrypted, key)
    End Function

    Public Function CreateHash(ByVal value As String) As Byte()
        Dim stream = New MemoryStream(Encoding.Default.GetBytes(value))
        Dim alg = HashAlgorithm.Create("MD5")
        Return alg.ComputeHash(stream)
    End Function

    Public Function GetHashString(ByVal value As String) As String

        Dim stream = New MemoryStream(Encoding.Default.GetBytes(value))
        Dim alg = HashAlgorithm.Create("MD5")
        Return Bytearraytostring(alg.ComputeHash(stream))
    End Function

    Public Function WebCall(ByVal siteCode As String, ByVal connection As String) As Boolean

        Dim exchange As New HarnessApplication.BGSWSService(siteCode, connection)
        Return exchange.InitializeSite()

        'Dim exchange As New HarnessApplication.BGSWSService("1001", connection)
        'Return exchange.InitializeSite()



    End Function

    Public Function GetConnectionString() As String
        Return DatabaseHelper.GetConnectionString()
    End Function

    Public Function StoreConnectionString(ByVal serverName As String, ByVal database As String, ByVal loginName As String, ByVal password As String, ByVal timeOutInSeconds As Integer) As Boolean
        Return DatabaseHelper.StoreConnectionString(serverName, database, loginName, password, timeOutInSeconds)
    End Function


    Public Function GeneratePassword() As String

        sPassword = PasswordHelper.Generate(6)
        If sPassword IsNot Nothing Then
            Return "New Password: " + sPassword
        End If
        Return "Unable to Reset"

    End Function

    Public Function InsertPassword(ByRef lUserID As String, ByRef staffPassword As String) As Boolean

        If lUserID IsNot Nothing And sPassword IsNot Nothing And staffPassword IsNot Nothing Then
            Return PasswordHelper.ResetUser(Convert.ToInt32(lUserID), sPassword, GetConnectionString())
        End If

    End Function
    Public Function CheckPasswordStrength(ByRef sUserPassword As String) As Boolean

        If sUserPassword IsNot Nothing Then
            Return PasswordHelper.CheckPasswordStrength(sUserPassword)
        End If

        Return False
    End Function

    Public Function ChangePassword(ByRef nUserID As String, ByRef sUserPassword As String, ByRef sUserName As String)
        Try
            If nUserID <> 0 And sUserPassword IsNot Nothing Then
                Return PasswordHelper.ChangePassword(Convert.ToInt32(nUserID), sUserPassword, sUserName, GetConnectionString())
            End If
        Catch ex As Exception
            ex.Source = "SecurityVB"
            ExceptionManager.Publish(ex)
        End Try

        Return False


    End Function

    Public Function LockUser(ByRef nSecurityID As String) As Boolean
        Try
            If nSecurityID IsNot Nothing Then
                Return PasswordHelper.LockUser(Convert.ToInt32(nSecurityID), GetConnectionString())
            End If
        Catch ex As Exception
            ex.Source = "SecurityVB"
            ExceptionManager.Publish(ex)
        End Try

    End Function


    Public Function GetExpiryDays() As String
        Return PasswordHelper.GetExpiryDays(GetConnectionString()).ToString()
    End Function

    Public Function GetNumberOfAttempts() As String
        Return PasswordHelper.GetNumberOfAttempts(GetConnectionString()).ToString()
    End Function

End Class


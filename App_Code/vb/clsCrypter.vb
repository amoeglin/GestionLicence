Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Public Class clsCrypter

  ' Cryptage pour le mot de passe pour LicencesFileBuilder :
  '
  ' si tu changes les clés
  ' rappel une fois Encrypte (le local de ce fichier)
  ' pour obtenir un nouveau cryptage du mot de passe
  ' tu peux aussi changer le mot de passe a cette occasion
    Public Const mdp As String = "88880B14012D0B34"   ' "88880B14012D0B34"     '50370B14012D0B34"
  Private Const k1 As String = "AZERTYUIOP"
  Private Const k2 As String = "WXCVBN"

  Public mErrObj As New ErrorServices.CErrors

  ' les fonctions suivantes sont dupliquées de CLicenceFile pour
  ' ne pas faire d'appel vers l'exterieur qui pourraient être interceptés
  Private Function KeyEncryption(ByRef strText As String, ByRef strKey As String) As String
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer

    Dim strBinText As String
    Dim strBinKey As String
    Dim strBuf As String

    KeyEncryption = ""

    On Error GoTo KeyEncryption_Error

    If strText = "" Then Exit Function
    If strKey = "" Then Exit Function

    For i = 1 To Len(strText)
      j = j + 1
      If j > Len(strKey) Then j = 1

      strBinText = LongToBinary(Asc(Mid(strText, i, 1)))
      strBinKey = LongToBinary(Asc(Mid(strKey, j, 1)))
      strBuf = ""

      For k = 1 To Len(strBinText)
        strBuf = strBuf & CStr(Mid(strBinText, k, 1) Xor Mid(strBinKey, k, 1))
      Next
      KeyEncryption = KeyEncryption & Chr(BinaryToLong(strBuf))
    Next
    Exit Function

KeyEncryption_Error:
    mErrObj.DisplayErrors(Err)
  End Function

  Private Function ConvertToAscii(ByRef strSource As String) As String
    Dim i As Integer

    ConvertToAscii = ""
    For i = 1 To Len(strSource)
      ConvertToAscii = ConvertToAscii & VB6.Format(Asc(Mid(strSource, i, 1)), "000")
    Next
  End Function

  Private Function ConvertToHexa(ByRef strSource As String) As String
    Dim i As Integer
    Dim strRes As String

    ConvertToHexa = ""

    If Len(strSource) / 2 <> Int(Len(strSource) / 2) Then Exit Function

    For i = 1 To Len(strSource) Step 2
      strRes = CStr(Hex(CInt(Mid(strSource, i, 2))))
      If Len(strRes) < 2 Then strRes = New String("0", 2 - Len(strRes)) & strRes
      ConvertToHexa = ConvertToHexa & strRes
    Next
  End Function

  Private Function RevertFromHexa(ByRef strSource As String) As String
    Dim i As Integer

    RevertFromHexa = ""

    If Len(strSource) / 2 <> Int(Len(strSource) / 2) Then Exit Function

    For i = 1 To Len(strSource) Step 2
      RevertFromHexa = RevertFromHexa & VB6.Format(Val("&H" & Mid(strSource, i, 2)), "00")
    Next
  End Function

  Private Function RevertFromAscii(ByRef strSource As String) As String
    Dim i As Integer

    RevertFromAscii = ""

    If Len(strSource) / 3 <> Int(Len(strSource) / 3) Then Exit Function

    For i = 1 To Len(strSource) Step 3
      RevertFromAscii = RevertFromAscii & Chr(CInt(Mid(strSource, i, 3)))
    Next
  End Function

  Private Function ScrambleAscii(ByRef strSource As String) As String
    ScrambleAscii = ""

    Randomize()

    Dim i As Integer

    If Len(strSource) / 3 <> Int(Len(strSource) / 3) Then Exit Function

    For i = 1 To Len(strSource) Step 3
      ScrambleAscii = ScrambleAscii & CStr(Mid(strSource, i + 1, 1)) & CStr(Mid(strSource, i, 1)) & CStr(Int(Rnd() * 10)) & CStr(Mid(strSource, i + 2, 1))
    Next
  End Function

  Private Function ReorderAscii(ByRef strSource As String) As String
    Dim i As Integer

    ReorderAscii = ""

    If Len(strSource) / 4 <> Int(Len(strSource) / 4) Then Exit Function

    For i = 1 To Len(strSource) Step 4
      ReorderAscii = ReorderAscii & Mid(strSource, i + 1, 1) & Mid(strSource, i, 1) & Mid(strSource, i + 3, 1)
    Next
  End Function

  public Function Decrypte(ByRef strText As String) As String
    Dim strRes As String

    On Error GoTo Decrypte_Error

    strRes = RevertFromAscii(ReorderAscii(RevertFromHexa(strText)))
    strRes = KeyEncryption(strRes, k2)
    Decrypte = KeyEncryption(strRes, k1)

    Exit Function
Decrypte_Error:
    mErrObj.DisplayErrors(Err)
  End Function

  Private Function LongToBinary(ByVal long_value As Integer) As String
    Dim hex_string As String
    Dim digit_num As Short
    Dim digit_value As Short
    Dim nibble_string As String
    Dim result_string As String
    Dim factor As Short
    Dim bit As Short

    result_string = ""

    hex_string = Hex(long_value)

    hex_string = Right(New String("0", 8) & hex_string, 8)

    For digit_num = 8 To 1 Step -1
      digit_value = CInt("&H" & Mid(hex_string, digit_num, 1))
      factor = 1
      nibble_string = ""
      For bit = 3 To 0 Step -1
        If digit_value And factor Then
          nibble_string = "1" & nibble_string
        Else
          nibble_string = "0" & nibble_string
        End If
        factor = factor * 2
      Next bit
      result_string = nibble_string & result_string
    Next digit_num

    LongToBinary = result_string
  End Function

  Private Function BinaryToLong(ByVal binary_value As String) As Integer
    Dim hex_result As String
    Dim nibble_num As Short
    Dim nibble_value As Short
    Dim factor As Short
    Dim bit As Short

    hex_result = ""

    On Error GoTo BinaryToLong_Err

    binary_value = UCase(Trim(binary_value))

    binary_value = Replace(binary_value, " ", "")
    binary_value = Right(New String("0", 32) & binary_value, 32)

    For nibble_num = 7 To 0 Step -1
      factor = 1
      nibble_value = 0

      For bit = 3 To 0 Step -1
        If Mid(binary_value, 1 + nibble_num * 4 + bit, 1) = "1" Then
          nibble_value = nibble_value + factor
        End If
        factor = factor * 2
      Next bit
      hex_result = Hex(nibble_value) & hex_result
    Next nibble_num

    BinaryToLong = CInt("&H" & hex_result)
    Exit Function

BinaryToLong_Err:
    mErrObj.DisplayErrors(Err)
  End Function

  Public Function Encrypte(ByRef strText As String) As String
    Dim strRes As String

    On Error GoTo Encrypte_Error

    strRes = KeyEncryption(strText, k1)
    strRes = KeyEncryption(strRes, k2)
    Encrypte = ConvertToHexa(ScrambleAscii(ConvertToAscii(strRes)))

    Exit Function

Encrypte_Error:
    mErrObj.DisplayErrors(Err)
  End Function
End Class
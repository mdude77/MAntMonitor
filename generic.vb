﻿Imports System.Runtime.CompilerServices

Public Class ControlsByRegistry

    Private dictRegkeyNames As Dictionary(Of String, String)
    Private sRegKey As String

    Public Sub AddControl(ByRef ctlAny As Control, ByVal sName As String)

        Me.dictRegkeyNames.Add(ctlAny.Name, sName)

    End Sub

    Public Sub SetControlByRegKeyAnt(ByRef chklstAny As CheckedListBox)

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey & "\Ants")
            If key Is Nothing Then
                My.Computer.Registry.CurrentUser.CreateSubKey(sRegKey & "\Ants")
            End If
        End Using

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey & "\Ants")
            Dim sTemp As String

            For Each sTemp In key.GetSubKeyNames
                chklstAny.Items.Add(sTemp)
            Next
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef trackAny As TrackBar, Optional iValue As Integer = 4)

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey)
            Call SetControlByRegKey(key, trackAny, iValue)
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef chkAny As CheckBox, Optional bDefault As Boolean = False)

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey)
            Call SetControlByRegKey(key, chkAny, bDefault)
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef optAny As RadioButton, Optional bDefault As Boolean = False)

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey)
            Call SetControlByRegKey(key, optAny, bDefault)
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef chkAny As CheckBox, Optional bDefault As Boolean = False)

        Dim sKey As String
        Dim sTemp As String

        If dictRegkeyNames.TryGetValue(chkAny.Name, sKey) = True Then
            sTemp = regKey.GetValue(sKey)

            If sTemp = "Y" Then
                chkAny.Checked = True
            Else
                If String.IsNullOrEmpty(sTemp) = True Then
                    chkAny.Checked = bDefault
                Else
                    chkAny.Checked = False
                End If
            End If
        Else
            Throw New Exception("Internal error: " & chkAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef trackAny As TrackBar, Optional iValue As Integer = 4)

        Dim sKey As String
        Dim sTemp As String

        If dictRegkeyNames.TryGetValue(trackAny.Name, sKey) = True Then
            sTemp = regKey.GetValue(sKey)

            If sTemp.IsNullOrEmpty = True Then
                trackAny.Value = iValue
            Else
                trackAny.Value = Val(sTemp)
            End If
        Else
            Throw New Exception("Internal error: " & trackAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef optAny As RadioButton, Optional bDefault As Boolean = False)

        Dim sKey As String
        Dim sTemp As String

        If dictRegkeyNames.TryGetValue(optAny.Name, sKey) = True Then
            sTemp = regKey.GetValue(sKey)

            If sTemp = "Y" Then
                optAny.Checked = True
            Else
                If String.IsNullOrEmpty(sTemp) = True Then
                    optAny.Checked = bDefault
                Else
                    optAny.Checked = False
                End If
            End If
        Else
            Throw New Exception("Internal error: " & optAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub SetControlByRegKey(ByRef cmbAny As ComboBox, Optional ByVal sDefault As String = "")

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey)
            Call SetControlByRegKey(key, cmbAny, sDefault)
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef txtAny As TextBox, Optional ByVal sDefault As String = "")

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey)
            Call SetControlByRegKey(key, txtAny, sDefault)
        End Using

    End Sub

    Public Sub SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef txtAny As TextBox, Optional ByVal sDefault As String = "")

        Call _SetControlByRegKey(regKey, txtAny, sDefault)

    End Sub

    Public Sub SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef cmbAny As ComboBox, Optional ByVal sDefault As String = "")

        Call _SetControlByRegKey(regKey, cmbAny, sDefault)

    End Sub

    Private Sub _SetControlByRegKey(ByRef regKey As Microsoft.Win32.RegistryKey, ByRef ctlAny As Control, Optional ByVal sDefault As String = "")

        Dim sKey As String
        Dim sReturn As String

        If dictRegkeyNames.TryGetValue(ctlAny.Name, sKey) = True Then
            sReturn = regKey.GetValue(sKey)

            If String.IsNullOrEmpty(sReturn) = True Then
                ctlAny.Text = sDefault
            Else
                ctlAny.Text = sReturn
            End If
        Else
            Throw New Exception("Internal error: " & ctlAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub SetRegKeyByControl(ByRef txtAny As TextBox)

        _SetRegKeyByControl(txtAny)

    End Sub

    Public Sub SetRegKeyByControl(ByRef trackAny As TrackBar)

        _SetRegKeyByControl(trackAny)

    End Sub

    Public Sub SetRegKeyByControl(ByRef optAny As RadioButton)

        _SetRegKeyByControl(optAny)

    End Sub

    Public Sub SetRegKeyByControl(ByRef txtAny As TextBox, ByVal sOverRide As String)

        _SetRegKeyByControl(txtAny, sOverRide)

    End Sub

    Public Sub SetRegKeyByControl(ByRef cmbAny As ComboBox)

        _SetRegKeyByControl(cmbAny)

    End Sub

    Private Sub _SetRegKeyByControl(ByRef ctlAny As Control, ByVal sOverRide As String)

        Dim sKeyname As String

        If dictRegkeyNames.TryGetValue(ctlAny.Name, sKeyname) = True Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey, sKeyname, sOverRide, Microsoft.Win32.RegistryValueKind.String)
        Else
            Throw New Exception("Internal error: " & ctlAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Private Sub _SetRegKeyByControl(ByRef ctlAny As Control)

        Dim sKeyname As String

        If dictRegkeyNames.TryGetValue(ctlAny.Name, sKeyname) = True Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey, sKeyname, ctlAny.Text, Microsoft.Win32.RegistryValueKind.String)
        Else
            Throw New Exception("Internal error: " & ctlAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Private Sub _SetRegKeyByControl(ByRef ctlAny As TrackBar)

        Dim sKeyname As String

        If dictRegkeyNames.TryGetValue(ctlAny.Name, sKeyname) = True Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey, sKeyname, ctlAny.Value, Microsoft.Win32.RegistryValueKind.String)
        Else
            Throw New Exception("Internal error: " & ctlAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub SetRegKeyByControl(ByRef chkLstAny As CheckedListBox)

        Dim sValue As String
        Dim p() As String

        Using key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sRegKey & "\" & chkLstAny.Name, True)
            If key IsNot Nothing Then
                For Each sTemp In key.GetValueNames
                    key.DeleteValue(sTemp)
                Next
            End If
        End Using

        For Each sValue In chkLstAny.Items
            p = sValue.Split(":")

            sValue = p(0) & ":" & p(1)

            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey & "\" & chkLstAny.Name, sValue, sValue, Microsoft.Win32.RegistryValueKind.String)
        Next

    End Sub

    Public Sub SetRegKeyByControl(ByRef chkAny As CheckBox)

        Dim sKeyname As String

        If dictRegkeyNames.TryGetValue(chkAny.Name, sKeyname) = True Then
            If chkAny.Checked = True Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey, sKeyname, "Y", Microsoft.Win32.RegistryValueKind.String)
            Else
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & sRegKey, sKeyname, "N", Microsoft.Win32.RegistryValueKind.String)
            End If
        Else
            Throw New Exception("Internal error: " & chkAny.Name & " not found in regKey dictionary.")
        End If

    End Sub

    Public Sub New(ByVal csRegKey As String)

        dictRegkeyNames = New Dictionary(Of String, String)
        Me.sRegKey = csRegKey

    End Sub

End Class

Public Module Extensions

    <Extension()> Public Function IsNullOrEmpty(ByVal sString As String) As Boolean

        Return String.IsNullOrEmpty(sString)

    End Function

    ''' <summary>
    ''' Returns the string located at the In String Reverse position
    ''' </summary>
    ''' <param name="sString"></param>
    ''' <param name="sSearch">String to search for</param>
    ''' <param name="iStart">Where to start searching</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function InstrRev(ByVal sString As String, ByVal sSearch As String, Optional ByVal iStart As Integer = -1) As String

        Return sString.Substring(Microsoft.VisualBasic.InStrRev(sString, sSearch, iStart))

    End Function

    ''' <summary>
    ''' Returns the leftmost part of the string minus the value passed
    ''' </summary>
    ''' <param name="sString"></param>
    ''' <param name="iMinusValue">How many chars from the end to omit</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function LeftMost(ByVal sString As String, ByVal iMinusValue As Integer) As String

        If sString.Length < iMinusValue Then
            Return sString
        Else
            Return sString.Substring(0, sString.Length - iMinusValue)
        End If

    End Function

End Module

Public Class enListbox

    Inherits ListBox

    Private dictTags As System.Collections.Generic.Dictionary(Of Integer, Object)

    Public Function AddItem(ByRef o As Object, ByRef t As Object) As Integer

        Dim i As Integer

        i = MyBase.Items.Add(o)

        dictTags.Add(i, t)

        Return i

    End Function

    Public Sub RemoveSelectedItem()

        Dim x As Integer
        Dim b As Boolean
        Dim o As Object

        'when an item is removed from the list collection, the list indexes for every item after it are renumbered!
        'this compensates for that in the dictionary of tags by renumbering them there as well
        For x = MyBase.SelectedIndex To Me.dictTags.Count - 1
            If b = False Then
                dictTags.Remove(x)

                MyBase.Items.RemoveAt(x)

                b = True
            Else
                o = (Me.dictTags(x))

                Me.dictTags.Remove(x)
                Me.dictTags.Add(x - 1, o)
            End If
        Next

    End Sub

    Public Property ItemTag(ByVal iIndex As Integer) As Object
        Get
            Dim o As Object

            If dictTags.TryGetValue(iIndex, o) = True Then
                Return o
            Else
                Return Nothing
            End If
        End Get

        Set(value)
            Dim o As Object

            If dictTags.TryGetValue(iIndex, o) = True Then
                dictTags(iIndex) = value
            Else
                dictTags.Add(iIndex, value)
            End If

        End Set

    End Property

    Public Sub New()

        MyBase.New()

        Me.dictTags = New Dictionary(Of Integer, Object)

    End Sub
End Class
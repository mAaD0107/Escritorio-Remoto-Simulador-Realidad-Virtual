Imports System.Runtime.InteropServices

Module Mouse


    Public stopleft As Boolean = False
    Public stopright As Boolean = False

    Private Structure MSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As Int32
        Public flags As Int32
        Public time As Int32
        Public extra As IntPtr
    End Structure

    Private mHook As IntPtr = IntPtr.Zero
    Private Const WH_MOUSE_LL As Int32 = &HE
    Private Const WM_RBUTTONDOWN As Int32 = &H204
    Private Const WM_LBUTTONDOWN As Int32 = &H201
    <MarshalAs(UnmanagedType.FunctionPtr)> Private mProc As MouseHookDelegate
    Private Declare Function SetWindowsHookExW Lib "user32.dll" (ByVal idHook As Int32, ByVal HookProc As MouseHookDelegate, ByVal hInstance As IntPtr, ByVal wParam As Int32) As IntPtr
    Private Declare Function UnhookWindowsHookEx Lib "user32.dll" (ByVal hook As IntPtr) As Boolean
    Private Declare Function CallNextHookEx Lib "user32.dll" (ByVal idHook As Int32, ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32
    Private Declare Function GetModuleHandleW Lib "kernel32.dll" (ByVal fakezero As IntPtr) As IntPtr
    Private Delegate Function MouseHookDelegate(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32

    Public Function SetHookMouse() As Boolean
        If mHook = IntPtr.Zero Then
            mProc = New MouseHookDelegate(AddressOf MouseHookProc)
            mHook = SetWindowsHookExW(WH_MOUSE_LL, mProc, GetModuleHandleW(IntPtr.Zero), 0)
        End If
        Return mHook <> IntPtr.Zero
    End Function

    Public Sub UnHookMouse()
        If mHook = IntPtr.Zero Then Return
        UnhookWindowsHookEx(mHook)
        mHook = IntPtr.Zero
    End Sub

    Private Function MouseHookProc(ByVal nCode As Int32, ByVal wParam As IntPtr, ByRef lParam As MSLLHOOKSTRUCT) As Int32
        If wParam.ToInt32 = WM_LBUTTONDOWN Then
            If stopleft = True Then Return 1
        End If
        If wParam.ToInt32 = WM_RBUTTONDOWN Then
            If stopright = True Then Return 1
        End If
        Return CallNextHookEx(WH_MOUSE_LL, nCode, wParam, lParam)

    End Function


End Module

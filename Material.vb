Imports System.Windows.Forms
Imports System.Data.SqlClient

Public MustInherit Class Substance

    Public Name As String
    Public Specification As String
    Public ERPCode As String
    Protected mCinvdefine9 As String
    Protected mCinvdefine10 As String

    Public Function hasSpecification() As Boolean
        If Specification = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    '顺序以SELECT语句顺序拟定
    Public Sub getValue(ByVal value As String)
        Dim values() As String = value.Split(";")
        Specification = values(0)
        Name = values(1)
        mCinvdefine9 = values(2)
        mCinvdefine10 = values(3)
        ERPCode = values(4)
    End Sub

    Public ReadOnly Property isNull() As Boolean
        Get
            If ERPCode = "" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Overloads Function equals(ByVal s As Substance) As Boolean
        If s.ERPCode = Me.ERPCode Then
            Return True
        Else
            Return False
        End If
    End Function


End Class

Public Class mPipe
    Inherits Substance

    Public Fittings As New List(Of mPipeFitting)
    Public Length As String
    Private mNum As String
    Private mPipeNo As Integer

    Public ReadOnly Property DN() As String
        Get
            Return mCinvdefine10
        End Get
    End Property

    Public ReadOnly Property Material() As String
        Get
            Return mCinvdefine9
        End Get
    End Property

    Public ReadOnly Property Unit() As String
        Get
            Return "米"
        End Get
    End Property

    Public Property pipeNo() As Integer
        Set(value As Integer)
            mPipeNo = value
        End Set
        Get
            Return mPipeNo
        End Get
    End Property

    '次序以材料表表头顺序拟定
    Public Function setString() As String()
        Dim values(6) As String
        values(0) = Name
        values(1) = Specification
        values(2) = mCinvdefine9
        values(3) = Length
        values(4) = Unit
        values(5) = ERPCode
        values(6) = pipeNo.ToString
        Return values
    End Function




End Class

Public Class mPipeFitting
    Inherits Substance
    Private mDN As String
    Public PCS As String
    Private mPipeNo As Integer

    Public ReadOnly Property Material() As String
        Get
            Return mCinvdefine9
        End Get
    End Property

    Public ReadOnly Property Unit() As String
        Get
            Return "件"
        End Get
    End Property
    Public ReadOnly Property ModelNo() As String
        Get
            If mCinvdefine10 = " " Then
                Return ""
            Else
                Return mCinvdefine10
            End If
        End Get
    End Property


    Public ReadOnly Property DN() As String
        Get
            If Me.hasSpecification = True Then
                Dim DNs As String()
                DNs = Me.Specification.Split(",")
                If Left(DNs(0), 2) = "DN" Then
                    mDN = DNs(0)
                Else
                    mDN = ""
                End If
            End If
            Return mDN
        End Get
    End Property

    Public Property pipeNo() As Integer
        Set(value As Integer)
            mPipeNo = value
        End Set
        Get
            Return mPipeNo
        End Get
    End Property

    Public Function setString() As String()
        Dim values(6) As String
        values(0) = Name
        values(1) = Specification
        values(2) = mCinvdefine9
        values(3) = PCS
        values(4) = Unit
        values(5) = ERPCode
        values(6) = pipeNo
        Return values
    End Function

End Class

Public Class getDatabaseData
    ''' <summary>
    ''' 通过设定的SQL语句选取所需的数据填入界面，供使用者选择使用。如果选择了表的多列，将多列内容用","号隔开一起放入combobox。
    ''' </summary>
    ''' <param name="sqlstring">SQL语句</param>
    ''' <param name="myComboBox">要填入的清单</param>
    ''' <remarks></remarks>
    Public Sub retrieveDatabase(ByVal sqlstring As String, ByVal myComboBox As ComboBox)

        Try
            ' Add your data task here. 
            'Dim connString As String = "Data Source=WIN-NR1URHCKAEP;Initial Catalog=UFDATA_008_2015;User ID=Datareader;Password=TE2015hz"
            Dim connString As String = "Data Source=XP-PROG\SQLEXPRESS;Initial Catalog=AWSDocManagement;User ID=AWSDoc;Password=AWSDoc5100"
            'Dim sqlString As String = "select cinvcode,cinvname,cinvstd,cinvccode,I_id,cinvdefine9,cinvdefine10 from inventory where cinvccode like '04%'"

            Dim command As SqlCommand = New SqlCommand(sqlstring, New SqlConnection(connString))
            command.Connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)

            myComboBox.Items.Clear()

            Do While reader.Read
                Dim recordString As String = reader.GetString(0)
                If reader.FieldCount > 1 Then
                    For i As Integer = 1 To reader.FieldCount - 1
                        Try
                            recordString = recordString & ";" & reader.GetString(i)
                        Catch ex As SqlTypes.SqlNullValueException
                            recordString = recordString & ";" & " "

                        End Try
                    Next
                End If
                myComboBox.Items.Add(recordString)
            Loop
            reader.Close()

        Catch ex As SqlException

            Select Case ex.Number
                Case -1
                    MsgBox("无法连接服务器，将使用缺省设置。如一定需要ERP数据，请重启程序！", MsgBoxStyle.Critical, "无法连接服务器")
                Case 18456
                    MsgBox("服务器登录密码错误，将使用缺省设置。如一定需要ERP数据，请重启程序！", MsgBoxStyle.Critical, "密码错误")
            End Select

            Throw ex
        Finally
        End Try

    End Sub
End Class
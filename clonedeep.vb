Imports System.Runtime.CompilerServices

Module clonedeep

    <Extension()>
    Public Function CloneDeep(list As List(Of Object)) As List(Of Object)
        Dim result As New List(Of Object)

        For Each obj In list
            ' Cast item to Dictionary
            Dim dict = CType(obj, Dictionary(Of String, Object))

            ' Clone the dictionary
            Dim copy As New Dictionary(Of String, Object)(dict)

            result.Add(copy)
        Next

        Return result
    End Function

End Module

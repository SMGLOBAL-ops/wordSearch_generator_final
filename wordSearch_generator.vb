Imports System
Imports System.Collections.Generic

Public Class wordSearch_generator
    Private puzzle As String(,)
    Private found As List(Of String)

    Public Sub New(ByVal theWords As List(Of String))
        Dim rnd As Random = New Random()
        found = New List(Of String)()
        Dim fill As String = "abcdefghijklmnopqrstuvwxyz"
        puzzle = New String(49, 49) {}

        For r As Integer = 0 To puzzle.GetLength(0) - 1

            For c As Integer = 0 To puzzle.GetLength(1) - 1
                puzzle(r, c) = "*"
            Next
        Next

        For k As Integer = 0 To theWords.Count - 1
            Dim row As Integer = rnd.[Next](0, 50)
            Dim col As Integer = rnd.[Next](0, 50)
            Dim dir As Integer = rnd.[Next](0, 2)

            While row + theWords(k).Length > puzzle.GetLength(0) OrElse col + theWords(k).Length > puzzle.GetLength(1)
                row = rnd.[Next](0, 50)
                col = rnd.[Next](0, 50)
            End While

            If check(dir, row, col, theWords(k)) Then
                found.Add(theWords(k))

                For m As Integer = 0 To theWords(k).Length - 1
                    puzzle(row, col) = theWords(k).Substring(m, 1)

                    If dir = 0 Then
                        row += 1
                    Else
                        col += 1
                    End If
                Next
            End If

            For r As Integer = 0 To puzzle.GetLength(0) - 1

                For c As Integer = 0 To puzzle.GetLength(1) - 1

                    If puzzle(r, c).Equals("*") Then
                        Dim spot As Integer = rnd.[Next](0, 26)
                        puzzle(r, c) = fill.Substring(spot, 1)
                    End If
                Next
            Next
        Next

        For r As Integer = 0 To puzzle.GetLength(0) - 1

            For c As Integer = 0 To puzzle.GetLength(1) - 1
                Console.Write(puzzle(r, c) & " ")
            Next

            Console.WriteLine()
        Next

        Console.WriteLine()

        For Each print As String In found
            Console.WriteLine(print)
        Next
    End Sub

    Public Function check(ByVal dir As Integer, ByVal row As Integer, ByVal col As Integer, ByVal word As String) As Boolean
        If dir = 0 Then

            For r As Integer = row To word.Length - 1
                If Not puzzle(r, col).Equals("*") Then Return False
            Next
        End If

        If dir <> 0 Then

            For c As Integer = col To word.Length - 1
                If Not puzzle(row, c).Equals("*") Then Return False
            Next
        End If

        Return True
    End Function

    Public Shared Sub Main(ByVal args As String())
        Dim rnd As Random = New Random()
        Dim wordList As List(Of String) = New List(Of String)()
        wordList.Add("encyclopedia")
        wordList.Add("Suhail")
        wordList.Add("deoxyribonucleic")
        wordList.Add("electromagnetic")
        wordList.Add("quantum")
        wordList.Add("visualbasic")
        Dim theGameW As wordSearch = New wordSearch(wordList)
    End Sub
End Class
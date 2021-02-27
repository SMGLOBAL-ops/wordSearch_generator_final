Imports System.IO
Imports System
Imports System.Collections.Generic

Public Class wordSearch_generator
    Private game_grid As String(,)
    Private found As List(Of String)

    Public Sub New(theWords As List(Of String))
        Dim rnd As Random = New Random()
        found = New List(Of String)()
        'Alphabet stored in string for randomly filling up wordsearch with letters
        Dim fill As String = "abcdefghijklmnopqrstuvwxyz"

        Dim row_input As Integer
        Dim col_input As Integer

        'Allows you to specify the row and column size for wordsearch (vbCrLf is needed for new line) 
        Console.Write("Input the row and column sizes in command prompt (must be same size!) ..." + vbCrLf)

        Console.Write("Please enter the desired row size: ")
        row_input = Console.ReadLine()

        Console.Write("Please enter the desired column size: ")
        col_input = Console.ReadLine()

        'Defining the wordsearch grid as empty 2D string array, where as indexing starts at 0, we need to -1 from row and -1 
        ' from the column index to give actual length of the wordsearch grid shown in command prompt
        game_grid = New String(row_input - 1, col_input - 1) {}

        For r As Integer = 0 To game_grid.GetLength(0) - 1

            For c As Integer = 0 To game_grid.GetLength(1) - 1
            'So to match any series of zero or more characters, use * - look up Regular Expressions ... 
                game_grid(r, c) = "*"
            Next
        Next

        For k As Integer = 0 To theWords.Count - 1
        
        ' Here we are using the rnd function to generate random positions 
        ' for the random letters that fill up the wordsearch rows and columns 
        ' (see: https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/rnd-function)
            Dim row As Integer = rnd.[Next](0, row_input)
            Dim col As Integer = rnd.[Next](0, col_input)
            Dim dir As Integer = rnd.[Next](0, 2)


        'While loop conditions that iterates through each word from 'theWords' List defined on line 9 and checks the length of the word
        ' being added to the row does not exceed the input game grid row size (GetLength(0)) or game grid column size (GetLength(1))
            While row + theWords(k).Length > game_grid.GetLength(0) OrElse col + theWords(k).Length > game_grid.GetLength(1)
                row = rnd.[Next](0, row_input)
                col = rnd.[Next](0, col_input)
            End While

            If check(dir, row, col, theWords(k)) Then
                found.Add(theWords(k))

                For m As Integer = 0 To theWords(k).Length - 1
                    game_grid(row, col) = theWords(k).Substring(m, 1)

                    If dir = 0 Then
                        row += 1
                    Else
                        col += 1
                    End If
                Next
            End If

            For r As Integer = 0 To game_grid.GetLength(0) - 1

                For c As Integer = 0 To game_grid.GetLength(1) - 1

                    If game_grid(r, c).Equals("*") Then
                        Dim spot As Integer = rnd.[Next](0, 26)
                        game_grid(r, c) = fill.Substring(spot, 1)
                    End If
                Next
            Next
        Next

        For r As Integer = 0 To game_grid.GetLength(0) - 1

            For c As Integer = 0 To game_grid.GetLength(1) - 1
                Console.Write(game_grid(r, c) & " ")
            Next

            Console.WriteLine()
        Next

        Console.WriteLine()

        'Prints out the used words in the wordsearch
        Console.WriteLine("Used words are :")
        For Each print As String In found
            Console.WriteLine(print)
        Next
    End Sub

    Public Function check(dir As Integer, row As Integer, col As Integer, word As String) As Boolean
        If dir = 0 Then

            For r As Integer = row To word.Length - 1
                If Not game_grid(r, col).Equals("*") Then Return False
            Next
        End If

        If dir <> 0 Then

            For c As Integer = col To word.Length - 1
                If Not game_grid(row, c).Equals("*") Then Return False
            Next
        End If

        Return True
    End Function


    Public Shared Sub Main(args As String())
        Dim wordList As List(Of String) = New List(Of String)()

        'File pathway for text file with words you want, where For loop goes through every word line by line as Strings
        For Each line As String In File.ReadLines("/uploads/message.txt")

            wordList.Add(line)
        Next line

        Dim theGameW As wordSearch_generator = New wordSearch_generator(wordList)
        Console.WriteLine("Made by Suhail Mustafa")

    End Sub
End Class

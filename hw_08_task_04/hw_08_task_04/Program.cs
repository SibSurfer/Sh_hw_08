class Program
{
    static bool Sudoku(string[,] board)
    {
        bool squares = true, rows = true, columns = true;
        Thread T_Cells = new Thread(() => squares = CheckCells(board));
        Thread T_Rows = new Thread(() => rows = CheckRows(board));
        Thread T_Columns = new Thread(() => columns = CheckColumns(board));

        T_Cells.Start();
        T_Rows.Start();
        T_Columns.Start();

        T_Cells.Join();
        T_Rows.Join();
        T_Columns.Join();

        return squares && rows && columns;
    }

    static bool CheckCells(string[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                HashSet<string> numbers = new HashSet<string>();
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        if (board[3 * i + di + 1, 3 * j + dj + 1] == ".")
                            continue;
                        if (!numbers.Add(board[3 * i + di + 1, 3 * j + dj + 1]))
                            return false;
                    }
                }
            }
        }
        return true;
    }

    static bool CheckRows(string[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            HashSet<string> numbers = new HashSet<string>();
            for (int j = 0; j < 9; j++)
            {
                if (board[i, j] == ".")
                    continue;
                if (!numbers.Add(board[i, j]))
                    return false;
            }
        }
        return true;
    }

    static bool CheckColumns(string[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            HashSet<string> numbers = new HashSet<string>();
            for (int j = 0; j < 9; j++)
            {
                if (board[j, i] == ".")
                    continue;
                if (!numbers.Add(board[j, i]))
                    return false;
            }
        }
        return true;
    }

    static void Main()
    {
        string[,] board = new string[,]
        {
            {"5","3",".",".","7",".",".",".","."},
            {"6",".",".","1","9","5",".",".","."},
            {".","9","8",".",".",".",".","6","."},
            {"8",".",".",".","6",".",".",".","3"},
            {"4",".",".","8",".","3",".",".","1"},
            {"7",".",".",".","2",".",".",".","6"},
            {".","6",".",".",".",".","2","8","."},
            {".",".",".","4","1","9",".",".","5"},
            {".",".",".",".","8",".",".","7","9"}
        };

        bool solvable = Sudoku(board);
        Console.WriteLine(solvable);

        string[,] board1 = new string[,]
        {
            {"8", "3", ".", ".", "7", ".", ".", ".", "."},
            {"6", ".", ".", "1", "9", "5", ".", ".", "."},
            {".", "9", "8", ".", ".", ".", ".", "6", "."},
            {"8", ".", ".", ".", "6", ".", ".", ".", "3"},
            {"4", ".", ".", "8", ".", "3", ".", ".", "1"},
            {"7", ".", ".", ".", "2", ".", ".", ".", "6"},
            {".", "6", ".", ".", ".", ".", "2", "8", "."},
            {".", ".", ".", "4", "1", "9", ".", ".", "5"},
            {".", ".", ".", ".", "8", ".", ".", "7", "9"}
        };

        bool solvable1 = Sudoku(board1);
        Console.WriteLine(solvable1); 
    }
}

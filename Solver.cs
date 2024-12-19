using System;
using System.Collections.Generic;

namespace SudokuProject
{
    internal class Solver
    {
        private int[,] board;

        // Κατασκευαστής που αντιγράφει τον αρχικό πίνακα
        public Solver(int[,] initialBoard)
        {
            board = new int[9, 9];
            Array.Copy(initialBoard, board, 9 * 9);
        }

        // Αναδρομική μέθοδος επίλυσης του Sudoku με backtracking
        public bool Solve()
        {
            if (!FindEmptyCell(board, out var row, out var col))
                return true;

            for (var num = 1; num <= 9; num++)
            {
                if (!IsSafe(board, row, col, num)) continue;
                board[row, col] = num;
                if (Solve())
                    return true;
                board[row, col] = 0; // Επαναφορά (backtrack)
            }
            return false;
        }

        // Επίλυση με BFS και χρήση ArrayList
        public bool SolveBFS_ArrayList()
        {
            var queue = new List<int[,]>();
            queue.Add((int[,])board.Clone());

            while (queue.Count > 0)
            {
                var currentBoard = queue[0];
                queue.RemoveAt(0);

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    queue.Add(newBoard);
                }
            }
            return false;
        }

        // Επίλυση με DFS και χρήση ArrayList
        public bool SolveDFS_ArrayList()
        {
            var stack = new List<int[,]> { (int[,])board.Clone() };

            while (stack.Count > 0)
            {
                var currentBoard = stack[^1]; // Χρήση του ^1 για το τελευταίο στοιχείο
                stack.RemoveAt(stack.Count - 1);

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    stack.Add(newBoard);
                }
            }
            return false;
        }

        // Επίλυση με DFS και χρήση Stack
        public bool SolveDFS_Stack()
        {
            var stack = new Stack<int[,]>();
            stack.Push((int[,])board.Clone());

            while (stack.Count > 0)
            {
                var currentBoard = stack.Pop();

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    stack.Push(newBoard);
                }
            }
            return false;
        }

        // Επίλυση με BFS και χρήση LinkedList
        public bool SolveBFS_LinkedList()
        {
            var queue = new LinkedList<int[,]>();
            queue.AddLast((int[,])board.Clone());

            while (queue.Count > 0)
            {
                var currentBoard = queue.First.Value;
                queue.RemoveFirst();

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    queue.AddLast(newBoard);
                }
            }
            return false;
        }

        // Μέθοδος για να βρούμε ένα κενό κελί
        private static bool FindEmptyCell(int[,] board, out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return true;
                }
            }

            row = -1;
            col = -1;
            return false; // Δεν υπάρχουν κενά κελιά
        }

        // Έλεγχος αν είναι ασφαλές να τοποθετήσουμε έναν αριθμό στο κελί
        private static bool IsSafe(int[,] currentBoard, int row, int col, int num)
        {
            return !UsedInRow(currentBoard, row, num) &&
                   !UsedInCol(currentBoard, col, num) &&
                   !UsedInBox(currentBoard, row - row % 3, col - col % 3, num);
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στη σειρά
        private static bool UsedInRow(int[,] currentBoard, int row, int num)
        {
            for (var col = 0; col < 9; col++)
            {
                if (currentBoard[row, col] == num)
                    return true;
            }
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στην στήλη
        private static bool UsedInCol(int[,] currentBoard, int col, int num)
        {
            for (var row = 0; row < 9; row++)
            {
                if (currentBoard[row, col] == num)
                    return true;
            }
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στο 3x3 τετράγωνο
        private static bool UsedInBox(int[,] currentBoard, int boxStartRow, int boxStartCol, int num)
        {
            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    if (currentBoard[row + boxStartRow, col + boxStartCol] == num)
                        return true;
                }
            }
            return false;
        }

        // Εκτύπωση της λύσης του Sudoku
        public void PrintSolution()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

namespace SudokuProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Έλεγχος για την ύπαρξη του ορίσματος
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Solver <inputFile>");
                return;
            }

            var inputFile = args[0];

            // Επιλογή μεθόδου επίλυσης από τον χρήστη
            Console.WriteLine("Select solving method:");
            Console.WriteLine("1: DFS with ArrayList");
            Console.WriteLine("2: BFS with ArrayList");
            Console.WriteLine("3: DFS with Stack");
            Console.WriteLine("4: BFS with LinkedList");
            Console.Write("Enter your choice (1-4): ");

            // Ανάγνωση επιλογής χρήστη και έλεγχος εγκυρότητας
            if (!int.TryParse(Console.ReadLine(), out var dataStructureNumber) ||
                dataStructureNumber < 1 || dataStructureNumber > 4)
            {
                Console.WriteLine("Invalid choice. Please choose a number between 1 and 4.");
                return;
            }

            var initialBoard = new int[9, 9];
            try
            {
                using StreamReader reader = new StreamReader(inputFile);
                // Ανάγνωση του πίνακα από το αρχείο
                for (var i = 0; i < 9; i++)
                {
                    var line = reader.ReadLine();
                    var numbers = line.Split(' ');
                    for (var j = 0; j < numbers.Length; j++)
                    {
                        initialBoard[i, j] = int.Parse(numbers[j]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the input file: " + ex.Message);
                return;
            }

            var sudoku = new Solver(initialBoard);
            var solved = false;

            // Επίλυση του Sudoku ανάλογα με την επιλογή του χρήστη
            switch (dataStructureNumber)
            {
                case 1:
                    solved = sudoku.SolveDFS_ArrayList();
                    break;
                case 2:
                    solved = sudoku.SolveBFS_ArrayList();
                    break;
                case 3:
                    solved = sudoku.SolveDFS_Stack();
                    break;
                case 4:
                    solved = sudoku.SolveBFS_LinkedList();
                    break;
            }

            // Εκτύπωση της λύσης ή του μηνύματος λάθους
            if (solved)
            {
                Console.WriteLine("Solved Sudoku:");
                sudoku.PrintSolution();
            }
            else
            {
                Console.WriteLine("No solution found for the Sudoku.");
            }
        }
    }
}

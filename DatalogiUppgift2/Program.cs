using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DatalogiUppgift2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            FileItems fileItem = new FileItems();
            // List to store divided txt-file-arrays
            

            // Adds txt-files
            string textFredskalla = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\DatalogiUppgift2\DatalogiUppgift2\fredskalla.txt").ToLower();
            string textKaladium = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\DatalogiUppgift2\DatalogiUppgift2\kaladium.txt").ToLower();
            string textMonstera = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\DatalogiUppgift2\DatalogiUppgift2\monstera.txt").ToLower();

            // Adds each words in a txt-files to an array
            string[] textFredskallaDiv = textFredskalla.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            string[] textKaladiumDiv = textKaladium.Split(new char[] { ' ', ',','.' }, StringSplitOptions.RemoveEmptyEntries);
            string[] textMonsteraDiv = textMonstera.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            List<FileItems> dividedFiles = new List<FileItems>();

            dividedFiles.Add(new FileItems() { Id = 0, Title = "Kaladium", Searchresult = 0, text = textKaladiumDiv });
            dividedFiles.Add(new FileItems() { Id = 1, Title = "Fredskalla", Searchresult = 0, text = textFredskallaDiv });
            dividedFiles.Add(new FileItems() { Id = 2, Title = "Monstera", Searchresult = 0, text = textMonsteraDiv });

            // List of arrays

            WordProgram(dividedFiles);


        }

        // Runs program
        public static void WordProgram(List<FileItems> txtfiles)
        {
            bool run = true;

            // Runs startmenu until user quits
            while (run)
            {
                PrintMainMenu();

                string menuInput = Console.ReadLine();

                switch (menuInput)
                {

                    case "1":
                        WordSeachInput(txtfiles);
                        break;
                    case "2":
                        PrintUnsortedList(txtfiles);
                        break;
                    case "3":
                        SortWordsInFiles(txtfiles);
                        break;
                    case "4":
                        run = false;
                        break;
                    default:
                        MenuError();
                        break;
                }
            }
        }

        // Prints main menu
        private static void PrintMainMenu()
        {
            Console.WriteLine("\n--MAIN MENU--\n" +
                    "1. Search word\n" +
                    "2. See files\n" +
                    "3. Sort words and get the x first\n" +
                    "4. Quit\n");
            Console.WriteLine("Enter your choice:");
        }

        // 1 - Starts a word search

        private static void WordSeachInput(List<FileItems> txtfiles)
        {
            bool runSearchInput = true;

            while (runSearchInput)
            {
                Console.WriteLine("Insert search word: ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == null)
                {
                    Console.WriteLine("Wrong input, Please enter a word");
                    break;
                }

                else
                {
                    string searchWord = userInput;
                    WordSearchMenu(txtfiles, searchWord);
                    runSearchInput = false;
                    break;
                }
            }
        }

        // 1 - After search, user gets options to print out result or go back till main menu
        public static void WordSearchMenu(List<FileItems> files,string word)
        {
            bool listMenuRun = true;

            while (listMenuRun)
            {
                PrintWordSearchMenu();

                string menuInput = Console.ReadLine();

                switch (menuInput)
                {

                    case "1":
                        ResultList(SearchInFiles(files, word));
                        break;
                    case "2":
                        listMenuRun = false;
                        break;
                    default:
                        MenuError();
                        break;
                }
            }

        }

        // 1 - Prints menu after user choose Search Word in main menu
        private static void PrintWordSearchMenu()
        {
            Console.WriteLine("" +
                    "1. See list of result\n" +
                    "2. Back to Main Menu\n");
            Console.WriteLine("Enter your choice:");
        }

        // 1.1 - Loops trough list of divided txt-file and search after word, adds searchresult to search tree
        public static List<FileItems> SearchInFiles(List<FileItems> txtfiles, string word)
        {
            BsearchTree resultsList = new BsearchTree();

            foreach (var file in txtfiles)
            {
                file.Searchresult = WordSeach(file.text, word);
                resultsList.insert(file.Searchresult);
            }

            return txtfiles;

        }

        // 1.1 - Search word i one file - Time complexity: O(n+1)
        public static int WordSeach(string[] file, string searchWord)
        {
            int result = 0;

            foreach (string word in file)
            {
                if (word == searchWord || word.Contains(searchWord))
                {
                    result++;
                }
            }

            return result;
        }

        // 1.1 - Prints our list of files sorted by searchresult
        public static void ResultList(List<FileItems> txtfiles)
        {
            Console.WriteLine($" \n" +
                " Titel: \t Results:");
            foreach (var file in txtfiles)
            {
                Console.WriteLine($" {file.Title} \t {file.Searchresult}");
            }

        }

        // 2 - Prints files unsorted
        private static void PrintUnsortedList(List<FileItems> txtfiles)
        {
            Console.WriteLine("Current files:");
            foreach (var file in txtfiles)
            {
                Console.WriteLine(file.Title);
            }
        }

        // 3 - Sorts words and shows the x amount of words to user

        public static void SortWordsInFiles(List<FileItems> txtfiles)
        {
            bool runSortedWords = true;

            while (runSortedWords)
            {
                Console.WriteLine("Insert amout of words to show: ");
                int userInput = Convert.ToInt32(Console.ReadLine());

                if (userInput == 0)
                {
                    Console.WriteLine("Wrong input, Please enter a word");
                    break;
                }

                else
                {
                    SortsWordInAlphabeticalOrder(txtfiles, userInput);
                    runSortedWords = false;
                    break;
                }
            }

        }

        // 3 - Sorts words in alphabetical order in every file - Time complexity: O(n^2+1+5)

        public static void SortsWordInAlphabeticalOrder(List<FileItems> txtfiles, int showWords)
        {
            List<string> words = new List<string>();

            foreach(var file in txtfiles)
            {
                foreach( string word in file.text)
                {
                    words.Add(word);
                }

                words.Sort();

                Console.Write($"\n{file.Title}:");
                for (int i = 0; i < showWords; i++)
                {
                    Console.Write($"{words[i]}");
                }
                Console.WriteLine("");
            }
        }

        // Error if user enters anything other then a number
        private static void MenuError()
        {
            Console.WriteLine("Wrong input, Enter a number between 1-2");
        }

    }

}

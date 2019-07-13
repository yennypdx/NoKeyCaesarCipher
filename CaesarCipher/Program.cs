using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

/* Part 1: Yenny Purwanto*/
namespace CaesarCipher
{
    public class Program
    {
        private static readonly string textFile = @"F:\Cryptography\Assignment1\CaesarCipher\samsung.txt";
        private static readonly string textFileDecrypted = @"F:\Cryptography\Assignment1\CaesarCipher\encrypted.txt";
        public static string TextFile => textFile;
        public static string TextFileDecrypted => textFileDecrypted;

        static void Main(string[] args)
        {
            var userKey = string.Empty;
            var userChoice = string.Empty;
            var userFileChoice = string.Empty;
            var userTextInput = string.Empty;
            var repeat = "y";

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine(":::::::::::::::::::::::::: Caesar Encryption :::::::::::::::::::::::::");
            Console.WriteLine("----------------------------------------------------------------------");
            do{
                Console.WriteLine("\nEnter key in integer: ");
                Console.Write(">> ");
                userKey = Console.ReadLine();
                Console.WriteLine("Would you like to [e]ncrypt or [d]ecrypt ? ");
                Console.Write(">> ");
                userChoice = Console.ReadLine();
                if (userChoice == "e"){
                    Console.WriteLine("Would you like to read already existing file (y/n)? ");
                    Console.Write(">> ");
                    userFileChoice = Console.ReadLine();

                    if (userFileChoice == "n") { 
                        Console.WriteLine("Enter your text here: ");
                        Console.Write(">> ");
                        userTextInput = Console.ReadLine();
                        Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                        CaesarEncryptionEngine(userTextInput, Int32.Parse(userKey));
                    }else{
                        Console.WriteLine("\nProgram is using a stored file called: samsung.txt");
                        Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                        string eFullText = TextFileReader(TextFile);
                        CaesarEncryptionEngine(eFullText, Int32.Parse(userKey));
                    }
                    
                }else if (userChoice == "d"){
                    Console.WriteLine("Would you like to read already existing file (y/n)? ");
                    Console.Write(">> ");
                    userFileChoice = Console.ReadLine();
                    if (userFileChoice == "n"){
                        Console.WriteLine("Enter your text here: ");
                        Console.Write(">> ");
                        userTextInput = Console.ReadLine();
                        Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                        CaesarDecryptionEngine(userTextInput, Int32.Parse(userKey));
                    }
                    else{
                        Console.WriteLine("\nProgram is using a stored file called: encrypted.txt");
                        Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                        string dFullText = TextFileReader(TextFileDecrypted);
                        CaesarDecryptionEngine(dFullText, Int32.Parse(userKey));
                    }
                }
                
                Console.WriteLine("\n\nDo another with new key (y/n)? ");
                Console.Write(">> ");
                repeat = Console.ReadLine();
            } while (repeat != "n");
        }

        public static string TextFileReader(string filepath)
        {
            var outText = string.Empty;
            if (File.Exists(filepath)){
                var fulltext = File.ReadAllText(filepath);
                outText = fulltext.ToLower();
            }else{
                Console.WriteLine("Failed to fetch the file");
            }
            return outText;
        }

        private static void CaesarEncryptionEngine(string fullText, int key)
        {
            var charToIntInAscii = 0;
            List<char> resultText = new List<char>();

            foreach (var letter in fullText)
            {
                charToIntInAscii = (int) letter;
                charToIntInAscii = (charToIntInAscii - 97 - key) % 26;
                if (charToIntInAscii < 0){
                    charToIntInAscii += 26;
                }
                charToIntInAscii += 97;
                
                var singleLetter = (char) charToIntInAscii;
                resultText.Add(singleLetter);
            }

            foreach (var letter in resultText){
                Console.Write(letter);
            }
        }

        private static void CaesarDecryptionEngine(string fullText, int key)
        {
            var charToIntInAscii = 0;
            List<char> resultText = new List<char>();

            foreach (var letter in fullText){
                charToIntInAscii = (int)letter;
                charToIntInAscii = (charToIntInAscii - 97 + key) % 26 + 97;

                var singleLetter = (char)charToIntInAscii;
                resultText.Add(singleLetter);
            }

            foreach (var letter in resultText){
                Console.Write(letter);
            }
        }
    }
}

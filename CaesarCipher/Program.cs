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
        public static string TextFile => textFile;

        static void Main(string[] args)
        {
            string userKey = string.Empty;
            string userFileChoice = string.Empty;
            string userFilePath = string.Empty;
            string repeat = "y";
            
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine(":::::::::::::::::::::::::: Caesar Encryption :::::::::::::::::::::::::");
            Console.WriteLine("----------------------------------------------------------------------");
            do{
                Console.WriteLine("\nPlease provide us with alphabetical key you would like to use: ");
                Console.Write(">> ");
                userKey = Console.ReadLine();
                Console.WriteLine("Would you like to read already existing file (y/n)? ");
                Console.Write(">> ");
                userFileChoice = Console.ReadLine();
                if (userFileChoice == "n"){
                    Console.WriteLine("Enter the file path here: ");
                    Console.Write(">> ");
                    userFilePath = Console.ReadLine();
                    Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                    TextFileReader(userFilePath, userKey);
                }else{
                    Console.WriteLine("Program is using a stored file called: samsung.txt");
                    Console.WriteLine("\n----- RESULT -------------------------------------------------\n");
                    TextFileReader(TextFile, userKey);
                }

                Console.WriteLine("\n\nWould you like to encrypt new file with new key (y/n)? ");
                Console.Write(">> ");
                repeat = Console.ReadLine();
                if (repeat != "y" || repeat != "n"){
                    Console.WriteLine("Please enter [n] or [y]: ");
                    Console.Write(">> ");
                    repeat = Console.ReadLine();
                }
            } while (repeat != "n");
        }

        public static string RemoveDuplicates(string inputText)
        {
            return new string(inputText.ToCharArray().Distinct().ToArray());
        }

        public static void TextFileReader(string filepath, string userKey)
        {
            string fulltext = string.Empty;
            string capitalizedText = string.Empty;
            string sortedUserKey = RemoveDuplicates(userKey);
            string capSortedUserKey = sortedUserKey.ToUpper();

            if (File.Exists(filepath)){
                fulltext = File.ReadAllText(filepath);
                capitalizedText = fulltext.ToUpper();

                CaesarEncryptionEngine(capitalizedText, capSortedUserKey);
            }else{
                Console.WriteLine("Failed to fetch the file");
            }
        }

        private static void CaesarEncryptionEngine(string capFullText, string capUserKey)
        {
            List<char> Plaintext = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
                'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            List<char> Alpha = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            List<char> Ciphertext = new List<char>();
            List<char> sortedAlpha = new List<char>();
            List<char> userKeyCharList = new List<char>();
            List<char> resultText = new List<char>();

            char[] tempkey = capUserKey.ToCharArray();
            for (int i = 0; i < capUserKey.Length; i++){
                userKeyCharList.Add(tempkey[i]);
            }

            foreach (char letter in Alpha){
                bool match = false;
                for (int i = 0; i < userKeyCharList.Count; i++){
                    if (letter.Equals(userKeyCharList[i])){
                        match = true;
                    }
                }
                if (match == false){
                    sortedAlpha.Add(letter);
                }
            }

            Ciphertext = userKeyCharList.Concat(sortedAlpha).ToList();

            var dictionary = Ciphertext.Zip(Plaintext, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v);
            foreach (char letter in capFullText)
            {
                bool match = false;
                char matchValue = ' ';
                for (int i = 0; i < dictionary.Count; i++)
                {
                    if (dictionary.ContainsKey(letter)){
                        match = true;
                        matchValue = dictionary[letter];
                    }else if(letter.Equals(' ')){
                        match = true;
                        matchValue = ' ';
                    }
                }
                if (match == true){
                    resultText.Add(matchValue);
                }
            }

            for (int i = 0; i < resultText.Count; i++){
                Console.Write(resultText[i]);
            }
        }
    }
}

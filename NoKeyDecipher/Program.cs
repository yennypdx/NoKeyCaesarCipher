using System;
using System.IO;
using System.Linq;

namespace NoKeyDecipher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var end = false;
            while (!end)
            {
                Console.WriteLine("Please type in location of desired file to decrypt: ");
                var fPath = Console.ReadLine();
                //var fPath = "C:\\emus\\code.txt";
                var text = File.ReadLines(fPath);


                var encryptedText = string.Join(",", text.ToArray()).ToLower();

                encryptedText = new String(encryptedText.Where(Char.IsLetter).ToArray());

                Console.WriteLine("Encrypted: " + encryptedText);

                String decryptedText = "";
                var decryptedCorrect = false;
                var key = 0;
                while (decryptedCorrect == false && key < 26)
                {
                    foreach (char testChar in encryptedText)
                    {
                        var asciiTestChar = (int) testChar;
                        asciiTestChar = (asciiTestChar - 97);
                        asciiTestChar = (asciiTestChar + key) % 26;
                        asciiTestChar += 97;
                        var realTestChar = (char) asciiTestChar;
                        decryptedText += realTestChar.ToString();
                    }

                    if (decryptedText.Contains("the"))
                    {
                        decryptedCorrect = true;
                        Console.WriteLine("Decrypted: " + decryptedText);
                    }

                    decryptedText = "";
                    key += 1;
                }

                if (decryptedCorrect != true)
                {

                    Console.WriteLine("Key Not Found");
                }
                else
                    Console.WriteLine("Key Found");

                Console.WriteLine("Would you like to decrypt another file? [y/n]");
                var read = Console.ReadLine();
                if (read == "n")
                    end = true;
            }
        }
    }
}

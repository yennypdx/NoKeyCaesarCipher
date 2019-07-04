﻿using System;
using System.IO;
using System.Linq;

namespace MonoAlphaDecipher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please type in location of desired file to decrypt: ");
            //var fPath = Console.ReadLine();
            var fPath = "C:\\emus\\code.txt";
            var text = File.ReadLines(fPath);

            var encryptedText = string.Join(",", text.ToArray()).ToLower();

            encryptedText = new String(encryptedText.Where(Char.IsLetter).ToArray());

            Console.WriteLine("Encrypted: " + encryptedText);

            String decryptedText = "";
            var startIndex = 0;

            var test = 0;
            var decryptedCorrect = false;
            var key = 0;
            while (decryptedCorrect == false || key < 26)
            {
                foreach (char testChar in encryptedText)
                {
                    var asciiTestChar = (int) testChar;
                    asciiTestChar = (asciiTestChar - 97);
                    asciiTestChar = (asciiTestChar + key) % 26;
                    asciiTestChar += 97;
                    var realTestChar = (char) asciiTestChar;
                    decryptedText += realTestChar.ToString();
                    startIndex++;
                }

                if (decryptedText.Contains("the"))
                {
                    decryptedCorrect = true;
                    Console.WriteLine("Decrypted: " + decryptedText);
                }

                decryptedText = "";
                key += 1;
            }

            Console.WriteLine("Key found");
            //var mostFreqLetter = encryptedText.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
            //var secondMostFreqLetter = encryptedText.GroupBy(x => x).OrderByDescending(x => x.Count()).;
            //Console.WriteLine("Most Freq Used Letter: " + mostFreqLetter);
            //var asciiValueOfLetter = (int) mostFreqLetter;


            //Console.WriteLine("Ascci Letter: " + asciiValueOfLetter);

            //var key = (asciiValueOfLetter + 128) % 128;

            //Console.WriteLine("Key: " + key);

            Console.ReadKey();
        }
    }
}

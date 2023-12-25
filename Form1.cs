using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerSecurity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comment_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (key1.Text.Length == 0)
            {
                ceaser.Text = "Please Enter The Key!!";
                ceaser.ForeColor = Color.Red;
            }

            else
            {
                int shift = int.Parse(key1.Text);
                string inputComment = comment.Text;
                ceaser.ForeColor = Color.Green;
                if (checkBox1.Checked)
                {
                    string DecryptedText = Dencrypt(inputComment, shift);
                    ceaser.Text = DecryptedText;
                }
                else
                {

                    string encryptedText = Encrypt(inputComment, shift);
                    ceaser.Text = encryptedText;
                }
            }
        }

        private static string Encrypt(string input, int shift)
        {
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    char offset = char.IsUpper(input[i]) ? 'A' : 'a';
                    result[i] = (char)((input[i] + shift - offset) % 26 + offset);
                }
                else
                {
                    // If the character is not a letter, keep it unchanged
                    result[i] = input[i];
                }
            }

            return new string(result);
        }

        private static string Dencrypt(string input, int shift)
        {
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    char offset = char.IsUpper(input[i]) ? 'A' : 'a';
                    result[i] = (char)((input[i] - shift - offset + 26) % 26 + offset);
                }
                else
                {
                    // If the character is not a letter, keep it unchanged
                    result[i] = input[i];
                }
            }

            return new string(result);
        }

        private void mono_Click(object sender, EventArgs e)
        {
            Dictionary<char, char> encryptionMap = CreateEncryptionMap();
            Dictionary<char, char> decryptionMap = CreateDencryptionMap();

            string Mono = comment.Text.ToUpper();
            if (checkBox2.Checked)
            {
                string DecryptionText = EncryptOrDecrypt(Mono, decryptionMap);
                textBox4.Text = DecryptionText;

            }
            else
            {
                string encryptedText = EncryptOrDecrypt(Mono, encryptionMap);
                textBox4.Text = encryptedText;

            }
            textBox4.ForeColor = Color.Green;
        }
        static Dictionary<char, char> CreateEncryptionMap()
        {
            // Create a simple encryption mapping (you can customize this)
            string plainAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string cipherAlphabet = "ZYXWVUTSRQPONMLKJIHGFEDCBA";

            Dictionary<char, char> encryptionMap = new Dictionary<char, char>();

            for (int i = 0; i < plainAlphabet.Length; i++)
            {
                encryptionMap.Add(plainAlphabet[i], cipherAlphabet[i]);
            }

            return encryptionMap;
        }

        static string EncryptOrDecrypt(string input, Dictionary<char, char> encryptionMap)
        {
            // Encrypt the input text using the provided mapping
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                // Check if the character is in the mapping
                if (encryptionMap.ContainsKey(currentChar))
                {
                    // Replace with the corresponding mapped character
                    result[i] = encryptionMap[currentChar];
                }
                else
                {
                    // If not in the mapping, keep the character unchanged
                    result[i] = currentChar;
                }
            }

            return new string(result);
        }


        static Dictionary<char, char> CreateDencryptionMap()
        {
            // Create a simple encryption mapping (you can customize this)
            string plainAlphabet = "ZYXWVUTSRQPONMLKJIHGFEDCBA";
            string cipherAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Dictionary<char, char> encryptionMap = new Dictionary<char, char>();

            for (int i = 0; i < plainAlphabet.Length; i++)
            {
                encryptionMap.Add(plainAlphabet[i], cipherAlphabet[i]);
            }

            return encryptionMap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string key = pfkey.Text.ToUpper();
            if (key.Length == 0)
            {
                textBox3.Text = "Please, Enter string key!!";
                textBox3.ForeColor = Color.Red;
            }
            else
            {
                char[,] matrix = CreateMatrix(key);
                string inputText = comment.Text.ToUpper().Replace("J", "I");

                // Insert "X" between consecutive identical characters
                inputText = InsertXBetweenIdenticalCharacters(inputText);
                string encryptedText = Encrypt(inputText, matrix);

/*                string encryptedText = ProcessText(inputText, matrix, true);
*/                /*                string encryptedText = Encrypt(inputText, matrix);
                */
                if (checkBox3.Checked)
                {
                    string decryptedText = Encrypt(inputText, matrix);
                    textBox3.Text = decryptedText;
                }
                else
                    textBox3.Text = encryptedText;
                textBox3.ForeColor = Color.Green;


            }
        }
        static string InsertXBetweenIdenticalCharacters(string input)
        {
            // Insert "X" between consecutive identical characters
            string modifiedText = "";
            char previousChar = '\0';//hello

            foreach (char currentChar in input)
            {

                if (currentChar == previousChar)
                {
                    modifiedText += 'X';
                }
                modifiedText += currentChar;

                previousChar = currentChar;
            }

            return modifiedText;
        }
        static char[,] CreateMatrix(string key)
        {
            // Create a matrix to store the Playfair key
            char[,] matrix = new char[5, 5];
            string keyWithoutDuplicates = RemoveDuplicateChars(key + "ABCDEFGHIKLMNOPQRSTUVWXYZ");

            int k = 0;

            // Fill the matrix with the key
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = keyWithoutDuplicates[k];
                    k++;
                }
            }

            return matrix;
        }

        static string RemoveDuplicateChars(string input)
        {
            // Remove duplicate characters from the input string
            HashSet<char> uniqueChars = new HashSet<char>(input);
            return new string(uniqueChars.ToArray());
        }

        static string Encrypt(string input, char[,] matrix)
        {
            string encryptedText = "";

            for (int i = 0; i < input.Length; i += 2)
            {
                char firstChar = input[i];
                char secondChar = (i + 1 < input.Length) ? input[i + 1] : 'X'; // Add 'X' if the input length is odd

                int[] firstPosition = FindPosition(matrix, firstChar);
                int[] secondPosition = FindPosition(matrix, secondChar);

                // If the letters are in the same row
                if (firstPosition[0] == secondPosition[0])
                {
                    firstPosition[1] = (firstPosition[1] + 1) % 5;
                    secondPosition[1] = (secondPosition[1] + 1) % 5;
                }
                // If the letters are in the same column
                else if (firstPosition[1] == secondPosition[1])
                {
                    firstPosition[0] = (firstPosition[0] + 1) % 5;
                    secondPosition[0] = (secondPosition[0] + 1) % 5;
                }
                // If the letters form a rectangle
                else
                {
                    int temp = firstPosition[1];
                    firstPosition[1] = secondPosition[1];
                    secondPosition[1] = temp;
                }

                encryptedText += matrix[firstPosition[0], firstPosition[1]];
                encryptedText += matrix[secondPosition[0], secondPosition[1]];
            }

            return encryptedText;
        }

        static int[] FindPosition(char[,] matrix, char target)
        {
            // Find the position of the target character in the matrix
            int[] position = new int[2];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == target)
                    {
                        position[0] = i;
                        position[1] = j;
                        return position;
                    }
                }
            }

            return position;
        }

        //Decrypt and Encrypt
        /*static string ProcessText(string input, char[,] matrix, bool encrypt)
        {
            string processedText = "";
            int direction = encrypt ? 1 : -1;

            for (int i = 0; i < input.Length; i += 2)
            {
                char firstChar = input[i];
                char secondChar = (i + 1 < input.Length) ? input[i + 1] : 'X'; // Add 'X' if the input length is odd

                int[] firstPosition = FindPosition(matrix, firstChar);
                int[] secondPosition = FindPosition(matrix, secondChar);

                // Adjust positions based on encryption/decryption direction
                firstPosition[0] = (firstPosition[0] + direction + 5) % 5;
                firstPosition[1] = (firstPosition[1] + direction + 5) % 5;
                secondPosition[0] = (secondPosition[0] + direction + 5) % 5;
                secondPosition[1] = (secondPosition[1] + direction + 5) % 5;

                processedText += matrix[firstPosition[0], firstPosition[1]];
                processedText += matrix[secondPosition[0], secondPosition[1]];
            }

            return processedText;
        }*/

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
using Microsoft.VisualBasic;
using System;

namespace ElChecador
{
    public class Encriptar
    {
        public string encript(string texto)
        {
            char c;
            int x, c_n;
            string newText = "";
            for (x = 0; x <= texto.Length - 1; x++)
            {
                c = Convert.ToChar(texto.Substring(x, 1));

                c_n = Strings.Asc(c) + 65;

                if (c_n > 255)
                    c = Strings.Chr(c_n - 255);
                else
                    c = Strings.Chr(c_n);
                newText += c;
            }
            return newText;
        }

        public string desencript(string texto)
        {
            char c;
            int c_n, x;
            string newText = "";
            for (x = 0; x <= texto.Length - 1; x++)
            {
                c = Convert.ToChar(texto.Substring(x, 1));
                c_n = Strings.Asc(c) - 65;
                if (c_n <= 0)
                    c = Strings.Chr(c_n + 255);
                else
                    c = Strings.Chr(c_n);
                newText += c;
            }
            return newText;
        }
    }
}

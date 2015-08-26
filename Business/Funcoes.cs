using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Reflection;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    public class Funcoes
    {
        public static string Formatar(string valor, string mascara)
        {

            MaskedTextProvider mtpCnpj = new MaskedTextProvider(mascara);
            mtpCnpj.Set(valor);
            var formatted = mtpCnpj.ToString();
            if (formatted.IndexOf(" ") == -1) return formatted;

            mascara = mascara.Replace("0", "#").Replace(@"\", "");
 
            StringBuilder dado = new StringBuilder();
            foreach (char c in valor)
            {
                if (Char.IsNumber(c) || c == 'x' || c == 'X')
                    dado.Append(c);
            }

            int indMascara = mascara.Length;
            int indCampo = dado.Length;
            for (; indCampo > 0 && indMascara > 0; )
            {
                if (mascara[--indMascara] == '#')
                    indCampo -= 1;
            }

            StringBuilder saida = new StringBuilder();
            for (; indMascara < mascara.Length; indMascara++)
            {
                saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);
            }

            return saida.ToString();
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            var rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }

        public static string KeyStript()
        {
            return "script" + new Random().Next(0, 9999).ToString();
        }
    }
}

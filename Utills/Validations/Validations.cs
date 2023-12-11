using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utills.Validations
{
    public static class Validations
    {
        public static bool IsEmailValid(string email)
        {
            return email.Contains("@");
        }

        public static bool IsCpfValid(string cpf)
        {
            //Verifica se o cpf contem apenas digitos
            if (cpf.Any(x => !char.IsDigit(x)))
            {
                return false;
            }

            // Verificar se o CPF possui 11 dígitos
            if (cpf.Length != 11)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais (caso contrário, não seria válido)
            if (cpf.Distinct().Count() == 1)
            {
                return false;
            }

            // Calcular e verificar os dígitos verificadores
            int[] cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += cpfArray[i] * (10 - i);
            }

            int resto = soma % 11;
            int digitoVerificador1 = (resto < 2) ? 0 : 11 - resto;

            if (digitoVerificador1 != cpfArray[9])
            {
                return false;
            }

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += cpfArray[i] * (11 - i);
            }

            resto = soma % 11;
            int digitoVerificador2 = (resto < 2) ? 0 : 11 - resto;

            return digitoVerificador2 == cpfArray[10];
        }
    }
}

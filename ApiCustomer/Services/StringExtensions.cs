namespace ApiCustomer.Services
{
    public static class StringExtension
    { 
        public static string FormatCPF(this string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace("-", "").Replace(",", "");
        }
    }
}

namespace Benner.Microondas.Comuns.Extensions
{
    public static class IntExtensions
    {
        public static bool IsNullOrZero(this int? valor)
        {
            if (valor == null)
                return true;

            return valor == 0;
        }
    }
}

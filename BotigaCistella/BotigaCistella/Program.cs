namespace BotigaCistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            (string[] productes, int[] quantitat, int nElem, double diners) cistella = (new string[5], new int[5], 0, 0);
            /*
            cistella.productes[0] = "Durum";
            cistella.quantitat[0] = 1;
            cistella.nElem++;
            cistella.diners += 5.20;
            */
        }
        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga ,string producte, int quantitat)
        {
            if (ExisteixABotiga(botiga, producte))
            {

            }
            else Console.WriteLine("ERROR El producte no existeix a la botiga");
        }
        static bool ExisteixABotiga((string[] productes, double[] preu, int nElem) botiga, string producte)
        {
            int i = 0;
            while(i < 0 && botiga.productes[i] != producte)
            {
                i++;
            }
            return botiga.productes[i] == producte;
        }
    }
}

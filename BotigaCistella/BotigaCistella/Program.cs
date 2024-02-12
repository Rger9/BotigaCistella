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

            MostrarBotiga(Botiga());
        }

        static void MostrarBotiga((string[] producte, double[] preu, int nElem) botiga)
        {
            for (int i = 0; i < botiga.nElem; i++)
            {
                LiniaBotiga(botiga.producte[i], botiga.preu[i]);
            }
        }

        static void LiniaBotiga(string producte, double preu)
        {
            Console.Write(producte.PadRight(20, '.') + Convert.ToString(preu).PadLeft(20, '.') + " E \n");
        }
         
        static (string[], double[], int) Botiga()
        {
            (string[] productes, double[] preu, int nElem) botiga = (new string[5], new double[5], 5);
            botiga.productes = ["Durum", "Kebab", "Patates", "Pizza", "Menu"];
            botiga.preu = [3, 3, 1.5, 4, 6];
            return botiga;
        }

        static void AfegirProducte(ref (string[] producte, double[] preu, int nElem) botiga)
        {
            string nouProducte;
            double nouPreu;
            Console.WriteLine("Quin producte vols afegir?");
            nouProducte = Console.ReadLine();
            Console.WriteLine("Preu del producte?");
            nouPreu = Convert.ToDouble(Console.ReadLine());

            botiga.nElem++;
            botiga.producte[botiga.nElem - 1] = nouProducte;
            botiga.preu[botiga.nElem - 1] = nouPreu;
        }
        

    }
}

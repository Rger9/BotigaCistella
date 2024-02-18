using System.Text.Json;

namespace BotigaCistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (string[] productes, int[] quantitat, int nElem, double diners) cistella = (new string[5], new int[5], 2, 10.7);
            AmpliarCistella(ref cistella);

            Console.WriteLine("Hello, World!");
            (string[] productes, int[] quantitat, int nElem, double diners) cistella1 = (new string[5], new int[5], 0, 0);
            /*
            cistella.productes[0] = "Durum vegetal";
            cistella.quantitat[0] = 1;
            cistella.nElem++;
            cistella.diners += 5.20;
            */
        }

        //MÈTODE Botiga - Crida a la Botida, amb tots els seus productes, preus i número d'elements
        static (string[], double[], int) Botiga()
        {
            (string[] productes, double[] preu, int nElem) botiga = (new string[5], new double[5], 5);
            botiga.productes = ["Durum", "Kebab", "Patates", "Pizza", "Menu"];
            botiga.preu = [3, 3, 1.5, 4, 6];
            return botiga;
        }
        //METODE Cistella - Afegeix un producte i una quantitat a la cistella
        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga ,string producte, int quantitat)
        {
            int posicioProducte = ExisteixABotiga(botiga, producte);
            if ( posicioProducte >= 0)
            {
                if(cistella.productes.Length == cistella.nElem)
                {
                    Console.WriteLine("No et queda espai a la cistella!");
                    AmpliarCistella(ref cistella);
                }
                double dinersNou = cistella.diners - botiga.preu[posicioProducte] * quantitat;
                if (dinersNou < 0)
                {
                    Console.WriteLine("No tens diners, estas pobre!");
                    //AfegirDiners(cistella);
                }
                if (ProductesPlens(botiga.producte, botiga.nElem))
                {

                }
                else
                {
                    Console.WriteLine("ERROR No hi ha espai a la botiga, entra el numero de elements que hi vols ampliar");

                }
            }
            else Console.WriteLine("ERROR El producte no existeix a la botiga");
        }
        //METODE AmpliarCistella - Amplia la cistella demanant el numero per consola
        static void AmpliarCistella(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella)
        {
            int num;
            do
            {
                Console.Clear();
                Console.WriteLine("Quants espais vols ampliar a la botiga?");
            } while (!int.TryParse(Console.ReadLine(), out num) || num <= 0);
            Console.WriteLine(num);

            int nouNumElem = cistella.productes.Length + cistella.nElem;
            AmpliarProductesQuantitatCistella(ref cistella.productes, ref cistella.quantitat, nouNumElem);
            cistella.nElem = nouNumElem;
        }
        //METODE AmpliarCistella - Amplia els arrays de productes i quantitat
        static void AmpliarProductesQuantitatCistella(ref string[] productes, ref int[] quantitat, int nouNumElem)
        {
            string[] productesNou = new string[nouNumElem];
            int[] quantitatNou = new int[nouNumElem];
            for (int i = 0; i < productes.Length; i++)
            {
                productesNou[i] = productes[i];
                quantitatNou[i] = quantitat[i];
            }
            productes = productesNou;
            quantitat = quantitatNou;
        }
        //METODE AfegirDiners - Afegeix diners a la cistella
        static void AfegirDiners(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella)
        {
            double num;
            do
            {
                Console.Clear();
                Console.WriteLine("Quants diners vols afegir?");
            } while (!double.TryParse(Console.ReadLine(), out num) || num <= 0);
            cistella.diners += num;
        }
        // MÈTODE ExistexABotiga - Comprova si ja existeix el producte
        static int ExisteixABotiga((string[] productes, double[] preu, int nElem) botiga, string producte)
        {
            int i = 0;
            while(i < 0 && botiga.productes[i] != producte)
            {
                i++;
            }
            if (botiga.productes[i] != producte) i = -1;
            return i;
        }

        

        static bool ProductesPlens(string[] productes, int nElem)
        {
            return productes.Length == nElem;
        }
        // MÈTODE MostrarBotiga - Mostra de forma amigable la botiga (producte + preu)
        static void MostrarBotiga((string[] producte, double[] preu, int nElem) botiga)
        {
            for (int i = 0; i < botiga.nElem; i++)
            {
                LiniaBotiga(botiga.producte[i], botiga.preu[i]);
            }
        }

        // MÈTODE LiniaBotiga - Mostra una linia de producte + preu
        static void LiniaBotiga(string producte, double preu)
        {
            Console.Write(producte.PadRight(20, '.') + Convert.ToString(preu).PadLeft(20, '.') + " E \n");
        }
         
        // MÈTODE AfegirProducte - Afegeix un producte a la Botiga.
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
        
        // MÈTODE ModificarPreu - Canvia el preu d'un producte, si existeix
        static void ModificarPreu(ref (string[] producte, double[] preu, int nElem) botiga, string producte, double preu)
        {
            if (ExisteixABotiga(Botiga(), producte) == -1)
            {
                Console.WriteLine($"No s'ha trobat el producte '{producte}' a la botiga");
            }
            else
            {
                int i;
                for (i = 0; botiga.producte[i] != producte; i++) ;
                botiga.preu[i] = preu;
            }
        }

        // MÈTODE ModificarProducte - Canvia un producte antic per un de nou, si existeix
        static void ModificarProducte(ref (string[] producte, double[] preu, int nElem) botiga, string producteAntic, string producteNou)
        {
            if (ExisteixABotiga(Botiga(), producteAntic) == -1)
            {
                Console.WriteLine($"No s'ha trobat el producte '{producteAntic}' a la botiga");
            }
            else
            {
                int i;
                for (i = 0; botiga.producte[i] != producteAntic; i++) ;
                botiga.producte[i] = producteNou;
            }
        }

        // MÈTODE OrdenarPreu - Ordena la botiga per preu, de menys a més
        static (string[], double[], int) OrdenarPreu((string[] producte, double[] preu, int nElem) botiga, int indexEsquerra, int indexDreta)
        {
            int i = indexEsquerra;
            int j = indexDreta;
            double pivot = botiga.preu[i];
            while (i <= j)
            {
                while (botiga.preu[i] < pivot)
                {
                    i++;
                }

                while (botiga.preu[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    Permutar(ref botiga.preu[i], ref botiga.preu[j]);
                    Permutar(ref botiga.producte[i], ref botiga.producte[j]);
                    i++;
                    j--;
                }
            }

            if (indexEsquerra < j)
                OrdenarPreu(botiga, indexEsquerra, j);
            if (i < indexDreta)
                OrdenarPreu(botiga, i, indexDreta);
            return botiga;
        }

        // MÈTODE OrdenarProducte - Ordena la botiga per producte, de A a Z.
        static (string[], double[], int) OrdenarProducte((string[] producte, double[] preu, int nElem) botiga, int indexEsquerra, int indexDreta)
        {
            int i = indexEsquerra;
            int j = indexDreta;
            string pivot = botiga.producte[i];
            while (i <= j)
            {
                while (botiga.producte[i].CompareTo(pivot) == -1)
                {
                    i++;
                }

                while (botiga.producte[j].CompareTo(pivot) == 1)
                {
                    j--;
                }
                if (i <= j)
                {
                    Permutar(ref botiga.preu[i], ref botiga.preu[j]);
                    Permutar(ref botiga.producte[i], ref botiga.producte[j]);
                    i++;
                    j--;
                }
            }

            if (indexEsquerra < j)
                OrdenarProducte(botiga, indexEsquerra, j);
            if (i < indexDreta)
                OrdenarProducte(botiga, i, indexDreta);
            return botiga;
        }

        // MÈTODE Permutar - Converteix a en b i viceversa
        static void Permutar(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }

        static void Permutar(ref double a, ref double b)
        {
            (a, b) = (b, a);
        }

        static void Permutar(ref string a, ref string b)
        {
            (a, b) = (b, a);
        }
    }
}

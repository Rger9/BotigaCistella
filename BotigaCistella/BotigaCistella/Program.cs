namespace BotigaCistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (string[] productes, double[] preu, int nElem) botiga = Botiga();
            MostrarBotiga(botiga);
            AfegirProducte(ref botiga);
            MostrarBotiga(botiga);


        }


        // MÈTODE UsuariBotiga - Pregunta a l'usuari si és el propietari, o un comprador. Si és el propietari preguntarà per una contrassenya
        static void UsuariBotiga()
        {
            string pregunta =   "1 - Vull entrar a la botiga com a PROPIETARI" +
                                "2 - Soc un comprador";
            Console.WriteLine(pregunta);
        }

        // MÈTODE Botiga - Crida a la Botida, amb tots els seus productes, preus i número d'elements
        static (string[], double[], int) Botiga()
        {
            (string[] productes, double[] preu, int nElem) botiga = (new string[5], new double[5], 5);
            botiga.productes = ["Durum", "Kebab", "Patates", "Pizza", "Me(nu"];
            botiga.preu = [3, 3, 1.5, 4, 6];
            return botiga;
        }

        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga ,string producte, int quantitat)
        {
            if (ExisteixABotiga(botiga, producte))
            {

            }
            else Console.WriteLine("ERROR El producte no existeix a la botiga");
        }

        // MÈTODE ExistexABotiga - Comprova si ja existeix el producte
        static bool ExisteixABotiga((string[] productes, double[] preu, int nElem) botiga, string producte)
        {
            int i = 0;
            while(i < 0 && botiga.productes[i] != producte)
            {
                i++;
            }
            return botiga.productes[i] == producte;
        }

        // MÈTODE MostrarBotida - Mostra de forma amigable la botiga (producte + preu)
        static void MostrarBotiga((string[] producte, double[] preu, int nElem) botiga)
        {
            for (int i = 0; i < botiga.nElem; i++)
            {
                LiniaBotiga(botiga.producte[i], botiga.preu[i]);
            }
        }

        // MÈTODE LiniaBotida - Mostra una linia de producte + preu
        static void LiniaBotiga(string producte, double preu)
        {
            Console.Write(producte.PadRight(20, '.') + Convert.ToString(preu).PadLeft(20, '.') + " E \n");
        }
         
        // MÈTODE AfegirProducte - Afegeix un o molts productes a la Botiga.
        static void AfegirProducte(ref (string[] producte, double[] preu, int nElem) botiga)
        {
            string nouProducte, nouPreu;
            Console.WriteLine("Quin producte/productes vols afegir?");
            nouProducte = Console.ReadLine();
            Console.WriteLine("Preu/Preus del producte?");
            nouPreu = Console.ReadLine();
            int i = botiga.nElem;
            // nouProducte = pomes
            // nouProducte = pomes, peres, mandarines, melons
            while (nouProducte != "")
            {
                botiga.nElem++;
                if (nouProducte.Contains(", "))
                {
                    botiga.producte[i] = nouProducte.Substring(0, nouProducte.IndexOf(','));
                    nouProducte = nouProducte.Substring(nouProducte.IndexOf(',')+2);
                    botiga.preu[i] = Convert.ToDouble(nouProducte.Substring(0, nouProducte.IndexOf(',')));
                    nouProducte = nouProducte.Substring(nouProducte.IndexOf(", ") + 2);
                    i++;
                }
                else
                {
                    botiga.producte[i] = nouProducte;
                    botiga.preu[i] = Convert.ToDouble(nouPreu);
                    nouProducte = "";
                }
            }
            Console.WriteLine("Els teus productes amb els respectius preus han estat afegits a la botiga");
        }

                
        // MÈTODE ModificarPreu - Canvia el preu d'un producte, si existeix
        static void ModificarPreu(ref (string[] producte, double[] preu, int nElem) botiga, string producte, double preu)
        {
            if (!ExisteixABotiga(Botiga(), producte))
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
            if (!ExisteixABotiga(Botiga(), producteAntic))
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

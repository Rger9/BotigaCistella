using System.Text.Json;

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


            Console.WriteLine("Hello, World!");
            (string[] productes, int[] quantitat, int nElem, double diners) cistella1 = (new string[5], new int[5], 0, 0);
            /*
            cistella.productes[0] = "Durum vegetal";
            cistella.quantitat[0] = 1;
            cistella.nElem++;
            cistella.diners += 5.20;
            */
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
            botiga.productes = ["Durum", "Kebab", "Patates", "Pizza", "Menu"];
            botiga.preu = [3, 3, 1.5, 4, 6];
            return botiga;
        }
        

        //METODE ComprarProducte - Afegeix un producte i una quantitat a la cistella
        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga ,string producte, int quantitat)
        {
            int posicioProducte = ExisteixABotiga(botiga, producte);
            if (posicioProducte >= 0)
            {
                if(cistella.productes.Length == cistella.nElem)
                {
                    Console.WriteLine("No et queda espai a la cistella!");
                    AmpliarCistella(ref cistella);
                }
                double dinersNous = cistella.diners - botiga.preu[posicioProducte] * quantitat;
                while (dinersNous < 0)
                {
                    Console.WriteLine($"No tens diners, estas pobre {-dinersNous} euros!");
                    AfegirDiners(ref cistella);
                }
                AppendCistella(ref cistella, producte, quantitat);
                ActualitzaDinersCistella(ref cistella, dinersNous);
            }
            else Console.WriteLine("ERROR El producte no existeix a la botiga");
        }
        //METODE ComprarProducte - Afegeix una taula de productes i una taula de quantitats a la cistella, el metode acaba si hi ha algun producte que no el troba a la botiga
        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga, string[] producte, int[] quantitat)
        {
            int[] posicioProductes = ExisteixABotiga(botiga, producte);
            if (posicioProductes.Length == 0)
            {
                if (cistella.productes.Length <= cistella.nElem + producte.Length)
                {
                    Console.WriteLine("No et queda espai a la cistella!");
                    AmpliarCistella(ref cistella);
                }
                double dinersNous = DiferenciaDiners(botiga, posicioProductes, quantitat, cistella.diners);
                while (dinersNous < 0)
                {
                    Console.WriteLine($"No tens diners, estas pobre, necessites {-dinersNous} euros!");
                    AfegirDiners(ref cistella);
                }
                for(int i = 0; i < producte.Length; i++)
                {
                    if(PosicioProducteACistella(cistella.productes, cistella.nElem, producte[i]) == -1)
                    {
                        AppendCistella(ref cistella, producte[i], quantitat[i]);
                    }
                    else
                    {
                        cistella.quantitat[posicioProductes[i]] += quantitat[i];
                    }
                }
                ActualitzaDinersCistella(ref cistella,dinersNous);
            }
            else Console.WriteLine("ERROR El producte no existeix a la botiga");
        }
        //METODE DiferenciaDiners - Calcula la diferencia de diners que hi hauria si es compressin tots els productes de la taula productes
        static double DiferenciaDiners((string[] producte, double[] preu, int nElem) botiga, int[] posicioProductes, int[] quantitat, double dinersActuals)
        {
            double dinersNous = dinersActuals;
            for(int i = 0; i < posicioProductes.Length; i++)
            {
                dinersNous -= botiga.preu[posicioProductes[i]] * quantitat[i];
            }
            return dinersNous;
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
                Console.WriteLine("Quants diners vols afegir?");
            } while (!double.TryParse(Console.ReadLine(), out num) || num <= 0);
            cistella.diners += num;
        }
        // METODE PosicioProducteACistella - Retorna la posicio del producte a la cistella, si no el troba, retorna -1
        static int PosicioProducteACistella(string[] productesCistella, int nElem, string producte)
        {
            int posicio = -1, i = 0;
            while (i < nElem && productesCistella[i] == producte) i++;
            if (productesCistella[i] == producte) posicio = i;
            return posicio;
        }
        // METODE AppendCistella - Posa un producte al final de la cistella
        static void AppendCistella(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, string producte, int quantitat)
        {
            if (cistella.nElem < cistella.productes.Length)
            {
                cistella.productes[cistella.nElem] = producte;
                cistella.quantitat[cistella.nElem] = quantitat;
                cistella.nElem++;
            }
            else throw new Exception("ERROR AL AFEGIR UN PRODUCTE EN APPENDCISTELLA");
        }
        // METODE ActualitzaDinersCistella
        static void ActualitzaDinersCistella(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, double dinersNous)
        {
            cistella.diners = dinersNous;
        }
        // MÈTODE ExistexABotiga - Comprova si ja existeix el producte
        static int ExisteixABotiga((string[] productes, double[] preu, int nElem) botiga, string producte)
        {
            int i = 0;
            while(i < botiga.productes.Length && botiga.productes[i] != producte)
            {
                i++;
            }
            if (botiga.productes[i] != producte) i = -1;
            return i;
        }
        // METODE OrdenarCistella - Ordena la cistella
        static void OrdenarCistella(string[] productes, int[] quantitat, int nElem)
        {
            for(int numVolta = 0; numVolta < nElem - 1; numVolta++)
            {
                for(int i=0; i < nElem - 1; i++)
                {
                    if (productes[i].CompareTo(productes[i + 1]) > 0)
                    {
                        Permutar(ref productes[i], ref productes[i + 1]);
                    }
                }
            }
        }
        // MÈTODE MostrarCistella - Mostra de forma amigable la cistella (producte + preu)
        static void MostrarCistella((string[] productes, int[] quantitat, int nElem, double diners) cistella, (string[] productes, double[] preu, int nElem) botiga)
        {
            Console.WriteLine(CistellaToString(cistella, botiga));
        }
        // METODE CistellaToString - Retorna en forma d'string la cistella
        static string CistellaToString((string[] productes, int[] quantitat, int nElem, double diners) cistella, (string[] productes, double[] preu, int nElem) botiga)
        {
            string s = "";
            for (int i = 0; i < cistella.nElem; i++)
            {
                int pos = ExisteixABotiga(botiga, cistella.productes[i]);
                int quantitatTiquet = cistella.quantitat[i];
                double preuUnitari = botiga.preu[pos];
                double preuTotal = quantitatTiquet * preuUnitari;
                s += cistella.productes[i].PadRight(10, '.') + (Convert.ToString(preuUnitari) + "X" + Convert.ToString(quantitatTiquet) + ".." + Convert.ToString(preuTotal)).PadLeft(10, '.') + " E \n";
            }
            return s;
        }

        // MÈTODE ExistexABotiga - Comprova si ja existeixen TOTS els productes de la taula de productes entrada
        static int[] ExisteixABotiga((string[] productes, double[] preu, int nElem) botiga, string[] producte)
        {
            int i = 0, j = 0;
            int[] posicionsTrobats = new int[producte.Length];
            while(j != -1 && i < producte.Length)
            {
                j = 0;
                while (j < botiga.productes.Length && botiga.productes[j] != producte[i])
                {
                    j++;
                }
                posicionsTrobats[i] = j;
                if (botiga.productes[j] != producte[i]) j = -1;
                
            }
            if (j == -1) return new int[0];
            return posicionsTrobats;
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
         

        // MÈTODE AfegirProducte - Afegeix un o molts productes a la Botiga.
        static void AfegirProducte(ref (string[] producte, double[] preu, int nElem) botiga)
        {
            string producteFrase, preuFrase;
            Console.WriteLine("Quin producte/productes vols afegir? FORMAT: 'pomes, peres, cireres, melons'");
            producteFrase = Console.ReadLine();
            // FORMAT: macarrons, pomes, peres
            string[] productes = producteFrase.Split(", ");
            Console.WriteLine("Preu/Preus del producte? FORMAT: '2, 2,5, 3, 3,5'");
            preuFrase = Console.ReadLine();
            //FORMAT: 1.5, 2, 3

            // Hem tret aquesta línia de codi d'internet, Fa un split de la linia de preus i la converteix a un Array de Double.
            double[] preus = Array.ConvertAll(preuFrase.Split(", "), new Converter<string, double>(Double.Parse));

            AppendBotiga(ref botiga, productes, preus);
            Console.WriteLine("Els teus productes amb els respectius preus han estat afegits a la botiga");
        }
        // MÈTODE AppendBotiga - Amplia UN espai la botiga
        static void AppendBotiga(ref (string[] productes, double[] preus, int nElem) botiga, string[] productes, double[] preus)
        {
            int elementsTotals = botiga.nElem + preus.Length;
            string[] productesNou = new string[elementsTotals];
            double[] preusNou = new double[elementsTotals];

            // EX: tenim 5 productes a la botiga i volem afegir-hi 3
            for (int i = 0; i < elementsTotals; i++)
            {
                if (i < botiga.nElem)
                {
                    // Index 0 a 4 son els productes ORIGINALS de la botiga.
                    productesNou[i] = botiga.productes[i];
                    preusNou[i] = botiga.preus[i];
                }
                else
                {
                    // Index 5 a 7 son els productes AFEGITS de l'usuari, els productes i - 5
                    productesNou[i] = productes[i - botiga.nElem];
                    preusNou[i] = preus[i - botiga.nElem];
                }
            }
            botiga.productes = productesNou;
            botiga.preus = preusNou;
            botiga.nElem = elementsTotals;
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

using System.Text.Json;

namespace BotigaCistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (string[] productes, double[] preu, int nElem) botiga = Botiga();
            (string[] productes, int[] quantitat, int nElem, double diners) cistella = (new string[5], new int[5], 0, 0);

            char opcio = ' ';
            while (opcio != 'q' && opcio != 'Q')
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine((Menu()));
                    opcio = Console.ReadKey().KeyChar;
                }
                while (!ValidarOpcio(opcio));
                Console.Clear();
                SeleccionarOpcio(opcio, ref botiga, ref cistella);

            Console.WriteLine("Hello, World!");
            (string[] productes, int[] quantitat, int nElem, double diners) cistella1 = (new string[5], new int[5], 0, 0);
            /*
            cistella.productes[0] = "Durum vegetal";
            cistella.quantitat[0] = 1;
            cistella.nElem++;
            cistella.diners += 5.20;
            */
            }
        }
        // Mètode MENU
        static string Menu()
        {
            string menu;

            menu =

                $"\n╔════════════════════════════════╗\n" +
                $"║             BOTIGA             ║\n" +
                $"╠════════════════════════════════╣\n" +
                $"║  0 - Mostrar botiga            ║\n" +
                $"║  1 - Afegir producte botiga    ║\n" +
                $"║  2 - Modificar preu botiga     ║\n" +
                $"║  3 - Modificar producte        ║\n" +
                $"║  4 - Ampliar botiga            ║\n" +
                $"║  5 - Ordenar botiga            ║\n" +
                $"╠════════════════════════════════╣\n" +
                $"║            CISTELLA            ║\n" +
                $"╠════════════════════════════════╣\n" +
                $"║  6 - Comprar producte          ║\n" +
                $"║  7 - Ordenar Cistella          ║\n" +
                $"║  8 - Mostrar Cistella          ║\n" +
                $"╚════════════════════════════════╝\n\n\n" +
                $"Prem 'Q' per sortir de l'interfaç...";

            return menu;
        }



        // Mètode ValidarOpció
        static bool ValidarOpcio(char lletra)
        {
            return (lletra >= '0' && lletra < '9' || lletra == 'q' || lletra == 'Q');
        }

        // Mètode MostrarOpcio
        static void SeleccionarOpcio(int opcio, ref (string[] productes, double[] preu, int nElem) botiga, ref (string[] productes, int[] quantitat, int nElem, double diners) cistella)
        {
            int a, b;
            char c = (char)opcio;
            switch (opcio)
            {
                case '0':
                    Console.WriteLine(BotigaToString(botiga));
                    PremPerContinuar();
                    break;

                case '1':
                    AfegirProducte(ref botiga);
                    PremPerContinuar();
                    break;

                case '2':
                    ModificarPreu(ref botiga);
                    PremPerContinuar();
                    break;

                case '3':
                    ModificarProducte(ref botiga);
                    PremPerContinuar();
                    break;

                case '4':
                    AmpliarBotiga(ref botiga);
                    PremPerContinuar();
                    break;

                case '5':
                    char pa = ' ';
                    Console.WriteLine("Vols ordenar per preu (introdueix 'p') o alfabèticament (introdueix 'a')?");
                    pa = Console.ReadKey().KeyChar;
                    while (pa != 'p' && pa != 'a')
                    {
                        Console.Clear();
                        Console.WriteLine("ERROR, introdueix 'p' o 'a'");
                        Console.WriteLine("Vols ordenar per preu (introdueix 'p') o alfabèticament (introdueix 'a')?");
                        pa = Console.ReadKey().KeyChar;
                    }
                    if (pa == 'p')
                    {
                        OrdenarPreu(botiga, 0, botiga.nElem - 1);
                    }
                    else if (pa == 'a')
                    {
                        OrdenarProducte(botiga, 0, botiga.nElem - 1);
                    }
                    PremPerContinuar();
                    break;

                case '6':
                    Console.WriteLine("Quin producte vols comprar?");
                    string producte = Console.ReadLine();
                    Console.WriteLine("Quantitat?");
                    int quantitat = Convert.ToInt32(Console.ReadLine());
                    ComprarProducte(ref cistella, ref botiga, producte, quantitat);
                    PremPerContinuar();
                    break;

                case '7':
                    OrdenarCistella(cistella.productes, cistella.quantitat, cistella.nElem);
                    PremPerContinuar();
                    break;

                case '8':
                    MostrarCistella(cistella, botiga);
                    PremPerContinuar();
                    break;
            }
        }

        // Mètode PremPerContinuar
        static void PremPerContinuar()
        {
            Console.WriteLine($"\n\n\t-----------------------------------------");
            Console.WriteLine($"\tPrem qualsevol botó per tornar al menú...");
            char continuar = Console.ReadKey().KeyChar;
        }



        // MÈTODE UsuariBotiga - Pregunta a l'usuari si és el propietari, o un comprador. Si és el propietari preguntarà per una contrassenya
        static void UsuariBotiga()
        {
            string pregunta = "1 - Vull entrar a la botiga com a PROPIETARI" +
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
        

        
        //METODE Cistella - Afegeix un producte i una quantitat a la cistella
        static void ComprarProducte(ref (string[] productes, int[] quantitat, int nElem, double diners) cistella, ref (string[] producte, double[] preu, int nElem) botiga, string producte, int quantitat)
        {
            int posicioProducte = ExisteixABotiga(botiga, producte);
            if (posicioProducte >= 0)
            {
                if (cistella.productes.Length == cistella.nElem)
                {
                    Console.WriteLine("No et queda espai a la cistella!");
                    AmpliarCistella(ref cistella);
                }
                double dinersNous = cistella.diners - botiga.preu[posicioProducte] * quantitat;
                while (dinersNous < 0)
                {
                    Console.WriteLine($"No tens diners, estas pobre {-dinersNous} euros!");
                    AfegirDiners(ref cistella);
                    dinersNous = cistella.diners - botiga.preu[posicioProducte] * quantitat;
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

        static void AmpliarBotiga(ref (string[] productes, double[] preus, int nElem) botiga)
        {
            int espais;
            Console.WriteLine("Quants espais vols ampliar la botiga?");
            espais = Convert.ToInt32(Console.ReadLine());

            if (espais > (botiga.productes.Length - botiga.nElem))
            {
                Console.WriteLine($"No pots ampliar la botiga {espais}, no en disposem de tants");
            }
            else
            {
                botiga.nElem += espais;
            }
        }
        //// MÈTODE AmpliarBotiga - Amplia UN espai la botiga
        //static void AppendBotiga(ref (string[] productes, double[] preus, int nElem) botiga, string[] productes, double[] preus)
        //{
        //    int elementsTotals = botiga.nElem + preus.Length;
        //    string[] productesNou = new string[elementsTotals];
        //    double[] preusNou = new double[elementsTotals];

        //    // EX: tenim 5 productes a la botiga i volem afegir-hi 3
        //    for (int i = 0; i < elementsTotals; i++)
        //    {
        //        if (i < botiga.nElem)
        //        {
        //            // Index 0 a 4 son els productes ORIGINALS de la botiga.
        //            productesNou[i] = botiga.productes[i];
        //            preusNou[i] = botiga.preus[i];
        //        }
        //        else
        //        {
        //            // Index 5 a 7 son els productes AFEGITS de l'usuari, els productes i - 5
        //            productesNou[i] = productes[i - botiga.nElem];
        //            preusNou[i] = preus[i - botiga.nElem];
        //        }
        //    }
        //    botiga.productes = productesNou;
        //    botiga.preus = preusNou;
        //    botiga.nElem = elementsTotals;
        //}

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
            while (i < botiga.nElem && botiga.productes[i] != producte)
            {
                i++;
            }
            if (i == botiga.nElem) i = -1;
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
                s += cistella.productes[i].PadRight(20, '.') + (Convert.ToString(preuUnitari) + "Ex" + Convert.ToString(quantitatTiquet) + "u.." + Convert.ToString(preuTotal)).PadLeft(20, '.') + " E \n";
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

        static string BotigaToString((string[] producte, double[] preu, int nElem) botiga)
        {
            string botigaString = "";
            for (int i = 0; i < botiga.nElem; i++)
            {
                LiniaBotigaToString(botiga.producte[i], botiga.preu[i], ref botigaString);
            }

            Console.WriteLine($"La botiga mostra {botiga.nElem} de {botiga.producte.Length} productes");
            return botigaString;
        }

        // MÈTODE LiniaBotiga - Mostra una linia de producte + preu
        static void LiniaBotiga(string producte, double preu)
        {
            Console.Write(producte.PadRight(20, '.') + Convert.ToString(preu).PadLeft(20, '.') + " E \n");
        }
        static void LiniaBotigaToString(string producte, double preu, ref string botigaString)
        {
            botigaString += (producte.PadRight(20, '.') + Convert.ToString(preu).PadLeft(20, '.') + " E \n");
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
        static void ModificarPreu(ref (string[] producte, double[] preu, int nElem) botiga)
        {
            string producte;
            double preu;
            Console.WriteLine("Quin producte en vols modificar el preu?");
            producte = Console.ReadLine();

            if (ExisteixABotiga(Botiga(), producte) == -1)
            {
                Console.WriteLine($"No s'ha trobat el producte '{producte}' a la botiga");
            }
            else
            {

                Console.WriteLine("Escriu el nou preu");
                preu = Convert.ToDouble(Console.ReadLine());
                int i;
                for (i = 0; botiga.producte[i] != producte; i++) ;
                botiga.preu[i] = preu;
            }
        }

        // MÈTODE ModificarProducte - Canvia un producte antic per un de nou, si existeix
        static void ModificarProducte(ref (string[] producte, double[] preu, int nElem) botiga)
        {
            string producteAntic, producteNou;
            Console.WriteLine("Quin producte vols modificar?");
            Console.WriteLine(botiga.nElem);
            producteAntic = Console.ReadLine();

            if (ExisteixABotiga(botiga, producteAntic) == -1)
            {
                Console.WriteLine($"No s'ha trobat el producte '{producteAntic}' a la botiga");
            }
            else
            {
                Console.WriteLine("Escriu el nom que tindrà el producte nou");
                producteNou = Console.ReadLine();
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


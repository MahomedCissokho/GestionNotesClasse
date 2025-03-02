namespace GestionNotesClasse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedList<int, Etudiant> etudiants = new SortedList<int, Etudiant>();
            int lignesParPage = 5; 

            Console.WriteLine("\n===== GESTION DES NOTES DE CLASSE =====\n");

            
            lignesParPage = DemanderLignesParPage();

            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\nChoisissez une option :");
                Console.WriteLine("1. Ajouter un étudiant");
                Console.WriteLine("2. Modifier ou ajouter les notes d'un étudiant");
                Console.WriteLine("3. Afficher le bulletin de la classe");
                Console.WriteLine("4. Quitter");
                Console.Write("\nVotre choix : ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        AjouterUnEtudiant(etudiants);
                        break;
                    case "2":
                        ModifierNotesEtudiant(etudiants);
                        break;
                    case "3":
                        if (etudiants.Count > 0)
                        {
                            AfficherBulletin(etudiants, lignesParPage);
                        }
                        else
                        {
                            Console.WriteLine("Aucune donnée à afficher. Veuillez d'abord saisir les données des étudiants.");
                        }
                        break;
                    case "4":
                        continuer = false;
                        Console.WriteLine("\nMerci d'avoir utilisé le programme de gestion des notes!");
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

       
        static int DemanderLignesParPage()
        {
            int lignesParPage = 5; 
            const int MIN_LIGNES = 1;
            const int MAX_LIGNES = 15;

            Console.WriteLine($"Configuration de l'affichage (entre {MIN_LIGNES} et {MAX_LIGNES} lignes par page)");
            Console.Write($"Nombre de lignes par page [{"*"}] : ");

            string saisie = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(saisie))
            {
                if (int.TryParse(saisie, out int valeur))
                {
                    if (valeur >= MIN_LIGNES && valeur <= MAX_LIGNES)
                    {
                        lignesParPage = valeur;
                    }
                    else
                    {
                        Console.WriteLine($"Valeur hors limites. Utilisation de la valeur par défaut : {lignesParPage}");
                    }
                }
                else
                {
                    Console.WriteLine($"Valeur invalide. Utilisation de la valeur par défaut : {lignesParPage}");
                }
            }else Console.WriteLine($"Valeur INCONNUE. Utilisation de la valeur par défaut : {lignesParPage}");


            return lignesParPage;
        }

        
        static void AjouterUnEtudiant(SortedList<int, Etudiant> etudiants)
        {
            Console.WriteLine("\n===== AJOUT D'UN ÉTUDIANT =====");

            Etudiant etudiant = new Etudiant();
            if (etudiant.SaisirDonneesEtudiant())
            {
                if (etudiants.ContainsKey(etudiant.NO))
                {
                    Console.WriteLine("Un étudiant avec le numéro d'ordre ", etudiant.NO ," existe déjà.");
                    return;
                }

                etudiants.Add(etudiant.NO, etudiant);
                Console.WriteLine("L'étudiant a été ajouté avec succès.");
            }
        }

        
        static void ModifierNotesEtudiant(SortedList<int, Etudiant> etudiants)
        {
            if (etudiants.Count == 0)
            {
                Console.WriteLine("Aucun étudiant n'a été saisi. Impossible de modifier les notes.");
                return;
            }

            Console.WriteLine("\n===== MODIFICATION DES NOTES =====");
            Console.Write("Entrez le numéro d'ordre de l'étudiant : ");

            if (!int.TryParse(Console.ReadLine(), out int no))
            {
                Console.WriteLine("Numéro d'ordre invalide.");
                return;
            }

            if (!etudiants.ContainsKey(no))
            {
                Console.WriteLine($"L'étudiant avec le numéro d'ordre {no} n'existe pas.");
                return;
            }

            Etudiant etudiant = etudiants[no];
            if (etudiant.ModifierNotesEtudiant())
            {
                Console.WriteLine("Les notes ont été modifiées avec succès.");
            }
        }

        
        static void AfficherBulletin(SortedList<int, Etudiant> etudiants, int lignesParPage)
        {
            int totalPages = (int)Math.Ceiling((double)etudiants.Count / lignesParPage);
            int pageCourante = 1;
            bool continuer = true;

            while (continuer && pageCourante <= totalPages)
            {
                Console.Clear();

                
                Console.WriteLine("\n==================== BULLETIN DE NOTES ====================\n");

                AfficherLigneSeparation();
                Console.WriteLine("| NO  |      NOM        |     PRÉNOM      |  CC   | DEVOIR | MOYENNE |");
                AfficherLigneSeparation();

                int debut = (pageCourante - 1) * lignesParPage;
                int fin = Math.Min(debut + lignesParPage, etudiants.Count);

                for (int i = debut; i < fin; i++)
                {
                    Etudiant etudiant = etudiants.Values[i];
                    Console.WriteLine($"| {etudiant.ToString()} |");
                }

                AfficherLigneSeparation();

                
                double moyenneClasse = etudiants.Values.Average(e => e.CalculerMoyenneEtudiant());
                Console.WriteLine($"| MOYENNE DE LA CLASSE : {moyenneClasse,5:F1}                                       |");

                AfficherLigneSeparation();

                
                Console.WriteLine($"\nPage {pageCourante}/{totalPages}");

                if (totalPages > 1)
                {
                    Console.WriteLine("\nOptions : [S]uivant, [P]récédent, [Q]uitter l'affichage");
                    string choix = Console.ReadLine().ToUpper();

                    switch (choix)
                    {
                        case "S":
                            if (pageCourante < totalPages)
                                pageCourante++;
                            break;
                        case "P":
                            if (pageCourante > 1)
                                pageCourante--;
                            break;
                        case "Q":
                            continuer = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nAppuyez sur une touche pour revenir au menu principal...");
                    Console.ReadKey();
                    continuer = false;
                }
            }
        }

        
        static void AfficherLigneSeparation()
        {
            Console.WriteLine("+-----+-----------------+-----------------+-------+--------+---------+");
        }
    }
    
}

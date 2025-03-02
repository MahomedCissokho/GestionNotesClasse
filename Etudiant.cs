using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotesClasse
{
    public class Etudiant : IComparable<Etudiant>
    {
        public int NO { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public double NoteCC { get; set; }
        public double NoteDevoir { get; set; }

        public Etudiant(int no, string nom, string prenom, double noteCC = 0, double noteDevoir = 0)
        {
            NO = no;
            Nom = nom;
            Prenom = prenom;
            NoteCC = noteCC;
            NoteDevoir = noteDevoir;
        }


        public Etudiant()
        {
            NO = 0;
            Nom = "";
            Prenom = "";
            NoteCC = 0;
            NoteDevoir = 0;
        }


        public int CompareTo(Etudiant other)
        {
            return this.NO.CompareTo(other.NO);
        }


        public override string ToString()
        {
            return $"{NO,3} | {Nom,-15} | {Prenom,-15} | {NoteCC,5:F1} | {NoteDevoir,6:F1} | {CalculerMoyenneEtudiant(),7:F1}";
        }


        public double CalculerMoyenneEtudiant()
        {
            return Math.Round(NoteCC * 0.33 + NoteDevoir * 0.67, 1);
        }


        public bool SaisirDonneesEtudiant()
        {
            Console.Write("Numéro d'ordre : ");
            if (!int.TryParse(Console.ReadLine(), out int no))
            {
                Console.WriteLine("Numéro d'ordre invalide.");
                return false;
            }
            NO = no;

            Console.Write("Nom : ");
            Nom = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(Nom))
            {
                Console.WriteLine("Le nom ne peut pas être vide.");
                return false;
            }

            Console.Write("Prénom : ");
            Prenom = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(Prenom))
            {
                Console.WriteLine("Le prénom ne peut pas être vide.");
                return false;
            }

            Console.Write("Voulez-vous saisir les notes maintenant? (O/N) : ");
            string reponse = Console.ReadLine().ToUpper();
            if (reponse == "O")
            {
                NoteCC = SaisirNote("Note de contrôle continu (sur 20) : ");
                NoteDevoir = SaisirNote("Note de devoir (sur 20) : ");
            }

            return true;
        }

        public bool ModifierNotesEtudiant()
        {
            NoteCC = SaisirNote("Note de contrôle continu (sur 20) : ");
            NoteDevoir = SaisirNote("Note de devoir (sur 20) : ");
            return true;
        }


        private double SaisirNote(string message)
        {
            double note = -1;
            bool estValide = false;

            while (!estValide)
            {
                Console.Write(message);
                if (!double.TryParse(Console.ReadLine(), out note))
                {
                    Console.WriteLine("Veuillez entrer un nombre valide.");
                    continue;
                }

                if (note < 0 || note > 20)
                {
                    Console.WriteLine("La note doit être comprise entre 0 et 20. Veuillez réessayer.");
                    continue;
                }

                estValide = true;
            }

            return note;
        }
    }
}

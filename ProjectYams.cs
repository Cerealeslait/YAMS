using System;
using System.IO; // Pour l'écriture du fichier JSON.

class Yams {  


// ----------------------------------------- Code des différents paramètres initiaux de la partie  -----------------------------------------------------
  static Challenge chall = new Challenge(); // Variable Challenge qui va servir d'initialisation pour les joueurs.
  static Des des = new Des(); // Variable qui va représenter les dés de la partie
  static int tour = 1;
  
  // Déclaration de la structure permettant de contenir les Dés.
  public struct Des {
      public int NbDes; // Nombre )de dés
    public int[] TDes; // Tableau pour contenir les valeurs des dés
    public bool[] DesDisp; // Tableau pour contenir la disponibilité des dés
    
  }
  
  // Déclaration de la structure permettant de contenir les informations sur les joueurs.
  
  public struct Joueur {
    
    public string pseudo; // Pseudo du joueur. 
    public int score; // Son score durant la partie.
    public int scoremin; // Son score bonus
    public int id;
    public bool obtenu_sm; // Permet de renseigner si le joueur a obtenu son bonus de score mineur
    public Challenge chall; // Stocke les challenges du joueur

    public Joueur(string pseudoo, int N1, int N2, Challenge challenge, bool obtenu, int ID){
      pseudo = pseudoo;
      score = N1;
      scoremin = N2;
      chall = challenge;
      obtenu_sm = obtenu;
      id = ID;
    }
  }
  
  // Déclaration de la structure permettant de contenir les informations sur les challenge.

  public struct Challenge {
    public int NbChallenge; // Nombre de challenge
    public int Ncnombre; // Nombre de challenge "Nombre"
    public bool[] Nombre; // Tableau pour stocker la disponibilité des différents challenge "Nombre"
    public bool Brelan;
    public bool Carre;
    public bool Full;
    public bool PetiteSuite;
    public bool GrandeSuite;
    public bool Yamss;
    public bool Chance;
  }

  /*
    Procédure initDes
    Idée : Mets les dés à leurs état inital de début de partie.
    Sortie : Void
  */
  
  static void initDes(){
    des.NbDes = 5; // Déclaration du nombre de dés utilisés par les joueurs.
    des.TDes = new int[5]; // Tableau pour stocker le score des dés. 
    des.DesDisp = new bool[5];   /* Tableau pour stocker les dés disponibles durant le tour. 
                                    True -> Dé disponible pour être lancé. 
  			      	    False -> Dé que le joueur a décide de garder ce dé, il ne peut alors plus être relancé.
                                 */
  }

  /*
    Procédure initChallenge
    Idée : Parcourir les challenge et les mettre tous disponibles
    Sortie : Void
  */
  
  static void initChallenge(ref Joueur J){
    J.chall.NbChallenge = 13;
    J.chall.Ncnombre = 6;
    J.chall.Brelan = true;
    J.chall.Carre = true;
    J.chall.Full = true;
    J.chall.PetiteSuite = true;
    J.chall.GrandeSuite = true;
    J.chall.Yamss = true;
    J.chall.Chance = true;
    J.chall.Nombre = new bool[6];
    for (int i = 0; i < J.chall.Ncnombre; i++){
      J.chall.Nombre[i] = true;
    }
  }

  /*
    Procédure initPartie
    Idée : Initialiser les paramètres de débuts de Partie
    Entrées : joueur J1, J2
    Entrées modifiées : J1, J2
    Sortie : Void
  */
  
  static void initPartie(ref Joueur J1, ref Joueur J2){
    initDes();
    initChallenge(ref J1);
    initChallenge(ref J2);
    Console.WriteLine(("<----------- BIENVENUE DANS LE JEU DU YAM'S----------->"));    
    Console.Write("Pseudo Joueur 1 : ");
    J1.pseudo = Console.ReadLine();
    Console.Write("Pseudo Joueur 2 : ");
    J2.pseudo = Console.ReadLine();
    Console.WriteLine(("<----------------------------------------------------->\n"));    
  }


// ------------------------------------- Code des actions à réaliser durant la partie  -----------------------------------------------------

  /*
    Procédure lanceDes
    Idée : Simule un jet de dés en générant 5 nombres aléatoies
    Local : Entier aléatoire X
    Sortie : Void
  */
  
  static void lanceDes(){
    Random X = new Random(); // Variable qui va contenir la valeure aléatoire 
    int nb = 0;
    for (int i = 0; i < des.NbDes; i++){
      nb=X.Next(1,7); // Génère un nombre aléatoire entre 1 et 6// Vérifie si le dé est disponible
      des.TDes[i] = nb;
    }
  }
  
  /*
     Procédure afficheChallenge
     Idée : Affiche la liste des challenge disponibles et le score de chaque joueur
     Sortie : Void
  */
  
  static void afficheRecapPartie (Joueur J){
    Console.WriteLine("<------------------- TOUR {0} ({1})------------------->", tour, J.pseudo);
    Console.WriteLine("<----------- CHALLENGES DISPONIBLES ({0}) ----------->", J.pseudo);
    for (int i = 0; i < J.chall.Ncnombre ; i++){
      if (J.chall.Nombre[i]){
        Console.WriteLine("{0}. Nombre {1}", i+1, i+1);
      }
    }  
    if (J.chall.Brelan){
        Console.WriteLine("7. Brelan");
    }
    if (J.chall.Carre){
        Console.WriteLine("8. Carre");
    }
    if (J.chall.Full){
        Console.WriteLine("9. Full");
    } 
    if (J.chall.PetiteSuite){
        Console.WriteLine("10. PetiteSuite");
    }
    if (J.chall.GrandeSuite){
        Console.WriteLine("11. GrandeSuite");
    }
    if (J.chall.Yamss){
        Console.WriteLine("12. Yams");
    }
    if (J.chall.Chance){
        Console.WriteLine("13. Chance");
    }
    Console.WriteLine("<---------------------------------------------->");
    Console.WriteLine("Score de {0} : {1}", J.pseudo, J.score);
    Console.WriteLine("Score mineur de {0} : {1}", J.pseudo, J.scoremin);
    Console.WriteLine("<---------------------------------------------->");
  }
  
  /*
     Procédure afficheDesScore
     Idée : Affiche le score de chaque dé
     Sortie : Void
  */
  
  static void afficheDesScore(){
    Console.Write("Jet de dés : < ");
    for (int i = 0; i < des.NbDes; i++){
      Console.Write("{0} ", des.TDes[i]);
    }
    Console.WriteLine(">");
  }

  /*
     Procédure relanceDe
     Idée : Demander au joueur quel dé veut-il garder
     Sortie : Void
  */

  static void relanceDes(){
    Console.WriteLine("Quels des voulez-vous garder ? (1 à 5)");
    string choix = Console.ReadLine();
    int[] tabTemp = new int[5]{0,0,0,0,0}; // Tableau temporaire pour stocker les dés à garder, initialisé à 0
    int n = choix.Length; // On récupère la longueur du choix
    for(int i = 0; i < n; i++){ 
        bool reussis = int.TryParse(choix[i].ToString(), out int nombre); // Tente de convertir chaque caractère en entier, "reussis" sera false si la conversion échoue
        nombre -=1;
        if (reussis){ 
            if (nombre >= 0 && nombre <= des.NbDes){ // Vérifie que le nombre est dans la plage autorisée
                tabTemp[nombre] = des.TDes[nombre]; // Stocke le dé choisi dans le tableau temporaire
            }
            else{ 
                Console.WriteLine("Les nombres doivent être entre 0 à {0}!", des.NbDes); // Message d'erreur si le nombre est hors plage
                relanceDes(); // Relance la fonction pour permettre une nouvelle saisie
            }
        }
    }
    initDes(); // Réinitialise les dés (fonction à définir ailleurs dans le programme)
    lanceDes(); // Lance les dés (fonction à définir ailleurs dans le programme)

    for(int i = 0; i < des.NbDes; i++){ //on ajoute a "TDes" les des conservés aprés un nouveau leancée
        if (tabTemp[i] != 0){ // Vérifie si le dé a été conservé
            des.TDes[i] = tabTemp[i]; // Met à jour le tableau des dés avec ceux qui ont été conservés
        }
    }
  }

  /*
     Procédure choixChallenge
     Idée : Demander au joueur quel challenge veut-il choisir
     Entrée : struct joueur J
     Sortie : Void
  */
  
  static string choixChallenge(ref Joueur J){
    int choix = -1;
    string challenge = string.Empty;
    while (choix < 1 || choix > 13){
      try
      {
        Console.Write("Quel Challenge choisissez vous ? (Entrer le numéro correspondant) ");
        choix = int.Parse(Console.ReadLine());
      }
      catch (System.FormatException) {Console.WriteLine("Veuillez saisir un Entier !");}
    }

    if (choix >= 1 && choix <= 6){
      if (J.chall.Nombre[choix-1]){
        Nombre(ref J, choix); 
        challenge = "nombre" + Convert.ToString(choix);
      }
      else
      {
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 7){
      if (J.chall.Brelan){	
        Brelan(ref J);
        challenge = "brelan";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }  
    else if (choix == 8){
      if (J.chall.Carre){
        Carre(ref J);
        challenge = "carre";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 9){
      if (J.chall.Full){
        Full(ref J);
        challenge = "full";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 10){
      if (J.chall.PetiteSuite){
        petiteSuite(ref J);
        challenge = "petite";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 11){
      if (J.chall.GrandeSuite){
        grandeSuite(ref J);
        challenge = "grande";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 12){
      if (J.chall.Yamss){
        Yamss(ref J);
        challenge = "yams";
      }
      else{
        Console.WriteLine("Ce Challenge n'est plus disponible ! Vous gagnez 0 points.\n");
      }
    }
    else if (choix == 13){
      if (J.chall.Chance){
        Chance(ref J);
        challenge = "chance";
      }
    }

    return challenge;  
    
  }
  
  
// ------------------------------------- Code des différents challenge -----------------------------------------------------
    
    
  /*
     Procédure Nombre 		
     Idée : Parcourir les dés qu'a lancé le joueur j et compter le nombre de dés ayant obtenu le score N.
            Ne pas oublier de rendre le challenge correspondant indisponible pour le reste de la partie.
     Entrée : référence structure joueur j, Entier N
     Entrée modifée : j
     Local : Entier compteur
     PRECONDITION : 1 <= N <= 6
  */
  
  static void Nombre(ref Joueur J, int N){
    int compteur = 0;
    bool present = false;
    for (int i = 0; i < des.NbDes; i++){
      if (des.TDes[i] == N){
        present = true;
        compteur ++;
      }
    }
    if (present){
      J.score += compteur * N;
      if(!J.obtenu_sm)
      { 
        J.scoremin += compteur * N;
      }
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, compteur * N);
    }else{
      Console.WriteLine("Il n'y a pas de {0} dans vos dés ! Vous gagner 0 points de score.\n", N);  
    }
    J.chall.Nombre[N-1] = false;
  }
  
  /*
    Fonction rechercheNombre
    Idée : Parcourir un tableau et regarder combien de fois apparaît l'entier X
    Entrées : Tableau d'entiers T, Entier X, Entier N (Taille du tableau)
    Local : Entier compteur
    Sortie : Entier
  */
  
  
  static int rechercheNombre(int[] T, int X, int N){
    int compteur = 0;
    for (int i = 0; i < N; i++){
      if (T[i] == X){
        compteur++;
      }
    }
    return compteur;
  }
  
  
  /*
    Procédure Brelan 
    Idée : Parcourir les dés et sommer le score des 3 dés de même valeur s'il y'en a en se servant de la fonction rechercheNombre
    Entrée : référence structure joueur J
    Entrée modifiée : jouer J
    Local : Entier indice
    Sortie : void
  */
  
  static void Brelan(ref Joueur J){
    int indice = 0;
    while ( indice < des.NbDes && rechercheNombre(des.TDes, des.TDes[indice], des.NbDes) < 3){
      indice++;
    }
    if (indice >= des.NbDes){
      Console.WriteLine("Il n'y a pas de Brelan dans vos dés ! Vous gagner 0 points de score.\n");
    } else {
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 3 * des.TDes[indice]);
      J.score += 3 * des.TDes[indice];
    }
    J.chall.Brelan = false;
  }
  
  /*
    Procédure Carre 
    Idée : Même chose que Brelan mais avec 4
    Entrée : référence structure joueur J
    Entrée modifiée : jouer J
    Local : Entier indice
    Sortie : void
  */
  
  static void Carre(ref Joueur J){
    int indice = 0;
    while ( indice < des.NbDes && rechercheNombre(des.TDes, des.TDes[indice], des.NbDes) < 4){
      indice++; 	
    }
    if (indice >= des.NbDes){
      Console.WriteLine("Il n'y a pas de Carré dans vos dés ! Vous gagner 0 points de score.\n");
    } else {
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 4 * des.TDes[indice]);
      J.score += 4 * des.TDes[indice];
    }
    J.chall.Carre = false;
  }
  
  /*
    Procédure Yamss
    Idée : Même chose que B relan mais avec tous les dés
    Entrée : référence structure joueur J
    Entrée modifiée : jouer J
    Local : Entier i, bool indentique
    Sortie : void
  */
  
  static void Yamss(ref Joueur J){
    int i = 0;
    bool identique = true;
    while (i < des.NbDes -1 && identique){
      if (des.TDes[i] != des.TDes[i+1]){
        identique = false;
      }
      i++; 	
    }
    if (!identique){
      Console.WriteLine("Il n'y a pas de Yams dans vos dés ! Vous gagner 0 points de score.\n");
    } else {
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 5 * des.TDes[0]);     
      J.score += 5 * des.TDes[0];
    }
    J.chall.Yamss = false;
  }
  
  /*
    Procédure Chance
    Idée : Sommer le total des dés obtenus
    Entrée : référence structure joueur J
    Entrée modifiée : joueur J
    Local : Entier somme
    Sortie : void
  */
  
  static void Chance(ref Joueur J){
    int somme = 0;
    for (int i = 0; i < des.NbDes; i++){
      somme += des.TDes[i];
    }
    Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, somme);       
    J.score += somme;
    J.chall.Chance = false;
  }
  
  /*
    Procédure Full
    Idée : Obtenir 3 dés de même valeur + 2 dés de même valeur
           On ré-utilise l'algorithme du Brelan.
           On sauvegarde la valeure qui apparait 3 fois et on regarde si la valeur restante apparaît 2 fois. 
    Entrée : référence Joueur J
    Entrée modifiée : joueur J
    Sortie : void  
  */
  
  static void Full (ref Joueur J){
    int indice = 0;
    while ( indice < des.NbDes && rechercheNombre(des.TDes, des.TDes[indice], des.NbDes) != 3){
      indice++;
    }
    if (indice >= des.NbDes){
      Console.WriteLine("Il n'y a pas de Full dans vos dés ! Vous gagner 0 points de score.\n");
    } else {
        int pvaleur = des.TDes[indice];
        indice = 0;
        while (des.TDes[indice] == pvaleur){
          indice ++;  
        }
        if (rechercheNombre(des.TDes, des.TDes[indice], des.NbDes) == 2) {
          Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 25);
          J.score += 25;
        } else {
          Console.WriteLine("Il n'y a pas de Full dans vos dés ! Vous gagner 0 points de score.\n");  
        }  
    }
    J.chall.Full = false;
  }
  
  /*
    Méthode TriBulles 
    Idée : Trier le Tableau de dés en utilisant l'algorithme du tri à bulles.
    Entrée : Tableau d'entiers T, Entier N // Taille du tableau
    Sortie : Le tableau T trié
  */
  
  static void TriBulles(int[] T, int N){
    bool tableau_trie;
    for (int i = N -1; i > 0; i--){
      tableau_trie = true;
      for (int j = 0; j < i; j++){
        if (T[j+1] < T[j]){
          tableau_trie = false;
          int tempon = T[j+1];
          T[j+1] = T[j];
          T[j] = tempon;
        }
      }
      if (tableau_trie == true){
        break;
      }
    }
  }  
  

  /*
    Procédure petiteSuite
    Idée : Trier le tableau de dés et vérifier qu'on a une suite en prenant en compte u éventuel doublon
    Entrée : référence Joueur J
    Entrée modifiée : joueur J
    Local : Entier Tsuite // Taille de la suite qu'on cherche
            Booléen trouve
            Entier i // incrémenteur 
    Sortie : void  
  */

  static void petiteSuite (ref Joueur J){
    int i = 0, Tsuite = 4, longueur = 1;
    bool trouve = false;
    TriBulles(des.TDes, des.NbDes); // Trie 
    while (i < des.NbDes-1 && !trouve){
      if (des.TDes[i] == des.TDes[i+1] - 1){
          longueur++;
        }
      if (longueur == Tsuite){
        trouve = true;
      }

      i++;
    }
    if (trouve){
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 30);
      J.score+=30;
    }else{
      Console.WriteLine("Il n'y a pas de petite suite dans vos dés ! Vous gagner 0 points de score.\n");
    }
    J.chall.PetiteSuite=false;
  }


  /*
    Procédure grandeSuite
    Idée : Même principe que petite suite mais avec une longueur de 5
    Entrée : référence Joueur J
    Entrée modifiée : joueur J
    Local : Entier Tsuite // Taille de la suite qu'on cherche
            Booléen trouve
            Entier i // incrémenteur 
    Sortie : void  
  */

  static void grandeSuite (ref Joueur J){
    int i = 0;
    bool trouve = true;
    TriBulles(des.TDes, des.NbDes);
    while (i < des.NbDes - 1 && trouve){
      if (des.TDes[i] != des.TDes[i+1] - 1){
        trouve = false;
      }
      i++;
    }
    if (trouve){
      Console.WriteLine("{0} gagne {1} points !\n", J.pseudo, 40);
      J.score += 40;   
    }else{
      Console.WriteLine("Il n'y a pas de grande suite dans vos dés ! Vous gagner 0 points de score.\n");
    }
    J.chall.GrandeSuite = false;
  }

  //-------------------------------------------------------POUR LE JSON---------------------------------------

  /*
    Fonction afficheTableau 
    Idée : afficher un tableau sous le format JS;
    Entrée : Tableau d'entiers T, Entier N (Taille du Tableau)
  */

  static string afficheTableauJS(int[] T, int N)
  {
    string t_js = string.Empty;
    t_js += "[";

    for (int i = 0; i < N; i++)
    {
     if(i != N-1)
     {
       t_js += Convert.ToString(T[i]) + ",";
     }
     else
     {
       t_js += Convert.ToString(T[i]);
     }
    }

    t_js += "]";

    return t_js;
  }
 
  /*
    Fonction afficheRound
    Idée : Afficher l'état d'un round dans le JSON
    Entrée : StreamWriter f
    Sortie : void
  */

  static void afficheCoupJoueur(StreamWriter leFichier, ref Joueur J, string challengeChoisi)
  {
    
    string ligne;

    // Affichage coup du joueur

    if (J.id == 1) 
    {
      ligne = "        {\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"id_player\": 1,\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"dice\": " + afficheTableauJS(des.TDes, des.NbDes) + ",\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"challenge\": " + "\"" + challengeChoisi + "\"" + ",\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"score\": " + Convert.ToString(J.score) +"\n";
      leFichier.WriteLine(ligne);

      ligne = "        },\n";
      leFichier.WriteLine(ligne);
    }
    else
    {
      ligne = "        {\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"id_player\": 2,\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"dice\": " + afficheTableauJS(des.TDes, des.NbDes) + ",\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"challenge\": " + "\"" + challengeChoisi + "\"" + ",\n";
      leFichier.WriteLine(ligne);

      ligne = "         \"score\": " + Convert.ToString(J.score) +"\n";
      leFichier.WriteLine(ligne);

      ligne = "        }\n";
      leFichier.WriteLine(ligne);
  
      ligne = "      ]\n";
      leFichier.WriteLine(ligne);
    
      if (tour == 13)
      {
        ligne = "    }\n";
        leFichier.WriteLine(ligne);

        ligne = "  ],\n";
        leFichier.WriteLine(ligne);     
      }
      else
      {
        ligne = "    },\n";
        leFichier.WriteLine(ligne);
      }
    }

  }

  
  
// ------------------------------------- Code de la procédure Main  -----------------------------------------------------

  
  public static void Main(){
      =
    // Pour la création du fichier JSON
    
    DateTime thisDay  = DateTime.Today;

    string dateajd = thisDay.ToString("d");

    FileStream fs = new FileStream("yams.json", FileMode.Create, FileAccess.Write);
    
    StreamWriter leFichier = new StreamWriter(fs);

    // Début du fichier JSON

    string ligne;
    ligne = "{\n";
    leFichier.WriteLine(ligne);

    ligne = "  \"parameters\": {\n";
    leFichier.WriteLine(ligne);

    ligne = "    \"code\": \"groupe2-001\",\n";
    leFichier.WriteLine(ligne);

    ligne = "    \"date\": " + "\"" + dateajd + "\"\n";
    leFichier.WriteLine(ligne);

    ligne = "  },\n";
    leFichier.WriteLine(ligne);

    // Initialisation des joueurs

    Joueur J1 = new Joueur(string.Empty, 0, 0, chall, false, 1);
    Joueur J2 = new Joueur(string.Empty, 0, 0, chall, false, 2);

    // Debut de la partie 

    initPartie(ref J1, ref J2);

    // Ecriture de la partie "Joueurs" dans le JSON

    ligne = "  \"players\": [\n";
    leFichier.WriteLine(ligne);

    ligne = "    {\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"id\": 1,\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"pseudo\": " + "\"" + J1.pseudo + "\"\n";
    leFichier.WriteLine(ligne);

    ligne = "    },\n";
    leFichier.WriteLine(ligne);

    ligne = "    {\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"id\": 2,\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"pseudo\": " + "\"" + J2.pseudo + "\"\n";
    leFichier.WriteLine(ligne);

    ligne = "    }\n";
    leFichier.WriteLine(ligne);

    ligne = "  ],\n";
    leFichier.WriteLine(ligne);

    //-----------------------------------------------

    // Afficher le début du round dans le JSON
      
    ligne = "  \"rounds\": [\n";
    leFichier.WriteLine(ligne);


    while (tour <= 13) {

      ligne = "    {\n";
      leFichier.WriteLine(ligne);

      ligne = "      \"id\": " + Convert.ToString(tour) + ",\n";
      leFichier.WriteLine(ligne);

      ligne = "      \"results\": [\n";
      leFichier.WriteLine(ligne);

      // coup du Joueur 1

      afficheRecapPartie(J1);
      lanceDes();
      afficheDesScore();
      string choix1 = string.Empty;
      while ( choix1 != "Y" && choix1 != "y" && choix1 != "N" && choix1 != "n" ){
        Console.Write("Voulez-vous relancer des dés ? (Y/N) ");
        choix1 = Console.ReadLine();
      }
      Console.Write("\n");
      if (choix1 == "Y"){
        relanceDes();
        afficheDesScore();
        string choix2 = string.Empty;
        while ( choix2 != "Y" && choix2 != "y" && choix2 != "N" && choix2 != "n" ){
          Console.Write("Voulez-vous relancer des dés ? (Y/N) ");
          choix2 = Console.ReadLine();
        } 
        Console.Write("\n");
        if (choix2 == "Y"){
          relanceDes();
          afficheDesScore();
        }
      }

      afficheCoupJoueur(leFichier, ref J1, choixChallenge(ref J1));

      if (J1.scoremin >= 63 && !J1.obtenu_sm){
        Console.WriteLine(" La somme de la partie mineure de {0} atteint 63 ! Il reçoit un bonus de 35 points", J1.pseudo);
        J1.obtenu_sm = true;
        J1.score += 35;
      }


      // coup du Joueur 2

      afficheRecapPartie(J2);
      lanceDes(); 
      afficheDesScore();
      string choix3 = string.Empty;
      while ( choix3 != "Y" && choix3 != "y" && choix3 != "N" && choix3 != "n" ){
        Console.Write("Voulez-vous relancer des dés ? (Y/N) ");
        choix3 = Console.ReadLine();
      }
      Console.Write("\n");
      if (choix3 == "Y"){
        relanceDes();
        afficheDesScore();
        string choix4 = string.Empty;
        while ( choix4 != "Y" && choix4 != "y" && choix4 != "N" && choix4 != "n" ){
          Console.Write("Voulez-vous relancer des dés ? (Y/N) ");
          choix4 = Console.ReadLine();
        } 
        Console.Write("\n");
        if (choix4 == "Y"){
          relanceDes();
          afficheDesScore();
        }
      }

      afficheCoupJoueur(leFichier, ref J2, choixChallenge(ref J2))  ;

      if (J2.scoremin >= 63 && !J2.obtenu_sm){
        Console.WriteLine(" La somme de la partie mineure de {0} atteint 63 ! Il reçoit un bonus de 35 points\n", J2.pseudo);
        J2.obtenu_sm = true;
        J2.score += 35;
      }
 
      Console.WriteLine("<---------------------------------------------->\n");  
      tour++;

    } 


    // Afficher la fin du round dans le JSON
    
    ligne = "  \"final result\": [\n";
    leFichier.WriteLine(ligne);

    ligne = "    {\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"id_player\": 1,\n";
    leFichier.WriteLine(ligne);

    if (J1.obtenu_sm)
    {
      ligne = "      \"bonus:\": 35,\n";
      leFichier.WriteLine(ligne);
    }
    else
    {
      ligne = "      \"bonus:\": 0,\n";
      leFichier.WriteLine(ligne);
    }

    ligne = "      \"score:\": " + Convert.ToString(J1.score) + "\n";
    leFichier.WriteLine(ligne);

    ligne = "    },\n";
    leFichier.WriteLine(ligne);

    ligne = "    {\n";
    leFichier.WriteLine(ligne);

    ligne = "      \"id_player\": 2,\n";
    leFichier.WriteLine(ligne);

    if (J2.obtenu_sm)
    {
      ligne = "      \"bonus:\": 35,\n";
      leFichier.WriteLine(ligne);
    }
    else
    {
      ligne = "      \"bonus:\": 0,\n";
      leFichier.WriteLine(ligne);
    }

    ligne = "      \"score:\": " + Convert.ToString(J2.score) + "\n";
    leFichier.WriteLine(ligne);

    ligne = "    }\n";
    leFichier.WriteLine(ligne);

    ligne = "  ]\n}";
    leFichier.WriteLine(ligne);     

    leFichier.Close();

    // Fin de Partie

    if (J1.score < J2.score){
      Console.WriteLine("{0} remporte la partie ! ", J2.pseudo);
    }
    else if (J2.score < J1.score){
      Console.WriteLine("{0} remporte la partie ! ", J1.pseudo);
    }
    else{
      Console.WriteLine("Egalité !");
    }
  }

}


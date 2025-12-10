# YAMS (ProjectYams)

## Description

Ce dépôt contient le code source d'un projet Yams (Yahtzee), avec le fichier principal `ProjectYams.cs` et un fichier de configuration `yams.json`.

## Prérequis

- .NET SDK (version 6.0 ou supérieure) installé sur la machine. Vérifier avec :

```bash
dotnet --version
```

- (Optionnel) Mono si vous souhaitez compiler avec `mcs`/`mono` plutôt que `dotnet`.

## Structure du dépôt

- `ProjectYams.cs` : fichier source principal (C#).
- `yams.json` : fichier de configuration ou de données utilisé par l'application.

## Instructions pour lancer

Il y a plusieurs façons de lancer le projet selon la présence d'un fichier `.csproj` ou non.

1) Si le projet est déjà un projet .NET (avec un `.csproj`)

- Ouvrez un terminal dans le dossier du dépôt :

```bash
cd /home/maxime/BUT_S1/SAE_P11_W11
```

- Restaurer (si nécessaire), construire et lancer :

```bash
dotnet restore
dotnet build
dotnet run --project ./<NomDuProjet>.csproj
```

Remplacez `<NomDuProjet>.csproj` par le nom réel du fichier projet si présent.

2) Si vous n'avez que le fichier `ProjectYams.cs` (pas de `.csproj`)

Option A — créer un projet console et exécuter :

```bash
cd /home/maxime/BUT_S1/SAE_P11_W11
dotnet new console -o YamsApp
# remplacer le fichier auto-généré Program.cs par ProjectYams.cs
cp ProjectYams.cs YamsApp/Program.cs
cd YamsApp
dotnet run
```

Option B — compiler avec Mono (si installé) :

```bash
cd /home/maxime/BUT_S1/SAE_P11_W11
mcs ProjectYams.cs -out:ProjectYams.exe
mono ProjectYams.exe
```

3) Passage d'arguments / fichier de config

Si le programme lit `yams.json` ou accepte des arguments, vous pouvez souvent lancer :

```bash
dotnet run -- <chemin_vers_yams.json>
# ou
dotnet run --project ./<NomDuProjet>.csproj -- <chemin_vers_yams.json>
```

Adaptez selon la manière dont `ProjectYams.cs` gère les arguments.

## Tests

Si le projet contient des tests, lancez :

```bash
dotnet test
```

## Notes et dépannage

- Si `dotnet` n'est pas disponible, installez le SDK depuis https://dotnet.microsoft.com/
- Pour des erreurs de compilation, vérifier les `using` et la version de C# ciblée dans le `.csproj`.
- Sur Linux, l'exécution d'un exécutable .NET peut se faire via `dotnet <nom>.dll` si vous avez une build SDK.

## Contribuer

- Ouvrez une issue pour demander des fonctionnalités ou signaler des bugs.
- Faites une branche, ajoutez des commits clairs, puis proposez une pull request.

---

Si tu veux, je peux :
- adapter le `README.md` pour préciser les commandes exactes si tu me dis s'il y a un fichier `.csproj` et son nom ;
- ou créer directement un projet `.csproj` minimal pour permettre un `dotnet run` immédiat.

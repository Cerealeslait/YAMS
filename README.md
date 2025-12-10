# YAMS (ProjectYams)

## Description

Ce dépôt contient le code source d'un projet Yams (Yahtzee), avec le fichier principal `ProjectYams.cs` et un fichier de configuration `yams.json`.

## Structure du dépôt

- `ProjectYams.cs` : fichier source principal (C#).
- `yams.json` : fichier de configuration ou de données utilisé par l'application.

## Instructions pour lancer

compiler avec Mono :

```bash
mcs ProjectYams.cs -out:ProjectYams.exe
mono ProjectYams.exe
```
Passage d'arguments / fichier de config

Si le programme lit `yams.json` ou accepte des arguments, vous pouvez souvent lancer :

```bash
dotnet run -- <chemin_vers_yams.json>
# ou
dotnet run --project ./<NomDuProjet>.csproj -- <chemin_vers_yams.json>
```



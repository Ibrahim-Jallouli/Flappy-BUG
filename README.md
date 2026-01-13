🎮 Présentation

Flappy Bug est une version volontairement buggée de Flappy Bird, créée comme premier jeu publié et surtout comme support pour apprendre le reverse engineering d’applications Unity.

➡️ Jouer ici :
https://ibratomique.itch.io/flappy-bug

🎯 Objectif

Explorer comment un jeu Unity Android compilé en IL2CPP fonctionne et apprendre à analyser son exécutable :

comprendre la structure d’un APK Unity

extraire les fichiers importants

étudier le comportement interne du programme

🛠️ Outils utilisés

Apktool — extraction et décompilation APK
https://bitbucket.org/iBotPeaches/apktool/downloads/

Il2CppDumper — exploiter
libil2cpp.so + global-metadata.dat
https://github.com/Perfare/Il2CppDumper/releases

Ghidra — analyse du binaire natif
https://github.com/NationalSecurityAgency/ghidra/releases

🔍 Démarche

1️⃣ Builder le jeu Android en IL2CPP

2️⃣ Utiliser Apktool pour récupérer :
  . libil2cpp.so
  . global-metadata.dat

3️⃣ Passer ces fichiers dans Il2CppDumper pour obtenir un dump exploitable par des outils comme ILSpy

4️⃣ Analyser le tout avec Ghidra


<img width="896" height="235" alt="Screenshot 2026-01-11 181242" src="https://github.com/user-attachments/assets/c210fb10-f4a2-40bb-81b3-b1dc5d42e197" />

<img width="855" height="467" alt="Screenshot 2026-01-11 181323" src="https://github.com/user-attachments/assets/4dc098ee-e447-47eb-a657-18c6145edf0a" />

<img width="1184" height="751" alt="image" src="https://github.com/user-attachments/assets/8c608460-6826-4b7e-9672-439b80d0f5c8" />

<img width="928" height="1136" alt="Gemini_Generated_Image_t789het789het789" src="https://github.com/user-attachments/assets/01c7e6e0-a18e-4a52-94f7-134e08c47e95" />

<img width="1279" height="718" alt="Screenshot " src="https://github.com/user-attachments/assets/9e6eaca3-0888-4740-905e-a4374ade0177" />


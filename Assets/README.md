# StillSwitch

Dieses Projekt ist Teil meiner Bachelorarbeit zum Thema "Fotografie als kybernetische Praxis: 
Zur Neuordnung des Chronotopos im Videospiel". Es dient zur Visualisierung der in der Arbeit beschriebenen Phänomene. 

Die zentrale Spielmechanik ist das Einfrieren beweglicher Objekte, indem man sie fotografiert. So soll sich ein Weg zum Ziel gebahnt werden.

## Projektstruktur

Die Projektstruktur ist simpel gehalten. Es gibt unter Assets Verzeichnisse für Materials, PNGs, Prefabs, Scenes, Scripts und TextMeshPro. Jedes Verzeichnis trägt einen Namen, der dessen Funktion erklärt. Außer im Ordner für TextMeshPro, gibt es keine Unterverzeichnisse. 

Die Gameobjects sind ebenfalls größtenteils ordentlich organisiert. Beispielsweise enthält das Gameobject "Player" alle dem Spieler zugehörigen Teile. Dazu gehören unter anderem vereinzelte Körperteile, ein Pivot für die richtige Rotation, die Orientierung und die Kamera, die er in der Hand hält. Darüber hinaus finden sich Wände im Ordner "Walls", für den Spieler schädliche Objekte in "Malicious" und bewegliche Plattformen in "Moving Platforms". Das strukturelle Konzept bedarf keiner weiteren Ausführung. 

## Wichtige Scripts

Streng genommen sind alle Scripts essenziell für das einwandfreie Funktionieren von StillSwitch, doch die folgenden bilden das Herzstück des Codes:

- WaypointFollower: Lässt Plattformen oder Objekte Wegpunkte ablaufen.
- PlayerLife: Tod- und Respawn-System.
- PlayerMovement: Sorgt für sämtliche Bewegungsmechaniken des Spielers.
- PhotoRaycaster: Fotografiert Objekte und friert sie ein.

## Inspiration für die Scripts

Einige Funktionalitäten sind das Resultat aus der gesammelten Erfahrung der vergangenen Semester meines Studiums. Da ich mir nicht zum Ziel gesetzt habe, das Rad neu zu erfinden, gibt es also Code-Passagen, die aus vorangegangenen Programmierprojekten übernommen wurden. 

Der Waypoint-Follower hat seinen Ursprung in einem YouTube-Tutorial, mit dessen Hilfe ich ein 2D-Jump-N-Run programmiert habe: https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCiFlOqRISiQWcBMJlvAeQp (Aufgerufen am 11.03.2026). Seitdem wurde er an die Bedürfnisse eines 3D-Games angepasst.

Die zentralen Mechaniken zur 3D-Bewegung des Spielers wurden folgendem Tutorial entnommen: https://www.youtube.com/watch?v=f473C43s8nE&list=PLmo33sM32UcqXt29JkFr4KP4CPktmgi9o

## Steuerung

- Bewegung: WASD
- Springen: Space
- Durch den Viewfinder schauen: Rechtsklick
- Foto schießen: Linksklick, während man Rechtsklick gedrückt hält
- Kameramenü öffnen: i

## Empfohlene Unity-Version zum Öffnen des Projekts

2022.3.12f1


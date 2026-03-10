# StillSwitch

Dieses Projekt ist Teil meiner Bachelorarbeit zum Thema "Fotografie als kybernetische Praxis: 
Zur Neuordnung des Chronotopos im Videospiel". Es dient zur Visualisierung der in der Arbeit beschriebenen Phänomene. 

Die zentrale Spielmechanik ist das Einfrieren beweglicher Objekte, indem man sie fotografiert. So soll sich ein Weg zum Ziel gebahnt werden.

## Projektstruktur

Die Projektstruktur ist simpel gehalten. Es gibt unter Assets Verzeichnisse für Materials, PNGs, Prefabs, Scenes, Scripts und TextMeshPro. Jedes Verzeichnis trägt einen Namen, der dessen Funktion erklärt. Außer im Ordner für TextMeshPro, gibt es keine Unterverzeichnisse. 

Die Gameobjects sind ebenfalls größtenteils ordentlich organisiert. Beispielsweise enthält das Gameobject "Player" alle dem Spieler zugehörigen Teile. Dazu gehören unter anderem vereinzelte Körperteile, ein Pivot für die richtige Rotation, die Orientierung und die Kamera, die er in der Hand hält. Darüber hinaus finden sich Wände im Ordner "Walls", für den Spieler schädliche Objekte in "Malicious" und bewegliche Plattformen in "Moving Platforms". Das strukturelle Konzept bedarf keiner weiteren Ausführung. 

## Wichtige Scripts

Streng genommen sind alle Skripts essenziell für das einwandfreie Funktionieren von StillSwitch, doch die folgenden bilden das Herzstück des Codes:

- WaypointFollower: Lässt Plattformen oder Objekte Wegpunkte ablaufen.
- PlayerLife: Tod- und Respawn-System.
- PlayerMovement: Sorgt für sämtliche Bewegungsmechaniken des Spielers.
- PhotoRaycaster: Fotografiert Objekte und friert sie ein.

## Steuerung

- Bewegung: WASD
- Springen: Space
- Durch den Viewfinder schauen: Rechtsklick
- Foto schießen: Linksklick, während man Rechtsklicken gedrückt hält
- Kameramenü öffnen: i

## Empfohlene Unity Version zum Öffnen des Projekts

2022.3.12f1


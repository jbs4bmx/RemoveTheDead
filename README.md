# RemoveTheDead


## Description
A BepInEx mod for Escape From Tarkov via the modding framework, SPT-AKI (aka, SPTarkov, Single Player Tarkov, SPT, AKI, etc.). This allows you to set a radius and time interval in which dead bots will be removed from the map. The aim of this mod is to boost performance and increase in-game fps by reducing the number of objects (in this case, dead bots) being processed by the CPU. <br>

Also known as RTD. <br>
Client mod for Single Player Tarkov (SPT-AKI) that removes dead bodies from the map.


## Credits
Author: jbs4bmx <br>
Contributors: DJLang, Helldiver, Devraccoon, CactusPie
  - Some code is borrowed from ["Body Disposal Service Maid (B.D.S.M.)" by DJLang](https://github.com/KillerDJLang/BDSM), which is an updated version of ["Body Disposal Service Maid (B.D.S.M.)" by Helldiver](https://github.com/Volomon/BDSM).
    - [SPT Link for B.D.S.M.](https://hub.sp-tarkov.com/files/file/1620-b-d-s-m-body-disposal-service-maid/?highlight=Body%20Disposal)

  - Some code is borrowed from [RAM Cleaner Fix by Devraccoon](https://github.com/CactusPie/SPT-RamCleanerInterval), which is an updated version of [RAM Cleaner Fix by CactusPie](https://github.com/CactusPie/SPT-RamCleanerInterval).
    - [SPT LINK for RAM Cleaner Fix](https://hub.sp-tarkov.com/files/file/1827-ram-cleaner-fix/?highlight=RAM%20Cleaner)


## Customizations and Features
Customizations:
  - Customizations can be accessed within the BepInEx menu while in game by pressing F12.
  - Change time interval between body removals. (time expressed in minutes)
  - Change minimum distance from player that bodies must be in order to be removed. (distance expressed in meters)

Feature:
  - Instant removal button in BepInEx menu (F12). Now you can instantly trigger the body removal to occur.


## Upcoming Feature(s)
  1. Bind instant removal button to desired key on keyboard for quicker removal.


## Installation
### How to Install this Mod.
"[SPT]" = Your SPT folder path
   1. Extract the contents of the zip file into the root of your [SPT] folder.
      - That's the same location as "SPT.Server.exe" and "SPT.Launcher.exe".
   2. Edit the Config to adjust the values to your likeing.
   3. Start SPT.Server.exe and wait until it fully loads.
   4. Start SPT.Launcher.exe but do not launch the game.
   5. Run the cache cleaner found in the launcher's settings menu.
   6. Now you can launch the game and profit.

### Common Questions
   1. Where do I report bugs found with the current version of the mod?
      - You can report bugs for the current version of this mod here: [RTD Mod Page]().


## Disclaimer
**This mod is provided _as-is_ with _no guarantee_ of support.**

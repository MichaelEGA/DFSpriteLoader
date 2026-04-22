# DFSpriteLoader
Loads sprites in Unity that have been made with the Dark Forces Character Creator V1 by BenDansie

<img width="1448" height="754" alt="SpriteLoader" src="https://github.com/user-attachments/assets/a7da4be1-bbc2-4b70-95a5-aa39eb08431a" />

**UnityPackage:** https://github.com/MichaelEGA/DFSpriteLoader/releases/download/v.1.1.0/DFSpriteLoader.unitypackage

**How to install the unity package**
1) Download the unity package
2) Open the Unity editor
3) Go to Assets (on the top menu bar) -> Import Package
4) Search for the downloaded unity package and click open
5) The 'Import Unity Window' will open, click 'Import'
6) The DFSprite Loader is installed, go to Tools (on the top menu bar) -> Set Up DF Sprite

**How to use:**
1) Place the files in your asset folder, either using the Unity Package or manually. (Note: the 'SetUpDFSprite' script must be at this location in your project 'Assets/Editor/SetUpDFSprite.cs', the other scripts can go wherever you want.)
2) Import the PNG sprites that you made with the DarkForces Character Creator
    - Make sure you set all the textures to 'Texture Type' -> 'Sprite(2D & UI)' in the inspector.
    - Make sure you set all the textures to 'Sprite Mode' -> 'Single' in the inspector.
4) Go to Tools -> Set Up DF Sprite
5) Select your sprite folder with the sprites
6) Select your folder for output (the folder needs to already exist!)
7) Write the name of the character, press 'generate all'.
8) The script will generate a folder with the animations and an animation controller as well as making a character in the scene
9) If you go to the child object of the character (CharacterName -> AnimatorGO) and go down to SpriteHandler on the right hand side in the inspector there is a dropdown box where you can change the animation that is playing.

**Other Notes:**
- The character in the scene will not be visible until you press 'play' in the editor.
- The script will automatically look for the main camera in the scene to ensure the correct rotation of the sprite. If there is no main camera the sprite will not rotate correctly or sprite handler script may throw an error.

**Dark Forces Character Creator Link**
https://df-21.net/wiki/?title=WAX_File_Generator

**Credits:**
- BenDansie, Dark Forces Character Creator, https://www.bendansie.com/
- SpawnCampGames, Unity DOOM tutorial series, https://youtu.be/qcXEcZmZ8kA?si=cM-4oukvW4bdK86a

**License:**
MIT License

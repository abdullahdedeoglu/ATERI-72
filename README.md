# ATERI-72

Five classic arcade games rebuilt in Unity and wrapped in one arcade-style menu, as a study of how those games actually work.

| Game | Based on | What's in it |
|---|---|---|
| Honkey King | Donkey Kong | Two levels, rolling barrels from a spawner, ladders and jumping |
| Puck Man | Pac-Man | Tile maze, pellets and power pellets, four ghosts with scatter / chase / frightened / home states, tunnel passages |
| Space Hockey | Breakout | Paddle, ball physics, bricks with several hit states and unbreakable ones, lives |
| Star Knight | Space Invaders | Invader formation that speeds up as it shrinks, bunkers that erode, projectiles from both sides |
| Tetrix | Tetris | All seven tetrominoes, rotation, line clears, pause and game over |

Each game starts with a countdown and ends on a shared Game Over / You Win screen, reached from an intro screen and a main menu.

Made while learning Unity: most of the games follow [Zigurous](https://www.youtube.com/@Zigurous)' game-clone tutorials, then were renamed, reskinned and joined into one arcade with its own intro, menu, countdowns and end screens.

## Tech

Unity 2022.3 · C# · 2D physics and tilemaps · TextMeshPro

## Running it

Open the project with Unity 2022.3.5f1 (or a later 2022.3), load `Assets/Scenes/IntroScreen.unity` and press Play.

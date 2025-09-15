# Shark bait <h6> (Current Version 0.3)

Hello! Welcome to the repository of my tiny mobile game. This is a personal project and must be noted that **This only works on mobiles at the moment, controls will not work on PC**. 
This will be changed in the future as im planning an overhaul to the control system.

## Downloads

The most up to date builds aswell as the source code can be found here: https://github.com/CKirkby/Shark-Bait/releases/tag/Downloads

## Installation

**Note:** The build of this game can **Only be played on Andriod**. 

1. Download the APK from either your pc or your phone.
   * If pc, you need to transfer it to your phone through a file transfer
   * If phone, it will download to your files.
2. Once on your phone, open the files and tap the APK. It will begin installation. (Note: Your phone may fight back against this as antivirus' do not like apps that are not from the app store, it will most likely be a 'are you sure you want to install this' kind of thing)
3. Wait for it to install, it will appear as a normal app like any other.
4. Click on it to open
5. Enjoy! 

## What is this?

Shark bait is a small personal project that I made mainly to futher test my abilities to develop a game on mobile as well as have some fun. The game was heavily inspired by the fishing minigame from the Jak and Daxter precursor legacy game.
Here is an example of that minigame: 

https://www.youtube.com/watch?v=WvOeHx-U2Nc
- <h6> Youtube Credit: JELLIS Gamer
- <h6> Game Credit: Naughty Dog

The game simply follows the player who can collect fish as they swim down a river, gaining more points the more they collect and avoiding hazard fish that can cause detrimental effects. The speed and spawning rate of the fish increase over time
to increase difficulty.

## The Types of Fish

- Small Fish
  
  <img src="https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Art/image%20(2).png" width="100" height="100">
  This fish is the most common type and will reward the player 1 point

- Big Fish
  
  <img src="https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Art/Sad_tuna%20(1).png" width="100" height="100">
  This fish is among the least common and will reward the player with 3 points

- Turtle
  
  <img src="https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Art/Dont_eat_sea_tortle%20(1).png" width="100" height="100">
  The turtle is too hard to eat, it will hurt the player if they try and will cost the player a life

- Toxic Fish
  
  (Not Implemented Yet)
  
  This fish is poisonous and if the player eats this, it will spoil some of the catch, the player will lose points if they eat this.

  ## Controls
  <- / -> Movement:

  The movement right now uses your finger to drag left and right to move your player. This will be updated in future due to feedback to have an additional system where you can simply tap one of X buttons to change the
  players lanes.

  ## Gameplay

   <p align="center">
   <img src="https://github.com/user-attachments/assets/e169f5af-223c-499a-9df7-508cbb07e2d1" width="250" height="500">
   </p>

  ## Notable Code:
  * [Spawning Manager](https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Scripts/Game%20Managers/Spawn%20Manager.cs)
  * [Difficulty Manager](https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Scripts/Game%20Managers/Difficulty%20Controller.cs)
  * [Player Controller](https://github.com/CKirkby/Shark-Bait/blob/main/Shark%20Bait/Assets/Scripts/Player/PlayerController.cs)

  ## Updates

  ### 0.1:
  - Created Basic player movement systems.
  - Created basic fish movement using splines.
  - Created Spawning system
 
  ### 0.2:
  - Added Object pooling system to fish.
  - Created a dynamic, random spawning system with random chances to spawn each fish.
  - Set up collision profiles to register hits, between player and fish
  - Set up 'catcher' to return missed fish to object pool
 
  ### 0.3:
  - Set up the score system
  - Set up the health system
  - Created UI for health and score
  - Investigated performance issues on mobile
  - Adapted screen for a variety of resolutions
  - Created adaptive difficulty settings (Spawn interval and speed increase the more score you have)
  - Created a new lane system (When the player reaches score thresholds the spawner will start using more then one lane)
 
  ### 0.4:
  - Fix for spawn interval multiplier not increasing
  - Redid chance system and tided up multipliers
  - Disabled multi lane activation (Was too difficult and not fun)
  - Game over Menu
  - Implemented Toxic fish
  - Implemented simple high score system
 
  ### 0.5: (In Progress)
  

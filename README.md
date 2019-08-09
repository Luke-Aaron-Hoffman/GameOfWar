# GameOfWar
The playing card game War created in C#

# Pictures
The images located in this GitHub show examples of some non-obvious behavior in the code.

## Started a War With 1 Card Left
This image shows what happens when the players tie with their cards and begin a war whiloe they both have at least 1 card remaining, but someone doesn't have all 3 cards. What occurs is that instead of the "top 3" cards being pulled, all the cards EXCEPT THE LAST CARD are pulled. In this example, you can see in the first 2 wars, Player 1 pulls 3 cards before flipping the last one to be used for the War (which resulted in Wars themselves). During the third and final War, Player 1 had only 2 cards remaining, so they pulled the first card (8) and then kept the last card remaining for the actual flip, whereas Player 2 pulled 3 cards and flipped their card as normal.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Started%20a%20War%20with%201%20Card%20Left.PNG?raw=true

## Running Out During a War
This image shows what happens when the 2 players tie, but one of the player's do not have cards in their deck. The program will notify which player does not have cards in their library actually commence a War (i.e. they need at least 1 card to be able to fight again) before moving on to declare the winner as normal when a player runs out of cards.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Running%20Out%20During%20War.PNG?raw=true

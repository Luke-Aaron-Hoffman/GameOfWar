# GameOfWar
The playing card game War created in C#

To play, the .exe is located here: https://github.com/Luke-Aaron-Hoffman/GameOfWar/tree/master/Executable

Also remember to download the .config file to the same location. You can edit the values in the config if you would like to change some of the settings of the game.

# Pictures
The images located in this GitHub show examples of some non-obvious behavior in the code. 

NOTE: While they are from older versions of the game, they function the exact same way.

## Started a War With 1 Card Left
This image shows what happens when the players tie with their cards and begin a war whiloe they both have at least 1 card remaining, but someone doesn't have all 3 cards. What occurs is that instead of the "top 3" cards being pulled, all the cards EXCEPT THE LAST CARD are pulled. In this example, you can see in the first 2 wars, Player 1 pulls 3 cards before flipping the last one to be used for the War (which resulted in Wars themselves). During the third and final War, Player 1 had only 2 cards remaining, so they pulled the first card (8) and then kept the last card remaining for the actual flip, whereas Player 2 pulled 3 cards and flipped their card as normal.

As an aside, it was purely coincidence that not only did I find this situation to screenshot, but it also had 3 simultaneous Wars going on to end the game.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Started%20a%20War%20with%201%20Card%20Left.PNG?raw=true

## Running Out During a War
This image shows what happens when the 2 players tie, but one of the player's do not have cards in their deck. The program will notify which player does not have cards in their library actually commence a War (i.e. they need at least 1 card to be able to fight again) before moving on to declare the winner as normal when a player runs out of cards.

This picture was done by changing the deck building and spliting code, so that the deck contained only 3 "2s", forcing a War where one person has 0 cards remaining.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Running%20Out%20During%20War.PNG?raw=true


# Multiplayer Update
These pictures are related to the updated code from 10/9/2019 that includes multiple players, not just 1 on 1. I'll also explain how some of the nuances work for multiplayer, along with some design choices.

## Rules
Because a 54 card deck can only go so far, I made the decision that a 52 card deck can be used for a maximum of 4 players. If another player joins, a new 52 card deck is added to the base deck to create a 104 card deck. Decks are divided by the number of players in a game and dolled out as such, leaving out excess cards. So in a 3 player game, you would divide a 52 card deck by 3 for a total of 3 decks with 17 cards each (51 total cards, leaving 1 card unused). These cards are shuffled before being split, so the 1 card left out is random.

For an actual example, this picture shows a 7 player game about to start. With 7 players, there are 2 decks being used for a total of 104 cards. Divide 104 by 7 and you get 14 cards with 6 left over.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Multiple%20Players%20with%20Decks.PNG

### New Rule: High Cards Only for Wars
This rule was made to give choice to a question with no legitimate answer. In a multiplayer game, does everyone participate in every war, or do only the people who started the war? There is now a variable in the config for the code called "highCardOnlyWar" that if set to True makes it so only players who have the highest cards will fight in a war. This repeats multiple times until only 1 player has the highest card, where they will then take all cards currently on the stack, including the players who did not participate in any war.

The first picture shows an example where the variable is true (only players with the highest card), whereas the second picture shows an example where the variable is false (all players participate in all wars until eliminated).

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/High%20Card%20Only%20War%20is%20True.PNG

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/High%20Card%20Only%20War%20is%20False.PNG

### Running Out During War UPDATE
If a player runs out in the middle of a war, even during one that they started, they are now immediately marked for elimination (the term "marked" is used because the actual elimination happens after the entire war has been displaed, including the current character being eliminated). This can be seen here in the following picture.

https://github.com/Luke-Aaron-Hoffman/GameOfWar/blob/master/Losing%20Cards%20in%20a%20War%20Now%20Eliminates%20You.PNG

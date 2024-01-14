# Card Game: Hearts

## Descriptions
* This project is a digital implementation of the classic card game, Hearts. 
* This project allows one player (the user) to play against three bots. 
* The application will calculate and display the score of each round for each player. Once a player’s score reaches one hundred, the system will display the
 final scores for each player and declare the winner. 
* The user will have the option to start a new game once the game is over.
<br/>


## Technologies Used
* C#
* Windows Presentation Foundation (WPF)
<br />

## Card Game Rules
#### The Objective
* To accumulate the least points.

#### Winning the Game
* At the end of each round, scores are counted. The game ends when a player reaches 100 points. The person with the lowest scores among all players wins the game.

#### Card Distributions
* Each member will get 13 random cards in total, excluding Jokers.

#### Card Ranks
* Cards are ranked from the Ace with the highest value, and then K, Q, J, 10 … and so on, with 2 being the lowest value.

#### Score Counts
* All heart cards are worth 1 point each. Queen of Spade counts as 13 points. Other cards do not count any points.
* Furthermore, if a player wins all the Heart cards and the Queen of Spades (Shooting the Moon), the player will get zero points
  and the rest will get 26 points.
<br/>


## How To Play
1. At the start of each round (after the cards have been dealt), players will swap any three cards of their choice to another player before they have
   looked at the cards received from their opponents (Not implemented yet).
2. Cards are passed to the left at the first deal, to the right at the second deal, and across at the third deal. At the fourth deal, there will be no cards passing.
   The process repeats until someone wins the game.
3. The player with the two of clubs starts the round. Each player after the lead must follow the suit if they have the same suit, otherwise, they can play any card.
   If a player plays a heart card, then the heart will be broken, meaning players can now lead the new trick with a Heart.
   Additionally, if a player has only heart cards, they can lead the game with a heart card even if the heart has not been broken yet.
4. A player wins a trick when they play the highest card of the suit that is led. The winner leads the next trick.
<br/>

#### Breaking Hearts
Players are not allowed to lead a heart card until the hearts are broken. 
Hearts are broken when a player does not have a card in the same suit that has been led, and they play a heart instead.

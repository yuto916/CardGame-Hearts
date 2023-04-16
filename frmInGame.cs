using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Hearts
{
    public partial class frmInGame : Form
    {
        /* ***************************************************** Declare **************************************************** */
        private TaskCompletionSource<Card> player4CardPlayedTask = new TaskCompletionSource<Card>();

        private Dictionary<PictureBox, Card> pictureBoxCardMap = new Dictionary<PictureBox, Card>();
        private List<Card> player1Hand;
        private List<Card> player2Hand;
        private List<Card> player3Hand;
        private List<Card> player4Hand;


        private List<PictureBox> player4PictureBoxes;


        int firstPlayer;
        int secondPlayer;
        int thirdPlayer;
        int fourthPlayer;

        PictureBox firstPlayerPicBox;
        PictureBox secondPlayerPicBox;
        PictureBox thirdPlayerPicBox;
        PictureBox fourthPlayerPicBox;


        int firstPlayerScore;
        int secondPlayerScore;
        int thirdPlayerScore;
        int fourthPlayerScore;


        int playCount = 1;
        int gameRound = 1;


        List<Card> cardList = new List<Card>(4);


        Suit leadingCardSuit;
        Value leadingCardValue;


        bool heartBroken = false;


        const int NUM_OF_PLAYERS = 4;
        /* ****************************************************************************************************************** */







        /* ****************************************************** Events ******************************************************** */
        public frmInGame()
        {
            InitializeComponent();

            player4PictureBoxes = new List<PictureBox>
            {
                player4PicBox1,
                player4PicBox2,
                player4PicBox3,
                player4PicBox4,
                player4PicBox5,
                player4PicBox6,
                player4PicBox7,
                player4PicBox8,
                player4PicBox9,
                player4PicBox10,
                player4PicBox11,
                player4PicBox12,
                player4PicBox13
            };
        }


        private async void frmInGame_Load(object sender, EventArgs e)
        {
            var hands = await StartGame();


            // Access each player's hand
            player1Hand = hands.player1;
            player2Hand = hands.player2;
            player3Hand = hands.player3;
            player4Hand = hands.player4;


            // Map each picture box to a card value
            pictureBoxCardMap[player4PicBox1] = player4Hand[0];
            pictureBoxCardMap[player4PicBox2] = player4Hand[1];
            pictureBoxCardMap[player4PicBox3] = player4Hand[2];
            pictureBoxCardMap[player4PicBox4] = player4Hand[3];
            pictureBoxCardMap[player4PicBox5] = player4Hand[4];
            pictureBoxCardMap[player4PicBox6] = player4Hand[5];
            pictureBoxCardMap[player4PicBox7] = player4Hand[6];
            pictureBoxCardMap[player4PicBox8] = player4Hand[7];
            pictureBoxCardMap[player4PicBox9] = player4Hand[8];
            pictureBoxCardMap[player4PicBox10] = player4Hand[9];
            pictureBoxCardMap[player4PicBox11] = player4Hand[10];
            pictureBoxCardMap[player4PicBox12] = player4Hand[11];
            pictureBoxCardMap[player4PicBox13] = player4Hand[12];


            // Add click event handlers to each picture box
            player4PicBox1.Click += PictureBox_Click;
            player4PicBox2.Click += PictureBox_Click;
            player4PicBox3.Click += PictureBox_Click;
            player4PicBox4.Click += PictureBox_Click;
            player4PicBox5.Click += PictureBox_Click;
            player4PicBox6.Click += PictureBox_Click;
            player4PicBox7.Click += PictureBox_Click;
            player4PicBox8.Click += PictureBox_Click;
            player4PicBox9.Click += PictureBox_Click;
            player4PicBox10.Click += PictureBox_Click;
            player4PicBox11.Click += PictureBox_Click;
            player4PicBox12.Click += PictureBox_Click;
            player4PicBox13.Click += PictureBox_Click;



            // Game set up
            firstPlayer = DetermineFirstPlayer(player1Hand, player2Hand, player3Hand, player4Hand);
            secondPlayer = (firstPlayer + 1) % NUM_OF_PLAYERS;
            thirdPlayer = (secondPlayer + 1) % NUM_OF_PLAYERS;
            fourthPlayer = (thirdPlayer + 1) % NUM_OF_PLAYERS;


            secondPlayer = secondPlayer == 0 ? 4 : secondPlayer;
            thirdPlayer = thirdPlayer == 0 ? 4 : thirdPlayer;
            fourthPlayer = fourthPlayer == 0 ? 4 : fourthPlayer;


            string firstPlayerPicBoxName = "player" + firstPlayer + "PicBoxPlayCard";
            string secondPlayerPicBoxName = "player" + secondPlayer + "PicBoxPlayCard";
            string thirdPlayerPicBoxName = "player" + thirdPlayer + "PicBoxPlayCard";
            string fourthPlayerPicBoxName = "player" + fourthPlayer + "PicBoxPlayCard";


            firstPlayerPicBox = (PictureBox)this.Controls.Find(firstPlayerPicBoxName, true)[0];
            secondPlayerPicBox = (PictureBox)this.Controls.Find(secondPlayerPicBoxName, true)[0];
            thirdPlayerPicBox = (PictureBox)this.Controls.Find(thirdPlayerPicBoxName, true)[0];
            fourthPlayerPicBox = (PictureBox)this.Controls.Find(fourthPlayerPicBoxName, true)[0];


            UpdateCurrentPlayerLabel(firstPlayer);
            await Task.Delay(2000);


            MessageBox.Show(firstPlayer.ToString() + secondPlayer.ToString() + thirdPlayer.ToString() + fourthPlayer.ToString());

            // Trick 1.1   
            Trick1Play1(firstPlayer, player1Hand, player2Hand, player3Hand, player4Hand);
            UpdateCurrentPlayerLabel(secondPlayer);
            await Task.Delay(2000);


            // Trick 1.2 
            await Task.Delay(5000);
            if (firstPlayerPicBox.Image != null)
            {
                Trick1Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                UpdateCurrentPlayerLabel(thirdPlayer);
                await Task.Delay(2000);
            }


            // Trick 1.3
            await Task.Delay(5000);
            if (secondPlayerPicBox.Image != null)
            {
                Trick1Play3(thirdPlayer, player1Hand, player2Hand, player3Hand);
                UpdateCurrentPlayerLabel(fourthPlayer);
                await Task.Delay(2000);
            }


            // Trick 1.4
            await Task.Delay(5000);
            if (thirdPlayerPicBox.Image != null)
            {
                Trick1Play4(fourthPlayer, player1Hand, player2Hand, player3Hand);
                UpdateCurrentPlayerLabel(firstPlayer);
                await Task.Delay(2000);
            }

            UpdateScore(cardList);

        }


        // ------------------------------------------------------------------------------------------------------------------------
        private void btnExitGame_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the game?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private void PictureBox_Click(object sender, EventArgs e)
        {
            // Get the clicked picture box and corresponding card value
            PictureBox pictureBox = sender as PictureBox;
            string pictureBoxName = pictureBox.Name;
            Card card = pictureBoxCardMap[pictureBox];


            leadingCardSuit = cardList[0].Suit;


            PictureBox targetPictureBox = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

            if (targetPictureBox != null)
            {
                if (player4PicBoxPlayCard.Image == null)
                {


                    if (SuitExistsInHand(player4Hand, leadingCardSuit))
                    {
                        if (card.Suit == leadingCardSuit)
                        {
                            playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);
                            player4CardPlayedTask.SetResult(card);
                        }
                        else
                        {
                            MessageBox.Show("You cannot play this card!");
                        }
                    }
                    else if (SuitExistsInHand(player4Hand, leadingCardSuit) == false)
                    {
                        playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);
                        player4CardPlayedTask.SetResult(card);
                    }

                    else
                    {
                        MessageBox.Show("Wait for your turn!");
                    }
                }
            }
        }
        /* ********************************************************************************************************************** */










        /* ****************************************************** Methods ******************************************************* */
        // ------------------------------------------------------------------------------------------------------------------------
        private async Task<(List<Card> player1, List<Card> player2, List<Card> player3, List<Card> player4)> StartGame()
        {
            // Create a new deck object
            CardDeck deck = new CardDeck();
            deck.Shuffle();


            // Create a list for each player
            List<Card> player1 = new List<Card>();
            List<Card> player2 = new List<Card>();
            List<Card> player3 = new List<Card>();
            List<Card> player4 = new List<Card>();


            // Deal out the cards to each player
            for (int i = 0; i < 13; i++)
            {
                player1.Add(deck.Draw());
                player2.Add(deck.Draw());
                player3.Add(deck.Draw());
                player4.Add(deck.Draw());
            }


            // Sort players' deck by value and suit
            SortDeck(player1);
            SortDeck(player2);
            SortDeck(player3);
            SortDeck(player4);


            // Fill out the card slot of human player
            for (int i = 0; i < 13; i++)
            {
                Card card = player4[i];
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\{deck.GetCardImage(card)}";
                PictureBox pictureBox = Controls.Find($"player4PicBox{i + 1}", true).FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                {
                    await Task.Delay(100);
                    pictureBox.Image = Image.FromFile(imagePath);
                }
            }


            // Fill out the card slot of bot players
            FillOutBotCardSlot(1);
            FillOutBotCardSlot(2);
            FillOutBotCardSlot(3);


            // Return each player's hands as a tuple
            return (player1, player2, player3, player4);
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private void SortDeck(List<Card> deck)
        {
            deck.Sort((a, b) =>
            {
                if (a.Suit != b.Suit)
                {
                    return a.Suit - b.Suit;
                }
                else
                {
                    return a.Value - b.Value;
                }

            });
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private async void FillOutBotCardSlot(int playerNumber)
        {
            for (int i = 13; i > 0; i--)
            {
                if (playerNumber == 2 || playerNumber == 4)
                {
                    string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\card-back-v.gif";
                    PictureBox pictureBox = Controls.Find($"player{playerNumber}PicBox{i}", true).FirstOrDefault() as PictureBox;
                    if (pictureBox != null)
                    {
                        await Task.Delay(100);
                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                }
                else if (playerNumber == 1 || playerNumber == 3)
                {
                    string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\card-back-h.gif";
                    PictureBox pictureBox = Controls.Find($"player{playerNumber}PicBox{i}", true).FirstOrDefault() as PictureBox;
                    if (pictureBox != null)
                    {
                        await Task.Delay(100);
                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                }
            }
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private async void UpdateCurrentPlayerLabel(int playerNum)
        {
            await Task.Delay(1500);

            if (playerNum == 4)
            {
                lblCurrentPlayer.Text = "Current Player: You";
            }
            else
            {
                lblCurrentPlayer.Text = "Current Player: Player " + playerNum;
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private void UpdateScore(List<Card> cardList)
        {
            Suit leadingSuit = cardList[0].Suit;
            List<Card> leadingSuitCards = cardList.Where(c => c.Suit == leadingSuit).ToList();

            // Sort the list in descending order based on card value
            leadingSuitCards.Sort((c1, c2) => c2.Value.CompareTo(c1.Value));

            // Get the index of the first item in the sorted list
            int highestValueIndex = cardList.IndexOf(leadingSuitCards[0]);


            int score = CalculateScore(cardList);


            if (highestValueIndex + 1 == firstPlayer)
            {
                firstPlayerScore += score;
                txtPlayer1Score.Text = firstPlayerScore.ToString();
            }
            else if (highestValueIndex + 1 == secondPlayer)
            {
                secondPlayerScore += score;
                txtPlayer2Score.Text = secondPlayerScore.ToString();
            }
            else if (highestValueIndex + 1 == thirdPlayer)
            {
                thirdPlayerScore += score;
                txtPlayer3Score.Text = thirdPlayerScore.ToString();
            }
            else if (highestValueIndex + 1 == fourthPlayer)
            {
                fourthPlayerScore += score;
                txtPlayer4Score.Text = fourthPlayerScore.ToString();
            }

            cardList.Clear();
            player1PicBoxPlayCard.Image = null;
            player2PicBoxPlayCard.Image = null;
            player3PicBoxPlayCard.Image = null;
            player4PicBoxPlayCard.Image = null;
        }



        private int CalculateScore(List<Card> cardList)
        {
            int score = 0;
            int spadeQueenScore = 0;

            int numberOfHearts = cardList.Count(c => c.Suit == Suit.Hearts);
            bool containsSpade12 = cardList.Any(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

            if (containsSpade12 == true)
            {
                spadeQueenScore += 13;
            }


            score = numberOfHearts + spadeQueenScore;

            return score;
        }
        /* ********************************************************************************************************************** */







        /* ************************************************* Methods For Logic ************************************************** */
        // ------------------------------------------------------------------------------------------------------------------------
        // Function to determine the player who has Two of Clubs
        private int DetermineFirstPlayer(List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand, List<Card> humanHand)
        {
            bool player1HasTwoOfClubs = player1Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool player2HasTwoOfClubs = player2Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool player3HasTwoOfClubs = player3Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool humanHasTwoOfClubs = humanHand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);

            if (player1HasTwoOfClubs)
            {
                return 1;
            }
            else if (player2HasTwoOfClubs)
            {
                return 2;
            }
            else if (player3HasTwoOfClubs)
            {
                return 3;
            }
            else if (humanHasTwoOfClubs)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private void Trick1Play1(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand, List<Card> player4Hand)
        {
            if (playerNum == 1)
            {
                playCard(player1Hand, Suit.Clubs, Value.Two, player1PicBox13, player1PicBoxPlayCard);
                playCount++;
            }
            else if (playerNum == 2)
            {
                playCard(player2Hand, Suit.Clubs, Value.Two, player2PicBox13, player2PicBoxPlayCard);
                playCount++;
            }
            else if (playerNum == 3)
            {
                playCard(player3Hand, Suit.Clubs, Value.Two, player3PicBox13, player3PicBoxPlayCard);
                playCount++;
            }
            else if (playerNum == 4)
            {
                int index = player4Hand.FindIndex(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                string controlName = "player4PicBox" + (index + 1).ToString();
                PictureBox picBoxToRemove = (PictureBox)this.Controls.Find(controlName, true)[0];

                playCard(player4Hand, Suit.Clubs, Value.Two, picBoxToRemove, player4PicBoxPlayCard);
                playCount++;
            }
        }


        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick1Play2(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            leadingCardSuit = cardList[0].Suit;
            leadingCardValue = cardList[0].Value;

            if (playerNum == 1)
            {
                if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayMiddle(player1Hand, leadingCardSuit, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player1Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player1Hand, Suit.Spades, Value.Queen, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Hearts, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Spades, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, player1PicBox13, player1PicBoxPlayCard);
                }
            }


            else if (playerNum == 2)
            {
                if (SuitExistsInHand(player2Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayMiddle(player2Hand, leadingCardSuit, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player2Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player2Hand, Suit.Spades, Value.Queen, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Hearts, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Spades, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, player2PicBox12, player2PicBoxPlayCard);
                }
            }

            else if (playerNum == 3)
            {
                if (SuitExistsInHand(player3Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayMiddle(player3Hand, leadingCardSuit, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player3Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player3Hand, Suit.Spades, Value.Queen, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Hearts, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Spades, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, player3PicBox13, player3PicBoxPlayCard);
                }
            }

            else if (playerNum == 4)
            {
                while (!player4CardPlayedTask.Task.IsCompleted)
                {
                    await Task.Delay(100);
                }
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick1Play3(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            leadingCardSuit = cardList[0].Suit;
            leadingCardValue = cardList[0].Value;

            if (playerNum == 1)
            {
                if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayBiggestLess(player1Hand, leadingCardSuit, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player1Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player1Hand, Suit.Spades, Value.Queen, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Hearts, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Spades, player1PicBox13, player1PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, player1PicBox13, player1PicBoxPlayCard);
                }
            }


            else if (playerNum == 2)
            {
                if (SuitExistsInHand(player2Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayBiggestLess(player2Hand, leadingCardSuit, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player2Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player2Hand, Suit.Spades, Value.Queen, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Hearts, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Spades, player2PicBox13, player2PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, player2PicBox12, player2PicBoxPlayCard);
                }
            }

            else if (playerNum == 3)
            {
                if (SuitExistsInHand(player3Hand, leadingCardSuit) == true)
                {
                    SuitExistsPlayBiggestLess(player3Hand, leadingCardSuit, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                {
                    Card card = player3Hand.Find(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

                    playCard(player3Hand, Suit.Spades, Value.Queen, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Hearts))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Hearts, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Spades))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Spades, player3PicBox13, player3PicBoxPlayCard);
                }

                else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                {
                    SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, player3PicBox13, player3PicBoxPlayCard);
                }
            }

            else if (playerNum == 4)
            {
                while (!player4CardPlayedTask.Task.IsCompleted)
                {
                    await Task.Delay(100);
                }
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick1Play4(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            leadingCardSuit = cardList[0].Suit;
            leadingCardValue = cardList[0].Value;

            if (playerNum == 1)
            {
                if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                {
                    if (SuitExistsInCardList(cardList, Suit.Hearts) == true || CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        SuitExistsPlayBiggestLess(player1Hand, leadingCardSuit, player1PicBox13, player1PicBoxPlayCard);
                    }
                    else
                    {
                        SuitExistsPlayBiggest(player1Hand, Suit.Clubs, player1PicBox13, player1PicBoxPlayCard);
                    }
                }
                else
                {
                    if (CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        playCard(player1Hand, Suit.Spades, Value.Queen, player1PicBox13, player1PicBoxPlayCard);
                    }
                    else
                    {
                        if (SuitExistsInHand(player1Hand, Suit.Hearts))
                        {
                            SuitExistsPlayBiggest(player1Hand, Suit.Hearts, player1PicBox13, player1PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player1Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Spades, player1PicBox13, player1PicBoxPlayCard);
                            }

                            else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, player1PicBox13, player1PicBoxPlayCard);
                            }
                        }
                    }
                }
            }


            else if (playerNum == 2)
            {
                if (SuitExistsInHand(player2Hand, leadingCardSuit) == true)
                {
                    if (SuitExistsInCardList(cardList, Suit.Hearts) == true || CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        SuitExistsPlayBiggestLess(player2Hand, leadingCardSuit, player2PicBox13, player2PicBoxPlayCard);
                    }
                    else
                    {
                        SuitExistsPlayBiggest(player2Hand, Suit.Clubs, player2PicBox13, player2PicBoxPlayCard);
                    }
                }
                else
                {
                    if (CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        playCard(player2Hand, Suit.Spades, Value.Queen, player2PicBox13, player2PicBoxPlayCard);
                    }
                    else
                    {
                        if (SuitExistsInHand(player2Hand, Suit.Hearts))
                        {
                            SuitExistsPlayBiggest(player2Hand, Suit.Hearts, player2PicBox13, player2PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player2Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Spades, player2PicBox13, player2PicBoxPlayCard);
                            }

                            else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, player2PicBox13, player2PicBoxPlayCard);
                            }
                        }
                    }
                }
            }

            else if (playerNum == 3)
            {
                if (SuitExistsInHand(player3Hand, leadingCardSuit) == true)
                {
                    if (SuitExistsInCardList(cardList, Suit.Hearts) == true || CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        SuitExistsPlayBiggestLess(player3Hand, leadingCardSuit, player3PicBox13, player3PicBoxPlayCard);
                    }
                    else
                    {
                        SuitExistsPlayBiggest(player3Hand, Suit.Clubs, player3PicBox13, player3PicBoxPlayCard);
                    }
                }
                else
                {
                    if (CardExistsInCardList(cardList, Suit.Spades, Value.Queen) == true)
                    {
                        playCard(player3Hand, Suit.Spades, Value.Queen, player3PicBox13, player3PicBoxPlayCard);
                    }
                    else
                    {
                        if (SuitExistsInHand(player3Hand, Suit.Hearts))
                        {
                            SuitExistsPlayBiggest(player3Hand, Suit.Hearts, player3PicBox13, player3PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player3Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Spades, player3PicBox13, player3PicBoxPlayCard);
                            }

                            else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, player3PicBox13, player3PicBoxPlayCard);
                            }
                        }
                    }
                }
            }

            else if (playerNum == 4)
            {
                while (!player4CardPlayedTask.Task.IsCompleted)
                {
                    await Task.Delay(100);
                }
            }
        }
        /* ********************************************************************************************************************** */







        /* ************************************************* Algorithm Methods ************************************************** */

        // Function to remove a specified card from a player's hand and also removes a card visually 
        private async void playCard(List<Card> hand, Suit playSuit, Value playValue, PictureBox picBox, PictureBox playPicBox)
        {
            hand.RemoveAll(card => card.Suit == playSuit && card.Value == playValue);
            await Task.Delay(500);
            this.Controls.Remove(picBox);


            string cardNumber = ((int)playValue).ToString();
            string cardSuit = playSuit.ToString().Substring(0, 1);
            string cardCode = cardNumber + cardSuit;


            string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\{cardCode}.gif";
            playPicBox.Image = Image.FromFile(imagePath);

            Card playedCard = new Card(playSuit, playValue);
            cardList.Add(playedCard);
        }




        // If the suit exists, and want to play the biggest value of that suit
        private void SuitExistsPlayBiggest(List<Card> hand, Suit suit, PictureBox picBox, PictureBox playPicBox)
        {
            List<Card> specificSuitCards = hand.FindAll(c => c.Suit == suit);
            specificSuitCards.Sort((a, b) => a.Value.CompareTo(b.Value));
            Card biggestCard = specificSuitCards[specificSuitCards.Count - 1];
            Card card = hand.Find(c => c.Suit == suit && c.Value == biggestCard.Value);

            playCard(hand, card.Suit, card.Value, picBox, playPicBox);
        }



        // If the suit exists, and want to play the biggest value of that suit that is less than the biggest value played
        private void SuitExistsPlayBiggestLess(List<Card> hand, Suit suit, PictureBox picBox, PictureBox playPicBox)
        {
            // Create a list from "cardList" where the suit is the same suit as the card that is the first item in the list
            List<Card> sameSuitCards = cardList.Where(c => c.Suit == suit).ToList();
            Card highestCard = sameSuitCards.Max();

            // Create a list from the player's hand (value less than "highestCard")
            List<Card> specificSuitCardsBig = hand.Where(c => c.Suit == suit && c.Value < highestCard.Value).ToList();

            // Create a list from the player's hand (all cards that is the same as the suit)
            List<Card> specificSuitCards = hand.Where(c => c.Suit == Suit.Clubs).ToList();

            if (specificSuitCardsBig.Count > 0)
            {
                Card cardToPlay = specificSuitCardsBig.OrderByDescending(c => c.Value).First();

                playCard(hand, cardToPlay.Suit, cardToPlay.Value, picBox, playPicBox);
            }
            else
            {
                Card cardToPlay = specificSuitCards.OrderBy(c => c.Value).First();

                playCard(hand, cardToPlay.Suit, cardToPlay.Value, picBox, playPicBox);
            }
        }



        // If the suit exists, and want to play the middlee value of that suit
        private void SuitExistsPlayMiddle(List<Card> hand, Suit suit, PictureBox picBox, PictureBox playPicBox)
        {
            List<Card> specificSuitCards = hand.FindAll(c => c.Suit == suit);
            specificSuitCards.Sort((x, y) => x.Value.CompareTo(y.Value));
            int middleIndex = specificSuitCards.Count / 2;
            Card middleCard = specificSuitCards[middleIndex];
            Card card = hand.Find(c => c.Suit == suit && c.Value == middleCard.Value);

            playCard(hand, card.Suit, card.Value, picBox, playPicBox);
        }



        public bool SuitExistsInHand(List<Card> hand, Suit suit)
        {
            return hand.Any(card => card.Suit == suit);
        }



        bool CardExistsInHand(List<Card> hand, Suit suit, Value value)
        {
            return hand.Any(c => c.Suit == suit && c.Value == value);
        }


        // Function to see if cardList contains a specific suit
        private bool SuitExistsInCardList(List<Card> cardList, Suit suit)
        {
            List<Card> suits = cardList.Where(c => c.Suit == suit).ToList();

            if (suits.Count > 0) { return true; }
            else { return false; }
        }


        // Function to see if cardList contains certain card
        private bool CardExistsInCardList(List<Card> cardList, Suit suit, Value value)
        {
            List<Card> suits = cardList.Where(card => card.Suit == suit).ToList();

            return suits.Any(card => card.Value == value);
        }
        /* ********************************************************************************************************************** */
    }
}

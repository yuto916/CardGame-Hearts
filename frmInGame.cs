using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
        private TaskCompletionSource<Card> player4CardPlayedTask1 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask2 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask3 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask4 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask5 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask6 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask7 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask8 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask9 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask10 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask11 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask12 = new TaskCompletionSource<Card>();
        private TaskCompletionSource<Card> player4CardPlayedTask13 = new TaskCompletionSource<Card>();

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


        int player1Score;
        int player2Score;
        int player3Score;
        int player4Score;

        bool go;
        int playCount = 1;
        int trick = 1;


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








            /* ----------------------------------------- Trick 1 ----------------------------------------- */
            // 1
            if (trick == 1 && playCount % 4 == 1)
            {
                firstPlayer = DetermineFirstPlayer(player1Hand, player2Hand, player3Hand, player4Hand);
                UpdateCurrentPlayerLabel(firstPlayer);
                lblCurrentTrick.Text = "1.1";
                StartRound(firstPlayer);


                await Task.Delay(1300);
                Trick1Play1(firstPlayer, player1Hand, player2Hand, player3Hand, player4Hand);
                await Task.Delay(1000);
                UpdateCurrentPlayerLabel(secondPlayer);
                playCount++;
            }

            // 2
            if (trick == 1 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "1.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask1.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick1Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
             }

            // 3
            if (trick == 1 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "1.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask1.Task.IsCompleted) { await Task.Delay(100); }

                }
                await Task.Delay(1000);
                Trick1Play3(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 1 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "1.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask1.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick1Play4(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;    
            }
            /* ----------------------------------------- Trick 2 ----------------------------------------- */
            // 1
            if (trick == 2 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "2.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask2.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 2
            if (trick == 2 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "2.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask2.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 3
            if (trick == 2 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "2.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask2.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 2 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "2.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask2.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;
            }
            /* ----------------------------------------- Trick 3 ----------------------------------------- */
            // 1
            if (trick == 3 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "3.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask3.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 2
            if (trick == 3 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "3.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask3.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 3
            if (trick == 3 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "3.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask3.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 3 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "3.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask3.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;
            }
            /* ----------------------------------------- Trick 4 ----------------------------------------- */
            // 1
            if (trick == 4 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "4.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask4.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 2
            if (trick == 4 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "4.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask4.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 3
            if (trick == 4 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "4.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask4.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 4 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "4.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask4.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;
            }
            /* ----------------------------------------- Trick 5 ----------------------------------------- */
            // 1
            if (trick == 5 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "5.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask5.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 2
            if (trick == 5 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "5.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask5.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 3
            if (trick == 5 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "5.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask5.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 5 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "5.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask5.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;
            }
            /* ----------------------------------------- Trick 6 ----------------------------------------- */
            // 1
            if (trick == 6 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "6.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask6.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 2
            if (trick == 6 && playCount % 4 == 2)
            {
                UpdateCurrentPlayerLabel(secondPlayer);
                lblCurrentTrick.Text = "6.2";
                if (secondPlayer == 4)
                {
                    while (!player4CardPlayedTask6.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play2(secondPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 3
            if (trick == 6 && playCount % 4 == 3)
            {
                UpdateCurrentPlayerLabel(thirdPlayer);
                lblCurrentTrick.Text = "6.3";
                if (thirdPlayer == 4)
                {
                    while (!player4CardPlayedTask6.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(thirdPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }

            // 4
            if (trick == 6 && playCount % 4 == 0)
            {
                UpdateCurrentPlayerLabel(fourthPlayer);
                lblCurrentTrick.Text = "6.4";
                if (fourthPlayer == 4)
                {
                    while (!player4CardPlayedTask6.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play34(fourthPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
                trick++;
            }
            /* ----------------------------------------- Trick 7 ----------------------------------------- */
            // 1
            if (trick == 7 && playCount % 4 == 1)
            {
                firstPlayer = UpdateScore(cardList);
                StartRound(firstPlayer);
                UpdateCurrentPlayerLabel(firstPlayer);


                lblCurrentTrick.Text = "7.1";
                if (firstPlayer == 4)
                {
                    while (!player4CardPlayedTask7.Task.IsCompleted) { await Task.Delay(100); }
                }
                await Task.Delay(1000);
                Trick2Play1(firstPlayer, player1Hand, player2Hand, player3Hand);
                await Task.Delay(1000);
                playCount++;
            }
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





            PictureBox targetPictureBox = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

            if (targetPictureBox != null)
            {
                if (lblCurrentPlayer.Text == "Current Player: You")
                {
                    if (firstPlayer != 4)
                    {
                        leadingCardSuit = cardList[0].Suit;

                        if (SuitExistsInHand(player4Hand, leadingCardSuit))
                        {
                            if (card.Suit == leadingCardSuit)
                            {
                                playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);
                                if (card.Suit == Suit.Hearts)
                                {
                                    heartBroken = true;
                                }

                                if (trick == 1) { player4CardPlayedTask1.SetResult(card); }
                                else if (trick == 2) { player4CardPlayedTask2.SetResult(card); }
                                else if (trick == 3) { player4CardPlayedTask3.SetResult(card); }
                                else if (trick == 4) { player4CardPlayedTask4.SetResult(card); }
                                else if (trick == 5) { player4CardPlayedTask5.SetResult(card); }
                                else if (trick == 6) { player4CardPlayedTask6.SetResult(card); }
                                else if (trick == 7) { player4CardPlayedTask7.SetResult(card); }
                                else if (trick == 8) { player4CardPlayedTask8.SetResult(card); }
                                else if (trick == 9) { player4CardPlayedTask9.SetResult(card); }
                                else if (trick == 10) { player4CardPlayedTask10.SetResult(card); }
                                else if (trick == 11) { player4CardPlayedTask11.SetResult(card); }
                                else if (trick == 12) { player4CardPlayedTask12.SetResult(card); }
                                else if (trick == 13) { player4CardPlayedTask13.SetResult(card); }
                            }
                            else
                            {
                                MessageBox.Show("You cannot play this card!");
                            }
                        }
                        else if (SuitExistsInHand(player4Hand, leadingCardSuit) == false)
                        {
                            playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);
                            if (card.Suit == Suit.Hearts)
                            {
                                heartBroken = true;
                            }

                            if (trick == 1) { player4CardPlayedTask1.SetResult(card); }
                            else if (trick == 2) { player4CardPlayedTask2.SetResult(card); }
                            else if (trick == 3) { player4CardPlayedTask3.SetResult(card); }
                            else if (trick == 4) { player4CardPlayedTask4.SetResult(card); }
                            else if (trick == 5) { player4CardPlayedTask5.SetResult(card); }
                            else if (trick == 6) { player4CardPlayedTask6.SetResult(card); }
                            else if (trick == 7) { player4CardPlayedTask7.SetResult(card); }
                            else if (trick == 8) { player4CardPlayedTask8.SetResult(card); }
                            else if (trick == 9) { player4CardPlayedTask9.SetResult(card); }
                            else if (trick == 10) { player4CardPlayedTask10.SetResult(card); }
                            else if (trick == 11) { player4CardPlayedTask11.SetResult(card); }
                            else if (trick == 12) { player4CardPlayedTask12.SetResult(card); }
                            else if (trick == 13) { player4CardPlayedTask13.SetResult(card); }
                        }
                    }
                    else
                    {
                        if (heartBroken == true)
                        {
                            playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);

                            if (trick == 1) { player4CardPlayedTask1.SetResult(card); }
                            else if (trick == 2) { player4CardPlayedTask2.SetResult(card); }
                            else if (trick == 3) { player4CardPlayedTask3.SetResult(card); }
                            else if (trick == 4) { player4CardPlayedTask4.SetResult(card); }
                            else if (trick == 5) { player4CardPlayedTask5.SetResult(card); }
                            else if (trick == 6) { player4CardPlayedTask6.SetResult(card); }
                            else if (trick == 7) { player4CardPlayedTask7.SetResult(card); }
                            else if (trick == 8) { player4CardPlayedTask8.SetResult(card); }
                            else if (trick == 9) { player4CardPlayedTask9.SetResult(card); }
                            else if (trick == 10) { player4CardPlayedTask10.SetResult(card); }
                            else if (trick == 11) { player4CardPlayedTask11.SetResult(card); }
                            else if (trick == 12) { player4CardPlayedTask12.SetResult(card); }
                            else if (trick == 13) { player4CardPlayedTask13.SetResult(card); }
                        }
                        else if (heartBroken == false)
                        {
                            if (card.Suit != Suit.Hearts)
                            {
                                playCard(player4Hand, card.Suit, card.Value, targetPictureBox, player4PicBoxPlayCard);

                                if (trick == 1) { player4CardPlayedTask1.SetResult(card); }
                                else if (trick == 2) { player4CardPlayedTask2.SetResult(card); }
                                else if (trick == 3) { player4CardPlayedTask3.SetResult(card); }
                                else if (trick == 4) { player4CardPlayedTask4.SetResult(card); }
                                else if (trick == 5) { player4CardPlayedTask5.SetResult(card); }
                                else if (trick == 6) { player4CardPlayedTask6.SetResult(card); }
                                else if (trick == 7) { player4CardPlayedTask7.SetResult(card); }
                                else if (trick == 8) { player4CardPlayedTask8.SetResult(card); }
                                else if (trick == 9) { player4CardPlayedTask9.SetResult(card); }
                                else if (trick == 10) { player4CardPlayedTask10.SetResult(card); }
                                else if (trick == 11) { player4CardPlayedTask11.SetResult(card); }
                                else if (trick == 12) { player4CardPlayedTask12.SetResult(card); }
                                else if (trick == 13) { player4CardPlayedTask13.SetResult(card); }
                            }
                            else
                            {
                                MessageBox.Show("You cannot lead with heart until heart is broken!");
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Wait for your turn!");
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
        private void UpdateCurrentPlayerLabel(int playerNum)
        {
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
        private int UpdateScore(List<Card> cardList)
        {
            Suit leadingSuit = cardList[0].Suit;
            List<Card> leadingSuitCards = cardList.Where(c => c.Suit == leadingSuit).ToList();

            // Sort the list in descending order based on card value
            leadingSuitCards.Sort((c1, c2) => c2.Value.CompareTo(c1.Value));

            // Get the index of the first item in the sorted list
            int highestValueIndex = cardList.IndexOf(leadingSuitCards[0]);
            Card highestValueCard = leadingSuitCards[0];

            
            if (highestValueCard == cardList[0])
            {
                int score = CalculateScore(cardList);

                string firstPlayerScoreText = "txtPlayer" + firstPlayer + "Score";
                TextBox textBox = this.Controls.Find(firstPlayerScoreText, true).FirstOrDefault() as TextBox;

                if (firstPlayer == 1) { player1Score += score; textBox.Text = player1Score.ToString(); }
                else if (firstPlayer == 2) { player2Score += score; textBox.Text = player2Score.ToString(); }
                else if (firstPlayer == 3) { player3Score += score; textBox.Text = player3Score.ToString(); }
                else if (firstPlayer == 4) { player4Score += score; textBox.Text = player4Score.ToString(); }
  
                cardList.Clear();
                player1PicBoxPlayCard.Image = null;
                player2PicBoxPlayCard.Image = null;
                player3PicBoxPlayCard.Image = null;
                player4PicBoxPlayCard.Image = null;
                return firstPlayer;
            }
            else if (highestValueCard == cardList[1])
            {
                int score = CalculateScore(cardList);

                string secondPlayerScoreText = "txtPlayer" + secondPlayer + "Score";
                TextBox textBox = this.Controls.Find(secondPlayerScoreText, true).FirstOrDefault() as TextBox;

                if (secondPlayer == 1) { player1Score += score; textBox.Text = player1Score.ToString(); }
                else if (secondPlayer == 2) { player2Score += score; textBox.Text = player2Score.ToString(); }
                else if (secondPlayer == 3) { player3Score += score; textBox.Text = player3Score.ToString(); }
                else if (secondPlayer == 4) { player4Score += score; textBox.Text = player4Score.ToString(); }

                cardList.Clear();
                player1PicBoxPlayCard.Image = null;
                player2PicBoxPlayCard.Image = null;
                player3PicBoxPlayCard.Image = null;
                player4PicBoxPlayCard.Image = null;
                return secondPlayer;
            }
            else if (highestValueCard == cardList[2])
            {
                int score = CalculateScore(cardList);

                string thirdPlayerScoreText = "txtPlayer" + thirdPlayer + "Score";
                TextBox textBox = this.Controls.Find(thirdPlayerScoreText, true).FirstOrDefault() as TextBox;

                if (thirdPlayer == 1) { player1Score += score; textBox.Text = player1Score.ToString(); }
                else if (thirdPlayer == 2) { player2Score += score; textBox.Text = player2Score.ToString(); }
                else if (thirdPlayer == 3) { player3Score += score; textBox.Text = player3Score.ToString(); }
                else if (thirdPlayer == 4) { player4Score += score; textBox.Text = player4Score.ToString(); }

                cardList.Clear();
                player1PicBoxPlayCard.Image = null;
                player2PicBoxPlayCard.Image = null;
                player3PicBoxPlayCard.Image = null;
                player4PicBoxPlayCard.Image = null;
                return thirdPlayer;
            }
            else if (highestValueCard == cardList[3])
            {
                int score = CalculateScore(cardList);

                string fourthPlayerScoreText = "txtPlayer" + fourthPlayer + "Score";
                TextBox textBox = this.Controls.Find(fourthPlayerScoreText, true).FirstOrDefault() as TextBox;

                if (fourthPlayer == 1) { player1Score += score; textBox.Text = player1Score.ToString(); }
                else if (fourthPlayer == 2) { player2Score += score; textBox.Text = player2Score.ToString(); }
                else if (fourthPlayer == 3) { player3Score += score; textBox.Text = player3Score.ToString(); }
                else if (fourthPlayer == 4) { player4Score += score; textBox.Text = player4Score.ToString(); }

                cardList.Clear();
                player1PicBoxPlayCard.Image = null;
                player2PicBoxPlayCard.Image = null;
                player3PicBoxPlayCard.Image = null;
                player4PicBoxPlayCard.Image = null;
                return fourthPlayer;
            }
            else
            {
                return -1;
            }
        }



        private int CalculateScore(List<Card> cardList)
        {
            int score = 0;
            int numberOfHearts = 0;
            bool containsSpade12 = false;

            numberOfHearts = cardList.Count(c => c.Suit == Suit.Hearts);
            containsSpade12 = cardList.Any(c => c.Suit == Suit.Spades && c.Value == Value.Queen);

            if (containsSpade12 == true)
            {
                score += 13;
            }

            score += numberOfHearts;

            return score;
        }



        private void StartRound(int firstPlayerNum)
        {
            // Round set up
            firstPlayer = firstPlayerNum;
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
            }
            else if (playerNum == 2)
            {
                playCard(player2Hand, Suit.Clubs, Value.Two, player2PicBox13, player2PicBoxPlayCard);
            }
            else if (playerNum == 3)
            {
                playCard(player3Hand, Suit.Clubs, Value.Two, player3PicBox13, player3PicBoxPlayCard);
            }
            else if (playerNum == 4)
            {
                int index = player4Hand.FindIndex(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                string controlName = "player4PicBox" + (index + 1).ToString();
                PictureBox picBoxToRemove = (PictureBox)this.Controls.Find(controlName, true)[0];

                playCard(player4Hand, Suit.Clubs, Value.Two, picBoxToRemove, player4PicBoxPlayCard);

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
                    heartBroken = true;
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
                    heartBroken = true;
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
                    heartBroken = true;
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
                    heartBroken = true;
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
                    heartBroken = true;
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
                    heartBroken = true;
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
                            heartBroken = true;
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
                            heartBroken = true;
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
                            heartBroken = true;
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
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick2Play1(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            if (playerNum == 1)
            {
                string picBoxName = "player1PicBox" + (14 - trick);
                PictureBox pl1PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;


                int numberOfHearts = player1Hand.Count(card => card.Suit == Suit.Hearts);
                int numberOfDiamonds = player1Hand.Count(card => card.Suit == Suit.Diamonds);
                int numberOfClubs = player1Hand.Count(card => card.Suit == Suit.Clubs);
                int numberOfSpades = player1Hand.Count(card => card.Suit == Suit.Spades);

                if (heartBroken == true)
                {
                    int[] suitCounts = new int[4] { numberOfHearts, numberOfDiamonds, numberOfClubs, numberOfSpades };
                    int minSuitIndex = Array.IndexOf(suitCounts, suitCounts.Where(c => c > 0).Min());
                    Suit minSuit = (Suit)minSuitIndex;

                    SuitExistsPlayMiddle(player1Hand, minSuit, pl1PicBox, player1PicBoxPlayCard);
                }
                else
                {
                    List<Card> noHearts = player1Hand.Where(c => c.Suit != Suit.Hearts).ToList();
                    noHearts.Sort((x, y) => x.Value.CompareTo(y.Value));
                    Card smallestCard = noHearts.FirstOrDefault();

                    SuitExistsPlayMiddle(player1Hand, smallestCard.Suit, pl1PicBox, player1PicBoxPlayCard);
                }
            }

            else if (playerNum == 2)
            {
                string picBoxName = "player2PicBox" + (14 - trick);
                PictureBox pl2PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                int numberOfHearts = player2Hand.Count(card => card.Suit == Suit.Hearts);
                int numberOfDiamonds = player2Hand.Count(card => card.Suit == Suit.Diamonds);
                int numberOfClubs = player2Hand.Count(card => card.Suit == Suit.Clubs);
                int numberOfSpades = player2Hand.Count(card => card.Suit == Suit.Spades);

                if (heartBroken == true)
                {
                    int[] suitCounts = new int[4] { numberOfHearts, numberOfDiamonds, numberOfClubs, numberOfSpades };
                    int minSuitIndex = Array.IndexOf(suitCounts, suitCounts.Where(c => c > 0).Min());
                    Suit minSuit = (Suit)minSuitIndex;

                    SuitExistsPlayMiddle(player2Hand, minSuit, pl2PicBox, player2PicBoxPlayCard);
                }
                else
                {
                    List<Card> noHearts = player2Hand.Where(c => c.Suit != Suit.Hearts).ToList();
                    noHearts.Sort((x, y) => x.Value.CompareTo(y.Value));
                    Card smallestCard = noHearts.FirstOrDefault();

                    SuitExistsPlayMiddle(player2Hand, smallestCard.Suit, pl2PicBox, player2PicBoxPlayCard);
                }
            }

            else if (playerNum == 3)
            {
                string picBoxName = "player3PicBox" + (14 - trick);
                PictureBox pl3PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                int numberOfHearts = player3Hand.Count(card => card.Suit == Suit.Hearts);
                int numberOfDiamonds = player3Hand.Count(card => card.Suit == Suit.Diamonds);
                int numberOfClubs = player3Hand.Count(card => card.Suit == Suit.Clubs);
                int numberOfSpades = player3Hand.Count(card => card.Suit == Suit.Spades);

                if (heartBroken == true)
                {
                    int[] suitCounts = new int[4] { numberOfHearts, numberOfDiamonds, numberOfClubs, numberOfSpades };
                    int minSuitIndex = Array.IndexOf(suitCounts, suitCounts.Where(c => c > 0).Min());
                    Suit minSuit = (Suit)minSuitIndex;

                    SuitExistsPlayMiddle(player3Hand, minSuit, pl3PicBox, player3PicBoxPlayCard);
                }
                else
                {
                    List<Card> noHearts = player3Hand.Where(c => c.Suit != Suit.Hearts).ToList();
                    noHearts.Sort((x, y) => x.Value.CompareTo(y.Value));
                    Card smallestCard = noHearts.FirstOrDefault();

                    SuitExistsPlayMiddle(player3Hand, smallestCard.Suit, pl3PicBox, player3PicBoxPlayCard);
                }
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick2Play2(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            leadingCardSuit = cardList[0].Suit;
            leadingCardValue = cardList[0].Value;

            if (playerNum == 1)
            {
                string picBoxName = "player1PicBox" + (14 - trick);
                PictureBox pl1PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitIsLeading(cardList, Suit.Hearts) == true)
                {
                    if (SuitExistsInHand(player1Hand, Suit.Hearts))
                    {
                        SuitExistsPlayMiddle(player1Hand, Suit.Hearts, pl1PicBox, player1PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player1Hand, Suit.Spades, Value.Queen, pl1PicBox, player1PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player1Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Clubs, pl1PicBox, player1PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player1Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Spades, pl1PicBox, player1PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, pl1PicBox, player1PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player1Hand, leadingCardSuit, pl1PicBox, player1PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player1Hand, Suit.Spades, Value.Queen, pl1PicBox, player1PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player1Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Hearts, pl1PicBox, player1PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player1Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Clubs, pl1PicBox, player1PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player1Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Spades, pl1PicBox, player1PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, pl1PicBox, player1PicBoxPlayCard);
                                }
                            }
                        }
                    }
                }
            }

            else if (playerNum == 2)
            {
                string picBoxName = "player2PicBox" + (14 - trick);
                PictureBox pl2PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitIsLeading(cardList, Suit.Hearts) == true)
                {
                    if (SuitExistsInHand(player2Hand, Suit.Hearts))
                    {
                        SuitExistsPlayMiddle(player2Hand, Suit.Hearts, pl2PicBox, player2PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player2Hand, Suit.Spades, Value.Queen, pl2PicBox, player2PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player2Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Clubs, pl2PicBox, player2PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player2Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Spades, pl2PicBox, player2PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, pl2PicBox, player2PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player2Hand, leadingCardSuit, pl2PicBox, player2PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player2Hand, Suit.Spades, Value.Queen, pl2PicBox, player2PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player2Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Hearts, pl2PicBox, player2PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player2Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Clubs, pl2PicBox, player2PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player2Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Spades, pl2PicBox, player2PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, pl2PicBox, player2PicBoxPlayCard);
                                }
                            }
                        }
                    }
                }
            }

            else if (playerNum == 3)
            {
                string picBoxName = "player3PicBox" + (14 - trick);
                PictureBox pl3PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitIsLeading(cardList, Suit.Hearts) == true)
                {
                    if (SuitExistsInHand(player3Hand, Suit.Hearts))
                    {
                        SuitExistsPlayMiddle(player3Hand, Suit.Hearts, pl3PicBox, player3PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player3Hand, Suit.Spades, Value.Queen, pl3PicBox, player3PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player3Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Clubs, pl3PicBox, player3PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player3Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Spades, pl3PicBox, player3PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, pl3PicBox, player3PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player3Hand, leadingCardSuit, pl3PicBox, player3PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player3Hand, Suit.Spades, Value.Queen, pl3PicBox, player3PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player3Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Hearts, pl3PicBox, player3PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player3Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Clubs, pl3PicBox, player3PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player3Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Spades, pl3PicBox, player3PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, pl3PicBox, player3PicBoxPlayCard);
                                }
                            }
                        }
                    }
                }
            }
        }



        // ------------------------------------------------------------------------------------------------------------------------
        private async void Trick2Play34(int playerNum, List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand)
        {
            leadingCardSuit = cardList[0].Suit;
            leadingCardValue = cardList[0].Value;

            if (playerNum == 1)
            {
                string picBoxName = "player1PicBox" + (14 - trick);
                PictureBox pl1PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitIsLeading(cardList, Suit.Hearts) == true)
                {
                    if (SuitExistsInHand(player1Hand, Suit.Hearts))
                    {
                        SuitExistsPlayBiggestLess(player1Hand, Suit.Hearts, pl1PicBox, player1PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player1Hand, Suit.Spades, Value.Queen, pl1PicBox, player1PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player1Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Clubs, pl1PicBox, player1PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player1Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Spades, pl1PicBox, player1PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, pl1PicBox, player1PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player1Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player1Hand, leadingCardSuit, pl1PicBox, player1PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player1Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player1Hand, Suit.Spades, Value.Queen, pl1PicBox, player1PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player1Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player1Hand, Suit.Hearts, pl1PicBox, player1PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player1Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Clubs, pl1PicBox, player1PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player1Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Spades, pl1PicBox, player1PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player1Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player1Hand, Suit.Diamonds, pl1PicBox, player1PicBoxPlayCard);
                                }
                            }
                        }
                    }
                }
            }

            else if (playerNum == 2)
            {
                string picBoxName = "player2PicBox" + (14 - trick);
                PictureBox pl2PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitExistsInCardList(cardList, Suit.Hearts) == true)
                {
                    if (SuitIsLeading(player2Hand, Suit.Hearts))
                    {
                        SuitExistsPlayBiggestLess(player2Hand, Suit.Hearts, pl2PicBox, player2PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player2Hand, Suit.Spades, Value.Queen, pl2PicBox, player2PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player2Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Clubs, pl2PicBox, player2PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player2Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Spades, pl2PicBox, player2PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, pl2PicBox, player2PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player2Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player2Hand, leadingCardSuit, pl2PicBox, player2PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player2Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player2Hand, Suit.Spades, Value.Queen, pl2PicBox, player2PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player2Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player2Hand, Suit.Hearts, pl2PicBox, player2PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player2Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Clubs, pl2PicBox, player2PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player2Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Spades, pl2PicBox, player2PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player2Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player2Hand, Suit.Diamonds, pl2PicBox, player2PicBoxPlayCard);
                                }
                            }
                        }
                    }
                }
            }

            else if (playerNum == 3)
            {
                string picBoxName = "player3PicBox" + (14 - trick);
                PictureBox pl3PicBox = this.Controls.Find(picBoxName, true).FirstOrDefault() as PictureBox;

                if (SuitIsLeading(cardList, Suit.Hearts) == true)
                {
                    if (SuitExistsInHand(player3Hand, Suit.Hearts))
                    {
                        SuitExistsPlayBiggestLess(player3Hand, Suit.Hearts, pl3PicBox, player3PicBoxPlayCard);
                        heartBroken = true;
                    }
                    else
                    {
                        if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player3Hand, Suit.Spades, Value.Queen, pl3PicBox, player3PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player3Hand, Suit.Clubs))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Clubs, pl3PicBox, player3PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player3Hand, Suit.Spades))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Spades, pl3PicBox, player3PicBoxPlayCard);
                            }
                            else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, pl3PicBox, player3PicBoxPlayCard);
                            }
                        }
                    }
                }
                else
                {
                    if (SuitExistsInHand(player3Hand, leadingCardSuit) == true)
                    {
                        SuitExistsPlayBiggestLess(player3Hand, leadingCardSuit, pl3PicBox, player3PicBoxPlayCard);
                    }
                    else
                    {
                        if (CardExistsInHand(player3Hand, Suit.Spades, Value.Queen))
                        {
                            playCard(player3Hand, Suit.Spades, Value.Queen, pl3PicBox, player3PicBoxPlayCard);
                        }
                        else
                        {
                            if (SuitExistsInHand(player3Hand, Suit.Hearts))
                            {
                                SuitExistsPlayBiggest(player3Hand, Suit.Hearts, pl3PicBox, player3PicBoxPlayCard);
                                heartBroken = true;
                            }
                            else
                            {
                                if (SuitExistsInHand(player3Hand, Suit.Clubs))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Clubs, pl3PicBox, player3PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player3Hand, Suit.Spades))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Spades, pl3PicBox, player3PicBoxPlayCard);
                                }
                                else if (SuitExistsInHand(player3Hand, Suit.Diamonds))
                                {
                                    SuitExistsPlayBiggest(player3Hand, Suit.Diamonds, pl3PicBox, player3PicBoxPlayCard);
                                }
                            }
                        }
                    }
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


        // If the suit exists, and want to play the least value of that suit
        private void SuitExistsPlayLeast(List<Card> hand, Suit suit, PictureBox picBox, PictureBox playPicBox)
        {
            List<Card> specificSuitCards = hand.FindAll(c => c.Suit == suit);
            specificSuitCards.Sort((a, b) => b.Value.CompareTo(a.Value)); // Sort in reverse order
            Card leastCard = specificSuitCards[specificSuitCards.Count - 1];
            Card card = hand.Find(c => c.Suit == suit && c.Value == leastCard.Value);

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
            List<Card> specificSuitCards = hand.Where(c => c.Suit == suit).ToList();

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


        // Function to see if the suit is leading
        private bool SuitIsLeading(List<Card> cardList, Suit suit)
        {
            if (cardList.Count > 0)
            {
                return cardList[0].Suit == suit;
            }
            else
            {
                return false;
            }
        }
        /* ********************************************************************************************************************** */
    }

}

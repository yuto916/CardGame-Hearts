using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Hearts
{
    public partial class frmInGame : Form
    {
        // Declare
        private Dictionary<PictureBox, Card> pictureBoxCardMap = new Dictionary<PictureBox, Card>();
        int currentPlayer;
        int player1 = 1;
        int player2 = 2;
        int player3 = 3;
        int humanPlayer = 4;


        public frmInGame()
        {
            InitializeComponent();
        }

        private async void frmInGame_Load(object sender, EventArgs e)
        {

            var hands = await StartGame();

            // Access each player's hand
            var player1Hand = hands.player1;
            var player2Hand = hands.player2;
            var player3Hand = hands.player3;
            var humanHand = hands.human;


            int trickCount = 1;
            int gameRound = 1;


            // Map each picture box to a card value
            pictureBoxCardMap[hmPicBox1] = humanHand[0];
            pictureBoxCardMap[hmPicBox2] = humanHand[1];
            pictureBoxCardMap[hmPicBox3] = humanHand[2];
            pictureBoxCardMap[hmPicBox4] = humanHand[3];
            pictureBoxCardMap[hmPicBox5] = humanHand[4];
            pictureBoxCardMap[hmPicBox6] = humanHand[5];
            pictureBoxCardMap[hmPicBox7] = humanHand[6];
            pictureBoxCardMap[hmPicBox8] = humanHand[7];
            pictureBoxCardMap[hmPicBox9] = humanHand[8];
            pictureBoxCardMap[hmPicBox10] = humanHand[9];
            pictureBoxCardMap[hmPicBox11] = humanHand[10];
            pictureBoxCardMap[hmPicBox12] = humanHand[11];
            pictureBoxCardMap[hmPicBox13] = humanHand[12];


            // Add click event handlers to each picture box
            hmPicBox1.Click += PictureBox_Click;
            hmPicBox2.Click += PictureBox_Click;
            hmPicBox3.Click += PictureBox_Click;
            hmPicBox4.Click += PictureBox_Click;
            hmPicBox5.Click += PictureBox_Click;
            hmPicBox6.Click += PictureBox_Click;
            hmPicBox7.Click += PictureBox_Click;
            hmPicBox8.Click += PictureBox_Click;
            hmPicBox9.Click += PictureBox_Click;
            hmPicBox10.Click += PictureBox_Click;
            hmPicBox11.Click += PictureBox_Click;
            hmPicBox12.Click += PictureBox_Click;
            hmPicBox13.Click += PictureBox_Click;



            /* ***************************************************** Trick 1 **************************************************** */
            var currentPlayer = DetermineFirstPlayer(player1Hand, player2Hand, player3Hand, humanHand);


            if (currentPlayer == 1)
            {
                player1Hand.RemoveAll(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                await Task.Delay(2000);
                this.Controls.Remove(player1PicBox13);
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\2C.gif"; 
                player1PicBoxPlayCard.Image = Image.FromFile(imagePath);
            }
            else if (currentPlayer == 2)
            {
                player2Hand.RemoveAll(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                await Task.Delay(2000);
                this.Controls.Remove(player2PicBox13);
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\2C.gif";
                player2PicBoxPlayCard.Image = Image.FromFile(imagePath);
            }
            else if (currentPlayer == 3)
            {
                player3Hand.RemoveAll(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                await Task.Delay(2000);
                this.Controls.Remove(player3PicBox13);
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\2C.gif";
                player3PicBoxPlayCard.Image = Image.FromFile(imagePath);
            }
            else if (currentPlayer == 4)
            {
                int index = humanHand.FindIndex(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
                humanHand.RemoveAt(index);

                await Task.Delay(2000);
                string controlName = "hmPicBox";
                string controlFullName = controlName + (index + 1).ToString();
                Controls.RemoveByKey(controlFullName);

                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\2C.gif";
                player4PicBoxPlayCard.Image = Image.FromFile(imagePath);
            }
            /* ********************************************************************************************************************** */

          
        }





        /* ****************************************************** Methods ******************************************************* */
        private async Task<(List<Card> player1, List<Card> player2, List<Card> player3, List<Card> human)> StartGame()
        {
            // Create a new deck object
            CardDeck deck = new CardDeck();
            deck.Shuffle();


            // Create a list for each player
            List<Card> player1 = new List<Card>();
            List<Card> player2 = new List<Card>();
            List<Card> player3 = new List<Card>();
            List<Card> human = new List<Card>();


            // Deal out the cards to each player
            for (int i = 0; i < 13; i++)
            {
                player1.Add(deck.Draw());
                player2.Add(deck.Draw());
                player3.Add(deck.Draw());
                human.Add(deck.Draw());
            }


            // Sort players' deck by value and suit
            SortDeck(player1);
            SortDeck(player2);
            SortDeck(player3);
            SortDeck(human);


            // Fill out the card slot of human player
            for (int i = 0; i < 13; i++)
            {
                Card card = human[i];
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\{deck.GetCardImage(card)}";
                PictureBox pictureBox = Controls.Find($"hmPicBox{i + 1}", true).FirstOrDefault() as PictureBox;
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
            return (player1, player2, player3, human);
        }




        private int DetermineFirstPlayer(List<Card> player1Hand, List<Card> player2Hand, List<Card> player3Hand, List<Card> humanHand)
        {
            bool player1HasTwoOfClubs = player1Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool player2HasTwoOfClubs = player2Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool player3HasTwoOfClubs = player3Hand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);
            bool humanHasTwoOfClubs = humanHand.Any(card => card.Suit == Suit.Clubs && card.Value == Value.Two);

            if (player1HasTwoOfClubs)
            {
                lblCurrentPlayer.Text = "Current Player: Player 1";

                return 1;
            }
            else if (player2HasTwoOfClubs)
            {
                lblCurrentPlayer.Text = "Current Player: Player 2";
                return 2;
            }
            else if (player3HasTwoOfClubs)
            {
                lblCurrentPlayer.Text = "Current Player: Player 3";
                return 3;
            }
            else if (humanHasTwoOfClubs)
            {
                lblCurrentPlayer.Text = "Current Player: You";
                return 4;
            }
            else
            {
                // None of the players have the Two of Clubs
                return -1;
            }
        }





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





        private async void FillOutBotCardSlot(int playerNumber)
        {
            for (int i = 13; i > 0; i--)
            {
                if (playerNumber == 2 || playerNumber == 4)
                {
                    string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\card-back-v.gif";
                    PictureBox pictureBox = Controls.Find($"player{playerNumber}PicBox{i + 1}", true).FirstOrDefault() as PictureBox;
                    if (pictureBox != null)
                    {
                        await Task.Delay(100);
                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                }
                else if (playerNumber == 1 || playerNumber == 3)
                {
                    string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\card-back-h.gif";
                    PictureBox pictureBox = Controls.Find($"player{playerNumber}PicBox{i + 1}", true).FirstOrDefault() as PictureBox;
                    if (pictureBox != null)
                    {
                        await Task.Delay(100);
                        pictureBox.Image = Image.FromFile(imagePath);
                    }
                }
            }
        }





        private void PictureBox_Click(object sender, EventArgs e)
        {
            // Get the clicked picture box and corresponding card value
            PictureBox pictureBox = sender as PictureBox;
            Card card = pictureBoxCardMap[pictureBox];


            MessageBox.Show($"Selected Card: {card.Value} of {card.Suit}");
        }
        /* ********************************************************************************************************************** */
    }
}

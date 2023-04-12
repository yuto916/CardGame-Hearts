using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hearts
{
    public partial class frmInGame : Form
    {
        public frmInGame()
        {
            InitializeComponent();
        }

        private void frmInGame_Load(object sender, EventArgs e)
        {

            StartGame();
        }


        /* ****************************************************** Methods  ****************************************************** */
        private void StartGame()
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
                    pictureBox.Image = Image.FromFile(imagePath);
                }
            }


            // Fill out the card slot of bot players
            FillOutBotCardSlot(2);




        }



        private void SortDeck(List<Card> deck)
        {
            deck.Sort((a, b) =>
            {
                if (a.Value != b.Value)
                {
                    return a.Value - b.Value;
                }
                else
                {
                    return a.Suit - b.Suit;
                }
            });
        }


        private void FillOutBotCardSlot(int playerNumber)
        {  
            for (int i = 0; i < 13; i++)
            {
                string imagePath = $"{Application.StartupPath}\\..\\..\\..\\img\\cards\\card-back-v.gif";
                PictureBox pictureBox = Controls.Find($"player{2}PicBox{i + 1}", true).FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.Image = Image.FromFile(imagePath);
                }
            }
        }
        /* ********************************************************************************************************************** */
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        List<List<char>> combinationsList = new List<List<char>>(); //contains all win combinations

        //if PC finds position which blocks other Player (if Player needs 1 more position to complete his
        //win combination and PC takes it), then this "win combination" has to be removed from
        //combinations
        List<List<char>> combinationsList_copy = new List<List<char>>(); //used for mode "1 Player" (vs PC)
        private void createCombinationsFor(List<List<char>> givenList)
        {
            List<char> row1 = new List<char> { '1', '2', '3' };
            List<char> row2 = new List<char> { '4', '5', '6' };
            List<char> row3 = new List<char> { '7', '8', '9' };
            List<char> col1 = new List<char> { '1', '4', '7' };
            List<char> col2 = new List<char> { '2', '5', '8' };
            List<char> col3 = new List<char> { '3', '6', '9' };
            List<char> diagonal1 = new List<char> { '1', '5', '9' };
            List<char> diagonal2 = new List<char> { '3', '5', '7' };

            givenList.Add(row1);
            givenList.Add(row2);
            givenList.Add(row3);
            givenList.Add(col1);
            givenList.Add(col2);
            givenList.Add(col3);
            givenList.Add(diagonal1);
            givenList.Add(diagonal2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createCombinationsFor(combinationsList);
            createCombinationsFor(combinationsList_copy);

            if (ModeWindow.Globals.selectedMode == "1 Player")
            {
                TextBoxPlayer2.Text = "PC";
                TextBoxPlayer2.Enabled = false;
            }
        }

        //contains all free positions
        List<char> positionsList = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        
        //change name of chosen button from empty ("") to "X" or "O"
        private void changeButtonTo(string currentTurn, char pos)
        {
            switch (pos)
            {
                case '1':
                    button1.Text = currentTurn;
                    break;
                case '2':
                    button2.Text = currentTurn;
                    break;
                case '3':
                    button3.Text = currentTurn;
                    break;
                case '4':
                    button4.Text = currentTurn;
                    break;
                case '5':
                    button5.Text = currentTurn;
                    break;
                case '6':
                    button6.Text = currentTurn;
                    break;
                case '7':
                    button7.Text = currentTurn;
                    break;
                case '8':
                    button8.Text = currentTurn;
                    break;
                case '9':
                    button9.Text = currentTurn;
                    break;
            }
        }
        private bool isListEmpty<T>(List<T> givenList)
        {
            return givenList.Count == 0;
        }

        bool isFoundSymbol = false;
        int positionIndex = 0;

        //generate position for PC in order to make PC winner or to block other Player
        private char getPositionForPC(List<char> givenList, List<List<char>> givenCombinations)
        {
            int same = 0; //save number of matching elements
            int miss = 0; //save number of elements that doesn't match
            char pos = ' '; //save the next position for PC
            isFoundSymbol = false;
            positionIndex = 0; //save index for position in list positions

            foreach (List<char> subList in givenCombinations)
            {
                same = 0;
                miss = 0;
                pos = ' ';
                foreach (char element in subList)
                {
                    if (givenList.Contains(element))
                    {
                        same++;
                    }
                    else
                    {
                        miss++;
                        if (positionsList.Contains(element))
                        {
                            pos = element;
                        }
                        else
                        {
                            pos = ' ';
                        }
                    }
                    if (miss == 2)
                    {
                        break;
                    }
                    if (same == 2 && miss == 1)
                    {
                        if (pos != ' ' && positionIndex != givenCombinations.Count - 1)
                        {
                            isFoundSymbol = true;
                        }
                        break;
                    }
                }
                if (isFoundSymbol)
                {
                    break;
                }
                positionIndex++;
            }
            return pos;
        }

        //generate random position for PC
        private char getRandomSymbol()
        {
            Random rand = new Random();
            int selected = rand.Next(positionsList.Count);

            char pos = positionsList[selected];

            return pos;
        }

        //check if one of the Players has winning combination
        private bool hasWinningCombinationIn(List<char> givenList)
        {
            int same;

            //always check in combinationsList (we remove elements only from combinationsList_copy)
            foreach (List<char> subList in combinationsList)
            {
                same = 0;
                foreach (char element in subList)
                {
                    if (givenList.Contains(element))
                    {
                        same++;
                        if (same == 3)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private bool hasWinnerIn(List<char> givenList)
        {
            if (givenList.Count >= 3)
            {
                if (hasWinningCombinationIn(givenList))
                {
                    return true;
                }
            }
            return false;
        }

        private void print(string str)
        {
            MessageBox.Show(str);
        }

        //if the game ends and we have free positions, we should forbid clicking on buttons 
        bool isButtonEnabled = true;
        private void displayResultsFor(List<char> givenList)
        {
            isButtonEnabled = false;
            if (givenList == playerOneList)
            {
                LabelWinnerName.Text = TextBoxPlayer1.Text;
                resultPlayer1++;
                LabelResultPlayer1.Text = resultPlayer1.ToString();
            }
            else if (givenList == playerTwoList)
            {
                LabelWinnerName.Text = TextBoxPlayer2.Text;
                resultPlayer2++;
                LabelResultPlayer2.Text = resultPlayer2.ToString();
            }
            LabelWinnerName.ForeColor = Color.Red;
        }

        List<char> playerOneList = new List<char>(); //contains taken positions from Player 1
        List<char> playerTwoList = new List<char>(); //contains taken positions from Player 2
        string turn = "X";

        int resultPlayer1 = 0; //saves record for Player 1
        int resultPlayer2 = 0; //saves record for Player 2

        bool isFoundWinner = false;
        bool canSaveResult = false;

        private void turnPlayer(string currentTurn, char pos)
        {
            changeButtonTo(currentTurn, pos);
            if (currentTurn == "X")
            {
                playerOneList.Add(pos); //Player 1 takes one more position from positionsList
                if (hasWinnerIn(playerOneList))
                {
                    isFoundWinner = true;
                    canSaveResult = true;
                    displayResultsFor(playerOneList);
                    return;
                }
            }
            else if (currentTurn == "O")
            {
                playerTwoList.Add(pos); //Player 2 takes one more position from positionsList
                if (hasWinnerIn(playerTwoList))
                {
                    isFoundWinner = true;
                    canSaveResult = true;
                    displayResultsFor(playerTwoList);
                    return;
                }
            }

            positionsList.Remove(pos); //taken position is removed from positionsList
            if (isListEmpty(positionsList))
            {
                isButtonEnabled = false;
                canSaveResult = true;
                LabelWinnerName.Text = "draw result";
                LabelWinnerName.ForeColor = Color.Red;
            }
        }

        private char getFinalPositionForPC()
        {
            char pos = ' '; //save position for PC

            //if PC has 2 or more taken positions, we need to search if PC can win in this turn
            if (playerTwoList.Count >= 2) 
            {
                //we search in PC's taken positions and in all win combinations
                pos = getPositionForPC(playerTwoList, combinationsList);
            }

            //if PC cannot win in this turn, if Player has 2 or more taken positions, we search if PC can block
            if (pos == ' ' && playerOneList.Count >= 2)
            {
                //we search in Player's taken positions and in the rest win combinations for Player 
                pos = getPositionForPC(playerOneList, combinationsList_copy);
                if (isFoundSymbol) //if position for PC is found (if pos is not ' ')
                {
                    combinationsList_copy.RemoveAt(positionIndex);
                }
            }

            //if PC doesn't have position to win or position to block, then PC can take one random position
            if (pos == ' ')
            {
                pos = getRandomSymbol();
            }
            return pos;
        }
        private void playWithPC(object sender)
        {
            if (isButtonEnabled)
            {
                var btn = (Button)sender; //recognise the clicked button
                char numberButton = btn.Name[btn.Name.Length - 1]; //take number of clicked button
                if (btn.Text == "")
                {
                    turnPlayer("X", numberButton);
                    if (!isFoundWinner && !isListEmpty(positionsList)) //if we still have free positions
                    {
                        char pos = getFinalPositionForPC();
                        turnPlayer("O", pos);
                    }
                }
            }
        }

        private void playWithOtherPlayer(object sender)
        {
            if (isButtonEnabled)
            {
                var btn = (Button)sender;
                char numberButton = btn.Name[btn.Name.Length - 1];
                if (btn.Text == "")
                {
                    if (turn == "X")
                    {
                        turnPlayer("X", numberButton);
                        turn = "O";
                    }
                    else if (turn == "O")
                    {
                        turnPlayer("O", numberButton);
                        turn = "X";
                    }
                    LabelXO.Text = turn;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ModeWindow.Globals.selectedMode == "1 Player")
            {
                playWithPC(sender);
            }
            else
            {
                playWithOtherPlayer(sender);
            }
        }

        private void activateGameButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        private void clearGameButtons()
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (TextBoxPlayer1.Text != "" && TextBoxPlayer2.Text != "")
            {
                if (TextBoxPlayer1.Text.Length > 10)
                {
                    print("Nickname of Player 1 should be less than 10 characters!");
                    TextBoxPlayer1.Focus();
                    return;
                }
                if (TextBoxPlayer2.Text.Length > 10)
                {
                    print("Nickname of Player 2 should be less than 10 characters!");
                    TextBoxPlayer2.Focus();
                    TextBoxPlayer2.Focus();
                    return;
                }
                if (TextBoxPlayer1.Text == TextBoxPlayer2.Text)
                {
                    print("Change one of the nicknames!");
                    TextBoxPlayer1.Focus();
                    return;
                }
                TextBoxPlayer1.Enabled = false;
                TextBoxPlayer2.Enabled = false;
                StartButton.Visible = false;
                AgainButton.Visible = true;
                RestartButton.Visible = true;
                activateGameButtons();
            }
            else if (TextBoxPlayer1.Text == "")
            {
                TextBoxPlayer1.Focus();
            }
            else if (TextBoxPlayer2.Text == "")
            {
                TextBoxPlayer2.Focus();
            }
        }

        void SaveDataTime()
        {
            for (int i = 0; i <= 300; i++)
            {
                Thread.Sleep(5);
            }
        }
        private void saveRecord()
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string path = startupPath + "\\Records.xls";
            Excel excel = new Excel(path, 1);
            string player1 = TextBoxPlayer1.Text;
            string player2 = TextBoxPlayer2.Text;
            string result = resultPlayer1 + ":" + resultPlayer2;
            WaitingWindow w = new WaitingWindow(SaveDataTime);
            w.ShowDialog(this);
            excel.findLastRow();
            int lastRow = excel.getLastRow();
            excel.WriteInCell(lastRow, 1, player1);
            excel.WriteInCell(lastRow, 2, player2);
            excel.WriteInCell(lastRow, 3, result);
            excel.Save();
            excel.Close();
            excel.Quit();
            excel.Release();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            if (canSaveResult || LabelResultPlayer1.Text != "0" || LabelResultPlayer2.Text != "0")
            {
                saveRecord();
                canSaveResult = false;
            }
            AgainButton_Click(sender, e);
            resultPlayer1 = 0;
            resultPlayer2 = 0;
            LabelResultPlayer1.Text = "0";
            LabelResultPlayer2.Text = "0";
        }

        private void AgainButton_Click(object sender, EventArgs e)
        {
            clearGameButtons();
            turn = "X";
            LabelXO.Text = "X";
            LabelWinnerName.Text = "(nobody)";
            LabelWinnerName.ForeColor = Color.Black;
            isButtonEnabled = true;
            isFoundWinner = false;
            canSaveResult = false;
            combinationsList_copy.Clear();
            createCombinationsFor(combinationsList_copy);
            if (!isListEmpty(positionsList))
            {
                positionsList.Clear();
            }
            for (int i = 49; i <= 57; i++)
            {
                positionsList.Add((char)i);
            }
            playerOneList.Clear();
            playerTwoList.Clear();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            ModeWindow m = new ModeWindow();
            m.ShowDialog();
            this.Close();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

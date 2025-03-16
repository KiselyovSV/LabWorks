using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SeaBattle
{
    public partial class PlFields : Form
    {

        private YouLost youLost = new YouLost();

        private const int mapSize = 11;

        private const int cellSize = 35;

        string letters = "AБВГДЕЖЗИК";

        public static int[,] myField = new int[mapSize, mapSize];

        public static int[,] enField = new int[mapSize, mapSize];

        private List<int> myShips = new List<int>(20) { 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 7 };

        private List<int> enShips = new List<int>(20) { 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 7 };

        private bool condition = false;

        private int quantity = 0;

        private Random random = new Random();

        private bool youShoot = false;

        string shipsOrient = "";

        private int myShots = 4;

        private int myShotsAgain = 4;

        private int enShots = 4;


        public PlFields()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Thread myThread = new Thread(this.CreateMyMap);
            myThread.Start();
            CreateEnMap();
            youLost.Activate();
            PerformLayout();
        }


        private void CreateEnMap()
        {
            Label enLabel = new Label();
            int enPanX = ((this.Width / 2 - cellSize * mapSize) / 2);
            // enPanel
            enPanel.Size = new Size(cellSize * mapSize, cellSize * mapSize);
            enPanel.Location = new Point(enPanX, 30);
            // enMap
            for (int i = 0, k = 0, f = 0; i < mapSize; i++, k += cellSize)
            {
                for (int j = 0, s = 0; j < mapSize; j++, s += cellSize)
                {
                    Button enBut = new();
                    SuspendLayout();
                    enBut.Location = new Point(k, s);
                    enBut.Name = $"enBut{i}{j}";
                    enBut.Size = new Size(cellSize, cellSize);
                    enBut.TabIndex = f++;
                    enBut.BackColor = Color.Aquamarine;
                    enBut.Tag = 0;
                    enBut.Click += new EventHandler(enBut_Click);
                    ButtonNamer(i, j, enBut);
                    enPanel.Controls.Add(enBut);
                    if (i != 0 && j != 0) enField[i, j] = 0;
                    else enField[i, j] = -1;
                    ResumeLayout(false);
                }
            }
            //enLabel
            enLabel.Size = new Size(300, 30);
            enLabel.Location = new Point(enPanX, 5);
            enLabel.Text = "ПОЛЕ ПРОТИВНИКА";
            this.Controls.Add(enLabel);
            ResumeLayout(false);
            textBox1.Text = $"РАСПОЛОЖИ СВОИ КОРАБЛИ НА ПОЛЕ БОЯ";
        }
        private void CreateMyMap()
        {
            Label myLabel = new Label();
            int enPanX = ((this.Width / 2 - cellSize * mapSize) / 2);
            int myPanX = enPanX + this.Width / 2;
            // myPanel
            myPanel.Size = new Size(cellSize * mapSize, cellSize * mapSize);
            myPanel.Location = new Point(myPanX, 30);
            // myMap
            for (int i = 0, k = 0, f = 120; i < mapSize; i++, k += cellSize)
            {
                for (int j = 0, s = 0; j < mapSize; j++, s += cellSize)
                {
                    Button myBut = new();
                    SuspendLayout();
                    myBut.Location = new Point(k, s);
                    myBut.Name = $"myBut{i}{j}";
                    myBut.Size = new Size(cellSize, cellSize);
                    myBut.TabIndex = f++;
                    myBut.BackColor = Color.Aquamarine;
                    myBut.Tag = 0;
                    myBut.Click += new EventHandler(myBut_Click);
                    ButtonNamer(i, j, myBut);
                    myPanel.Controls.Add(myBut);
                    if (i != 0 && j != 0) myField[i, j] = 0;
                    else myField[i, j] = -1;
                    ResumeLayout(false);
                }
            }
            //myLabel
            SuspendLayout();
            myLabel.Size = new Size(300, 30);
            myLabel.Location = new Point(myPanX, 5);
            myLabel.Text = "ПОЛЕ ИГРОКА";
            this.Controls.Add(myLabel);
        }
        // Name and numbering of coordinate fields
        private void ButtonNamer(int i, int j, Button but)
        {
            if (i == 0 && j > 0)
            {
                but.Text = j.ToString();
                but.BackColor = SystemColors.ScrollBar;
                but.FlatStyle = FlatStyle.Flat;
                but.Tag = -1;
            }
            else if (i > 0 && j == 0)
            {
                but.Text = letters[i - 1].ToString();
                but.BackColor = SystemColors.ScrollBar;
                but.FlatStyle = FlatStyle.Flat;
                but.Tag = -1;
            }
            else if (i == 0 && j == 0)
            {
                but.Text = "";
                but.BackColor = SystemColors.ScrollBar;
                but.FlatStyle = FlatStyle.Flat;
                but.Tag = -1;
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (Ship.Quantity1Deck > 0)
            {
                condition = true;
                Ship.Decks = 1;
                quantity = Ship.Quantity1Deck; 
                textBox1.Text = $"КОЛИЧЕСТВО: {Ship.Quantity1Deck}\n  ОБЩЕЕ КОЛИЧЕСТВО: {Total()}";
            }

        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (Ship.Quantity2Deck > 0)
            {
                condition = true;
                Ship.Decks = 2;
                quantity = Ship.Quantity2Deck;
                textBox1.Text = $"КОЛИЧЕСТВО: {Ship.Quantity2Deck}\n  ОБЩЕЕ КОЛИЧЕСТВО: {Total()}";
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (Ship.Quantity3Deck > 0)
            {
                condition = true;
                Ship.Decks = 3;
                quantity = Ship.Quantity3Deck;
                textBox1.Text = $"КОЛИЧЕСТВО: {Ship.Quantity3Deck}\n  ОБЩЕЕ КОЛИЧЕСТВО: {Total()}";
            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (Ship.Quantity4Deck > 0)
            {
                condition = true;
                Ship.Decks = 4;
                quantity = Ship.Quantity4Deck;
                textBox1.Text = $"КОЛИЧЕСТВО: {Ship.Quantity4Deck}\n  ОБЩЕЕ КОЛИЧЕСТВО: {Total()}";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Ship.Quantity4Deck == 0 && Ship.Quantity2Deck == 0 && Ship.Quantity3Deck == 0 && Ship.Quantity4Deck == 0)
            {
                int s = 0;
                foreach (int en in enField)
                {
                    if (en == 1) s++;
                }
                if (s != 20) ShipsStacker(enField, enPanel, "enBut", Color.Aquamarine);
                textBox1.Text = "ТВОЙ ХОД. У ТЕБЯ 4 ВЫСТРЕЛА";
                youShoot = true;
                startButton.Click -= StartButton_Click;
            }
        }

        private void enBut_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            int tag = Convert.ToInt32(but.Tag);
            if (tag != -1 && myShots != 0 && youShoot && Ship.Quantity4Deck == 0 && Ship.Quantity2Deck == 0 && Ship.Quantity3Deck == 0 && Ship.Quantity4Deck == 0)
            {
                if (Convert.ToInt32(but.Tag) > 0)
                {
                    but.BackColor = Color.Red;
                    switch (Convert.ToInt32(but.Tag))
                    {
                        case 1: enShips.Remove(1); if (!enShips.Contains(1)) enShots -= 1; break;
                        case 2: enShips.Remove(2); if (!enShips.Contains(2) && !enShips.Contains(3) && !enShips.Contains(4)) enShots -= 1; break;
                        case 3: enShips.Remove(3); if (!enShips.Contains(2) && !enShips.Contains(3) && !enShips.Contains(4)) enShots -= 1; break;
                        case 4: enShips.Remove(4); if (!enShips.Contains(2) && !enShips.Contains(3) && !enShips.Contains(4)) enShots -= 1; break;
                        case 5: enShips.Remove(5); if (!enShips.Contains(5) && !enShips.Contains(6)) enShots -= 1; break;
                        case 6: enShips.Remove(6); if (!enShips.Contains(5) && !enShips.Contains(6)) enShots -= 1; break;
                        case 7: enShips.Remove(7); if (!enShips.Contains(7)) enShots -= 1; break;

                    }
                    if (enShips.Count == 0)
                    {
                        try
                        {
                            youLost.BackColor = Color.Gold;
                            youLost.Controls["label1"].Text = "    ПОБЕДА!!!";
                            youLost.FormClosed += youLost_FormClosed;
                            youLost.Controls["LostButton"].Click += LostButton_Click;
                            youLost.Show();
                        }
                        catch (Exception ex)
                        {
                            youLost = new YouLost();
                            youLost.Activate();
                            youLost.BackColor = Color.Gold;
                            youLost.Controls["label1"].Text = "    ПОБЕДА!!!";
                            youLost.FormClosed += youLost_FormClosed;
                            youLost.Controls["LostButton"].Click += LostButton_Click;
                            youLost.Show();
                        }
                    }
                }
                else but.Text = "X";
                myShots--;
                switch (myShots)
                {
                    case 0: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛОВ"; break;
                    case 1: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛ"; break;
                    default: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛА"; break;
                }
            }
            if (myShots == 0)
            {
                textBox1.Text = "ХОД ПРОТИВНИКА";
                for (int f = 0; f < enShots;)
                {
                    Task task = new Task(()=> Thread.Sleep(2000));
                    task.Start();
                    task.Wait();
                    int i = random.Next(1, 11);
                    int j = random.Next(1, 11);
                    if (Convert.ToInt32(myPanel.Controls[$"myBut{i}{j}"].Tag) > 0 && myPanel.Controls[$"myBut{i}{j}"].BackColor != Color.Red)
                    {
                        myPanel.Controls[$"myBut{i}{j}"].BackColor = Color.Red;
                        f++;
                        switch (Convert.ToInt32(myPanel.Controls[$"myBut{i}{j}"].Tag))
                        {
                            case 1: myShips.Remove(1); if (!myShips.Contains(1)) myShotsAgain -= 1; break;
                            case 2: myShips.Remove(2); if (!myShips.Contains(2) && !myShips.Contains(3) && !myShips.Contains(4)) myShotsAgain -= 1; break;
                            case 3: myShips.Remove(3); if (!myShips.Contains(2) && !myShips.Contains(3) && !myShips.Contains(4)) myShotsAgain -= 1; break;
                            case 4: myShips.Remove(4); if (!myShips.Contains(2) && !myShips.Contains(3) && !myShips.Contains(4)) myShotsAgain -= 1; break;
                            case 5: myShips.Remove(5); if (!myShips.Contains(5) && !myShips.Contains(6)) myShotsAgain -= 1; break;
                            case 6: myShips.Remove(6); if (!myShips.Contains(5) && !myShips.Contains(6)) myShotsAgain -= 1; break;
                            case 7: myShips.Remove(7); if (!myShips.Contains(7)) myShotsAgain -= 1; break;

                        }
                        if (myShips.Count == 0)
                        {
                            try
                            {
                                youLost.BackColor = Color.Red;
                                youLost.Controls["label1"].Text = "ТЫ ПРОИГРАЛ!";
                                youLost.FormClosed += youLost_FormClosed;
                                youLost.Controls["LostButton"].Click += LostButton_Click;
                                youLost.Show();
                            }
                            catch (Exception ex)
                            {
                                youLost = new YouLost();
                                youLost.Activate();
                                youLost.BackColor = Color.Red;
                                youLost.Controls["label1"].Text = "ТЫ ПРОИГРАЛ!";
                                youLost.FormClosed += youLost_FormClosed;
                                youLost.Controls["LostButton"].Click += LostButton_Click;
                                youLost.Show();
                            }
                        }
                    }
                    else if (Convert.ToInt32(myPanel.Controls[$"myBut{i}{j}"].Tag) == 0 && myPanel.Controls[$"myBut{i}{j}"].Text != "X")
                    {
                        myPanel.Controls[$"myBut{i}{j}"].Text = "X";
                        f++;
                    }
                }
                myShots = myShotsAgain;
                switch (myShots)
                {
                    case 0: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛОВ"; break;
                    case 1: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛ"; break;
                    default: textBox1.Text = $"ТВОЙ ХОД. У ТЕБЯ {myShots} ВЫСТРЕЛА"; break;
                }
            }

        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            int k = 0;
            foreach (int m in myField)
            {
                if (m == 1) k = 1;
            }
            if (k != 1)
            {
                ShipsStacker(myField, myPanel, "myBut", Color.Black);
                ShipsStacker(enField, enPanel, "enBut", Color.Aquamarine);
                textBox1.Text = "НАЖМИ КНОПКУ \"СТАРТ\"";
                autoButton.Click -= AutoButton_Click;
                condition = false;
                quantity = 0;
                radioButton1.CheckedChanged -= RadioButton1_CheckedChanged;
                radioButton2.CheckedChanged -= RadioButton2_CheckedChanged;
                radioButton3.CheckedChanged -= RadioButton3_CheckedChanged;
                radioButton4.CheckedChanged -= RadioButton4_CheckedChanged;
            }

        }

        // Automatic placement of ships
        private void ShipsStacker(int[,] array, Panel panel, string str, Color color)
        {
            BuilderOf4Deck(array, panel, str, color);
            BuilderOf3Deck(array, panel, str, color);
            BuilderOf2Deck(array, panel, str, color);
            BuilderOf1Deck(array, panel, str, color);


        }

        // Manual placement of ships
        private void myBut_Click(object sender, EventArgs e)
        {

            if (youShoot == false && quantity != 0 || condition)
            {
                int tag = 0;
                switch (Ship.Decks)
                {
                    case 1: tag = 1; break;
                    case 2:
                        switch (quantity)
                        {
                            case 3: tag = 4; break;
                            case 2: tag = 3; break;
                            case 1: tag = 2; break;
                        }
                        break;
                    case 3:
                        switch (quantity)
                        {
                            case 2: tag = 6; break;
                            case 1: tag = 5; break;
                        }
                        break;
                    case 4: tag = 7; break;
                }
                Button but0 = sender as Button;
                int i = but0.Location.X / cellSize;
                int j = but0.Location.Y / cellSize;
                if (i != 0 && j != 0 && myField[i, j] != 2 && myField[i, j] != 1)
                {
                    but0.BackColor = Color.Black;
                    but0.Tag = tag;
                    myField[i, j] = 1;
                    if (shipsOrient == "j++" && (j == 10 && Ship.Decks > 1 || j == 9 && Ship.Decks > 2 || j == 8 && Ship.Decks > 3)) ShipRenderer(i, j, "j--", tag);
                    else if (shipsOrient == "i++" && (i == 10 && Ship.Decks > 1 || i == 9 && Ship.Decks > 2 || i == 8 && Ship.Decks > 3)) ShipRenderer(i, j, "i--", tag);
                    else ShipRenderer(i, j, shipsOrient, tag);
                    quantity--;
                }
                condition = false;
                switch (Ship.Decks)
                {
                    case 1:
                        Ship.Quantity1Deck -= 1;
                        break;
                    case 2:
                        Ship.Quantity2Deck -= 1;
                        break;
                    case 3:
                        Ship.Quantity3Deck -= 1;
                        break;
                    case 4:
                        Ship.Quantity4Deck -= 1;
                        break;
                }
                textBox1.Text = $"КОЛИЧЕСТВО: {quantity}\n  ОБЩЕЕ КОЛИЧЕСТВО: {Total()}";

                if (youShoot == false && Ship.Quantity1Deck == 0 && Ship.Quantity2Deck == 0 && Ship.Quantity3Deck == 0 && Ship.Quantity4Deck == 0)
                {
                    textBox1.Text = "НАЖМИ КНОПКУ \"СТАРТ\"";
                    condition = false;
                }
            }
        }
        // Ship renderer in manual placement of ships
        private void ShipRenderer(int i, int j, string incr, int tag)
        {
            for (int b = 1; b < Ship.Decks; b++)
            {
                switch (incr)
                {
                    case "i++": i++; break;
                    case "i--": i--; break;
                    case "j++": j++; break;
                    case "j--": j--; break;
                }

                int t = Convert.ToInt32(myPanel.Controls[$"myBut{i}{j}"].Tag);
                if (t > 0)
                {
                    for (int c = 1; c < Ship.Decks; c++)
                    {
                        switch (incr)
                        {
                            case "i++": i--; break;
                            case "i--": i++; break;
                            case "j++": j--; break;
                            case "j--": j++; break;
                        }
                        if (i > 10 || j > 10) break;
                        myPanel.Controls[$"myBut{i}{j}"].BackColor = Color.Aquamarine;
                        myPanel.Controls[$"myBut{i}{j}"].Tag = 0;
                        myField[i, j] = 0;
                    }
                    switch (Ship.Decks)
                    {
                        case 1:
                            Ship.Quantity1Deck += 1;
                            break;
                        case 2:
                            Ship.Quantity2Deck += 1;
                            break;
                        case 3:
                            Ship.Quantity3Deck += 1;
                            break;
                        case 4:
                            Ship.Quantity4Deck += 1;
                            break;
                    }
                    quantity++;
                    return;
                }
                myPanel.Controls[$"myBut{i}{j}"].BackColor = Color.Black;
                myPanel.Controls[$"myBut{i}{j}"].Tag = tag;
                myField[i, j] = 1;
            }
        }

        // Automatically set single-deck ships
        private void BuilderOf1Deck(int[,] mas, Panel panel, string s, Color color)
        {
            for (int l = 0; l < 4; l++)
            {
                while (true)
                {
                    int i = random.Next(1, 11);
                    int j = random.Next(1, 11);
                    if (mas[i, j] == 0)
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = color;
                        panel.Controls[$"{s}{i}{j}"].Tag = 1;
                        Fence(i, j, mas);
                        mas[i, j] = 1;
                        break;
                    }
                }
                if (Ship.Quantity2Deck > 0) Ship.Quantity1Deck -= 1;
            }
        }
        // Automatically set double-deck ships
        private void BuilderOf2Deck(int[,] mas, Panel panel, string s, Color color)
        {
            int i = 0;
            int j = 0;
            for (int n = 0, v = 2; n < 3; n++, v++)
            {
                while (true)
                {

                    while (true)
                    {
                        i = random.Next(1, 11);
                        j = random.Next(1, 11);
                        if (mas[i, j] == 0)
                        {
                            panel.Controls[$"{s}{i}{j}"].BackColor = color;
                            panel.Controls[$"{s}{i}{j}"].Tag = v;
                            break;
                        }
                    }

                    int[] mas1 = new int[] { i, j };
                    int k = mas1[random.Next(0, mas1.Length)];
                    int[] mas2 = new int[] { i - 1, i + 1 };
                    int i_ = mas2[random.Next(0, mas2.Length)];
                    int[] mas3 = new int[] { j - 1, j + 1 };
                    int j_ = mas3[random.Next(0, mas3.Length)];
                    if (k == i && k < 11 && j_ < 11 && mas[k, j_] == 0)
                    {
                        panel.Controls[$"{s}{k}{j_}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j_}"].Tag = v;
                        Fence(i, j, mas);
                        mas[i, j] = 1;
                        Fence(k, j_, mas);
                        mas[k, j_] = 1;
                        break;
                    }
                    else if (k == j && i_ < 11 && k < 11 && mas[i_, k] == 0)
                    {
                        panel.Controls[$"{s}{i_}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i_}{k}"].Tag = v;
                        Fence(i, j, mas);
                        mas[i, j] = 1;
                        Fence(i_, k, mas);
                        mas[i_, k] = 1;
                        break;
                    }
                    else
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{i}{j}"].Tag = 0;
                        mas[i, j] = 0;
                    }
                }
                if (Ship.Quantity2Deck > 0) Ship.Quantity2Deck -= 1;
            }
        }
        // Automatically set three-deck ships
        private void BuilderOf3Deck(int[,] mas, Panel panel, string s, Color color)
        {
            int i = 0;
            int j = 0;
            for (int n = 0, v = 5; n < 2; n++, v++)
            {
                while (true)
                {

                    while (true)
                    {
                        i = random.Next(1, 10);
                        j = random.Next(1, 10);
                        if (mas[i, j] == 0)
                        {
                            panel.Controls[$"{s}{i}{j}"].BackColor = color;
                            panel.Controls[$"{s}{i}{j}"].Tag = v;
                            break;
                        }
                    }

                    int[] mas1 = new int[] { i, j };
                    int k = mas1[random.Next(0, mas1.Length)];
                    int[] mas2 = new int[] { i - 1, i + 1 };
                    int i_ = mas2[random.Next(0, mas2.Length)];
                    int[] mas3 = new int[] { j - 1, j + 1 };
                    int j_ = mas3[random.Next(0, mas3.Length)];
                    if (k == i && mas[k, j_] == 0)
                    {
                        panel.Controls[$"{s}{k}{j_}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j_}"].Tag = v;
                        if (j - 2 > 0 && j_ == j - 1 && mas[k, j - 2] == 0)
                        {
                            panel.Controls[$"{s}{k}{j - 2}"].BackColor = color;
                            panel.Controls[$"{s}{k}{j - 2}"].Tag = v;
                            Fence(k, j - 2, mas);
                            mas[k, j - 2] = 1;
                        }
                        else if (j + 2 < 11 && j_ == j + 1 && mas[k, j + 2] == 0)
                        {
                            panel.Controls[$"{s}{k}{j + 2}"].BackColor = color;
                            panel.Controls[$"{s}{k}{j + 2}"].Tag = v;
                            Fence(k, j + 2, mas);
                            mas[k, j + 2] = 1;
                        }
                        else
                        {
                            panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                            panel.Controls[$"{s}{i}{j}"].Tag = 0;
                            mas[i, j] = 0;
                            panel.Controls[$"{s}{k}{j_}"].BackColor = Color.Aquamarine;
                            panel.Controls[$"{s}{k}{j_}"].Tag = 0;
                            mas[k, j_] = 0;
                            continue;
                        }
                        Fence(i, j, mas);
                        mas[i, j] = 1;
                        Fence(k, j_, mas);
                        mas[k, j_] = 1;
                        break;
                    }
                    else if (k == j && mas[i_, k] == 0)
                    {
                        panel.Controls[$"{s}{i_}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i_}{k}"].Tag = v;
                        if (i - 2 > 0 && i_ == i - 1 && mas[i - 2, k] == 0)
                        {
                            panel.Controls[$"{s}{i - 2}{k}"].BackColor = color;
                            panel.Controls[$"{s}{i - 2}{k}"].Tag = v;
                            Fence(i - 2, k, mas);
                            mas[i - 2, k] = 1;
                        }
                        else if (i + 2 < 11 && i_ == i + 1 && mas[i + 2, k] == 0)
                        {
                            panel.Controls[$"{s}{i + 2}{k}"].BackColor = color;
                            panel.Controls[$"{s}{i + 2}{k}"].Tag = v;
                            Fence(i + 2, k, mas);
                            mas[i + 2, k] = 1;
                        }
                        else
                        {
                            panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                            panel.Controls[$"{s}{i}{j}"].Tag = 0;
                            mas[i, j] = 0;
                            panel.Controls[$"{s}{i_}{k}"].BackColor = Color.Aquamarine;
                            panel.Controls[$"{s}{i_}{k}"].Tag = 0;
                            mas[i_, k] = 0;
                            continue;
                        }
                        Fence(i, j, mas);
                        mas[i, j] = 1;
                        Fence(i_, k, mas);
                        mas[i_, k] = 1;
                        break;
                    }
                    else
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{i}{j}"].Tag = 0;
                        mas[i, j] = 0;
                    }

                }
                if (Ship.Quantity2Deck > 0) Ship.Quantity3Deck -= 1;
            }
        }
        // Automatically set four-deck ships
        private void BuilderOf4Deck(int[,] mas, Panel panel, string s, Color color)
        {
            int i = 0;
            int j = 0;

            while (true)
            {

                while (true)
                {
                    i = random.Next(1, 10);
                    j = random.Next(1, 10);
                    if (mas[i, j] == 0)
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = color;
                        panel.Controls[$"{s}{i}{j}"].Tag = 7;
                        break;
                    }
                }

                int[] mas1 = new int[] { i, j };
                int k = mas1[random.Next(0, mas1.Length)];
                int[] mas2 = new int[] { i - 1, i + 1 };
                int i_ = mas2[random.Next(0, mas2.Length)];
                int[] mas3 = new int[] { j - 1, j + 1 };
                int j_ = mas3[random.Next(0, mas3.Length)];
                if (k == i && mas[k, j_] == 0)
                {
                    panel.Controls[$"{s}{k}{j_}"].BackColor = color;
                    panel.Controls[$"{s}{k}{j_}"].Tag = 7;
                    if (j - 3 > 0 && j_ == j - 1 && mas[k, j - 2] == 0)
                    {
                        panel.Controls[$"{s}{k}{j - 2}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j - 2}"].Tag = 7;
                        Fence(k, j - 2, mas);
                        mas[k, j - 2] = 1;
                        panel.Controls[$"{s}{k}{j - 3}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j - 3}"].Tag = 7;
                        Fence(k, j - 3, mas);
                        mas[k, j - 3] = 1;
                    }
                    else if (j + 3 < 11 && j_ == j + 1 && mas[k, j + 2] == 0)
                    {
                        panel.Controls[$"{s}{k}{j + 2}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j + 2}"].Tag = 7;
                        Fence(k, j + 2, mas);
                        mas[k, j + 2] = 1;
                        panel.Controls[$"{s}{k}{j + 3}"].BackColor = color;
                        panel.Controls[$"{s}{k}{j + 3}"].Tag = 7;
                        Fence(k, j + 3, mas);
                        mas[k, j + 3] = 1;
                    }
                    else
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{i}{j}"].Tag = 0;
                        mas[i, j] = 0;
                        panel.Controls[$"{s}{k}{j_}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{k}{j_}"].Tag = 0;
                        mas[k, j_] = 0;
                        continue;
                    }
                    Fence(i, j, mas);
                    mas[i, j] = 1;
                    Fence(k, j_, mas);
                    mas[k, j_] = 1;
                    break;
                }
                else if (k == j && mas[i_, k] == 0)
                {
                    panel.Controls[$"{s}{i_}{k}"].BackColor = color;
                    panel.Controls[$"{s}{i_}{k}"].Tag = 7;
                    if (i - 3 > 0 && i_ == i - 1 && mas[i - 2, k] == 0)
                    {
                        panel.Controls[$"{s}{i - 2}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i - 2}{k}"].Tag = 7;
                        Fence(i - 2, k, mas);
                        mas[i - 2, k] = 1;
                        panel.Controls[$"{s}{i - 3}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i - 3}{k}"].Tag = 7;
                        Fence(i - 3, k, mas);
                        mas[i - 3, k] = 1;
                    }
                    else if (i + 3 < 11 && i_ == i + 1 && mas[i + 2, k] == 0)
                    {
                        panel.Controls[$"{s}{i + 2}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i + 2}{k}"].Tag = 7;
                        Fence(i + 2, k, mas);
                        mas[i + 2, k] = 1;
                        panel.Controls[$"{s}{i + 3}{k}"].BackColor = color;
                        panel.Controls[$"{s}{i + 3}{k}"].Tag = 7;
                        Fence(i + 3, k, mas);
                        mas[i + 3, k] = 1;
                    }
                    else
                    {
                        panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{i}{j}"].Tag = 0;
                        mas[i, j] = 0;
                        panel.Controls[$"{s}{i_}{k}"].BackColor = Color.Aquamarine;
                        panel.Controls[$"{s}{i_}{k}"].Tag = 0;
                        mas[i_, k] = 0;
                        continue;
                    }
                    Fence(i, j, mas);
                    mas[i, j] = 1;
                    Fence(i_, k, mas);
                    mas[i_, k] = 1;
                    break;
                }
                else
                {
                    panel.Controls[$"{s}{i}{j}"].BackColor = Color.Aquamarine;
                    panel.Controls[$"{s}{i}{j}"].Tag = 0;
                    mas[i, j] = 0;
                }

            }
            if (Ship.Quantity2Deck > 0) Ship.Quantity4Deck -= 1;
        }
        // Circles ships with array elements with a value equal to 2
        private void Fence(int v, int z, int[,] mas)
        {
            for (int a = v - 1; a < v + 2; a++)
            {
                for (int b = z - 1; b < z + 2; b++)
                {
                    if (a > 10 || b > 10 || mas[a, b] == 2 || mas[a, b] == -1 || mas[a, b] == 1)
                        continue;
                    mas[a, b] = 2;
                }
            }
        }
        private void youLost_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void LostButton_Click(object sender, EventArgs e)
        {
            this.myShips = new List<int>(20) { 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 7 };

            this.enShips = new List<int>(20) { 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 7 };

            this.condition = false;

            this.quantity = 0;

            this.youShoot = false;

            this.myShots = 4;

            this.myShotsAgain = 4;

            this.enShots = 4;

            Ship.Quantity1Deck = 4;

            Ship.Quantity2Deck = 3;

            Ship.Quantity3Deck = 2;

            Ship.Quantity4Deck = 1;

            this.textBox1.Text = $"РАСПОЛОЖИ СВОИ КОРАБЛИ НА ПОЛЕ БОЯ";
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    myPanel.Controls[$"myBut{i}{j}"].BackColor = Color.Aquamarine;
                    myPanel.Controls[$"myBut{i}{j}"].Text = "";
                    myPanel.Controls[$"myBut{i}{j}"].Tag = 0;
                    myField[i, j] = 0;
                }
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    enPanel.Controls[$"enBut{i}{j}"].BackColor = Color.Aquamarine;
                    enPanel.Controls[$"enBut{i}{j}"].Text = "";
                    enPanel.Controls[$"enBut{i}{j}"].Tag = 0;
                    enField[i, j] = 0;
                }
            }
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;
            startButton.Click += StartButton_Click;
            autoButton.Click += AutoButton_Click;
            youLost.Hide();
        }

        private void VerticalBut_Click(object sender, EventArgs e)
        {
            shipsOrient = "j++";
        }

        private void HorizontalBut_Click(object sender, EventArgs e)
        {
            shipsOrient = "i++";
        }

        private int Total()
        {
            int x = Ship.Quantity1Deck + Ship.Quantity2Deck + Ship.Quantity3Deck + Ship.Quantity4Deck;
            return x;
        }
    }
}



   
















        

        



















    


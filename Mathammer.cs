using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mathammer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddArmies(); //SQL to load Army Names and Units
            FrontRadio1.Checked = true;
            FrontRadio2.Checked = true;
        }

        private void ArmyBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ArmyName = ArmyBox1.GetItemText(ArmyBox1.SelectedItem);
            AddUnits(ArmyName, 1);
        }


        private void ArmyBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ArmyName = ArmyBox2.GetItemText(ArmyBox2.SelectedItem);
            AddUnits(ArmyName, 2);
        }

        private void UnitBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ArmyName = ArmyBox1.GetItemText(ArmyBox1.SelectedItem);
            string UnitName = UnitBox1.GetItemText(UnitBox1.SelectedItem);
            AddCommand(ArmyName, UnitName, 1);
        }

        private void UnitBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ArmyName = ArmyBox2.GetItemText(ArmyBox2.SelectedItem);
            string UnitName = UnitBox2.GetItemText(UnitBox2.SelectedItem);
            AddCommand(ArmyName, UnitName, 2);
        }

        private void ShieldEnable1_CheckedChanged(object sender, EventArgs e)
        {
            CheckShield1();
        }

        private void ShieldEnable2_CheckedChanged(object sender, EventArgs e)
        {
            CheckShield2();
        }

        private void ChargeBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckCharge1();
        }

        private void ChargeBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckCharge2();
        }

        public void FightButton_Click(object sender, EventArgs e)
        {
            FightBox.Text = "";
            FightTime();
        }

        private void AddArmies()
        {
            string ArmyName = "";
            MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
            MyConnect.Open();
            MySqlCommand MyCommand = new MySqlCommand("SHOW TABLES from mathammer WHERE Tables_in_mathammer != 'armoury'", MyConnect);
            MySqlDataReader Reader = MyCommand.ExecuteReader();
            while (Reader.Read())
            {
                int Armies = Reader.VisibleFieldCount; //Counts Tables to limit for loops
                for (var x = 0; x < Armies; x++)
                {
                    ArmyName = Reader[x].ToString();
                    ArmyBox1.Items.Insert(x, ArmyName);
                    ArmyBox2.Items.Insert(x, ArmyName);
                    ArmyBox1.SelectedIndex = 0;
                    ArmyBox2.SelectedIndex = 0; 
                }
            }
            Reader.Close();
            MyConnect.Close();
            AddUnits(ArmyName,3);
        }


        private void AddUnits(string ArmyName, int SwitchCase)
        {
            switch (SwitchCase)
            {
                case 1:
                    UnitBox1.Items.Clear();
                    break;
                case 2:
                    UnitBox2.Items.Clear();
                    break;
                case 3:
                    UnitBox1.Items.Clear();
                    UnitBox2.Items.Clear();
                    break;
            }
            string UnitName = "";
            MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
            MyConnect.Open();
            MySqlCommand MyCommand = new MySqlCommand("SELECT UnitName From " + ArmyName, MyConnect);
            MySqlDataReader Reader = MyCommand.ExecuteReader();
            while (Reader.Read())
            {
                int Units = Reader.VisibleFieldCount;
                for (var x = 0; x < Units; x++)
                {
                    UnitName = Reader[x].ToString();
                    switch (SwitchCase)
                    { 
                        case 1:
                            UnitBox1.Items.Insert(x, UnitName);
                            UnitBox1.SelectedIndex = 0;
                            break;
                        case 2:
                            UnitBox2.Items.Insert(x, UnitName);
                            UnitBox2.SelectedIndex = 0;
                            break;
                        case 3:
                            UnitBox1.Items.Insert(x, UnitName);
                            UnitBox2.Items.Insert(x, UnitName);
                            UnitBox1.SelectedIndex = 0;
                            UnitBox2.SelectedIndex = 0;
                            break;
                    }
                }
            }
            Reader.Close();
            MyConnect.Close();
            AddCommand(ArmyName,UnitName,SwitchCase);
        }


        private void AddCommand(string ArmyName, string UnitName, int SwitchCase)
        {
            MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
            MyConnect.Open();
            MySqlCommand MyCommand = new MySqlCommand("SELECT ChampEnable, BannerEnable, MusicEnable, RankFiles FROM " + ArmyName + " WHERE UnitName = '" + UnitName + "'", MyConnect);
            MySqlDataReader Reader = MyCommand.ExecuteReader();
            while (Reader.Read())
            {
                string ChampEnable = Reader[0].ToString();
                string BannerEnable = Reader[1].ToString();
                string MusicEnable = Reader[2].ToString();
                string RankFiles = Reader[3].ToString();
                switch (SwitchCase)
                { 
                    case 1:
                        if (ChampEnable == "True") { ChampBox1.Enabled = true; }
                        else ChampBox1.Enabled = false;

                        if (BannerEnable == "True") { BannerBox1.Enabled = true; }
                        else BannerBox1.Enabled = false;

                        if (MusicEnable == "True") { MusicianBox1.Enabled = true; }
                        else MusicianBox1.Enabled = false;

                        if (RankFiles == "True") { RankBox1.Enabled = true; FileBox1.Enabled = true; }
                        else { RankBox1.Enabled = false; FileBox1.Enabled = false; RankBox1.Text = "1"; FileBox1.Text = "1"; }
                        break;

                    case 2:
                        if (ChampEnable == "True") { ChampBox2.Enabled = true; }
                        else ChampBox2.Enabled = false;

                        if (BannerEnable == "True") { BannerBox2.Enabled = true; }
                        else BannerBox2.Enabled = false;

                        if (MusicEnable == "True") { MusicianBox2.Enabled = true; }
                        else MusicianBox2.Enabled = false;

                        if (RankFiles == "True") { RankBox2.Enabled = true; FileBox2.Enabled = true; }
                        else { RankBox2.Enabled = false; FileBox2.Enabled = false; RankBox2.Text = "1"; FileBox2.Text = "1"; }
                        break;
                    
                    case 3:
                        if (ChampEnable == "True") { ChampBox1.Enabled = true; ChampBox2.Enabled = true; }
                        else { ChampBox1.Enabled = false; ChampBox2.Enabled = false; }

                        if (BannerEnable == "True") { BannerBox1.Enabled = true; BannerBox2.Enabled = true; }
                        else { BannerBox1.Enabled = false; BannerBox2.Enabled = false; }

                        if (MusicEnable == "True") { MusicianBox1.Enabled = true; MusicianBox2.Enabled = true; }
                        else { MusicianBox1.Enabled = false; MusicianBox2.Enabled = false; }

                        if (RankFiles == "True")
                        {
                            RankBox1.Enabled = true;
                            FileBox1.Enabled = true;
                            RankBox2.Enabled = true;
                            FileBox2.Enabled = true;
                        }
                        else
                        {
                            RankBox1.Enabled = false;
                            FileBox1.Enabled = false;
                            RankBox2.Enabled = false;
                            FileBox2.Enabled = false;
                            RankBox1.Text = "1"; 
                            FileBox1.Text = "1";
                            RankBox2.Text = "1"; 
                            FileBox2.Text = "1";
                        }
                        break;
                }
            }
            Reader.Close();
            MyConnect.Close();
            AddEquipment(UnitName, SwitchCase);
        }

        private void AddEquipment(string UnitName, int SwitchCase)
        { 
            MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
            MyConnect.Open();
            MySqlCommand MyCommand = new MySqlCommand("SELECT SpearEnable, DualEnable, HalberdEnable, GreatEnable, FlailEnable, LanceEnable, MorningEnable, ShieldEnable, LArmourEnable FROM armoury WHERE UnitName = '" + UnitName + "'", MyConnect);
            MySqlDataReader Reader = MyCommand.ExecuteReader();
            while (Reader.Read())
            {
                string SpearEnable = Reader[0].ToString();
                string DualEnable = Reader[1].ToString();
                string HalberdEnable = Reader[2].ToString();
                string GreatEnable = Reader[3].ToString();
                string FlailEnable = Reader[4].ToString();
                string LanceEnable = Reader[5].ToString();
                string MorningEnable = Reader[6].ToString();
                string ShieldEnable = Reader[7].ToString();
                string ArmourEnable = Reader[8].ToString();

                switch (SwitchCase)
                { 
                    case 1:
                        WeaponRadio1.Checked = true;
                        ShieldEnable1.Checked = false;
                        ArmourEnable1.Checked = false;

                        if (SpearEnable == "True") { SpearRadio1.Enabled = true; }
                        else { SpearRadio1.Enabled = false; }

                        if (DualEnable == "True") { DualRadio1.Enabled = true; }
                        else { DualRadio1.Enabled = false; }

                        if (HalberdEnable == "True") { HalberdRadio1.Enabled = true; }
                        else { HalberdRadio1.Enabled = false; }

                        if (GreatEnable == "True") { GreatRadio1.Enabled = true; }
                        else { GreatRadio1.Enabled = false; }

                        if (FlailEnable == "True") { FlailRadio1.Enabled = true; }
                        else { FlailRadio1.Enabled = false; }

                        if (LanceEnable == "True") { LanceRadio1.Enabled = true; }
                        else { LanceRadio1.Enabled = false; }

                        if (MorningEnable == "True") { MorningRadio1.Enabled = true; }
                        else { MorningRadio1.Enabled = false; }

                        if (ShieldEnable == "True") { ShieldEnable1.Enabled = true; }
                        else { ShieldEnable1.Enabled = false; }

                        if (ArmourEnable == "True") { ArmourEnable1.Enabled = true; }
                        else { ArmourEnable1.Enabled = false; }
                        break;

                    case 2:
                        WeaponRadio2.Checked = true;
                        ShieldEnable2.Checked = false;
                        ArmourEnable2.Checked = false;

                        if (SpearEnable == "True") { SpearRadio2.Enabled = true; }
                        else { SpearRadio2.Enabled = false; }

                        if (DualEnable == "True") { DualRadio2.Enabled = true; }
                        else { DualRadio2.Enabled = false; }

                        if (HalberdEnable == "True") { HalberdRadio2.Enabled = true; }
                        else { HalberdRadio2.Enabled = false; }

                        if (GreatEnable == "True") { GreatRadio2.Enabled = true; }
                        else { GreatRadio2.Enabled = false; }

                        if (FlailEnable == "True") { FlailRadio2.Enabled = true; }
                        else { FlailRadio2.Enabled = false; }

                        if (LanceEnable == "True") { LanceRadio2.Enabled = true; }
                        else { LanceRadio2.Enabled = false; }

                        if (MorningEnable == "True") { MorningRadio2.Enabled = true; }
                        else { MorningRadio2.Enabled = false; }

                        if (ShieldEnable == "True") { ShieldEnable2.Enabled = true; }
                        else { ShieldEnable2.Enabled = false; }

                        if (ArmourEnable == "True") { ArmourEnable2.Enabled = true; }
                        else { ArmourEnable2.Enabled = false; }
                        break;

                    case 3:
                        WeaponRadio1.Checked = true;
                        WeaponRadio2.Checked = true;
                        ShieldEnable1.Checked = false;
                        ArmourEnable1.Checked = false;
                        ShieldEnable2.Checked = false;
                        ArmourEnable2.Checked = false;

                        if (SpearEnable == "True") { SpearRadio1.Enabled = true; SpearRadio2.Enabled = true; }
                        else { SpearRadio1.Enabled = false; SpearRadio2.Enabled = false; }

                        if (DualEnable == "True") { DualRadio1.Enabled = true; DualRadio2.Enabled = true; }
                        else { DualRadio1.Enabled = false; DualRadio2.Enabled = false; }

                        if (HalberdEnable == "True") { HalberdRadio1.Enabled = true; HalberdRadio2.Enabled = true; }
                        else { HalberdRadio1.Enabled = false; HalberdRadio2.Enabled = false; }

                        if (GreatEnable == "True") { GreatRadio1.Enabled = true; GreatRadio2.Enabled = true; }
                        else { GreatRadio1.Enabled = false; GreatRadio2.Enabled = false; }

                        if (FlailEnable == "True") { FlailRadio1.Enabled = true; FlailRadio2.Enabled = true; }
                        else { FlailRadio1.Enabled = false; FlailRadio2.Enabled = false; }

                        if (LanceEnable == "True") { LanceRadio1.Enabled = true; LanceRadio2.Enabled = true; }
                        else { LanceRadio1.Enabled = false; LanceRadio2.Enabled = false; }

                        if (MorningEnable == "True") { MorningRadio1.Enabled = true; MorningRadio2.Enabled = true; }
                        else { MorningRadio1.Enabled = false; MorningRadio2.Enabled = false; }

                        if (ShieldEnable == "True") { ShieldEnable1.Enabled = true; ShieldEnable2.Enabled = true; }
                        else { ShieldEnable1.Enabled = false; ShieldEnable2.Enabled = false; }

                        if (ArmourEnable == "True") { ArmourEnable1.Enabled = true; ArmourEnable2.Enabled = true; }
                        else { ArmourEnable1.Enabled = false; ArmourEnable2.Enabled = false; }
                        break;
                }
            }
            Reader.Close();
            MyConnect.Close();
        }

        private void CheckShield1()
        {
            if (ShieldEnable1.Checked == true)
            {
                DualRadio1.Enabled = false;
                HalberdRadio1.Enabled = false;
                FlailRadio1.Enabled = false;
                GreatRadio1.Enabled = false;
                if (DualRadio1.Checked == true) { WeaponRadio1.Checked = true; }
                else if (HalberdRadio1.Checked == true) { WeaponRadio1.Checked = true; }
                else if (FlailRadio1.Checked == true) { WeaponRadio1.Checked = true; }
                else if (GreatRadio1.Checked == true) { WeaponRadio1.Checked = true; }
            }
            else if (ShieldEnable1.Checked == false)
            {
                string UnitName = "";
                UnitName = UnitBox1.SelectedItem.ToString();
                MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
                MyConnect.Open();
                MySqlCommand MyCommand = new MySqlCommand("SELECT DualEnable, HalberdEnable, FlailEnable, GreatEnable FROM armoury WHERE UnitName = '" + UnitName + "'", MyConnect);
                MySqlDataReader Reader = MyCommand.ExecuteReader();
                while (Reader.Read())
                {
                    string DualEnable = Reader[0].ToString();
                    string HalberdEnable = Reader[1].ToString();
                    string FlailEnable = Reader[2].ToString();
                    string GreatEnable = Reader[3].ToString();

                    if (DualEnable == "True") { DualRadio1.Enabled = true; }
                    if (HalberdEnable == "True") { HalberdRadio1.Enabled = true; }
                    if (FlailEnable == "True") { FlailRadio1.Enabled = true; }
                    if (GreatEnable == "True") { GreatRadio1.Enabled = true; }
                }
            }
        }

        private void CheckShield2()
        {
            if (ShieldEnable2.Checked == true)
            {
                DualRadio2.Enabled = false;
                HalberdRadio2.Enabled = false;
                FlailRadio2.Enabled = false;
                GreatRadio2.Enabled = false;
                if (DualRadio2.Checked == true) { WeaponRadio2.Checked = true; }
                else if (HalberdRadio2.Checked == true) { WeaponRadio2.Checked = true; }
                else if (FlailRadio2.Checked == true) { WeaponRadio2.Checked = true; }
                else if (GreatRadio2.Checked == true) { WeaponRadio2.Checked = true; }
            }
            else if (ShieldEnable2.Checked == false)
            {
                string UnitName = "";
                UnitName = UnitBox2.SelectedItem.ToString();
                MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
                MyConnect.Open();
                MySqlCommand MyCommand = new MySqlCommand("SELECT DualEnable, HalberdEnable, FlailEnable, GreatEnable FROM armoury WHERE UnitName = '" + UnitName + "'", MyConnect);
                MySqlDataReader Reader = MyCommand.ExecuteReader();
                while (Reader.Read())
                {
                    string DualEnable = Reader[0].ToString();
                    string HalberdEnable = Reader[1].ToString();
                    string FlailEnable = Reader[2].ToString();
                    string GreatEnable = Reader[3].ToString();

                    if (DualEnable == "True") { DualRadio2.Enabled = true; }
                    if (HalberdEnable == "True") { HalberdRadio2.Enabled = true; }
                    if (FlailEnable == "True") { FlailRadio2.Enabled = true; }
                    if (GreatEnable == "True") { GreatRadio2.Enabled = true; }
                }
            }
        }

        private void CheckCharge1()
        {
            if (ChargeBox1.Checked == true)
            {
                DownHillBox1.Enabled = true;
                if (ChargeBox2.Checked == true)
                {
                    ChargeBox2.Checked = false;
                    DownHillBox2.Checked = false;
                    DownHillBox2.Enabled = false;
                }
            }
            else if (ChargeBox1.Checked == false)
            {
                DownHillBox1.Checked = false;
                DownHillBox1.Enabled = false;
            }
        }

        private void CheckCharge2()
        {
            if (ChargeBox2.Checked == true)
            {
                DownHillBox2.Enabled = true;
                if (ChargeBox1.Checked == true)
                {
                    ChargeBox1.Checked = false;
                    DownHillBox1.Checked = false;
                    DownHillBox1.Enabled = false;
                }
            }
            else if (ChargeBox2.Checked == false)
            {
                DownHillBox2.Checked = false;
                DownHillBox2.Enabled = false;
            }
        }

        public int[] GetStats(int SwitchCase)
        { 
            int[] Stats = new int[11];
            string UnitName = "";
            string ArmyName = "";
            switch (SwitchCase)
            {
                case 1:
                    ArmyName = ArmyBox1.SelectedItem.ToString();
                    UnitName = UnitBox1.SelectedItem.ToString();
                    break;
                case 2:
                    ArmyName = ArmyBox2.SelectedItem.ToString();
                    UnitName = UnitBox2.SelectedItem.ToString();
                    break;
            }
            MySqlConnection MyConnect = new MySqlConnection("SERVER=localhost;DATABASE=mathammer;UID=root;PASSWORD=password;");
            MyConnect.Open();
            MySqlCommand MyCommand = new MySqlCommand("SELECT MinSize, Movement, WeaponSkill, BalisticSkill, Strength, Toughness, Wounds, Initative, Attacks, Leadership, ArmourValue FROM " + ArmyName + " WHERE UnitName = '" + UnitName + "'", MyConnect);
            MySqlDataReader Reader = MyCommand.ExecuteReader();
            while (Reader.Read())
            {
                int Columns = Reader.VisibleFieldCount;
                for (var x = 0; x < Columns; x++)
                {
                    Stats[x] = Reader.GetInt32(x);
                }
            }
            Reader.Close();
            MyConnect.Close();
            return Stats;
        }

        class Unit
        {
            public string Name;
            public void GetName(string YourName) { Name = YourName; }

            public int MinSize;
            public void GetSize(int YourSize) { MinSize = YourSize; }

            public int Movement;
            public void GetMove(int YourMove) { Movement = YourMove; }

            public int WeaponSkill;
            public void GetWS(int YourWS) { WeaponSkill = YourWS; }

            public int BalisticSkill;
            public void GetBS(int YourBS) { BalisticSkill = YourBS; }

            public int Strength;
            public void GetStrength(int YourStr) { Strength = YourStr; }

            public int Toughness;
            public void GetTough(int YourTough) { Toughness = YourTough; }

            public int Wounds;
            public void GetWounds(int YourWound) { Wounds = YourWound; }

            public int Initative;
            public void GetInit(int YourInit) { Initative = YourInit; }

            public int Attacks;
            public void GetAttacks(int YourAtk) { Attacks = YourAtk; }

            public int Leadership;
            public void GetLeadership(int YourLD) { Leadership = YourLD; }

            public int Armour;
            public void GetArmour(int YourArmour) { Armour = YourArmour; }

            public bool StrikeLast;
            public void GetLast(bool GreatWeapon) { StrikeLast = GreatWeapon; }
        }

        public void FightTime()
        {
            string Verdict = "";

            Unit BlueUnit = new Unit();
            int[] Stats = GetStats(1);
            BlueUnit.GetName(UnitBox1.SelectedItem.ToString());
            BlueUnit.GetSize(Stats[0]);
            BlueUnit.GetMove(Stats[1]);
            BlueUnit.GetWS(Stats[2]);
            BlueUnit.GetBS(Stats[3]);
            BlueUnit.GetStrength(Stats[4]);
            BlueUnit.GetTough(Stats[5]);
            BlueUnit.GetWounds(Stats[6]);
            BlueUnit.GetInit(Stats[7]);
            BlueUnit.GetAttacks(Stats[8]);
            BlueUnit.GetLeadership(Stats[9]);
            BlueUnit.GetArmour(Stats[10]);
            BlueUnit.GetLast(GreatRadio1.Checked);

            Unit RedUnit = new Unit();
            Stats = GetStats(2);
            RedUnit.GetName(UnitBox2.SelectedItem.ToString());
            RedUnit.GetSize(Stats[0]);
            RedUnit.GetMove(Stats[1]);
            RedUnit.GetWS(Stats[2]);
            RedUnit.GetBS(Stats[3]);
            RedUnit.GetStrength(Stats[4]);
            RedUnit.GetTough(Stats[5]);
            RedUnit.GetWounds(Stats[6]);
            RedUnit.GetInit(Stats[7]);
            RedUnit.GetAttacks(Stats[8]);
            RedUnit.GetLeadership(Stats[9]);
            RedUnit.GetArmour(Stats[10]);
            RedUnit.GetLast(GreatRadio2.Checked);

            if (RankBox1.Text == "" || FileBox1.Text == "")
            {
                RankBox1.Text = "1"; FileBox1.Text = "1";
                FightBox.Text = "Rank and File boxes must be filled.";
                return;
            }
            else if (RankBox2.Text == "" || FileBox2.Text == "")
            {
                RankBox2.Text = "1"; FileBox2.Text = "1";
                FightBox.Text = "Rank and File boxes must be filled.";
                return;
            }

            int BlueMaxSize = Int32.Parse(RankBox1.Text) * Int32.Parse(FileBox1.Text);
            int BluePoints = 0;
            int BlueTotalAttacks;

            int RedMaxSize = Int32.Parse(RankBox2.Text) * Int32.Parse(FileBox2.Text);
            int RedPoints = 0;
            int RedTotalAttacks;

            if (BlueUnit.MinSize > BlueMaxSize)
            {
                Verdict = "Blue Team's unit size is too small!" + Environment.NewLine + BlueUnit.Name + " total unit size must be " + BlueUnit.MinSize + " or higher!";
                FightBox.Text = Verdict;
                return;
            }
            else if (RedUnit.MinSize > RedMaxSize)
            {
                Verdict = "Red Team's unit size is too small!" + Environment.NewLine + RedUnit.Name + " total unit size must be " + RedUnit.MinSize + " or higher!";
                FightBox.Text = Verdict;
                return;
            }

            for (var x = 10; x > 0; x--)
            {
                if (BlueUnit.Initative == x && BlueUnit.StrikeLast == false && RedUnit.Initative == x && RedUnit.StrikeLast == false)
                {
                    Verdict = "- Initative Step " + x + " -" + Environment.NewLine + "The " + BlueUnit.Name + " and " + RedUnit.Name + " attack at the same time." + Environment.NewLine + Environment.NewLine;
                    FightBox.Text += Verdict;
                    BlueTotalAttacks = HowManyAttacks(BlueUnit.Name, BlueUnit.Attacks, BlueMaxSize, 1);
                    double BlueAttacks = RollToHit(BlueTotalAttacks, BlueUnit.WeaponSkill, RedUnit.WeaponSkill, 1);
                    double BlueWounds = RollToWound(BlueAttacks, BlueUnit.Strength, RedUnit.Toughness, 1);
                    double BlueKills = RollArmour(BlueWounds, BlueUnit.Strength, RedUnit.Armour, 1);
                    BlueKills = ParryRoll(1,BlueKills);
                    int BlueScore = Convert.ToInt32(BlueKills);
                    BluePoints += BlueScore;
                    if (BlueScore > 0) { Verdict = BlueScore + " " + RedUnit.Name + " have been killed. But they can fight back before dying!" + Environment.NewLine + Environment.NewLine; }
                    else { Verdict = "No " + RedUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                    FightBox.Text += Verdict;

                    RedTotalAttacks = HowManyAttacks(RedUnit.Name, RedUnit.Attacks, RedMaxSize, 2);
                    double RedAttacks = RollToHit(RedTotalAttacks, RedUnit.WeaponSkill, BlueUnit.WeaponSkill, 2);
                    double RedWounds = RollToWound(RedAttacks, RedUnit.Strength, BlueUnit.Toughness, 2);
                    double RedKills = RollArmour(RedWounds, RedUnit.Strength, BlueUnit.Armour, 2);
                    RedKills = ParryRoll(2, RedKills);
                    int RedScore = Convert.ToInt32(RedKills);
                    RedPoints += RedScore;
                    if (RedScore > 0) { Verdict = RedScore + " " + BlueUnit.Name + " have been killed. But they can fight back before dying!" + Environment.NewLine + Environment.NewLine; }
                    else { Verdict = "No " + BlueUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                    FightBox.Text += Verdict;

                    BlueMaxSize = BlueMaxSize - RedScore;
                    RedMaxSize = RedMaxSize - BlueScore;
                }
                else if (BlueUnit.Initative == x && BlueUnit.StrikeLast == false)
                {
                    Verdict = "- Initative Step " + x + " -" + Environment.NewLine + BlueUnit.Name + " attack." + Environment.NewLine + Environment.NewLine;
                    FightBox.Text += Verdict;
                    BlueTotalAttacks = HowManyAttacks(BlueUnit.Name, BlueUnit.Attacks, BlueMaxSize, 1);
                    double AttacksHit = RollToHit(BlueTotalAttacks, BlueUnit.WeaponSkill, RedUnit.WeaponSkill, 1);
                    double AttacksWound = RollToWound(AttacksHit, BlueUnit.Strength, RedUnit.Toughness, 1);
                    double Casualties = RollArmour(AttacksWound, BlueUnit.Strength, RedUnit.Armour, 1);
                    Casualties = ParryRoll(1, Casualties);
                    int Kills = Convert.ToInt32(Casualties);
                    BluePoints += Kills;
                    RedMaxSize = RedMaxSize - Kills;
                    if (Kills > 0) { Verdict = Kills + " " + RedUnit.Name + " have been killed. "; 
                        if (RedMaxSize <= 0) { Verdict += "None are alive to fight back!" + Environment.NewLine + Environment.NewLine;}
                        else { Verdict += "Leaving " + RedMaxSize + " left to fight on!" + Environment.NewLine + Environment.NewLine;} }
                    else { Verdict = "No " + RedUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                    FightBox.Text += Verdict;
                    
                }
                else if (RedUnit.Initative == x && RedUnit.StrikeLast == false)
                {
                    Verdict = "- Initative Step " + x + " -" + Environment.NewLine + RedUnit.Name + " attack." + Environment.NewLine + Environment.NewLine;
                    FightBox.Text += Verdict;
                    RedTotalAttacks = HowManyAttacks(RedUnit.Name, RedUnit.Attacks, RedMaxSize, 2);
                    double AttacksHit = RollToHit(RedTotalAttacks, RedUnit.WeaponSkill, BlueUnit.WeaponSkill, 2);
                    double AttacksWound = RollToWound(AttacksHit, RedUnit.Strength, BlueUnit.Toughness, 2);
                    double Casualties = RollArmour(AttacksWound, RedUnit.Strength, BlueUnit.Armour, 2);
                    Casualties = ParryRoll(2, Casualties);
                    int Kills = Convert.ToInt32(Casualties);
                    RedPoints += Kills;
                    BlueMaxSize = BlueMaxSize - Kills;
                    if (Kills > 0) { Verdict = Kills + " " + BlueUnit.Name + " have been killed. "; 
                        if (BlueMaxSize <= 0) { Verdict += "None are alive to fight back!" + Environment.NewLine + Environment.NewLine;}
                        else { Verdict += "Leaving " + BlueMaxSize + " left to fight on!" + Environment.NewLine + Environment.NewLine;} }
                    else { Verdict = "No " + BlueUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                    FightBox.Text += Verdict;
                    
                }
                else
                {
                    //Find something useful to put in here if no one is attacking.
                }

                //Check if units are destroyed
                if (BlueMaxSize <= 0)
                {
                    Verdict = "The " + BlueUnit.Name + " have been completely wiped out!";
                    FightBox.Text += Verdict;
                    return;
                }
                else if (RedMaxSize <= 0)
                {
                    Verdict = "The " + RedUnit.Name + " have been completely wiped out!";
                    FightBox.Text += Verdict;
                    return;
                }
                else if (BlueMaxSize <= 0 && RedMaxSize <= 0)
                {
                    Verdict = "Both units have wiped each other out!";
                    FightBox.Text += Verdict;
                    return;
                }
            }

            //Always Strike Last
            if (BlueUnit.StrikeLast == true && RedUnit.StrikeLast == true)
            {
                Verdict = "- Always Strike Last Step -" + Environment.NewLine + "The " + BlueUnit.Name + " and " + RedUnit.Name + " attack at the same time." + Environment.NewLine + Environment.NewLine;
                FightBox.Text += Verdict;
                BlueTotalAttacks = HowManyAttacks(BlueUnit.Name, BlueUnit.Attacks, BlueMaxSize, 1);
                double BlueAttacks = RollToHit(BlueTotalAttacks, BlueUnit.WeaponSkill, RedUnit.WeaponSkill, 1);
                double BlueWounds = RollToWound(BlueAttacks, BlueUnit.Strength, RedUnit.Toughness, 1);
                double BlueKills = RollArmour(BlueWounds, BlueUnit.Strength, RedUnit.Armour, 1);
                BlueKills = ParryRoll(1, BlueKills);
                int BlueScore = Convert.ToInt32(BlueKills);
                BluePoints += BlueScore;
                if (BlueScore > 0) { Verdict = BlueScore + " " + RedUnit.Name + " have been killed. But they can fight back before dying!" + Environment.NewLine + Environment.NewLine; }
                else { Verdict = "No " + RedUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                FightBox.Text += Verdict;

                RedTotalAttacks = HowManyAttacks(RedUnit.Name, RedUnit.Attacks, RedMaxSize, 2);
                double RedAttacks = RollToHit(RedTotalAttacks, RedUnit.WeaponSkill, BlueUnit.WeaponSkill, 2);
                double RedWounds = RollToWound(RedAttacks, RedUnit.Strength, BlueUnit.Toughness, 2);
                double RedKills = RollArmour(RedWounds, RedUnit.Strength, BlueUnit.Armour, 2);
                RedKills = ParryRoll(2, RedKills);
                int RedScore = Convert.ToInt32(RedKills);
                RedPoints += RedScore;
                if (RedScore > 0) { Verdict = RedScore + " " + BlueUnit.Name + " have been killed. But they can fight back before dying!" + Environment.NewLine + Environment.NewLine; }
                else { Verdict = "No " + BlueUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                FightBox.Text += Verdict;

                BlueMaxSize = BlueMaxSize - RedScore;
                RedMaxSize = RedMaxSize - BlueScore;
            }
            else if (BlueUnit.StrikeLast == true)
            {
                Verdict = "- Always Strike Last Step -" + Environment.NewLine + BlueUnit.Name + " attack." + Environment.NewLine + Environment.NewLine;
                FightBox.Text += Verdict;
                BlueTotalAttacks = HowManyAttacks(BlueUnit.Name, BlueUnit.Attacks, BlueMaxSize, 1);
                double AttacksHit = RollToHit(BlueTotalAttacks, BlueUnit.WeaponSkill, RedUnit.WeaponSkill, 1);
                double AttacksWound = RollToWound(AttacksHit, BlueUnit.Strength, RedUnit.Toughness, 1);
                double Casualties = RollArmour(AttacksWound, BlueUnit.Strength, RedUnit.Armour, 1);
                Casualties = ParryRoll(1, Casualties);
                int Kills = Convert.ToInt32(Casualties);
                BluePoints += Kills;
                RedMaxSize = RedMaxSize - Kills;
                if (Kills > 0)
                {
                    Verdict = Kills + " " + RedUnit.Name + " have been killed. ";
                    if (RedMaxSize <= 0) { Verdict += "None are alive to fight back!" + Environment.NewLine + Environment.NewLine; }
                    else { Verdict += "Leaving " + RedMaxSize + " left to fight on!" + Environment.NewLine + Environment.NewLine; }
                }
                else { Verdict = "No " + RedUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                FightBox.Text += Verdict;
            }
            else if (RedUnit.StrikeLast == true)
            {
                Verdict = "- Always Strike Last Step -" + Environment.NewLine + RedUnit.Name + " attack." + Environment.NewLine + Environment.NewLine;
                FightBox.Text += Verdict;
                RedTotalAttacks = HowManyAttacks(RedUnit.Name, RedUnit.Attacks, RedMaxSize, 2);
                double AttacksHit = RollToHit(RedTotalAttacks, RedUnit.WeaponSkill, BlueUnit.WeaponSkill, 2);
                double AttacksWound = RollToWound(AttacksHit, RedUnit.Strength, BlueUnit.Toughness, 2);
                double Casualties = RollArmour(AttacksWound, RedUnit.Strength, BlueUnit.Armour, 2);
                Casualties = ParryRoll(2, Casualties);
                int Kills = Convert.ToInt32(Casualties);
                RedPoints += Kills;
                BlueMaxSize = BlueMaxSize - Kills;
                if (Kills > 0)
                {
                    Verdict = Kills + " " + BlueUnit.Name + " have been killed. ";
                    if (BlueMaxSize <= 0) { Verdict += "None are alive to fight back!" + Environment.NewLine + Environment.NewLine; }
                    else { Verdict += "Leaving " + BlueMaxSize + " left to fight on!" + Environment.NewLine + Environment.NewLine; }
                }
                else { Verdict = "No " + BlueUnit.Name + " die!" + Environment.NewLine + Environment.NewLine; }
                FightBox.Text += Verdict;
            }

            //Check if units are wiped out last time before going to Combat Ress
            if (BlueMaxSize <= 0)
            {
                Verdict = "The " + BlueUnit.Name + " have been completely wiped out!";
                FightBox.Text += Verdict;
                return;
            }
            else if (RedMaxSize <= 0)
            {
                Verdict = "The " + RedUnit.Name + " have been completely wiped out!";
                FightBox.Text += Verdict;
                return;
            }
            else if (BlueMaxSize <= 0 && RedMaxSize <= 0)
            {
                Verdict = "Both units have wiped each other out!";
                FightBox.Text += Verdict;
                return;
            }

            Verdict = "- Combat Resolution -" + Environment.NewLine;
            FightBox.Text += Verdict;
            CombatResolution(BlueUnit.Name, RedUnit.Name, BluePoints, BlueMaxSize, RedPoints, RedMaxSize, BlueUnit.Leadership, RedUnit.Leadership);
        }

        public int HowManyAttacks(string UnitName, int UnitAttack, int MaxSize, int SwitchCase)
        {
            string Verdict = "";
            int Attacks = 0;
            int Support = 0;
            int Ranks = 0;
            int Files = 0;
            int MaxAttacks = 0;
            bool DualWeapons = false;
            bool Champion = false;
            bool flank = false;
            bool rear = false;

            switch (SwitchCase)
            { 
                case 1:
                    Ranks = Int32.Parse(RankBox1.Text);
                    Files = Int32.Parse(FileBox1.Text);
                    DualWeapons = DualRadio1.Checked;
                    Champion = ChampBox1.Checked;
                    flank = FlankRadio1.Checked;
                    rear = RearRadio1.Checked;
                    break;

                case 2:
                    Ranks = Int32.Parse(RankBox2.Text);
                    Files = Int32.Parse(FileBox2.Text);
                    DualWeapons = DualRadio2.Checked;
                    Champion = ChampBox2.Checked;
                    flank = FlankRadio2.Checked;
                    rear = RearRadio2.Checked;
                    break;
            }

            if (DualWeapons == true) { UnitAttack++; }

            if (flank == true)
            {
                int x = MaxSize % Ranks;
                if (x == 0) { Attacks = Ranks * UnitAttack; } else { Attacks = x * UnitAttack; }
                Support = 0;
            }
            else if (rear == true)
            {
                int x = MaxSize % Files;
                if (x == 0) { Attacks = Files * UnitAttack; } else { Attacks = x * UnitAttack; }
                Support = 0;
            }
            else if (Files >= MaxSize)
            {
                Attacks = MaxSize * UnitAttack;
            }
            else if (Files >= 10)
            {
                Attacks = Files * UnitAttack;
                int x = MaxSize / Files;
                if (x >= 3) { Support = Files * 2; }
                else if (x == 2) { x = MaxSize % Ranks; Support = (Files + x); }
                else { Support = MaxSize % Ranks; }
            }
            else
            {
                Attacks = Files * UnitAttack;
                int x = MaxSize / Files;
                if (x < 2) { Support = MaxSize % Files; } else { Support = Files; }
            }
            if (Champion == true) { Attacks++; }
            MaxAttacks = Attacks + Support;
            Verdict = "The " + UnitName + " have a total of " + MaxAttacks + " attacks. (" + Attacks + " normal, " + Support + " supporting)." +Environment.NewLine;
            FightBox.Text += Verdict;
            return MaxAttacks;
        }

        public double RollToHit(int TotalAttacks, int AttackSkill, int DefendSkill, int SwitchCase)
        {
            string Verdict = "";
            string NeedHit = "";
            double AttacksHit = 0;
            float ToHit = 0;

            if (DefendSkill > (AttackSkill * 2)) { ToHit = 0.333f; NeedHit = "5+"; }
            else if (AttackSkill > DefendSkill) { ToHit = 0.666f; NeedHit = "3+"; }
            else { ToHit = 0.5f; NeedHit = "4+";}

            AttacksHit = TotalAttacks * ToHit;
            AttacksHit = Math.Round(AttacksHit);

            Verdict = "Needing a " + NeedHit + " to hit, on average " + AttacksHit + " attack(s) hit." + Environment.NewLine;
            FightBox.Text += Verdict;
            return AttacksHit;
        }

        public double RollToWound(double TotalAttacks, int Strength, int Toughness, int SwitchCase)
        {
            string Verdict = "";
            string NeedWound = "";
            double AttacksWound = 0;
            float ToWound = 0;

            switch (SwitchCase)
            { 
                case 1:
                    if (HalberdRadio1.Checked == true) { Strength++; }
                    else if (GreatRadio1.Checked == true) { Strength = Strength + 2; }
                    else if (FlailRadio1.Checked == true) { Strength = Strength + 2; }
                    else if (MorningRadio1.Checked == true) { Strength++; }
                    else if (LanceRadio1.Checked == true && ChargeBox1.Checked == true) { Strength = Strength + 2; }
                    break;

                case 2:
                    if (HalberdRadio2.Checked == true) { Strength++; }
                    else if (GreatRadio2.Checked == true) { Strength = Strength + 2; }
                    else if (FlailRadio2.Checked == true) { Strength = Strength + 2; }
                    else if (MorningRadio2.Checked == true) { Strength++; }
                    else if (LanceRadio2.Checked == true && ChargeBox2.Checked == true) { Strength = Strength + 2; }
                    break;
            }

            if (Strength > (Toughness + 1)) { ToWound = 0.833f; NeedWound = "2+"; }
            else if (Strength > Toughness) { ToWound = 0.666f; NeedWound = "3+"; }
            else if (Strength == Toughness) { ToWound = 0.5f; NeedWound = "4+"; }
            else if (Strength < Toughness) { ToWound = 0.333f; NeedWound = "5+"; }
            else if ((Strength + 1) < Toughness) { ToWound = 0.166f; NeedWound = "6+"; }

            AttacksWound = TotalAttacks * ToWound;
            AttacksWound = Math.Round(AttacksWound);

            Verdict = "Needing a " + NeedWound + " to wound, on average " + AttacksWound + " wound(s) land." + Environment.NewLine;
            FightBox.Text += Verdict;
            return AttacksWound;
        }

        public double RollArmour(double TotalAttacks, int Strength, int Armour, int SwitchCase)
        {
            string Verdict = "";
            string ArmourSave = "";
            double FailedSaves = 0;
            float ToSave = 0;
            int ArmourValue = 0;

            switch (SwitchCase)
            { 
                case 1:
                    if (HalberdRadio1.Checked == true) { Strength++; }
                    else if (GreatRadio1.Checked == true) { Strength = Strength + 2; }
                    else if (FlailRadio1.Checked == true) { Strength = Strength + 2; }
                    else if (MorningRadio1.Checked == true) { Strength++; }
                    else if (LanceRadio1.Checked == true) { Strength = Strength + 2; }

                    if (ArmourEnable2.Checked == true) { Armour++; }
                    if (ShieldEnable2.Checked == true) { Armour++; }
                    break;

                case 2:
                    if (HalberdRadio2.Checked == true) { Strength++; }
                    else if (GreatRadio2.Checked == true) { Strength = Strength + 2; }
                    else if (FlailRadio2.Checked == true) { Strength = Strength + 2; }
                    else if (MorningRadio2.Checked == true) { Strength++; }
                    else if (LanceRadio2.Checked == true) { Strength = Strength + 2; }
                    
                    if (ArmourEnable1.Checked == true) { Armour++; }
                    if (ShieldEnable1.Checked == true) { Armour++; }
                    break;
            }

            ArmourValue = Armour - Strength;

            if (ArmourValue >= 6) { ToSave = 0.166f; ArmourSave = "1+"; }
            else if (ArmourValue == 5) { ToSave = 0.166f; ArmourSave = "2+"; }
            else if (ArmourValue == 4) { ToSave = 0.333f; ArmourSave = "3+"; }
            else if (ArmourValue == 3) { ToSave = 0.5f; ArmourSave = "4+"; }
            else if (ArmourValue == 2) { ToSave = 0.666f; ArmourSave = "5+"; }
            else if (ArmourValue == 1) { ToSave = 0.833f; ArmourSave = "6+"; }
            else { ToSave = 1f; ArmourSave = "None"; }

            FailedSaves = TotalAttacks * ToSave;
            FailedSaves = Math.Round(FailedSaves);

            if (ArmourSave == "None") { Verdict = "The enemy unit has no armour. They take on average " + FailedSaves + " unsaved wound(s)." + Environment.NewLine; }
            else { Verdict = "The enemy unit has a " + ArmourSave + " Armour Save. They take on average " + FailedSaves + " unsaved wound(s)." + Environment.NewLine; }

            FightBox.Text += Verdict;
            return FailedSaves;
        }

        public void CombatResolution(string BlueName, string RedName, int BlueKills, int BlueSize, int RedKills, int RedSize, int BlueLeadership, int RedLeadership)
        {
            string Verdict = "";
            int BlueTotal = 0;
            int RedTotal = 0;
            int BlueRanks = 0;
            int RedRanks = 0;
            int BlueBanner = 0;
            int RedBanner = 0;
            int BlueExtra = 0;
            int RedExtra = 0;
            int BlueMusic = 0;
            int RedMusic = 0;
            int BlueFiles = Convert.ToInt32(FileBox1.Text);
            int RedFiles = Convert.ToInt32(FileBox2.Text);
            int BlueCharge = 0;
            int RedCharge = 0;
            int BlueFlank = 0;
            int RedFlank = 0;

            //Check who is flanked
            if (RearRadio1.Checked == true) { BlueFlank = 2; }
            else if (FlankRadio1.Checked == true) { BlueFlank = 1; }

            if (RearRadio2.Checked == true) { RedFlank = 2; }
            else if (FlankRadio2.Checked == true) { RedFlank = 1; }

            //Check Ranks
            if (BlueFiles >= BlueSize) { BlueRanks = 0; }
            else
            {
                BlueRanks = BlueSize / BlueFiles;
                int y = BlueSize % BlueFiles;
                if (y >= 5) { BlueRanks++; }
                int x = BlueRanks - 1; //Front rank excluded.
                if (x > 3) { x = 3; } //Rank bonus caps at +3
                if (x < 0) { x = 0; }
                BlueExtra = x;
            }

            if (RedFiles >= RedSize) { RedRanks = 0; }
            else
            {
                RedRanks = RedSize / RedFiles;
                int y = RedSize % RedFiles;
                if (y >= 5) { RedRanks++; }
                int x = RedRanks - 1; //Front rank excluded.
                if (x > 3) { x = 3; } //Rank bonus caps at +3
                if (x < 0) { x = 0; }
                RedExtra = x;
            }

            //Check Disruption
            if (BlueFlank >= 1 && RedRanks >= 2) {BlueExtra = 0;} //Blue is disrupted
            else if (RedFlank >= 1 && BlueRanks >= 2) {RedExtra = 0;} //Red is disrupted

            //Check if BlueUnit has a Banner
            if (BannerBox1.Checked == true && ChampBox1.Checked == true && BlueSize >= 2) { BlueBanner++; }
            else if (BannerBox1.Checked == true && ChampBox1.Checked == false && BlueSize >= 1) { BlueBanner++; }

            //Check if RedUnit has a Banner
            if (BannerBox2.Checked == true && ChampBox2.Checked == true && RedSize >= 2) { RedBanner++; }
            else if (BannerBox2.Checked == true && ChampBox2.Checked == false && RedSize >= 1) { RedBanner++; }
        
            //Check if BlueUnit has a Musician
            if (MusicianBox1.Checked == true && BannerBox1.Checked == true && ChampBox1.Checked == true && BlueSize >= 3) { BlueMusic++; }
            else if (MusicianBox1.Checked == true && BannerBox1.Checked == true && ChampBox1.Checked == false && BlueSize >= 2) { BlueMusic++; }
            else if (MusicianBox1.Checked == true && BannerBox1.Checked == false && ChampBox1.Checked == true && BlueSize >= 2) { BlueMusic++; }
            else if (MusicianBox1.Checked == true && BannerBox1.Checked == false && ChampBox1.Checked == false && BlueSize >= 1) { BlueMusic++; }

            //Check if RedUnit has a Musician
            if (MusicianBox2.Checked == true && BannerBox2.Checked == true && ChampBox2.Checked == true && BlueSize >= 3) { RedMusic++; }
            else if (MusicianBox2.Checked == true && BannerBox2.Checked == true && ChampBox2.Checked == false && BlueSize >= 2) { RedMusic++; }
            else if (MusicianBox2.Checked == true && BannerBox2.Checked == false && ChampBox2.Checked == true && BlueSize >= 2) { RedMusic++; }
            else if (MusicianBox2.Checked == true && BannerBox2.Checked == false && ChampBox2.Checked == false && BlueSize >= 1) { RedMusic++; }

            //Check to see if a unit charged and if it went downhill
            if (ChargeBox1.Checked == true) { BlueCharge++; }
            if (DownHillBox1.Checked == true) { BlueCharge++; }
            if (ChargeBox2.Checked == true) { RedCharge++; }
            if (DownHillBox2.Checked == true) { RedCharge++; }

            BlueTotal = BlueKills + BlueExtra + BlueBanner + BlueCharge + RedFlank;
            RedTotal = RedKills + RedExtra + RedBanner + RedCharge + BlueFlank;
            int Score = 0;

            Verdict = "The " + BlueName + " inflicted " + BlueKills + " casualties, ";
            if (BlueFlank >= 1 && RedRanks >= 2) { Verdict += "do not gain a rank bonus due to disruption, "; }
            else {Verdict += "have a +" + BlueExtra + " rank bonus, "; }
            if (BlueCharge == 2) { Verdict += " gained an additional +2 due to successfuly charging downhill, "; }
            else if (BlueCharge == 1) { Verdict += " gained an additional +1 due to successfuly charging, "; }
            else { Verdict += "did not make any charges, "; }
            if (RedFlank == 2) { Verdict += "gained +2 for fighting in the enemy's rear, "; }
            else if (RedFlank == 1) { Verdict += "gained +1 for fighting in the enemy's flank, "; }
            if (BlueBanner == 1) { Verdict += "and have an additional +1 due to having a Banner."; }
            else { Verdict += "and the unit does not have a Banner."; }
            Verdict +=  Environment.NewLine + "They have a Combat Resolution of " +BlueTotal+ "!" + Environment.NewLine + Environment.NewLine;
            FightBox.Text += Verdict;

            Verdict = "The " + RedName + " inflicted " + RedKills + " casualties, ";
            if (RedFlank >= 1 && BlueRanks >= 2) { Verdict += "do not gain a rank bonus due to disruption, "; }
            else {Verdict += "have a +" + RedExtra + " rank bonus, "; }
            if (RedCharge == 2) { Verdict += " gained an additional +2 due to successfuly charging downhill, "; }
            else if (RedCharge == 1) { Verdict += " gained an additional +1 due to successfuly charging, "; }
            else { Verdict += "did not make any charges, "; }
            if (BlueFlank == 2) { Verdict += "gained +2 for fighting in the enemy's rear, "; }
            else if (BlueFlank == 1) { Verdict += "gained +1 for fighting in the enemy's flank, "; }
            if (RedBanner == 1) { Verdict += "and have an additional +1 due to having a Banner."; }
            else {Verdict += "and the unit does not have a Banner.";}
            Verdict +=  Environment.NewLine + "They have a Combat Resolution of " +RedTotal+ "!" + Environment.NewLine + Environment.NewLine;
            FightBox.Text += Verdict;

            if (BlueTotal > RedTotal) 
            {
                Score = BlueTotal - RedTotal;
                Verdict = "The " +BlueName+ " have won combat by " + Score + " points!";
                FightBox.Text += Verdict;
                BreakCheck(RedName, Score, RedLeadership, BlueRanks, RedRanks, 2);
            }
            else if (RedTotal > BlueTotal) 
            { 
                Score = RedTotal - BlueTotal;
                Verdict = "The " +RedName+ " have won combat by " + Score + " points!";
                FightBox.Text += Verdict;
                BreakCheck(BlueName, Score, BlueLeadership, BlueRanks, RedRanks, 1);
            }
            else 
            { 
                if(BlueMusic == 1 && RedMusic == 1) {Verdict = "Both units have a Musician. The combat is a Draw!";}
                else if(BlueMusic == 1 && RedMusic == 0) 
                {
                    Verdict = BlueName + " have a Musician, they have won the combat!"; Score++;
                    BreakCheck(RedName, Score, RedLeadership, BlueRanks, RedRanks, 2);
                }
                else if(BlueMusic == 0 && RedMusic == 1) 
                {
                    Verdict = RedName + " have a Musician, they have won the combat!"; Score++;
                    BreakCheck(BlueName, Score, BlueLeadership, BlueRanks, RedRanks, 1);
                }
                else {Verdict = "Both units do not have a Musician. The combat is a draw!";}
                FightBox.Text += Verdict;
            }
        }

        public void BreakCheck(string Name, int Score, int Leadership, int BlueRanks, int RedRanks, int SwitchCase)
        {
            bool SteadFast = true;
            string Verdict;

            switch (SwitchCase)
            { 
                case 1:
                    if (RedRanks > BlueRanks) { SteadFast = false; }
                    else { SteadFast = true; }
                    break;

                case 2:
                    if (BlueRanks > RedRanks) { SteadFast = false; }
                    else { SteadFast = true; }
                    break;
            }

            if (SteadFast == false)
            {
                Leadership = Leadership - Score;
                if (Leadership < 2) { Leadership = 2; } //Insane Courage
                Verdict = "The " + Name + " are not steadfast. They have a Leadership of " + Leadership + "!";
            }
            else
            {
                Verdict = "The " + Name + " have more ranks, and are steadfast. They roll on their normal Leadership of " + Leadership + "!";
            }

            FightBox.Text += Environment.NewLine + Verdict;
        }

        public double ParryRoll(int SwitchCase, double Wounds)
        {
            string Verdict;
            float Parry = 0.833f;
            string ToParry = "6+";

            switch (SwitchCase)
            { 
                case 1:
                    if (WeaponRadio2.Checked == true && ShieldEnable2.Checked == true && FrontRadio2.Checked == true)
                    {
                        Wounds = Wounds * Parry;
                        Wounds = Math.Round(Wounds);
                        Verdict = "The enemy unit also has a " + ToParry + " Parry Save. They take on average " + Wounds + " unsaved wound(s)" + Environment.NewLine;
                        FightBox.Text += Verdict;
                    }
                    break;

                case 2:
                    if (WeaponRadio1.Checked == true && ShieldEnable1.Checked == true && FrontRadio1.Checked == true)
                    {
                        Wounds = Wounds * Parry;
                        Wounds = Math.Round(Wounds);
                        Verdict = "The enemy unit also has a " + ToParry + " Parry Save. They take on average " + Wounds + " unsaved wound(s)" + Environment.NewLine;
                        FightBox.Text += Verdict;
                        return Wounds;
                    }
                    break;
            }
            return Wounds;
        }

    }
}

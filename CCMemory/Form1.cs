using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CCMemory
{
    public partial class Form1 : Form
    {
        public const int BlipWavToStateOffset = -0xA48; // Bytes from the marker to the beginning of game state.
        public const int DataOffset = -0x4458;          // Bytes from beginning of game state to the beginning of the data segment.
        public const int TeleportListOffset = -0x2C218;
        public const int MonsterListOffset = -0xF1D0;
        public const int ToggleListOffset = -0xF098;

        static readonly byte[] Marker = { 0x62, 0x6C, 0x69, 0x70, 0x32, 0x2E, 0x77, 0x61, 0x76, 0x00, 0x00 };
        static readonly IntPtr NotFound = new IntPtr(-1);

        readonly ChipsState chipsState = new ChipsState();
        readonly Monster[] monsterList = new Monster[1024];
        Process process;
        IntPtr chipsStateAddress = NotFound;
        IntPtr monsterListAddress = NotFound;

        public Form1()
        {
            InitializeComponent();
            propertyGrid.SelectedObject = chipsState;
            monstersPropertyGrid.SelectedObject = monsterList;
        }

        private void FindBlip2Marker()
        {
            var address = ProcessMemory.Find(process, Marker);
            if (address != NotFound)
            {
                chipsStateAddress = address + BlipWavToStateOffset;
                Log($"Success! Start address = {chipsStateAddress.ToInt32():X}\r\n" +
                    $"data offset = {(chipsStateAddress + DataOffset).ToInt32():X}\r\n" +
                    $"-0xF160 = {(chipsStateAddress + MonsterListOffset).ToInt32():X}");
                return;
            }
            else
            {
                chipsStateAddress = NotFound;
            }
        }

        private void Hook()
        {
            process = Process.GetProcessesByName(processTextBox.Text).FirstOrDefault();
            if (process != null && !process.HasExited)
            {
                FindBlip2Marker();
            }
        }

        private bool IsHooked()
        {
            return process != null && !process.HasExited && chipsStateAddress != NotFound;
        }

        private void Read()
        {
            var success = ProcessMemory.ReadChipsState(process, chipsStateAddress, chipsState);
            if (!success)
            {
                Log("Error encountered when reading");
            }
        }

        private void ReadMonsters()
        {
            if (monsterListAddress != NotFound)
            {
                var success = ProcessMemory.ReadMonsterList(process, monsterListAddress, monsterList, chipsState.MonsterList.Cap);
                if (!success)
                {
                    monsterListAddress = NotFound;
                    Log("Could not read monster list");
                }
            }
        }

        private void SoftRefreshPropertyGrid(PropertyGrid propertyGrid)
        {
            if (propertyGrid.GetType().GetField("peMain", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(propertyGrid) is GridItem peMain)
            {
                var refreshMethod = peMain.GetType().GetMethod("Refresh");
                if (refreshMethod != null)
                {
                    refreshMethod.Invoke(peMain, null);
                    propertyGrid.Invalidate(true);
                }
            }
        }

        private void Log(string text)
        {
            logTextBox.AppendText(text + Environment.NewLine);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (!IsHooked())
                return;

            var success = ProcessMemory.WriteChipsState(process, chipsStateAddress, chipsState);
            if (!success)
            {
                Log($"Error encountered when writing");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!IsHooked())
            {
                propertyGrid.Enabled = false;
                timer.Interval = 1000;
                Log("Looking for Chip's Challenge process...");
                Hook();
            }
            if (IsHooked())
            {
                propertyGrid.Enabled = true;
                timer.Interval = 125;
                Read();
                ReadMonsters();
                if (autoRefreshCheckBox.Checked)
                {
                    SoftRefreshPropertyGrid(propertyGrid);
                    SoftRefreshPropertyGrid(monstersPropertyGrid);
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            propertyGrid.Refresh();
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                monsterListAddress = new IntPtr(Convert.ToInt64(textBox1.Text, 16));
            }
            catch (FormatException) { }
        }

        private void MonstersPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (!IsHooked())
                return;

            var success = ProcessMemory.WriteMonsterList(process, monsterListAddress, monsterList, chipsState.MonsterList.Length);
            if (!success)
            {
                Log($"Error encountered when writing");
            }
        }
    }
}

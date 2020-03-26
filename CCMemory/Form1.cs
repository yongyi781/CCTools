using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CCMemory
{
    public partial class Form1 : Form
    {
        public const int BlipWavToStateOffset = -0xA48;
        public const int DataOffset = -0x4458;
        public const int TeleportListOffset = -0x2C218;
        public const int MonsterListOffset = -0xF160;
        public const int ToggleListOffset = -0xF098;

        static readonly byte[] Marker = { 0x62, 0x6C, 0x69, 0x70, 0x32, 0x2E, 0x77, 0x61, 0x76, 0x00, 0x00 };
        readonly ChipsState chipsState = new ChipsState();
        Process process;
        IntPtr chipsStateAddress;

        public Form1()
        {
            InitializeComponent();
            propertyGrid.SelectedObject = chipsState;
        }

        private int IndexOfSubsequence(byte[] seq, byte[] subseq)
        {
            bool IsMatch(int offset)
            {
                for (int j = 0; j < subseq.Length; j++)
                    if (seq[j + offset] != subseq[j])
                        return false;
                return true;
            }

            for (int i = 0; i <= seq.Length - subseq.Length; i++)
                if (IsMatch(i))
                    return i;
            return -1;
        }

        private void FindBlip2Marker()
        {
            const int BUFFER_SIZE = 0x1000;
            var buffer = new byte[BUFFER_SIZE];
            for (IntPtr address = (IntPtr)0x1000000; (int)address < 0x5000000; address += BUFFER_SIZE - Marker.Length)
            {
                if (NativeMethods.ReadProcessMemory(process.Handle, address, buffer, BUFFER_SIZE, out IntPtr _))
                {
                    var i = IndexOfSubsequence(buffer, Marker);
                    if (i != -1)
                    {
                        chipsStateAddress = address + i + BlipWavToStateOffset;
                        Log($"Success! Start address = {chipsStateAddress.ToInt32():X}\r\n" +
                            $"data offset = {(chipsStateAddress + DataOffset).ToInt32():X}\r\n" +
                            $"-0xF160 = {(chipsStateAddress + MonsterListOffset).ToInt32():X}");
                        return;
                    }
                }
            }
            chipsStateAddress = IntPtr.Zero;
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
            return process != null && !process.HasExited && chipsStateAddress != IntPtr.Zero;
        }

        private void Read()
        {
            var success = NativeMethods.ReadProcessMemory(process.Handle, chipsStateAddress, chipsState, Marshal.SizeOf<ChipsState>(), out _);
            if (!success)
            {
                Log("Error encountered when reading");
            }
        }

        private void SoftRefreshPropertyGrid()
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

            var success = NativeMethods.WriteProcessMemory(process.Handle, chipsStateAddress, chipsState, Marshal.SizeOf<ChipsState>(), out IntPtr bytesWritten);
            if (success)
            {
                logTextBox.AppendText($"Successfully wrote {bytesWritten} bytes\r\n");
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
                if (autoRefreshCheckBox.Checked)
                    SoftRefreshPropertyGrid();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            propertyGrid.Refresh();
        }
    }
}

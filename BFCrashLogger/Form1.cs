using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BFCrashLogger
{
    public partial class Form1 : Form
    {
        private const string ProcessName = "";
        private const string FautltWindowTitle = "";
        private const int TimerInterval = 10000;

        private Timer _monitortimer;
        private bool _curState;

        public Form1()
        {
            InitializeComponent();
            _monitortimer = new Timer() {Interval = TimerInterval};
            _monitortimer.Tick += CheckProcess;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetInitialState();
        }

        private void CheckProcess(object sender, EventArgs e)
        {
            var procList = Process.GetProcessesByName(ProcessName);
            var process = procList.FirstOrDefault();

            SetState(process);
        }

        public void CheckForCrash(Process process)
        {
            if (process == null)
                return;

            if (process.Responding) return;
            
            OutputMessage("Process not responding. ");
            process.Kill();
            OutputMessage(process.WaitForExit(20000) ? "Process killed." : "Process not killed.");

            FindWerFault();
        }

        public void FindWerFault()
        {
            var werFault = Process.GetProcessesByName("WerFault");
            var werFaultProcess = werFault.FirstOrDefault(x => x.MainWindowTitle == FautltWindowTitle);

            if (werFaultProcess == null)
                return;

            OutputMessage("Killing fault window. ");
            werFaultProcess.Kill();
            OutputMessage(werFaultProcess.WaitForExit(20000) ? "Fault window killed." : "Fault window not killed.");
        }

        private void SetInitialState()
        {
            var procList = Process.GetProcessesByName(ProcessName);
            var process = procList.FirstOrDefault();

            _curState = (process != null);

            OutputMessage(_curState ? "Game is running." : "Game is not running.");
        }

        private void SetState(Process process)
        {
            var tempState = (process != null);

            if(tempState != _curState)
                OutputMessage("Game is running.");
            else
                OutputMessage("Game is stopped.");

            _curState = tempState;
        }

        private void OutputMessage(string message)
        {
            throw new NotImplementedException();
        }
    }

    
}

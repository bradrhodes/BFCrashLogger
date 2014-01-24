using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BFCrashLogger
{
    public partial class Form1 : Form
    {
        private readonly LogMessage _logMessage;
        private const string ProcessName = "";
        private const string FautltWindowTitle = "";
        private const int TimerInterval = 10000;

        private Timer _monitortimer;
        private bool _curState;

        public Form1(LogMessage logMessage)
        {
            if (logMessage == null) throw new ArgumentNullException("logMessage");
            _logMessage = logMessage;

            InitializeComponent();
            _monitortimer = new Timer() {Interval = TimerInterval};
            _monitortimer.Tick += CheckProcess;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupBindings();

            Log(MessageIds.SessionStart, "", "Session started.");
            SetInitialState();
        }

        private void SetupBindings()
        {
            listBox1.DisplayMember = "TextMessage";
            listBox1.ValueMember = "TextMessage";
            listBox1.DataSource = _logMessage.Log;
        }

        private void CheckProcess(object sender, EventArgs e)
        {
            var procList = Process.GetProcessesByName(ProcessName);
            var process = procList.FirstOrDefault();

            SetState(process);
            CheckForCrash(process);
        }

        public void CheckForCrash(Process process)
        {
            if (process == null)
                return;

            if (process.Responding)
            {
                FindFaultFirst(process);
                return;
            }
            
            Log(MessageIds.BfStoppedResponding, "", "BF stopped responding. ");
            process.Kill();
            if (process.WaitForExit(20000))
                Log(MessageIds.BfKilled, true.ToString(), "BF killed.");
            else
                Log(MessageIds.BfKilled, false.ToString(), "BF not killed.");

            FindWerFault();
        }

        public void FindWerFault()
        {
            var werFault = Process.GetProcessesByName("WerFault");
            var werFaultProcess = werFault.FirstOrDefault(x => x.MainWindowTitle == FautltWindowTitle);

            if (werFaultProcess == null)
                return;

            werFaultProcess.Kill();
            if (werFaultProcess.WaitForExit(20000))
                Log(MessageIds.BfFaultWindowKilled, true.ToString(), "Fault window killed.");
            else
                Log(MessageIds.BfFaultWindowKilled, false.ToString(), "Fault window not killed.");
        }

        public void FindFaultFirst(Process process)
        {
            var werFault = Process.GetProcessesByName("WerFault");
            var werFaultProcess = werFault.FirstOrDefault(x => x.MainWindowTitle == FautltWindowTitle);

            if (werFaultProcess == null)
                return;

            werFaultProcess.Kill();
            if (werFaultProcess.WaitForExit(20000))
                Log(MessageIds.BfFaultWindowKilledFirst , true.ToString(), "Fault window found first and killed.");
            else
                Log(MessageIds.BfFaultWindowKilled, false.ToString(), "Fault window found first but not killed.");

            process.Kill();
            if (process.WaitForExit(20000))
                Log(MessageIds.BfKilledAfter, true.ToString(), "BF killed after fault window.");
            else
                Log(MessageIds.BfKilledAfter, false.ToString(), "BF not killed after fault window.");
        }

        private void SetInitialState()
        {
            var procList = Process.GetProcessesByName(ProcessName);
            var process = procList.FirstOrDefault();

            _curState = (process != null);

            if (_curState)
                Log(MessageIds.BfStateChange, true.ToString(), "Game is running.");
            else
                Log(MessageIds.BfStateChange, false.ToString(), "Game is not running.");
        }

        private void SetState(Process process)
        {
            var tempState = (process != null);

            if(tempState != _curState)
                Log(MessageIds.BfStateChange, true.ToString(), "Game is running.");
            else
                Log(MessageIds.BfStateChange, true.ToString(), "Game is stopped.");

            _curState = tempState;
        }

        private void Log(int messageId, string messageValue, string textMessage)
        {
            var timeStamp = DateTime.UtcNow;
            _logMessage.Log.Add(new LoggerData(timeStamp, messageId, messageValue, textMessage));
        }

        private void reportCrash_Click(object sender, EventArgs e)
        {
            using (var unhandledForm = new UnhandledForm())
            {
                var result = unhandledForm.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                Log(MessageIds.BfCrashUnhandled, unhandledForm.ErrorDescription, "Unhandled error reported.");
            }
        }

        private void viewData_Click(object sender, EventArgs e)
        {
            var serializedData = JsonConvert.SerializeObject(_logMessage, Formatting.Indented);
            using (var displayForm = new DisplayDataForm(serializedData))
            {
                displayForm.ShowDialog();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class LoggerData
    {
        public LoggerData(DateTime timestamp, int messageId, string messageValue = "", string textMessage = "")
        {
            TimeStamp = timestamp;
            MessageId = messageId;
            MessageValue = messageValue;
            TextMessage = textMessage;
        }

        public DateTime TimeStamp { get; private set; }
        public int MessageId { get; private set; }
        public string MessageValue { get; private set; }
        [JsonIgnore]
        public string TextMessage { get; private set; }
    }

    public class LogMessage
    {
        public LogMessage(Guid installId, Guid sessionId)
        {
            Log = new BindingList<LoggerData>();
            InstallId = installId;
            SessionId = sessionId;

            OsVersion = Environment.OSVersion.VersionString;
            OsServicePack = Environment.OSVersion.ServicePack;
        }

        public Guid InstallId { get; private set; }
        public Guid SessionId { get; private set; }
        public BindingList<LoggerData> Log { get; private set; }
        public string OsVersion { get; private set; }
        public string OsServicePack { get; private set; }
    }

    public class Settings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        public Guid InstallId
        {
            get
            {
                if (this["InstallId"] == null)
                {
                    var newGuid = Guid.NewGuid();
                    this.InstallId = newGuid;
                    this.Save();
                    return newGuid;
                }
                return (Guid) this["InstallId"];
            }
            private set { this["InstallId"] = (Guid) value; }
        }
    }

    public static class MessageIds
    {
        public const int SessionStart = 01;
        public const int BfStateChange = 10;
        public const int BfStoppedResponding = 20;
        public const int BfKilled = 30;
        public const int BfKilledAfter = 30;
        public const int BfFaultWindowKilled = 35;
        public const int BfFaultWindowKilledFirst = 36;
        public const int BfCrashUnhandled = 40;
        public const int SessionEnd = 99;
    }
}

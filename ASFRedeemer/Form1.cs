using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASFRedeemer
{
    [ServiceContract]
    internal interface IWCF
    {
        [OperationContract]
        string GetStatus();

        [OperationContract]
        string HandleCommand(string input);
    }

    public partial class Form1 : Form
    {
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        private static readonly string ExecutableFile = Assembly.Location;
        private static readonly string ExecutableDirectory = Path.GetDirectoryName(ExecutableFile);

        private Client Client;

        private string WCFHost = "localhost";
        private string WCFPort = "1242";

        public Form1()
        {
            InitializeComponent();
        }

        private void ResponseRedeem()
        {
            richTextBox_result.Clear();
            button_redeem.Enabled = false;

            var checkedRButton = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            string input = checkedRButton.Tag.ToString();
            if (checkBox_botname.Checked) { input += " " + comboBox_botname.Text; }
            Regex steamkey_Regex = new Regex("([A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5})");
            MatchCollection steamkey_Matches = steamkey_Regex.Matches(richTextBox_keys.Text);
            if (steamkey_Matches.Count > 0)
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < steamkey_Matches.Count; i++)
                {
                    keys.Add(steamkey_Matches[i].Groups[1].Value);
                }
                input += " " + string.Join(",", keys.ToArray());

                if (Client == null)
                {
                    Client = new Client(
                        new NetTcpBinding
                        //new BasicHttpBinding
                        {
                            // We use SecurityMode.None for Mono compatibility
                            // Yes, also on Windows, for Mono<->Windows communication
                            Security = { Mode = SecurityMode.None },
                            //Security = { Mode = BasicHttpSecurityMode.None },
                            SendTimeout = new TimeSpan(1, 0, 0)
                        },
                        new EndpointAddress("net.tcp://" + WCFHost + ":" + WCFPort + "/ASF")
                    //new EndpointAddress("http://" + WCFHost + ":" + WCFPort + "/ASF")
                    );
                }

                richTextBox_result.Text = Client.HandleCommand(input);

                listView_result.FindItemWithText("Total").SubItems[1].Text = Regex.Matches(richTextBox_result.Text, "Status: ").Count.ToString();
                listView_result.FindItemWithText("OK").SubItems[1].Text = Regex.Matches(richTextBox_result.Text, "Status: OK").Count.ToString();
                listView_result.FindItemWithText("AlreadyOwned").SubItems[1].Text = Regex.Matches(richTextBox_result.Text, "Status: AlreadyOwned").Count.ToString();
                listView_result.FindItemWithText("DuplicatedKey").SubItems[1].Text = Regex.Matches(richTextBox_result.Text, "Status: DuplicatedKey").Count.ToString();
            }
            else { MessageBox.Show("No keys..."); }

            button_redeem.Enabled = true;
        }

        private async void button_redeem_Click(object sender, EventArgs e)
        {
            await Task.Run(new Action(ResponseRedeem));
        }

        private void richTextBox_keys_TextChanged(object sender, EventArgs e)
        {
            Regex steamkey_Regex = new Regex("([A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5})");
            MatchCollection steamkey_Matches = steamkey_Regex.Matches(richTextBox_keys.Text);
            label_found_keys.Text = "Found keys: " + steamkey_Matches.Count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cfg_path = Path.Combine(ExecutableDirectory, "config");
            if (Directory.Exists(cfg_path))
            {
                string[] botPaths = Directory.GetFiles(cfg_path, "*.json");
                foreach (string botPath in botPaths)
                {
                    string botname = Path.GetFileNameWithoutExtension(botPath);
                    if (botname.Equals("ASF")) { continue; }
                    comboBox_botname.Items.Add(botname);
                }
                comboBox_botname.SelectedIndex = 0;

                JObject ASFcfg = JObject.Parse(File.ReadAllText(Path.Combine(cfg_path, "ASF.json")));
                WCFHost = (string)ASFcfg["WCFHost"];
                WCFPort = (string)ASFcfg["WCFPort"];
            }
            else
            {
                checkBox_botname.Enabled = false;
                comboBox_botname.Enabled = false;
            }
        }
    }

    internal sealed class Client : ClientBase<IWCF>
    {
        internal Client(System.ServiceModel.Channels.Binding binding, EndpointAddress address) : base(binding, address) { }

        internal string HandleCommand(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "input data is null!";
            }

            try
            {
                return Channel.HandleCommand(input);
            }
            catch (Exception e)
            {
                return "HandleCommand return error:\r\n" + e.Message;
            }
        }
    }
}

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

namespace ASFWCFClient
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

        private Client client;
        private Config config = new Config();

        private string WCFHost = "127.0.0.1";
        private string WCFPort = "1242";

        public Form1()
        {
            InitializeComponent();
        }

        internal string SendCommand(string input)
        {
            if (client == null)
            {
                client = new Client(
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

            string result = client.HandleCommand(input);

            StopClient();

            return result;
        }

        private void StopClient()
        {
            if (client == null) { return; }
            if (client.State != CommunicationState.Closed) { client.Close(); }
            client = null;
        }

        private void ResponseExecute()
        {
            if(string.IsNullOrEmpty(comboBox_command.Text)) { MessageBox.Show("Command is empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            richTextBox_result.Clear();
            button_execute.Enabled = false;

            string command = comboBox_command.Text;
            string[] selectedBots = { };
            string input = command;

            if (command.Contains("<BOT>") && listView_botname.SelectedItems.Count > 0)
            {
                selectedBots = listView_botname.SelectedItems.Cast<ListViewItem>().Select(item => item.Text).ToArray();
                //input = input.Replace("<BOT>", string.Join(",", listView_botname.SelectedItems.Cast<ListViewItem>().Select(item => item.Text).ToArray()));
            }

            if (!string.IsNullOrEmpty(textBox_args.Text) && textBox_args.Text != "Arguments...")
            {
                input += " " + textBox_args.Text;
            }

            if (selectedBots.Length > 0 && input.Contains("<BOT>"))
            {
                foreach (string botName in selectedBots)
                {
                    string res = SendCommand(input.Replace("<BOT>", botName)).Trim();
                    richTextBox_result.Text += ((res.Contains(botName)) ? "" : "<" + botName + "> ") + res.Trim() + "\r\n";
                    //richTextBox_result.Text += input.Replace("<BOT>", botName);
                    if(numericUpDown_delay.Value > 0) { Thread.Sleep((int)numericUpDown_delay.Value * 1000); }
                }
            }
            else
            {
                richTextBox_result.Text = SendCommand(input).Trim();
                //richTextBox_result.Text = input;
            }

            File.AppendAllText(Path.GetFileNameWithoutExtension(ExecutableFile) + ".log", richTextBox_result.Text + "\r\n**********\r\n");

            button_execute.Enabled = true;
        }

        private async void button_execute_Click(object sender, EventArgs e)
        {
            richTextBox_result.ForeColor = Color.LimeGreen;

            config.Save();

            await Task.Run(new Action(ResponseExecute));
        }

        private void textBox_args_Enter(object sender, EventArgs e)
        {
            if(textBox_args.Text == "Arguments...")
            {
                textBox_args.Text = "";
                textBox_args.ForeColor = SystemColors.WindowText;
                textBox_args.Font = new Font(textBox_args.Font, FontStyle.Regular);
            }
        }

        private void textBox_args_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_args.Text))
            {
                textBox_args.Text = "Arguments...";
                textBox_args.ForeColor = Color.Gray;
                textBox_args.Font = new Font(textBox_args.Font, FontStyle.Italic);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_botname.Items)
            {
                item.Selected = true;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listView_botname.SelectedIndices.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox_result.Font = new Font(FontFamily.GenericMonospace, richTextBox_result.Font.Size, richTextBox_result.Font.Style);

            string cfg_file = Path.Combine(ExecutableDirectory, Path.GetFileNameWithoutExtension(ExecutableFile) + ".json");
            
            if (!File.Exists(cfg_file)) { File.WriteAllText(cfg_file, JsonConvert.SerializeObject(config, Formatting.Indented)); }
            config = Config.Load(cfg_file);

            comboBox_command.Items.AddRange(config.Commands.ToArray());

            string asf_cfg_dir = Path.Combine(ExecutableDirectory, "config");
            if (Directory.Exists(asf_cfg_dir))
            {
                string[] botPaths = Directory.GetFiles(asf_cfg_dir, "*.json");
                foreach (string botPath in botPaths)
                {
                    string botname = Path.GetFileNameWithoutExtension(botPath);
                    if (botname.Equals("ASF")) { continue; }
                    listView_botname.Items.Add(new ListViewItem(botname, listView_botname.Groups[0]));
                }

                JObject ASFcfg = JObject.Parse(File.ReadAllText(Path.Combine(asf_cfg_dir, "ASF.json")));
                WCFHost = (string)ASFcfg["WCFHost"];
                WCFPort = (string)ASFcfg["WCFPort"];
            }
            else
            {
                //checkBox_botname.Enabled = false;
                //comboBox_botname.Enabled = false;
            }

            File.WriteAllText(Path.GetFileNameWithoutExtension(ExecutableFile) + ".log", "********** " + DateTime.Now + " **********\r\n");

            string checkASF = SendCommand("version");
            if(string.IsNullOrEmpty(checkASF) || checkASF.Contains("HandleCommand return error:")) { richTextBox_result.ForeColor = Color.Red; richTextBox_result.Text = "********** ASF Server ERROR! **********\r\nWCFHost: " + WCFHost + "\r\nWCFPort: " + WCFPort + "\r\n\r\nError message:\r\n\r\n" + checkASF; }
            else { richTextBox_result.Text = "********** ASF Server OK: " + checkASF + " **********"; }
        }

        private void textBox_search_in_res_Enter(object sender, EventArgs e)
        {
            if (textBox_search_in_res.Text == "Search...")
            {
                textBox_search_in_res.Text = "";
                textBox_search_in_res.ForeColor = SystemColors.WindowText;
                textBox_search_in_res.Font = new Font(textBox_search_in_res.Font, FontStyle.Regular);
            }
        }

        private void textBox_search_in_res_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_search_in_res.Text))
            {
                textBox_search_in_res.Text = "Search...";
                textBox_search_in_res.ForeColor = Color.Gray;
                textBox_search_in_res.Font = new Font(textBox_search_in_res.Font, FontStyle.Italic);
            }
        }

        private void textBox_search_in_res_KeyUp(object sender, KeyEventArgs e)
        {
            richTextBox_result.SelectAll();
            richTextBox_result.SelectionColor = richTextBox_result.ForeColor;
            richTextBox_result.SelectionBackColor = richTextBox_result.BackColor;
            int startIndex = richTextBox_result.Find(textBox_search_in_res.Text, 0, RichTextBoxFinds.None);
            if (startIndex >= 0 && richTextBox_result.Text.IndexOf(textBox_search_in_res.Text, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                richTextBox_result.Select(startIndex, textBox_search_in_res.Text.Length);

                richTextBox_result.SelectionColor = Color.White;
                richTextBox_result.SelectionBackColor = Color.Blue;

                richTextBox_result.ScrollToCaret();
                richTextBox_result.Refresh();
                //richTextBox_result.Focus();
            }
        }

        private void listView_botname_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView_botname.SelectedItems.Count > 0)
            {
                button_execute.Text = "Execute\r\n(" + listView_botname.SelectedItems.Count + ")";
            }
            else
            {
                button_execute.Text = "Execute";
            }
        }

        private void textBox_search_botname_Enter(object sender, EventArgs e)
        {
            if (textBox_search_botname.Text == "Botname...")
            {
                textBox_search_botname.Text = "";
                textBox_search_botname.ForeColor = SystemColors.WindowText;
                textBox_search_botname.Font = new Font(textBox_search_botname.Font, FontStyle.Regular);
            }
        }

        private void textBox_search_botname_Leave(object sender, EventArgs e)
        {
            if (textBox_search_botname.Text == "")
            {
                textBox_search_botname.Text = "Botname...";
                textBox_search_botname.ForeColor = Color.Gray;
                textBox_search_botname.Font = new Font(textBox_search_botname.Font, FontStyle.Italic);
            }
        }

        private void textBox_search_botname_KeyUp(object sender, KeyEventArgs e)
        {
            listView_botname.SelectedIndices.Clear();
            ListViewItem fitem = listView_botname.FindItemWithText(textBox_search_botname.Text);
            if(fitem != null) { fitem.Selected = true; fitem.EnsureVisible(); }
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

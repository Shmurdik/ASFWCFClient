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

        private Client Client;

        private string WCFHost = "127.0.0.1";
        private string WCFPort = "1242";

        public Form1()
        {
            InitializeComponent();
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

            if (selectedBots.Length > 0 && input.Contains("<BOT>"))
            {
                foreach (string botName in selectedBots)
                {
                    string res = Client.HandleCommand(input.Replace("<BOT>", botName));
                    richTextBox_result.Text += ((res.Contains(botName)) ? "" : "<" + botName + "> ") + res + (res.EndsWith("\n") ? "" : "\r\n");
                    //richTextBox_result.Text += input.Replace("<BOT>", botName);
                }
            }
            else
            {
                richTextBox_result.Text = Client.HandleCommand(input);
                //richTextBox_result.Text = input;
            }

            File.AppendAllText(Path.GetFileNameWithoutExtension(ExecutableFile) + ".log", richTextBox_result.Text + "\r\n**********\r\n");

            button_execute.Enabled = true;
        }

        private async void button_execute_Click(object sender, EventArgs e)
        {
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
            foreach (ListViewItem item in listView_botname.Items)
            {
                item.Selected = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cfg_file = Path.Combine(ExecutableDirectory, Path.GetFileNameWithoutExtension(ExecutableFile) + ".json");
            Config config = new Config();
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

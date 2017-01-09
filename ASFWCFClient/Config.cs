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
    internal sealed class Config
    {
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace, Required = Required.DisallowNull)]
        internal readonly HashSet<string> Commands = new HashSet<string> {
            "2fa",
            "2fa <BOT>",
            "2fano",
            "2fano <BOT>",
            "2faok",
            "2faok <BOT>",
            "addlicense",
            "addlicense <BOT>",
            "api",
            "exit",
            "farm",
            "farm <BOT>",
            "help",
            "leave",
            "loot",
            "loot <BOT>",
            "lootall",
            "owns",
            "owns <BOT>",
            "ownsall",
            "password",
            "password <BOT>",
            "pause",
            "pause <BOT>",
            "pause^",
            "pause^ <BOT>",
            "play",
            "play <BOT>",
            "redeem",
            "redeem <BOT>",
            "redeem^",
            "redeem^ <BOT>",
            "redeem&",
            "redeem& <BOT>",
            "rejoinchat",
            "restart",
            "resume",
            "resume <BOT>",
            "start",
            "start <BOT>",
            "startall",
            "status",
            "status <BOT>",
            "statusall",
            "stop",
            "stop <BOT>",
            "update",
            "version"
        };

        public Config() { }

        internal static Config Load(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            Config config;

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                
                return null;
            }

            if (config == null)
            {
                return null;
            }

            return config;
        }
    }
}

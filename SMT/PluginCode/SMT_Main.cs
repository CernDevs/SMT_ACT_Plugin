using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using SMT.DTO;
using SMT.GoogleDoc;
using SMT.GUI;
using SMT.Repositories;
using FFXIV_ACT_Plugin;
using FFXIV;
using System.Text.RegularExpressions;

namespace SMT.PluginCode
{
    public class SMT_Main : IActPluginV1
    {
        private UserInterface userInterface;
        private Label lblStatus;

        private List<CharacterNameDTO> staticMembers;

        private string primaryCharacter = "";

        private List<string> trackableDrops;

        private List<string> trackedDrops;

        public SMT_Main()
        {
            staticMembers = ReaderStaticMembers.GetStaticMembers();

            trackableDrops = TrackableDrops.GetDrops();

            userInterface = new UserInterface(staticMembers, trackableDrops);

            userInterface.InitializeComponent();

            userInterface.TrackedDropsChanged += UserInterface_TrackedDropsChanged;

        }

        

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            lblStatus = pluginStatusText;   // Hand the status label's reference to our local var

            pluginScreenSpace.Controls.Add(userInterface); // Add this UserControl to the tab ACT provides

            lblStatus.Text = "Plugin Started";

            ActGlobals.oFormActMain.OnLogLineRead += OFormActMain_OnLogLineRead

        }

        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            // i'm making the check here every time. I had it only once, but then i remembered, that some people might change chars to raid twice without closing ACT.
            // it doesn't cost any notable resources to do this, so this is a safe way to keep track of what "You" should be replaced by when tracing the parses

            if (logInfo.logLine.Contains("Changed primary player to"))
            {
                primaryCharacter = ExtractPrimaryPlayer(logInfo.logLine);
                userInterface.DisplayMessage(true, "primary character: " + primaryCharacter);
            }
            userInterface.DisplayMessage(true, logInfo.logLine);
        }

        private void UserInterface_TrackedDropsChanged()
        {
            this.trackedDrops = userInterface.trackedDrops;
        }

        public void DeInitPlugin()
        {
            //ActGlobals.oFormActMain.OnLogLineRead -= OFormActMain_OnLogLineRead();
            lblStatus.Text = "Plugin Exited";
        }

        //helper
        private string ExtractPrimaryPlayer(string logline)
        {
            //pattern searches for this pattern: [15:04:12.495] 02:Changed primary player to {name}.
            //i've used a named capture group to make it less arbitrary what group we're pulling the info from in the resulting match
            string pattern = @"\[\d{2}:\d{2}:\d{2}\.\d{3}\] 02:Changed primary player to (?<name>.*)\.";
            Regex primPlayer = new Regex(pattern);
            return primPlayer.Match(logline).Groups["name"].Value;

        }

       
    }

}

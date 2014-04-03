using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Uninstaller0._01
{
    public partial class mainWindow : Form
    {
        public mainWindow(bool killBrowsersEnabled, ListContainer list)
        {
            InitializeComponent();
            initializeLists(list);
            
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void initializeLists(ListContainer list)
        {
            int i;
            //batchList = new CheckedListBox();
            batchList.CheckOnClick = true;
            batchList.Name = "batchList";
            batchList.ThreeDCheckBoxes = true;

            List<UninstallItem> uItem = new List<UninstallItem>();
            uItem = list.uItem;
            for (i = 0; i < uItem.Count; i++)
            {
                batchList.Items.Add(uItem[i], false);
            }
            batchList.Items.Add(batchList);
            Controls.Add(this.batchList);

           // manualBox = new CheckedListBox();
            manualBox.CheckOnClick = true;
            manualBox.Name = "manualBox";
            manualBox.ThreeDCheckBoxes = true;

            List<UserUninstallItem> uUitem = new List<UserUninstallItem>();
            uUitem = list.uUItem;
            for (i = 0; i < uUitem.Count; i++)
            {
                manualBox.Items.Add(uUitem[i], false);
            }
            manualBox.Items.Add(manualBox);
            Controls.Add(this.manualBox);
        }
    }
}

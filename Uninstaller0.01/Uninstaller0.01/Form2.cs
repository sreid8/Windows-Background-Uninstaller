using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace Uninstaller0._01
{
    public partial class starting : Form
    {
        /// <summary>
        /// a progress bar to start the program... 
        /// gets all the info for the installed programs
        /// </summary>
        /// <param name="killBrowsersEnabled">
        /// TRUE if the killBrowsers thread will be started
        /// FALSE if the killBrowsers thread will NOT be started</param>
        public starting(bool killBrowsersEnabled)
        {
            InitializeComponent();
            progressBar1.Style = ProgressBarStyle.Marquee;
            System.Threading.Thread.Sleep(100);
            this.Show();
            //gather info for installed programs, pass results to main window.

            ListContainer items = new ListContainer();
            items = getPrograms();
            this.Hide();
            var mainWin = new mainWindow(killBrowsersEnabled, items);
            mainWin.ShowDialog();
            this.Close();
            
            //mainWin.ShowDialog();

            //AT THIS POINT, THE TWO LIST HAVE BEEN GENERATED. Time to make the new form.

        }

        public ListContainer getPrograms()
        {
            // BELOW IS COPIED FROM ANOTHER VERSION OF THE PROGRAM AND HAS 
            //  BEEN INTEGRATED HERE

            /// First check OS Arch. If 64, do 64bit programs, then go to 32 bit. If 32 bit, just go to 32 bit programs. For each program, build list of those
            /// using MsiExec.exe and build their command line arguments. Store all of these in UninstallItem items in a list. 
            /// Then sort list by program name and print to screen. Use this as a basis for future batch uninstaller iterations.


            //DECLARATIONS
            int first, last, pos; //variables for the first and last positions in strings for substring calculation.
            string tmpName, val; //strings used to store strings for processing (mostly from registry values)
            RegistryKey rk, tmp; //registrykey objects that will be used to parse registry data for uninstall values
            string[] progs32; //array of strings for 32 bit applications.
            string[] progs64; //array of strings for 64 bit applications.
            List<UninstallItem> uList = new List<UninstallItem>(); //final list of batch uninstallable items, eventually will be sorted.
            List<UserUninstallItem> uUList = new List<UserUninstallItem>(); //final list of User uninstallable items.
            UninstallItem tUItem; //temp uninstall item object to be pushed to uList.
            UserUninstallItem tUUItem; //temp User Uninstall item object to be pushed to uUList.
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")); //test to see if OS that program is running on is 64 bit or not (used for early .NET version compatibility)

            //THIS IS A TEST VARIABLE
            UserUninstallItem tItem = new UserUninstallItem();
            //THIS IS A TEST VARIABLE

            //64 bit
            if (is64bit)
            {
                //is a 64 bit OS, must parse entire registry.
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                rk = rk.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Uninstall");
                //64 bit Uninstall tree open. Start enumerating programs
                progs64 = new string[rk.ValueCount];
                progs64 = rk.GetSubKeyNames();
                foreach (string j in progs64)
                {
                    tUItem = new UninstallItem();
                    tmp = rk.OpenSubKey(j);
                    val = (string)tmp.GetValue("UninstallString");
                    if (!String.IsNullOrEmpty(val))
                    {
                        if (val.StartsWith("MsiExec", true, null))
                        {
                            //if these two are true, we've found a key that has a valid uninstall string.
                            //time to find GUID of application.
                            first = val.IndexOf('{');
                            last = val.LastIndexOf('}');
                            tmpName = val.Substring(first, (last - first) + 1);
                            tUItem.guid = tmpName;

                            //create new uninstall string.
                            tUItem.uninstallString = " /x " + tUItem.guid + " " + "/qn";

                            //get program name
                            val = (string)tmp.GetValue("DisplayName");
                            tUItem.progName = val;
                            uList.Add(tUItem);
                        }
                        else //this is the case where the uninstall is not an MSI and must be handled manually. 
                        {//basically, all I need to do is get the name of the program and uninstall string and attempt to run it.
                            if (!val.ToUpperInvariant().Contains(Environment.GetEnvironmentVariable("windir").ToUpperInvariant()))
                            {
                                tUUItem = new UserUninstallItem();
                                tUUItem.progName = (string)tmp.GetValue("DisplayName");
                                tUUItem.uninstallString = (string)tmp.GetValue("UninstallString");
                                if (tUUItem.uninstallString[0] == '\\')
                                {
                                    try
                                    {
                                        tUUItem.uninstallString = System.Text.RegularExpressions.Regex.Replace(tUUItem.uninstallString, "\\", " ");
                                    }
                                    catch (Exception e)
                                    {
                                        goto skip1;
                                    }
                                }
                                if (tUUItem.uninstallString.Contains('\"'))
                                {
                                    tUUItem.uninstallString = System.Text.RegularExpressions.Regex.Replace(tUUItem.uninstallString, "\"", " ");
                                }
                                if (!String.IsNullOrEmpty(tUUItem.progName) && !String.IsNullOrEmpty(tUUItem.uninstallString))
                                {
                                    uUList.Add(tUUItem);
                                    Console.WriteLine(tUUItem.progName + "\n" + tUUItem.uninstallString + "\n\n");
                                }
                            }
                        skip1: ; //this is where we go if there is an issue parsing the uninstall string.
                        }
                    }
                }
            }
            //always will do the 32 bit run because we're always running on at least a 32 bit system.
            rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            rk = rk.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Uninstall");
            //32 bit Uninstall tree open. Start/Add keys.
            progs32 = new string[rk.ValueCount];
            progs32 = rk.GetSubKeyNames();
            foreach (string j in progs32)
            {
                tUItem = new UninstallItem();
                tmp = rk.OpenSubKey(j);
                val = (string)tmp.GetValue("UninstallString");
                if (!String.IsNullOrEmpty(val))
                {
                    if (val.StartsWith("MsiExec", true, null))
                    {
                        //found a key that can be unintalled. find guid
                        first = val.IndexOf('{');
                        last = val.LastIndexOf('}');
                        tmpName = val.Substring(first, (last - first) + 1);
                        tUItem.guid = tmpName;

                        //create new uninstall string
                        tUItem.uninstallString = " /x " + tUItem.guid + " " + "/qn";

                        //get program name
                        val = (string)tmp.GetValue("DisplayName");
                        tUItem.progName = val;
                        uList.Add(tUItem);
                    }
                    else //this is the case where the uninstall is not an MSI and must be handled manually. 
                    {//basically, all I need to do is get the name of the program and uninstall string and attempt to run it.
                        if (!val.ToUpperInvariant().Contains(Environment.GetEnvironmentVariable("windir").ToUpperInvariant()))
                        {
                            tUUItem = new UserUninstallItem();
                            tUUItem.progName = (string)tmp.GetValue("DisplayName");
                            tUUItem.uninstallString = (string)tmp.GetValue("UninstallString");
                            if (tUUItem.uninstallString[0] == '\\')
                            {
                                try
                                {
                                    tUUItem.uninstallString = System.Text.RegularExpressions.Regex.Replace(tUUItem.uninstallString, "\\", " ");
                                }
                                catch (Exception e)
                                {
                                    goto skip2;
                                }
                            }
                            if (tUUItem.uninstallString.Contains('\"'))
                            {
                                tUUItem.uninstallString = System.Text.RegularExpressions.Regex.Replace(tUUItem.uninstallString, "\"", " ");
                            }
                            if (!String.IsNullOrEmpty(tUUItem.progName) && !String.IsNullOrEmpty(tUUItem.uninstallString))
                            {
                                uUList.Add(tUUItem);
                                Console.WriteLine(tUUItem.progName + "\n" + tUUItem.uninstallString + "\n\n");
                            }
                        }
                    }
                skip2: ; //this is where we go if there is an exception in parsing the uninstall string.
                }
            }

            ListContainer items = new ListContainer();
            items.uItem = uList;
            items.uUItem = uUList;
            return items;
        }
    }
}

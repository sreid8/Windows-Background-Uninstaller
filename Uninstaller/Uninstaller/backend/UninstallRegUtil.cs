using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace Uninstaller
{
    public sealed class UninstallRegUtil
    {
        public UninstallRegUtil()
        {

        }


        public List<IUninstallItem> getUninstallItems()
        {
            return new List<IUninstallItem>();
        } 

        private List<IUninstallItem> getUninstallKey(Int16 bits)
        {
            List<IUninstallItem> items = new List<IUninstallItem>();
            switch (bits)
            {
                case 64:

                    break;
                case 32:
                    break;
            }
            return null;
        }


        /**
         * Opens the registry key for the bit depth and priveleges of the user
         * params: int -> the number of bits, bool -> whether the user is an admin
         * returns: RegistryKey -> the opened key to the correct uninstall key
         **/
        private RegistryKey openUninstallKey(Int16 bits, bool isAdmin)
        {
            RegistryKey rk;
            if (isAdmin)
            {
                rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (bits == 64 ? RegistryView.Registry64 : RegistryView.Registry32) );
            }
            else
            {
                rk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, (bits == 64 ? RegistryView.Registry64 : RegistryView.Registry32) );
            }

            rk = rk.OpenSubKey("Software");
            rk = rk.OpenSubKey("Microsoft");
            rk = rk.OpenSubKey("Windows");
            rk = rk.OpenSubKey("CurrentVersion");
            rk = rk.OpenSubKey("Uninstall");
            return rk;
        }

        /**
         * determines whether the item is valid for uninstallation
         * reasons that something wouldn't be valid:
         * 1. Windows update
         * 2. No uninstall string
         * param: RegistryKey -> the item that is being examined
         * returns: bool -> true if the item is valid, false if it is not
         **/
        private bool isUninstallable(RegistryKey item)
        {
            Object temp = item.GetValue("InstallLocation");
            if (temp != null)
            {
                if (!((String)temp).Contains("C:\\Windows"))
                {
                    temp = item.GetValue("UninstallString");
                    if (temp != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

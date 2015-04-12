using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uninstaller
{
    public class AutoUninstallItem : IUninstallItem
    {
        bool IUninstallItem.Uninstall()
        {
            return false;
        }

        void IUninstallItem.setUninstallString(String pUninstallString)
        {
            return;
        }

        void IUninstallItem.setDisplayName(String pDisplayName)
        {
            return;
        }

        String IUninstallItem.getUninstallString()
        {
            return null;
        }

        String IUninstallItem.getDisplayName()
        {
            return null;
        }

        bool IUninstallItem.isAuto()
        {
            return true;
        }

        void IUninstallItem.setAuto(bool pAuto)
        {
            return;
        }
    }
}

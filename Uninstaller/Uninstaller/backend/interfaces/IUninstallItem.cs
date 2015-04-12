using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uninstaller
{
    /**
     * The Interface IUninstallItem
     * Classes implementing this interface are applications that
     * can be uninstalled using this Uninstaller.
     **/
    public interface IUninstallItem
    {
        /**
         * runs the uninstall
         * returns true if uninstall was a success, false otherwise
         **/
        bool Uninstall();

        /**
         * sets the uninstall string
         * (this is the command that will be executed 
         * to launch the application's uninstaller)
         **/
        void setUninstallString(String pUninstallString);

        /**
         * sets the display name of the application
         **/
        void setDisplayName(String pDisplayName);

        /**
         * gets the uninstall string of the application
         **/
        String getUninstallString();

        /**
         * gets the display name of the application
         **/
        String getDisplayName();

        /**
         * whether the application can be
         * uninstalled automatically or not
         **/
        bool isAuto();

        /**
         * sets whether the application
         * can be uninstalled automatically or not
         **/
        void setAuto(bool pAuto);

    }
}

using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static Base.EventAggregators;

namespace Helpers
{
    public class ViewRegionNames
    {
        #region Regions
        
        public const string ShellWindowRegion = "SHELLWINDOWREGION";
        public const string MainViewerRegion = "MAINVIEWERREGION";

        #endregion

        #region Views

        public const string UIContainer = "UICONTAINER";
        public const string UIContainerRegion = "UICONTAINERREGION";
        public const string AddContestWindow = "ADDCONTESTWINDOW";
        public const string MainViewerForm = "MAINVIEWERFORM";

        // MessageBoxes


        #endregion
    }

    public class DialogNames
    {
        public const string UIContainerDialog = "UICONTAINERDIALOG";
        public const string SaveContestDialog = "SAVECONTESTDIALOG";
        public const string SuccessDialog = "SUCCESSDIALOG";
        public const string YesNoDialog = "YESNODIALOG";
        public const string WarningDialog = "WARNINGDIALOG";
    }


    public class ErrorNotification
    {
        public static void SendErrorNotification(string message, IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SendExceptionNotification>().Publish(message);
        }
    }
}

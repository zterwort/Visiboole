using System;
using VisiBoole.Events;

namespace VisiBoole
{
    public interface IMainWindow
    {
        event ProcessNewFileHandler ProcessNewFile;
        event LoadDisplayHandler LoadDisplay;

        void ShowDisplay(DisplayBase display);

        void DisplayErrorMessage(Exception ex);
    }
}
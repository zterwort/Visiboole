using System;
using VisiBoole.Events;

namespace VisiBoole
{
    public interface IMainWindow
    {
        event ProcessNewFileHandler ProcessNewFile;
        event LoadDisplayHandler LoadDisplay;

        void ShowDisplay(DisplayBase previous, DisplayBase current);

        void DisplayErrorMessage(Exception ex);
    }
}
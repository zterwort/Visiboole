using System;
using VisiBoole.Events;

namespace VisiBoole
{
    public interface IMainWindow
    {
        event ProcessNewFileHandler ProcessNewFile;
        event LoadDisplayHandler LoadDisplay;
        event SaveFileHandler SaveFile;
        event SaveAsHandler SaveAs;

        void ShowDisplay(DisplayBase previous, DisplayBase current);

        void DisplayErrorMessage(Exception ex);
    }
}
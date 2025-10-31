namespace Juhendid;

// See on programmi peaklass. Siin algab kõik.
static class Program
{
    /// <summary>
    /// See on programmi alguspunkt.
    /// </summary>
    [STAThread] 
    static void Main()
    {
        // Siin tehakse programm ilusaks.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        // Siin käivitatakse peamenüü.
        Application.Run(new MainForm());

    }
}
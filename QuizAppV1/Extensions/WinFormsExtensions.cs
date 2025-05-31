using System.Runtime.InteropServices;

namespace QuizAppV1.Extensions;

internal static class WinFormsExtensions
{
    public static void SetValueInstantly(this ProgressBar progressBar, int value)
    {
        ++progressBar.Maximum;
        progressBar.Value = value + 1;
        progressBar.Value = value;
        --progressBar.Maximum;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }

    public static void SetText(this Label label, string text, Color color)
    {
        label.Text = text;
        label.ForeColor = color;
    }
}

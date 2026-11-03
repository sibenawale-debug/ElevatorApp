using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ElevatorApp
{
    /// <summary>
    /// The one place where BackgroundWorker is used: work runs off the GUI thread, the result (or the
    /// exception) comes back on it. Call from the GUI thread only.
    /// </summary>
    public static class BackgroundTask
    {
        public static void Run<T>(Control owner, Func<T> work, Action<T> onSuccess, Action<Exception> onError)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) => e.Result = work();
            worker.RunWorkerCompleted += (s, e) =>
            {
                try
                {
                    if (owner.IsDisposed)
                    {
                        return; // the window was closed while the work was running
                    }
                    if (e.Error != null)
                    {
                        if (onError != null) onError(e.Error);
                    }
                    else if (onSuccess != null)
                    {
                        onSuccess((T)e.Result);
                    }
                }
                finally
                {
                    worker.Dispose();
                }
            };
            worker.RunWorkerAsync();
        }
    }
}

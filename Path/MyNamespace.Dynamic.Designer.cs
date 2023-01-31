using Simple_Screen_Recorder;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace GrabadorPantalla.My
{
    internal static partial class MyProject
    {
        internal partial class MyForms
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public RecorderScreenMainWindow m_RecorderScreenMainWindow;

            public RecorderScreenMainWindow RecorderScreenMainWindow
            {
                [DebuggerHidden]
                get
                {
                    m_RecorderScreenMainWindow = Create__Instance__(m_RecorderScreenMainWindow);
                    return m_RecorderScreenMainWindow;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_RecorderScreenMainWindow))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_RecorderScreenMainWindow);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Core
{
    public interface IUI
    {
        event Action<object, Avalonia.Input.PointerPressedEventArgs> onMouseClick;
        event Action<object, Avalonia.Input.KeyEventArgs> onKeyDown;
        event Action<object, object> onPageLoad;
    }
}

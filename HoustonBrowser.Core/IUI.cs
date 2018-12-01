using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Core
{
    public interface IUI
    {
        event EventHandler<Avalonia.Input.PointerPressedEventArgs> onMouseClick;
        event EventHandler<Avalonia.Input.KeyEventArgs> onKeyDown;
        event EventHandler<PageLoadEventArgs> onPageLoad;

        event EventHandler<Avalonia.AvaloniaPropertyChangedEventArgs> onSizeChanged;
    }
}

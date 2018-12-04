using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HoustonBrowser.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Interactivity;
using HoustonBrowser.Core;
using HoustonBrowser.JS;

namespace HoustonBrowser
{
    public class MainWindow : Window, IUI
    {
        private Avalonia.Controls.Button backButton;
        private Avalonia.Controls.Button forwardButton;
        private Avalonia.Controls.Button refreshButton;
        private MyPanel drawPanel;
        private Avalonia.Controls.TextBox urlTextBox;
        private Avalonia.Controls.Button searchButton;
        private Core.Core core;
        private IJS js;
        private ScrollViewer scrollViewer;

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<PageLoadEventArgs> onPageLoad;
        public event EventHandler<AvaloniaPropertyChangedEventArgs> onSizeChanged;

        public MainWindow()
        {
            InitializeComponent();

            backButton = this.Find<Avalonia.Controls.Button>("btnBack");
            forwardButton = this.Find<Avalonia.Controls.Button>("btnForward");
            refreshButton = this.Find<Avalonia.Controls.Button>("btnRefresh");  
            drawPanel = this.Find<MyPanel>("drawingCanvas"); 
            urlTextBox = this.Find<Avalonia.Controls.TextBox>("urlInputBox");
            searchButton = this.Find<Avalonia.Controls.Button>("btnSearch"); 
            scrollViewer = this.Find<ScrollViewer>("scroll");
            drawPanel.MinHeight = scrollViewer.Height;

            core = new Core.Core(this);
            core.onRender+= Core_onRender;
            js  =  core.Js;

            searchButton.Click+= searchButton_OnClick;     
            refreshButton.Click+= searchButton_OnClick;
            urlTextBox.KeyDown+= urlTextBox_OnKeyDown;
            js.onAlert +=  Js_onAlert;    
            this.PropertyChanged+=window_onChanged;
        }

        private void window_onChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        { 
            if(e.Property.Name=="ClientSize")
            {
                Size newSize = (Size)e.NewValue;
                Size oldSize = (Size)e.OldValue;
                scrollViewer.Height += newSize.Height - oldSize.Height;
                this.onSizeChanged?.Invoke(sender, e);
            }
        }

        private void Js_onAlert(object sender, string e)
        {
            ShowAlert(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);            
        }

        public void ShowAlert(string text)
        {
            Window alertWindow  =  new Window();
            alertWindow.Position = this.Position;
            alertWindow.Width  =  150;
            alertWindow.Height = 100;
            StackPanel stackPanel  =  new StackPanel();
            stackPanel.Orientation  =  Orientation.Vertical;
            TextBlock alertText  =  new TextBlock();
            alertText.Margin  =  new Thickness(10, 10, 0, 10);
            alertText.Text  =  text;
            stackPanel.Children.Add(alertText);
            Avalonia.Controls.Button okButton  =  new Avalonia.Controls.Button();
            okButton.Width  =  30;
            okButton.Content  = "Ok";
            stackPanel.Children.Add(okButton);
            alertWindow.Content  =  stackPanel;
            alertWindow.ShowDialog();
        }

        private void searchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var arg  =  new PageLoadEventArgs(this.urlTextBox.Text);
            this.onPageLoad(sender, arg);
        }

        private void urlTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
            var arg  =  new PageLoadEventArgs(this.urlTextBox.Text);
            this.onPageLoad(sender, arg);
            }
        }

        private void Core_onRender(object sender, RenderEventArgs e)
        {
            drawPanel.Controls = e.Page;
            if(drawPanel.Controls!=null)
            {
                foreach(var cntrl in drawPanel.Controls)
                {
                    if(cntrl is LinkLabel)
                    {
                        cntrl.PointerPressed+=drawPanelLink_OnPointerPressed;
                    }
                    if(cntrl is RichText)
                    {
                        ((RichText)cntrl).LinkPressed+=drawPanelRichText_OnLinkPressed;
                    }
                }
            }
            drawPanel.InvalidateVisual();
        }

        private void drawPanelRichText_OnLinkPressed(object sender, string e)
        {
            var arg  =  new PageLoadEventArgs(e);
            urlTextBox.Text=e;
            this.onPageLoad(sender, arg);
        }

        private void drawPanelLink_OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            var linkUrl=((LinkLabel)sender).URL;
            var arg  =  new PageLoadEventArgs(linkUrl);
            urlTextBox.Text=linkUrl;
            this.onPageLoad(sender, arg);
        }

    }
}
using System;
using System.Collections.Generic;
using HoustonBrowser.HttpModule;
using HoustonBrowser;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HoustonBrowser.Controls;
using HoustonBrowser.JS;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.HTML;
using HoustonBrowser.Parsing;
using HoustonBrowser.Render;


namespace HoustonBrowser.Core
{
    public class Core
    {
        INetworkClient httpClient;
        IParser parser;
        IBrowserControl control;
        IJS js;
        IUI ui;
        MockDocument dom;

        RenderTree renderTree;

        public IJS Js { get => js;}

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;

            this.httpClient=new NetworkClient();
            this.parser=new Parser();

            this.control=new BrowserControl();
            this.js=new MockJS();
            this.dom = new MockDocument();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;
            parser.onNonHtmlEvent += Parser_onNonHtmlEvent;

            // button.Click+=Button_onMouseClick;

       
            //var doc=parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nfunction myFunction() {\r\n    var x=document.getElementById(\"myDIV\");\r\n    if (x) {\r\n      x.style.display=\"none\";\r\n    }\r\n}\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>");
            //var renderTree=new RenderTree(doc);
            //var tmp=renderTree.GetPage();
            
        }

        private void Parser_onNonHtmlEvent(object sender, string e)
        {
            js.Process(e);
        }

        private void Ui_onPageLoad(object sender, PageLoadEventArgs e)
        {
            //var doc=parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nalert(TEST)\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>");
            //RenderTree renderTree=new RenderTree(parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nfunction myFunction() {\r\n    var x=document.getElementById(\"myDIV\");\r\n    if (x) {\r\n      x.style.display=\"none\";\r\n    }\r\n}\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>"));

            RenderTree renderTree  =new RenderTree(parser.Parse(httpClient.Get(e.UrlString)));
            RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.GetPage());
            onRender(this,renderEventArgs);
            //parser.Parse("<html><head><meta http - equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\"><title> TinyTIM Home Page and Advice Column </title><link rev=\"MADE\" href=\"mailto:emp@yay.tim.org\"><link rel=\"SHORTCUT ICON\" href=\"http://www.tim.org/favicon.ico\"></head><body background=\"./TinyTIM Home Page and Advice Column_files/timback.gif\"><h1> Welcome to the TinyTIM WWW Page!</h1><img src=\"./TinyTIM Home Page and Advice Column_files/TIMtitle.GIF\" alt=\"Go to ftp://ftp.tim.org/pub/tim/GIFs/TIMtitle.GIF to see the spiffy picture your browser isn &#39;t showing you.  Why should I do everything ? \" width=\"534\" height=\"245\"><h2><i> Commonly known as our HTML Resource, or, simply, \"Owen's Brother, Phil\"</i></h2><hr>This site is provided as a service to help people who have had their interaction with the Internet reduced to a series of Icon Clicking and moving a little bar around.It's time to expand your horizons with TinyTIM! <p>TinyTIM is the world's oldest running MUSH (Multi-User Shared Hallucination), over <a href=\"http://www.tim.org/history/index.html\"> 18 years old</a>.It's a lot of fun and beats getting run over by a car. </p><p>To get more pages and stuff, simply choose one of the following three areas and follow the link that best describes what's going through your mind at the moment. Nothing could be easier!</p><h3> Never used TinyTIM</h3>\"I've never used TinyTIM before. I was sure this was going to be a<a href= \"http://www.tim.org/thesinger.html\"> WWW page about the singer</a> !Where's <a href=\"http://www.tim.org/sounds/tiptoe.au\">\"Tiptoe through the Tulips\" in .au form</a>?Oh, well. <a href=\"http://www.tim.org/whatstim.html\"> What the hell is this TinyTIM thing?</a><a href=\"http://www.lysator.liu.se:7500/mud/faq/faq1.html\"> What are MUSHes</a>? Where am I ? \"<h3> Used TinyTIM, want to expand experience</h3> \"Of <em>course</em> I use TinyTIM! <a href=\"http://www.tim.org/programs/players.php\"> Everybody uses TinyTIM!</a> Some people are even using TinyTIM <a href=\"http://www.tim.org/programs/WHO.php\"> right now!</a> TinyTIM is one of the basic four food groups.What I really want is to expand my addiction to TinyTIM by getting <a href= \"http://www.tim.org/timpics\"> pictures </a>, sounds,and <a href=\"http://www.tim.org/blurbs\"> blurbs </a> about my all-time favorite Internet Experience and its <a href=\"http://www.tim.org/reviews\"> well - read </a><a href=\"http://www.tim.org/wizards\"> wizards </a>...and then I'll get the <a href=\"http://www.tim.org/merchandise\"> t - shirt </a>.\"<h3> Clicks on random icons in WWW pages</h3>\"Uh, I'm looking for <a href=\"http://weather.msn.com/region.aspx?wealocations=africa\">weather maps of Africa</a>.\"<hr><a href=\"telnet://yay.tim.org:5440/\"><img align=\"middle\" alt=\"\" src=\"./TinyTIM Home Page and Advice Column_files/Jackin2.GIF\" width=\"167\" height=\"134\"> Play TinyTIM!(telnet yay.tim.org 5440)</a><p>And remember, <a href=\"http://www.tim.org/wizhats.html\"> Accept<i> NO </i> Imitations!</a></p><p></p><hr>his mess is maintained by <a href=\"http://www.tim.org/wizards//emphome.html\"> Empedocles the Ash Ock </a> who learned HTML in several passionate and info-filled minutes and lives in a condo.<strong> At the same time </strong>.Most of the main text, including this biography of Empedocles that Emp would never think to write, is by <a href=\"http://www.tim.org/wizards/sketchhome.html\"> Sketch the Art Cow </a>, who has a <a href=\"http://www.arl.wustl.edu/~brian/Office/LavaLamp/lamp.gif\"> lava lamp </a> and spends too much time with <a href=\"http://www.tim.org/sister.html\"> your sister </a>.</body></html>");
        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" +parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            onRender(this, new RenderEventArgs(null));

        }

        private void Ui_onKeyDown(object sender, KeyEventArgs e)
        {

            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            onRender(this, new RenderEventArgs(null));
        }

        private void Button_onMouseClick(object sender, RoutedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            //onRender(this, new RenderEventArgs(null));
        }
    }
}

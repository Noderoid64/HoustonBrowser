using System;

namespace ISDBrowser.HTMLParser
{
    public class Parser
    {
        private string HTMLDoc;

        public Parser()
        {

        }

        HTMLDOM ParseHTMLDoc()
        {
            HTMLDOM domTree = new HTMLDOM();
            Conditions conditions;
            int currentCondition = 0;
            int currentSymIndex = 0;
            bool endOfDocument = false;
            while(!endOfDocument)
            {
                switch(currentCondition)
                {
                    case (int)Conditions.Data:
                    {
                        if(HTMLDoc[currentSymIndex]=='<')
                        {
                            currentCondition = (int)Conditions.OpenTag;
                        }
                        break;
                    }

                }
                currentSymIndex++;
            }
            return domTree;
        }
    }
}

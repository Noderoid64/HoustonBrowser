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
            int currentCondition = 0,currentSymIndex = 0;
            bool endOfDocument = false;
            string currentTag = "";
            while(!endOfDocument)
            {
                switch(currentCondition)
                {
                    case (int)Conditions.Data://Определение состояния
                    {
                        if(HTMLDoc[currentSymIndex]=='<')
                        {
                            if(currentSymIndex + 1 < HTMLDoc.Length)
                            {
                                if(HTMLDoc[currentSymIndex + 1]=='/')
                                {
                                    currentCondition = (int)Conditions.CloseTagStart;
                                }
                                else
                                {
                                    currentCondition = (int)Conditions.OpenTagStart;
                                }
                            }
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

namespace HoustonBrowser.JS
{
    public enum TokenType
    {
        Keyword,
        /* 
            break       else        new         var
            case        finally     return      void
            catch       for         switch      while
            continue    function    this        with
            default     if          throw
            delete      in          try
            do          instanceof  typeof
        */
        FutureReservedWord,
        /*
            abstract    enum        int         short
            boolean     export      interface   static
            byte        extends     long        super
            char        final       native      synchronized
            class       float       package     throws
            const       goto        private     transient
            debugger    implements  protected   volatile
            double      import      public
        */
        NullLiteral, // null
        BooleanLiteral, // true, false
        Identifier,
        Punctuator,
        /*
            {       }       (       )       [       ]
            .       ;       ,       <       >       <=
            >=      ==      !=      ===     !== 
            +       -       *       %       ++      --
            <<      >>      >>>     &       |       ^
            !       ~       &&      ||      ?       :
            =       +=      -=      *=      %=      <<=
            >>=     >>>=    &=      |=      ^=  
        */
        NumericLiteral,
        StringLiteral,
        Literal
    }
}
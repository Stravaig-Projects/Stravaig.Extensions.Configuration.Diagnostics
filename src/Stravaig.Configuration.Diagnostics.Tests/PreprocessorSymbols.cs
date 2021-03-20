namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public static class PreprocessorSymbols
    {
        public static string StringList => string.Join("; ", ArrayOfSymbols);

        public static readonly string[] ArrayOfSymbols = new string[]
        {
#if NETFRAMEWORK
                "NETFRAMEWORK",
#endif
     
#if NET48
                "NET48",
#endif

#if NET472
                "NET472",
#endif

#if NET471
                "NET471",
#endif

#if NET47
                "NET47",
#endif

            
#if NET462
                "NET462",
#endif

#if NET461
                "NET461",
#endif

#if NET46
                "NET46",
#endif

#if NET452
                "NET452",
#endif
            
#if NET451
                "NET451",
#endif

#if NET45
                "NET45",
#endif

#if NET40
                "NET40",
#endif

#if NET35
                "NET35",
#endif

#if NET20
                "NET20",
#endif

#if NETSTANDARD
                "NETSTANDARD",
#endif

#if NETSTANDARD2_1
                "NETSTANDARD2_1",
#endif
            
#if NETSTANDARD2_0
                "NETSTANDARD2_0",
#endif

#if NETSTANDARD1_6
                "NETSTANDARD1_6",
#endif

#if NETSTANDARD1_5
                "NETSTANDARD1_5",
#endif
            
#if NETSTANDARD1_4
                "NETSTANDARD1_4",
#endif

#if NETSTANDARD1_3
                "NETSTANDARD1_3",
#endif

#if NETSTANDARD1_2
                "NETSTANDARD1_2",
#endif

#if NETSTANDARD1_1
                "NETSTANDARD1_1",
#endif

#if NETSTANDARD1_0
                "NETSTANDARD1_0",
#endif

#if NET
                "NET",
#endif
            
#if NET5_0
                "NET5_0",
#endif

#if NETCOREAPP
                "NETCOREAPP",
#endif

#if NETCOREAPP3_1
            "NETCOREAPP3_1",
#endif
            
#if NETCOREAPP3_0
            "NETCOREAPP3_0",
#endif

#if NETCOREAPP2_2
            "NETCOREAPP2_2",
#endif

#if NETCOREAPP2_1
            "NETCOREAPP2_1",
#endif

#if NETCOREAPP2_0
            "NETCOREAPP2_0",
#endif

#if NETCOREAPP1_1
            "NETCOREAPP1_1",
#endif

#if NETCOREAPP1_0
            "NETCOREAPP1_0",
#endif
        };
    }
    
}
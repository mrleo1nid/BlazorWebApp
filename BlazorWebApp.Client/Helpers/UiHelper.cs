using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApp.Client.Helpers
{
    public static class UiHelper
    {
        public static Blazorise.Color GetRandomColor()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            return (Blazorise.Color) random.Next(10);
        }
    }
}

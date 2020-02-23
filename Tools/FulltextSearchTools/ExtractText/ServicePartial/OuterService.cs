using System;
using System.Collections.Generic;
using System.Text;


public partial class OuterService
{
    public OuterService(string url)
    {
        this.Url = url.TrimEnd('/') + "/Services/OuterService.asmx";
    }
}


﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APIWeb.Models
{
    public class Request
    {
      
            public string Id { get; set; }
            // أضف الخصائص الأخرى المطلوبة للطلب هنا
            public IdentityUser User { get; set; }
            public Document Document { get; set; }
    



    }

}

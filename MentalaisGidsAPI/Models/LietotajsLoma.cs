﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MentalaisGidsAPI.Models;

public partial class LietotajsLoma
{
    public int LietotajsLomaID { get; set; }

    public int LietotajsID { get; set; }

    public string LomaNosaukums { get; set; }

    public virtual Lietotajs Lietotajs { get; set; }

    public virtual Loma LomaNosaukumsNavigation { get; set; }
}
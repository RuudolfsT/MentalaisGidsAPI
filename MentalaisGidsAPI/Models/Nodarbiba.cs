﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MentalaisGidsAPI.Models;

public partial class Nodarbiba
{
    public int NodarbibaID { get; set; }

    public int SpecialistsID { get; set; }

    public int? LietotajsID { get; set; }

    public DateTime Sakums { get; set; }

    public DateTime Beigas { get; set; }

    public virtual Lietotajs Lietotajs { get; set; }

    public virtual Lietotajs Specialists { get; set; }
}
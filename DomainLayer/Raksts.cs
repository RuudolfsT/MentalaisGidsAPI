﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MentalaisGidsAPI.Domain;

public partial class Raksts
{
    public int RakstsID { get; set; }

    public int SpecialistsID { get; set; }

    public string Virsraksts { get; set; }

    public string Saturs { get; set; }

    public DateTime DatumsUnLaiks { get; set; }

    public virtual ICollection<LietotajsRakstsKomentars> LietotajsRakstsKomentars { get; set; } = new List<LietotajsRakstsKomentars>();

    public virtual ICollection<LietotajsRakstsVertejums> LietotajsRakstsVertejums { get; set; } = new List<LietotajsRakstsVertejums>();

    public virtual Lietotajs Specialists { get; set; }
}